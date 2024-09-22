#region References
using Server.ContextMenus;
using Server.Engines.PartySystem;
using Server.Engines.Quests;
using Server.Engines.Quests.Doom;
using Server.Guilds;
using Server.Misc;
using Server.Mobiles;
using Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace Server.Items
{
    public interface IDevourer
    {
        bool Devour(Corpse corpse);
    }

    [Flags]
    public enum CorpseFlag
    {
        None = 0x00000000,

        /// <summary>
        ///     Has this corpse been carved?
        /// </summary>
        Carved = 0x00000001,

        /// <summary>
        ///     If true, this corpse will not turn into bones
        /// </summary>
        NoBones = 0x00000002,

        /// <summary>
        ///     If true, the corpse has turned into bones
        /// </summary>
        IsBones = 0x00000004,

        /// <summary>
        ///     Has this corpse yet been visited by a taxidermist?
        /// </summary>
        VisitedByTaxidermist = 0x00000008,

        /// <summary>
        ///     Has this corpse yet been used to channel spiritual energy? (AOS Spirit Speak)
        /// </summary>
        Channeled = 0x00000010,

        /// <summary>
        ///     Was the owner criminal when he died?
        /// </summary>
        Criminal = 0x00000020,

        /// <summary>
        ///     Has this corpse been animated?
        /// </summary>
        Animated = 0x00000040,

        /// <summary>
        ///     Has this corpse been self looted?
        /// </summary>
        SelfLooted = 0x00000080,

        /// <summary>
        /// Does this corpse flag looters as criminal?
        /// </summary>
        LootCriminal = 0x00000100,

        /// <summary>
        ///     Was the owner a murderer when he died?
        /// </summary>
        Murderer = 0x00000200
    }

    public class Corpse : Container, ICarvable
    {
        private Mobile m_Owner; // Whos corpse is this?
        private Mobile m_Killer; // Who killed the owner?
        private CorpseFlag m_Flags; // @see CorpseFlag

        private List<Mobile> m_Looters; // Who's looted this corpse?

        private List<Item> m_EquipItems;
        // List of dropped equipment when the owner died. Ingame, these items display /on/ the corpse, not just inside

        private List<Mobile> m_Aggressors;
        // Anyone from this list will be able to loot this corpse; we attacked them, or they attacked us when we were freely attackable

        private List<Item> m_HasLooted;
        // Keeps a list of items that have been dragged from corpse. This prevents Loot Event Handler from triggering from the same item more than once.

        private string m_CorpseName;
        // Value of the CorpseNameAttribute attached to the owner when he died -or- null if the owner had no CorpseNameAttribute; use "the remains of ~name~"

        private IDevourer m_Devourer; // The creature that devoured this corpse

        // For notoriety:
        private AccessLevel m_AccessLevel; // Which AccessLevel the owner had when he died

        // For Forensics Evaluation
        public string m_Forensicist; // Name of the first PlayerMobile who used Forensic Evaluation on the corpse

        public static readonly TimeSpan MonsterLootRightSacrifice = TimeSpan.FromMinutes(2.0);


        public override bool IsChildVisibleTo(Mobile m, Item child)
        {


            return true;
        }

        public override void AddItem(Item item)
        {
            base.AddItem(item);


        }

      
       
     
        public void AddCarvedItem(Item carved, Mobile carver)
        {
            DropItem(carved);        
        }
		private bool m_SnoopingBonusApplied;

		public override bool IsDecoContainer => false;

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime TimeOfDeath { get; set; }


		public override bool DisplayWeight => false;

        public HairInfo Hair { get; }

        public FacialHairInfo FacialHair { get; }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool IsBones => GetFlag(CorpseFlag.IsBones);

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Devoured => m_Devourer != null;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Carved { get => GetFlag(CorpseFlag.Carved); set => SetFlag(CorpseFlag.Carved, value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool VisitedByTaxidermist { get => GetFlag(CorpseFlag.VisitedByTaxidermist); set => SetFlag(CorpseFlag.VisitedByTaxidermist, value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Channeled { get => GetFlag(CorpseFlag.Channeled); set => SetFlag(CorpseFlag.Channeled, value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Animated { get => GetFlag(CorpseFlag.Animated); set => SetFlag(CorpseFlag.Animated, value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool SelfLooted { get => GetFlag(CorpseFlag.SelfLooted); set => SetFlag(CorpseFlag.SelfLooted, value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool LootCriminal { get => GetFlag(CorpseFlag.LootCriminal); set => SetFlag(CorpseFlag.LootCriminal, value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public AccessLevel AccessLevel => m_AccessLevel;

        public List<Mobile> Aggressors => m_Aggressors;

        public List<Mobile> Looters => m_Looters;

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Killer => m_Killer;

        public List<Item> EquipItems => m_EquipItems;

        public List<Item> RestoreEquip { get; set; }

        public Guild Guild { get; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Kills { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Criminal { get => GetFlag(CorpseFlag.Criminal); set => SetFlag(CorpseFlag.Criminal, value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Murderer { get => GetFlag(CorpseFlag.Murderer); set => SetFlag(CorpseFlag.Murderer, value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Owner => m_Owner;

        #region Decay
        private static readonly string m_TimerID = "CorpseDecayTimer";
        private DateTime m_DecayTime;

        public void TurnToBones()
        {
            if (Deleted)
            {
                return;
            }

            ProcessDelta();
            SendRemovePacket();
            ItemID = Utility.Random(0xECA, 9); // bone graphic
            Hue = 0;
            ProcessDelta();

            SetFlag(CorpseFlag.NoBones, true);
            SetFlag(CorpseFlag.IsBones, true);

            var delay = Owner?.CorpseDecayTime ?? Mobile.DefaultCorpseDecay;
            m_DecayTime = DateTime.UtcNow + delay;

            if (!TimerRegistry.UpdateRegistry(m_TimerID, this, delay))
            {
                TimerRegistry.Register(m_TimerID, this, delay, TimerPriority.FiveSeconds, c => c.DoDecay());
            }
        }

        public void BeginDecay(TimeSpan delay)
        {
            m_DecayTime = DateTime.UtcNow + delay;

            TimerRegistry.Register(m_TimerID, this, delay, TimerPriority.FiveSeconds, c => c.DoDecay());
        }

        private void DoDecay()
        {
            if (Owner is CustomPlayerMobile)
            {
                return;
            }
			
			else if (!GetFlag(CorpseFlag.NoBones))
            {
                TurnToBones();
            }
            else
            {
                Delete();
            }
        }
        #endregion

        public static string GetCorpseName(Mobile m)
        {
            if (m is BaseCreature bc && bc.CorpseNameOverride != null)
            {
                return bc.CorpseNameOverride;
            }

            Type t = m.GetType();

            object[] attrs = t.GetCustomAttributes(typeof(CorpseNameAttribute), true);

            if (attrs.Length > 0 && attrs[0] is CorpseNameAttribute attr)
            {
                return attr.Name;
            }

            return null;
        }

        public static void Initialize()
        {
            Mobile.CreateCorpseHandler += Mobile_CreateCorpseHandler;
        }

        public static Container Mobile_CreateCorpseHandler(
            Mobile owner, HairInfo hair, FacialHairInfo facialhair, List<Item> initialContent, List<Item> equipItems)
        {
            var c = new Corpse(owner, hair, facialhair, equipItems);

            owner.Corpse = c;

            for (int i = 0; i < initialContent.Count; ++i)
            {
                Item item = initialContent[i];

                if (owner.Player && item.Parent == owner.Backpack)
                {
                    c.AddItem(item);
                }
                else
                {
                    c.DropItem(item);
                }

                if (owner.Player)
                {
                    c.SetRestoreInfo(item, item.Location);
                }
            }

           
                if (owner is PlayerMobile pm)
                {
                    c.RestoreEquip = pm.EquipSnapshot;
                }
            

            Point3D loc = owner.Location;
            Map map = owner.Map;

            if (map == null || map == Map.Internal)
            {
                loc = owner.LogoutLocation;
                map = owner.LogoutMap;
            }

            c.MoveToWorld(loc, map);

            return c;
        }

        public override bool IsPublicContainer => false;

        public Corpse(Mobile owner, List<Item> equipItems)
            : this(owner, null, null, equipItems)
        { }

        public Corpse(Mobile owner, HairInfo hair, FacialHairInfo facialhair, List<Item> equipItems)
            : base(0x2006)
        {
            Movable = false;

            Stackable = true; // To supress console warnings, stackable must be true
            Amount = owner.Body; // Protocol defines that for itemid 0x2006, amount=body
            Stackable = false;

            Name = owner.Name;
            Hue = owner.Hue;
			m_SnoopingBonusApplied = false;
			Direction = owner.Direction;
            Light = (LightType)Direction;

            m_Owner = owner;

            m_CorpseName = GetCorpseName(owner);

            TimeOfDeath = DateTime.UtcNow;

            m_AccessLevel = owner.AccessLevel;
            Guild = owner.Guild as Guild;
            Kills = owner.Kills;

            SetFlag(CorpseFlag.Criminal, owner.Criminal);
            SetFlag(CorpseFlag.Murderer, owner.Murderer);

            Hair = hair;
            FacialHair = facialhair;

            // This corpse does not turn to bones if: the owner is not a player
            SetFlag(CorpseFlag.NoBones, !owner.Player);

            // Flagging looters as criminal can happen by default
            SetFlag(CorpseFlag.LootCriminal, true);

            m_Looters = new List<Mobile>();
            m_EquipItems = equipItems;

            m_Aggressors = new List<Mobile>(owner.Aggressors.Count + owner.Aggressed.Count);
            //bool addToAggressors = !( owner is BaseCreature );

            BaseCreature bc = owner as BaseCreature;

            TimeSpan lastTime = TimeSpan.MaxValue;

            for (int i = 0; i < owner.Aggressors.Count; ++i)
            {
                AggressorInfo info = owner.Aggressors[i];

                if (DateTime.UtcNow - info.LastCombatTime < lastTime)
                {
                    m_Killer = info.Attacker;
                    lastTime = DateTime.UtcNow - info.LastCombatTime;
                }

                if (bc == null && !info.CriminalAggression)
                {
                    m_Aggressors.Add(info.Attacker);
                }
            }

            for (int i = 0; i < owner.Aggressed.Count; ++i)
            {
                AggressorInfo info = owner.Aggressed[i];

                if (DateTime.UtcNow - info.LastCombatTime < lastTime)
                {
                    m_Killer = info.Defender;
                    lastTime = DateTime.UtcNow - info.LastCombatTime;
                }

                if (bc == null)
                {
                    m_Aggressors.Add(info.Defender);
                }
            }

            if (bc != null)
            {
                Mobile master = bc.GetMaster();

                if (master != null)
                {
                    m_Aggressors.Add(master);
                }

                List<DamageStore> rights = bc.GetLootingRights();
                for (int i = 0; i < rights.Count; ++i)
                {
                    DamageStore ds = rights[i];

                    if (ds.m_HasRight)
                    {
                        m_Aggressors.Add(ds.m_Mobile);
                    }
                }
            }

            BeginDecay(owner.CorpseDecayTime);
            DevourCorpse();

            if (owner is PlayerMobile)
            {
                if (PlayerCorpses == null)
                    PlayerCorpses = new Dictionary<Corpse, int>();

                PlayerCorpses[this] = 0;
            }
        }

        public Corpse(Serial serial)
            : base(serial)
        { }

        public bool GetFlag(CorpseFlag flag)
        {
            return (m_Flags & flag) != 0;
        }

        public void SetFlag(CorpseFlag flag, bool on)
        {
            m_Flags = on ? m_Flags | flag : m_Flags & ~flag;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(13); // version

			writer.Write(m_SnoopingBonusApplied);
			if (RestoreEquip == null)
            {
                writer.Write(false);
            }
            else
            {
                writer.Write(true);
                writer.Write(RestoreEquip);
            }

            writer.Write((int)m_Flags);

            writer.WriteDeltaTime(TimeOfDeath);

            List<KeyValuePair<Item, Point3D>> list = m_RestoreTable == null ? null : new List<KeyValuePair<Item, Point3D>>(m_RestoreTable);
            int count = list?.Count ?? 0;

            writer.Write(count);

            for (int i = 0; i < count; ++i)
            {
                KeyValuePair<Item, Point3D> kvp = list[i];
                Item item = kvp.Key;
                Point3D loc = kvp.Value;

                writer.Write(item);

                if (item.Location == loc)
                {
                    writer.Write(false);
                }
                else
                {
                    writer.Write(true);
                    writer.Write(loc);
                }
            }

            bool decaying = m_DecayTime != DateTime.MinValue;
            writer.Write(decaying);

            if (decaying)
            {
                writer.WriteDeltaTime(m_DecayTime);
            }

            writer.Write(m_Looters);
            writer.Write(m_Killer);

            writer.Write(m_Aggressors);

            writer.Write(m_Owner);

            writer.Write(m_CorpseName);

            writer.Write((int)m_AccessLevel);
            writer.Write(Guild);
            writer.Write(Kills);

            writer.Write(m_EquipItems);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

			switch (version)
			{
				case 13:
					{

						m_SnoopingBonusApplied = reader.ReadBool();

						goto case 12;
					}
				case 12:
					{
                        if (reader.ReadBool())
                        {
                            RestoreEquip = reader.ReadStrongItemList();
                        }

                        goto case 11;
                    }
                case 11:
                    {
                        // Version 11, we move all bools to a CorpseFlag
                        m_Flags = (CorpseFlag)reader.ReadInt();
                        TimeOfDeath = reader.ReadDeltaTime();

                        int count = reader.ReadInt();

                        for (int i = 0; i < count; ++i)
                        {
                            Item item = reader.ReadItem();

                            if (reader.ReadBool())
                            {
                                SetRestoreInfo(item, reader.ReadPoint3D());
                            }
                            else if (item != null)
                            {
                                SetRestoreInfo(item, item.Location);
                            }
                        }

                        if (reader.ReadBool())
                        {
                            BeginDecay(reader.ReadDeltaTime() - DateTime.UtcNow);
                        }

                        m_Looters = reader.ReadStrongMobileList();
                        m_Killer = reader.ReadMobile();

                        m_Aggressors = reader.ReadStrongMobileList();
                        m_Owner = reader.ReadMobile();

                        m_CorpseName = reader.ReadString();

                        m_AccessLevel = (AccessLevel)reader.ReadInt();
                        reader.ReadInt(); // guild reserve
                        Kills = reader.ReadInt();

                        m_EquipItems = reader.ReadStrongItemList();
                        break;
                    }
            }

            if (m_Owner is PlayerMobile)
            {
                if (PlayerCorpses == null)
                    PlayerCorpses = new Dictionary<Corpse, int>();

                PlayerCorpses[this] = 0;
            }
        }

        public bool DevourCorpse()
        {
            if (Devoured || Deleted || m_Killer == null || m_Killer.Deleted || !m_Killer.Alive || !(m_Killer is IDevourer) ||
                m_Owner == null || m_Owner.Deleted)
            {
                return false;
            }

            m_Devourer = (IDevourer)m_Killer; // Set the devourer the killer
            return m_Devourer.Devour(this); // Devour the corpse if it hasn't
        }

        public override void SendInfoTo(NetState state, bool sendOplPacket)
        {
            base.SendInfoTo(state, sendOplPacket);

            if (((Body)Amount).IsHuman && ItemID == 0x2006)
            {
                state.Send(new CorpseContent(state.Mobile, this));

                state.Send(new CorpseEquip(state.Mobile, this));
            }
        }

        public bool IsCriminalAction(Mobile from)
        {
            if (from == m_Owner || from.AccessLevel >= AccessLevel.GameMaster)
            {
                return false;
            }

            if (!GetFlag(CorpseFlag.LootCriminal))
                return false;

            Party p = Party.Get(m_Owner);

            if (p != null && p.Contains(from))
            {
                PartyMemberInfo pmi = p[m_Owner];

                if (pmi != null && pmi.CanLoot)
                {
                    return false;
                }
            }

            return NotorietyHandlers.CorpseNotoriety(from, this) == Notoriety.Innocent;
        }

        public override bool CheckItemUse(Mobile from, Item item)
        {
            if (!base.CheckItemUse(from, item))
            {
                return false;
            }

            if (item != this)
            {
                return CanLoot(from);
            }

            return true;
        }

        public override bool CheckLift(Mobile from, Item item, ref LRReason reject)
        {
            if (!base.CheckLift(from, item, ref reject))
            {
                return false;
            }

            bool canLoot = CanLoot(from);

            if (m_HasLooted == null)
                m_HasLooted = new List<Item>();

            if (canLoot && !m_HasLooted.Contains(item))
            {
                m_HasLooted.Add(item);
                EventSink.InvokeCorpseLoot(new CorpseLootEventArgs(from, this, item));
            }

            return canLoot;
        }

        public override void OnItemUsed(Mobile from, Item item)
        {
            base.OnItemUsed(from, item);

            if (item is Food)
            {
                from.RevealingAction();
            }

            if (item != this && IsCriminalAction(from))
            {
                from.CriminalAction(true);
            }

            if (!m_Looters.Contains(from))
            {
                m_Looters.Add(from);
            }
        }

        public override void OnItemLifted(Mobile from, Item item)
        {
            base.OnItemLifted(from, item);

            if (item != this && from != m_Owner)
            {
                from.RevealingAction();
            }

            if (item != this && IsCriminalAction(from))
            {
                from.CriminalAction(true);
            }

            if (!m_Looters.Contains(from))
            {
                m_Looters.Add(from);
            }
        }

        private class OpenCorpseEntry : ContextMenuEntry
        {
            public OpenCorpseEntry()
                : base(6215, 2)
            { }

            public override void OnClick()
            {
                if (Owner.Target is Corpse corpse && Owner.From.CheckAlive())
                {
                    corpse.Open(Owner.From, false);
                }
            }
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);

            if (m_Owner == from && from.Alive)
            {
                list.Add(new OpenCorpseEntry());
            }
        }

        private Dictionary<Item, Point3D> m_RestoreTable;

        public bool GetRestoreInfo(Item item, ref Point3D loc)
        {
            if (m_RestoreTable == null || item == null)
            {
                return false;
            }

            return m_RestoreTable.TryGetValue(item, out loc);
        }

        public void SetRestoreInfo(Item item, Point3D loc)
        {
            if (item == null)
            {
                return;
            }

            if (m_RestoreTable == null)
            {
                m_RestoreTable = new Dictionary<Item, Point3D>();
            }

            m_RestoreTable[item] = loc;
        }

        public void ClearRestoreInfo(Item item)
        {
            if (m_RestoreTable == null || item == null)
            {
                return;
            }

            m_RestoreTable.Remove(item);

            if (m_RestoreTable.Count == 0)
            {
                m_RestoreTable = null;
            }
        }

        public bool CanLoot(Mobile from)
        {
            if (!IsCriminalAction(from))
            {
                return true;
            }

            Map map = Map;

            if (map == null || (map.Rules & MapRules.HarmfulRestrictions) != 0)
            {
                return false;
            }

            return true;
        }

        public bool CheckLoot(Mobile from)
        {
            if (!CanLoot(from))
            {
                if (m_Owner == null || !m_Owner.Player)
                {
                    from.SendLocalizedMessage(1005035); // You did not earn the right to loot this creature!
                }
                else
                {
                    from.SendLocalizedMessage(1010049); // You may not loot this corpse.
                }

                return false;
            }

            if (IsCriminalAction(from))
            {
                if (m_Owner == null || !m_Owner.Player)
                {
                    from.SendLocalizedMessage(1005036); // Looting this monster corpse will be a criminal act!
                }
                else
                {
                    from.SendLocalizedMessage(1005038); // Looting this corpse will be a criminal act!
                }
            }

            return true;
        }

        public virtual void Open(Mobile from, bool checkSelfLoot)
        {
            if (from.IsStaff() || from.InRange(GetWorldLocation(), 2))
            {
                #region Self Looting
                bool selfLoot = checkSelfLoot && from == m_Owner;

                if (selfLoot)
                {
                    SetFlag(CorpseFlag.SelfLooted, true);

                    List<Item> items = new List<Item>(Items);

                    bool gathered = false;

                    for (int k = 0; k < EquipItems.Count; ++k)
                    {
                        Item item2 = EquipItems[k];

                        if (!items.Contains(item2) && item2.IsChildOf(from.Backpack))
                        {
                            items.Add(item2);
                            gathered = true;
                        }
                    }

                    bool didntFit = false;

                    Container pack = from.Backpack;

                    for (int i = 0; !didntFit && i < items.Count; ++i)
                    {
                        Item item = items[i];
                        Point3D loc = item.Location;

                        if (item.Layer == Layer.Hair || item.Layer == Layer.FacialHair || !item.Movable)
                        {
                            continue;
                        }

                        if (from.FindItemOnLayer(Layer.OuterTorso) is DeathRobe robe)
                        {
                            robe.Delete();
                        }

                        if (m_EquipItems.Contains(item) && from.EquipItem(item))
                        {
                            gathered = true;
                        }
                        else if (m_RestoreTable != null && pack != null && pack.CheckHold(from, item, false, true) && m_RestoreTable.ContainsKey(item))
                        {
                            item.Location = loc;
                            pack.AddItem(item);
                            gathered = true;
                        }
                        else
                        {
                            didntFit = true;
                        }
                    }

                    if (gathered && !didntFit)
                    {
                        SetFlag(CorpseFlag.Carved, true);

                        if (ItemID == 0x2006)
                        {
                            ProcessDelta();
                            SendRemovePacket();
                            ItemID = Utility.Random(0xECA, 9); // bone graphic
                            Hue = 0;
                            ProcessDelta();
                        }

                        from.PlaySound(0x3E3);
                        from.SendLocalizedMessage(1062471); // You quickly gather all of your belongings.
                        items.Clear();
                        m_EquipItems.Clear();
                        return;
                    }

                    if (gathered)
                    {
                        from.SendLocalizedMessage(1062472); // You gather some of your belongings. The rest remain on the corpse.
                    }
                }
                #endregion

                if (!CheckLoot(from))
                {
                    return;
                }

                #region Quests
                if (from is PlayerMobile player)
                {
                    QuestSystem qs = player.Quest;

                    if (qs is TheSummoningQuest && qs.FindObjective(typeof(VanquishDaemonObjective)) is VanquishDaemonObjective obj && obj.Completed && obj.CorpseWithSkull == this)
                    {
                        GoldenSkull sk = new GoldenSkull();

                        if (player.PlaceInBackpack(sk))
                        {
                            obj.CorpseWithSkull = null;
                            qs.Complete();
                            player.SendLocalizedMessage(1050022); // For your valor in combating the devourer, you have been awarded a golden skull.
                        }
                        else
                        {
                            sk.Delete();
                            player.SendLocalizedMessage(1050023); // You find a golden skull, but your backpack is too full to carry it.
                        }
                    }
                }
				#endregion

				if (from is PlayerMobile && from != m_Owner && !(m_Owner is PlayerMobile))
				{
					ApplySnoopingBonus(from);
				}


				base.OnDoubleClick(from);
            }
            else
            {
                from.SendLocalizedMessage(500446); // That is too far away.
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            Open(from, true);

            if (m_Owner == from)
            {
                if (from.Corpse != null)
                    from.NetState.Send(new RemoveWaypoint(from.Corpse.Serial));
            }
			if (from is PlayerMobile player && !m_SnoopingBonusApplied)
			{
				ApplySnoopingBonus(player);
			}
		}

        public override bool CheckContentDisplay(Mobile from)
        {
            return false;
        }

        public override bool DisplaysContent => false;

        public override void AddNameProperty(ObjectPropertyList list)
        {
            if (ItemID == 0x2006) // Corpse form
            {
                if (m_CorpseName != null)
                {
                    list.Add(m_CorpseName);
                }
                else
                {
                    list.Add(1046414, Name); // the remains of ~1_NAME~
                }
            }
            else // Bone form
            {
                list.Add(1046414, Name); // the remains of ~1_NAME~
            }
        }

        public override void OnAosSingleClick(Mobile from)
        {
            int hue = Notoriety.GetHue(NotorietyHandlers.CorpseNotoriety(from, this));
            ObjectPropertyList opl = PropertyList;

            if (opl.Header > 0)
            {
                from.Send(new MessageLocalized(Serial, ItemID, MessageType.Label, hue, 3, opl.Header, Name, opl.HeaderArgs));
            }
        }

        public bool Carve(Mobile from, Item item)
        {
            if (IsCriminalAction(from) && Map != null && (Map.Rules & MapRules.HarmfulRestrictions) != 0)
            {
                if (m_Owner == null || !m_Owner.Player)
                {
                    from.SendLocalizedMessage(1005035); // You did not earn the right to loot this creature!
                }
                else
                {
                    from.SendLocalizedMessage(1010049); // You may not loot this corpse.
                }

                return false;
            }

            Mobile dead = m_Owner;

            if (GetFlag(CorpseFlag.Carved) || dead == null)
            {
                PrivateOverheadMessage(MessageType.Regular, 0x3B2, 500485, from.NetState); // You see nothing useful to carve from the corpse.
            }
            else if (dead is PlayerMobile && ((Body)Amount).IsHuman && ItemID == 0x2006)
            {
                new Blood(0x122D).MoveToWorld(Location, Map);

                new Torso().MoveToWorld(Location, Map);
                new LeftLeg().MoveToWorld(Location, Map);
                new LeftArm().MoveToWorld(Location, Map);
                new RightLeg().MoveToWorld(Location, Map);
                new RightArm().MoveToWorld(Location, Map);
                new Head(dead.Name).MoveToWorld(Location, Map);

                SetFlag(CorpseFlag.Carved, true);

                ProcessDelta();
                SendRemovePacket();
                ItemID = Utility.Random(0xECA, 9); // bone graphic
                Hue = 0;
                ProcessDelta();

                if (IsCriminalAction(from))
                {
                    from.CriminalAction(true);
                }
            }
            else if (dead is BaseCreature creature)
            {
                creature.OnCarve(from, this, item);
            }
            else
            {
                from.SendLocalizedMessage(500485); // You see nothing useful to carve from the corpse.
            }

            return true;
        }
		public void ApplySnoopingBonus(Mobile from)
		{
			if (!(from is PlayerMobile player) || m_Owner is PlayerMobile || m_SnoopingBonusApplied)
				return;

			if (m_Owner is BaseCreature ownerCreature && ownerCreature.Tamable)
				return;

			m_SnoopingBonusApplied = true;

			double snoopingSkill = player.Skills[SkillName.Snooping].Value;
			double baseChance = 0.0; // 0% chance de base
			double snoopingBonus = snoopingSkill * 0.005; // 1% par point de compétence
			double totalChance = Math.Min(baseChance + snoopingBonus, 0.50); // Plafond à 50%

			if (Utility.RandomDouble() < totalChance)
			{
				Item bonusLoot = CreateBonusLoot();
				if (bonusLoot != null)
				{
					DropItem(bonusLoot);
					player.SendMessage("Fouiller la dépouille vous a permis de trouver : " + bonusLoot.Name);
					player.CheckSkill(SkillName.Snooping, 0.0, 100.0); // Chance d'augmenter la compétence Snooping
				}
			}
			else
			{
				player.SendMessage("Vous ne trouvez rien d'intéressant.");
			}
		}

		private Item CreateBonusLoot()
		{
			// Liste des types d'items pour le bonus de loot
			Type[] bonusItemTypes = new Type[]
			{
		typeof(Gold),
	typeof(Bandage),
	typeof(Bottle),
	typeof(Lantern),
	typeof(Candle),
	typeof(Torch),
	typeof(LesserHealPotion),
	typeof(LesserCurePotion),
	typeof(LesserPoisonPotion),
	typeof(LesserExplosionPotion),
	typeof(AgilityPotion),
	typeof(LesserStrengthPotion),
	typeof(GreaterHealPotion),
	typeof(GreaterCurePotion),
	typeof(GreaterPoisonPotion),
	typeof(GreaterExplosionPotion),
	typeof(GreaterAgilityPotion),
	typeof(GreaterStrengthPotion),
	typeof(TotalRefreshPotion),
	typeof(Arrow),
	typeof(Bolt),
	typeof(Bow),
	typeof(Crossbow),
	typeof(Dagger),
	typeof(Katana),
	typeof(Kryss),
	typeof(Longsword),
	typeof(Mace),
	typeof(Spear),
	typeof(WarHammer),
	typeof(BlackPearl),
	typeof(Bloodmoss),
	typeof(Garlic),
	typeof(Ginseng),
	typeof(MandrakeRoot),
	typeof(Nightshade),
	typeof(SulfurousAsh),
	typeof(SpidersSilk),
	typeof(BreadLoaf),
	typeof(Fish),
	typeof(Apple),
	typeof(CheesePizza),
	typeof(RawRibs),
	typeof(CookedBird),
	typeof(WoodenBox),
	typeof(MetalBox),
	typeof(Key),
	typeof(Lockpick),
	typeof(Bedroll),
	typeof(Backpack),
	typeof(LeatherGorget),
	typeof(LeatherGloves),
	typeof(LeatherArms),
	typeof(LeatherLegs),
	typeof(LeatherChest),
	typeof(Robe),
	typeof(Diamant),
	typeof(Rubis),
	typeof(Citrine),
	typeof(Tourmaline),
	typeof(Amethyste),
	typeof(Emeraude),
	typeof(Sapphire),
	typeof(SaphirEtoile),
	typeof(ClockworkAssembly),
	typeof(Clock),
	typeof(BarrelHoops),
	typeof(BarrelStaves),
	typeof(Springs),
	typeof(Gears),
	typeof(Hinge),
	typeof(SextantParts),
	typeof(Axle),
	typeof(Nails),
	typeof(JointingPlane),
	typeof(MouldingPlane),
	typeof(SmoothingPlane),
	typeof(Saw),
	typeof(Scorp),
	typeof(Inshave),
	typeof(Froe),
	typeof(Shovel),
	typeof(Hammer),
	typeof(DrawKnife),
	typeof(Pickaxe),
	typeof(Pitchfork),
	typeof(Tinker),
	typeof(TinkerTools),
	typeof(SmithHammer),
	typeof(SewingKit),
	typeof(FletcherTools),
	typeof(MapmakersPen),
	typeof(ScribesPen),
	typeof(Scales),
	typeof(MortarPestle),
	typeof(Spellbook),
	typeof(SkillCard),
	typeof(BlankScroll)
};


			Type itemType = bonusItemTypes[Utility.Random(bonusItemTypes.Length)];
			Item item = null;

			try
			{
				if (itemType == typeof(Gold))
				{
					int amount = Utility.RandomMinMax(15, 75);
					item = new Gold(amount);
				}
				else
				{
					item = (Item)Activator.CreateInstance(itemType);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Erreur lors de la création de l'item bonus : " + e.Message);
			}

			return item;
		}

		public override void Delete()
        {
            base.Delete();

            if (PlayerCorpses != null && PlayerCorpses.Remove(this))
            {
                if (PlayerCorpses.Count == 0)
                    PlayerCorpses = null;
            }

            if (m_HasLooted != null)
            {
                ColUtility.Free(m_HasLooted);
                m_HasLooted = null;
            }
        }

        public static Dictionary<Corpse, int> PlayerCorpses { get; set; }
    }
}

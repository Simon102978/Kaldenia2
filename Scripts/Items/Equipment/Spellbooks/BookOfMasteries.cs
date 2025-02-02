using Server.ContextMenus;
using Server.Gumps;
using Server.Mobiles;
using Server.Spells.SkillMasteries;
using Server.Spells.Spellweaving;
using System;
using System.Collections.Generic;

namespace Server.Items
{
    [Flipable(0x225A, 0x225B, 0xA3F3, 0xA3F9)]
    public class BookOfMasteries : Spellbook
    {
        public override SpellbookType SpellbookType => SpellbookType.SkillMasteries;
        public override int BookOffset => 700;
        public override int BookCount => 45;

        private ulong _Content;

        [CommandProperty(AccessLevel.GameMaster)]
        public new ulong Content
        {
            get
            {
                return _Content;
            }
            set
            {
                if (_Content != value)
                {
                    _Content = value;

                    InvalidateProperties();
                }
            }
        }

        [Constructable]
        public BookOfMasteries() : this(0x1FFFFFFFFFFF)
        {
        }

        [Constructable]
        public BookOfMasteries(ulong content) : base(content, 0x225A)
        {
            _Content = content;
			Name = "Grand Livre du Barde";
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);

            SimpleContextMenuEntry menu = new SimpleContextMenuEntry(from, 1151948, m =>
                {
                    if (m is PlayerMobile && IsChildOf(m.Backpack) && CheckCooldown(m))
                        BaseGump.SendGump(new MasterySelectionGump(m as PlayerMobile, this));
                });

            if (!IsChildOf(from.Backpack) || !CheckCooldown(from))
                menu.Enabled = false;

            list.Add(menu);
        }

        private static Dictionary<Mobile, DateTime> m_Cooldown = new Dictionary<Mobile, DateTime>();

        public static void AddToCooldown(Mobile from)
        {
            if (m_Cooldown == null)
                m_Cooldown = new Dictionary<Mobile, DateTime>();

            m_Cooldown[from] = DateTime.UtcNow + TimeSpan.FromMinutes(10);
        }

        public static bool CheckCooldown(Mobile from)
        {
            if (from.AccessLevel > AccessLevel.Player)
                return true;

            if (m_Cooldown != null && m_Cooldown.ContainsKey(from))
            {
                if (m_Cooldown[from] < DateTime.UtcNow)
                {
                    m_Cooldown.Remove(from);
                    return true;
                }

                return false;
            }

            return true;
        }

        public override void AddProperty(ObjectPropertyList list)
        {
            base.AddProperty(list);

            if (RootParent is Mobile)
            {
                SkillName sk = ((Mobile)RootParent).Skills.CurrentMastery;

                if (sk > 0)
                {
                    list.Add(MasteryInfo.GetLocalization(sk));

                    if (sk == SkillName.Spellweaving)
                    {
                        list.Add(1060485, ArcanistSpell.GetMasteryFocusLevel((Mobile)RootParent).ToString()); // strength bonus ~1_val~
                    }
                }
            }
        }

        public BookOfMasteries(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.WriteEncodedInt(1); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadEncodedInt();
        }
    }
}
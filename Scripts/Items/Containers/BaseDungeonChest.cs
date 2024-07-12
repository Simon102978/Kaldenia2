using System;
using Server.Mobiles;

namespace Server.Items
{
    public abstract class BaseDungeonChest : LockableContainer
    {
        public static readonly string m_DeleteTimerID = "DungeonChest";
        public override int DefaultGumpID => 0x42;
        public override int DefaultDropSound => 0x42;
        public override Rectangle2D Bounds => new Rectangle2D(20, 105, 150, 180);
        public override bool IsDecoContainer => false;

		public virtual int Level => 0;

		public bool Mimic { get; set; }

		public BaseDungeonChest(int itemID) : base(itemID)
        {
            Locked = true;
            Movable = false;

            Key key = (Key)FindItemByType(typeof(Key));

            if (key != null)
                key.Delete();

            RefinementComponent.Roll(this, 1, 0.08);
        }

        public BaseDungeonChest(Serial serial) : base(serial)
        {
        }

		private void AddRandomLoot(int minItems, int maxItems)
		{
			int itemCount = Utility.RandomMinMax(minItems, maxItems);
			for (int i = 0; i < itemCount; i++)
			{
				switch (Utility.Random(10))
				{
					case 0:
						DropItem(new Bandage(Utility.RandomMinMax(1, 5)));
						break;
					case 1:
						DropItem(Loot.RandomPotion());
						break;
					case 2:
						DropItem(Loot.RandomReagent());
						break;
					case 3:
						DropItem(Loot.RandomGem());
						break;
					case 4:
						DropItem(Loot.RandomJewelry());
						break;
				
					case 5:
						DropItem(Loot.RandomScroll(0, 39, SpellbookType.Regular));
						break;
					case 6:
						DropItem(Loot.RandomArmorOrShieldOrWeaponOrJewelry());
						break;
						case 7:
						DropItem(Loot.RandomWeapon());
						break;
					case 8:
						DropItem(new Bottle());
						break;
				}
			}
		}

		public override void Open(Mobile from)
		{
			if (CheckLocked(from))
				return;

			if (!from.IsStaff() && !Mimic && Utility.Random(100) <= Level * 3)
			{

				TransformMimic(from);
			}
			else
			{

				if (!Mimic)
				{
					Mimic = true;
				}

				base.Open(from);
			}
		
		}

		public virtual void TransformMimic(Mobile Combatant)
		{

			Mimic helper = new Mimic();


			helper.Home = this.Location;
			helper.RangeHome = 4;
			helper.Combatant = Combatant;
			helper.Warmode = true;

		
			helper.MoveToWorld(this.Location, Map);

			

			this.Delete();




		}









		public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(1);

            writer.Write(TimerRegistry.HasTimer(m_DeleteTimerID, this));
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version == 1 && reader.ReadBool())
            {
                StartDeleteTimer();
            }
        }

        public override void OnTelekinesis(Mobile from)
        {
            if (CheckLocked(from))
            {
                Effects.SendLocationParticles(EffectItem.Create(Location, Map, EffectItem.DefaultDuration), 0x376A, 9, 32, 5022);
                Effects.PlaySound(Location, Map, 0x1F5);
                return;
            }

            base.OnTelekinesis(from);
            Name = "coffre au trésor";
			StartDeleteTimer();
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (CheckLocked(from))
                return;

            base.OnDoubleClick(from);
            Name = "coffre au trésor";
            StartDeleteTimer();
        }

        protected void AddLoot(Item item)
        {
            if (item == null)
                return;

            if (RandomItemGenerator.Enabled)
            {
                int min, max;
 ///               TreasureMapChest.GetRandomItemStat(out min, out max);

      ///          RunicReforging.GenerateRandomItem(item, 0, min, max);
            }

            DropItem(item);
        }








		private void StartDeleteTimer()
        {
            TimerRegistry.Register(m_DeleteTimerID, this, TimeSpan.FromMinutes(Utility.RandomMinMax(10, 15)), chest => chest.Delete());
        }
    }
}

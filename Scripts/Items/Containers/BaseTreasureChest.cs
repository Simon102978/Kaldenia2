using System;

namespace Server.Items
{
    public class BaseTreasureChest : LockableContainer
    {
        private TreasureLevel m_TreasureLevel;
        private short m_MaxSpawnTime = 60;
        private short m_MinSpawnTime = 10;
        private TreasureResetTimer m_ResetTimer;
        public BaseTreasureChest(int itemID)
            : this(itemID, TreasureLevel.Level2)
        {
        }

        public BaseTreasureChest(int itemID, TreasureLevel level)
            : base(itemID)
        {
            m_TreasureLevel = level;
            Locked = true;
            Movable = false;

            SetLockLevel();
            GenerateTreasure();
        }

        public BaseTreasureChest(Serial serial)
            : base(serial)
        {
        }

        public enum TreasureLevel
        {
            Level1,
            Level2,
            Level3,
            Level4,
            Level5,
            Level6,
        }

		public int GetLevel()
		{
			switch (m_TreasureLevel)
			{
				case TreasureLevel.Level1:
					return 1;
				case TreasureLevel.Level2:
					return 2;
				case TreasureLevel.Level3:
					return 3;
				case TreasureLevel.Level4:
					return 4;
				case TreasureLevel.Level5:
					return 5;
				case TreasureLevel.Level6:
					return 6;
				default:
					return 0;
			}



		}








        [CommandProperty(AccessLevel.GameMaster)]
        public TreasureLevel Level
        {
            get
            {
                return m_TreasureLevel;
            }
            set
            {
                m_TreasureLevel = value;
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public short MaxSpawnTime
        {
            get
            {
                return m_MaxSpawnTime;
            }
            set
            {
                m_MaxSpawnTime = value;
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public short MinSpawnTime
        {
            get
            {
                return m_MinSpawnTime;
            }
            set
            {
                m_MinSpawnTime = value;
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public override bool Locked
        {
            get
            {
                return base.Locked;
            }
            set
            {
                if (base.Locked != value)
                {
                    base.Locked = value;

                    if (!value)
                        StartResetTimer();
                }
            }
        }
        public override bool IsDecoContainer => false;
        public override string DefaultName
        {
            get
            {
                if (Locked)
                    return "a locked treasure chest";

                return "a treasure chest";
            }
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0);
            writer.Write((byte)m_TreasureLevel);
            writer.Write(m_MinSpawnTime);
            writer.Write(m_MaxSpawnTime);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_TreasureLevel = (TreasureLevel)reader.ReadByte();
            m_MinSpawnTime = reader.ReadShort();
            m_MaxSpawnTime = reader.ReadShort();

            if (!Locked)
                StartResetTimer();
        }

        public void ClearContents()
        {
            for (int i = Items.Count - 1; i >= 0; --i)
            {
                if (i < Items.Count)
                    Items[i].Delete();
            }
        }

        public void Reset()
        {
            if (m_ResetTimer != null)
            {
                if (m_ResetTimer.Running)
                    m_ResetTimer.Stop();
            }

            Locked = true;
            ClearContents();
            GenerateTreasure();
        }

        protected virtual void SetLockLevel()
        {
            switch (m_TreasureLevel)
            {
                case TreasureLevel.Level1:
                    RequiredSkill = LockLevel = 5;
                    break;
                case TreasureLevel.Level2:
                    RequiredSkill = LockLevel = 20;
                    break;
                case TreasureLevel.Level3:
                    RequiredSkill = LockLevel = 50;
                    break;
                case TreasureLevel.Level4:
                    RequiredSkill = LockLevel = 70;
                    break;
                case TreasureLevel.Level5:
                    RequiredSkill = LockLevel = 90;
                    break;
                case TreasureLevel.Level6:
                    RequiredSkill = LockLevel = 100;
                    break;
            }
        }

		protected virtual void GenerateTreasure()
		{
			int MinGold = 1;
			int MaxGold = 2;

			switch (m_TreasureLevel)
			{
				case TreasureLevel.Level1:
					MinGold = 50;
					MaxGold = 100;
					GenerateLevel1Loot();
					break;
				case TreasureLevel.Level2:
					MinGold = 200;
					MaxGold = 400;
					GenerateLevel2Loot();
					break;
				case TreasureLevel.Level3:
					MinGold = 500;
					MaxGold = 1000;
					GenerateLevel3Loot();
					break;
				case TreasureLevel.Level4:
					MinGold = 1000;
					MaxGold = 2000;
					GenerateLevel4Loot();
					break;
				case TreasureLevel.Level5:
					MinGold = 2000;
					MaxGold = 5000;
					GenerateLevel5Loot();
					break;
				case TreasureLevel.Level6:
					MinGold = 5000;
					MaxGold = 10000;
					GenerateLevel6Loot();
					break;
			}

			DropItem(new Gold(MinGold, MaxGold));
		}

		private void GenerateLevel1Loot()
		{
			DropItem(new Bolt(Utility.RandomMinMax(10, 20)));
			DropItem(new Arrow(Utility.RandomMinMax(10, 20)));
			DropItem(new Tourmaline(Utility.RandomMinMax(1, 5)));
			DropItem(new Ambre(Utility.RandomMinMax(1, 5)));
			DropItem(new Citrine(Utility.RandomMinMax(2, 8)));

			int itemCount = Utility.RandomMinMax(5, 10);
			for (int i = 0; i < itemCount; i++)
			{
				switch (Utility.Random(9))
				{
					case 0: DropItem(new Bandage(Utility.RandomMinMax(5, 15))); break;
					case 1: DropItem(Loot.RandomPotion()); break;
					case 2: DropItem(Loot.RandomReagent()); break;
					case 3: DropItem(Loot.RandomGem()); break;
					case 4: DropItem(Loot.RandomWeapon()); break;
					case 5: DropItem(Loot.RandomScroll(0, 39, SpellbookType.Regular)); break;
					case 6: DropItem(Loot.RandomArmorOrShieldOrWeaponOrJewelry()); break;
					case 7: DropItem(new Bottle()); break;
					case 8: DropItem(Loot.RandomInstrument()); break;
				}
			}

			if (Utility.RandomDouble() < 0.10) // Augmenté à 10%
				DropItem(Loot.RandomStealableArtifact());
		}

		private void GenerateLevel2Loot()
		{
			DropItem(new Tourmaline(Utility.Random(5)));
			DropItem(new Ambre(Utility.Random(4)));
			DropItem(new Citrine(Utility.Random(7)));
			DropItem(new DragonBlood(Utility.Random(1, 2)));
			DropItem(new Bolt(Utility.RandomMinMax(15, 30)));
			DropItem(new Arrow(Utility.RandomMinMax(15, 30)));
			DropItem(Loot.RandomPotion());

			for (int i = Utility.Random(2, 3); i > 0; i--)
			{
				Item ReagentLoot = Loot.RandomReagent();
				ReagentLoot.Amount = Utility.Random(2, 5);
				DropItem(ReagentLoot);
			}

			if (Utility.RandomBool())
				for (int i = Utility.Random(10) + 1; i > 0; i--)
					DropItem(Loot.RandomScroll(0, 39, SpellbookType.Regular));

			if (Utility.RandomBool())
				for (int i = Utility.Random(8) + 1; i > 0; i--)
					DropItem(Loot.RandomGem());

			int itemCount = Utility.RandomMinMax(10, 15);
			for (int i = 0; i < itemCount; i++)
			{
				switch (Utility.Random(11))
				{
					case 0: DropItem(new Arrow(Utility.RandomMinMax(15, 30))); break;
					case 1: DropItem(new Bolt(Utility.RandomMinMax(15, 30))); break;
					case 2: DropItem(Loot.RandomWeapon()); break;
					case 3: DropItem(Loot.RandomArmor()); break;
					case 4: DropItem(Loot.RandomClothing()); break;
					case 5: DropItem(Loot.RandomHat()); break;
					case 6: DropItem(Loot.RandomJewelry()); break;
					case 7: DropItem(Loot.RandomPotion()); break;
					case 8: DropItem(Loot.RandomScroll(0, 39, SpellbookType.Regular)); break;
					case 9: DropItem(Loot.RandomReagent()); break;
					case 10: DropItem(Loot.RandomInstrument()); break;
				}
			}

			if (Utility.RandomDouble() < 0.20) // Augmenté à 20%
				DropItem(Loot.RandomStealableArtifact());
		}

		private void GenerateLevel3Loot()
		{
			DropItem(new DragonBlood(Utility.Random(2, 4)));
			DropItem(new Arrow(Utility.RandomMinMax(20, 40)));
			DropItem(new Bolt(Utility.RandomMinMax(20, 40)));

			for (int i = Utility.Random(2, 4); i > 0; i--)
			{
				Item ReagentLoot = Loot.RandomReagent();
				ReagentLoot.Amount = Utility.Random(5, 15);
				DropItem(ReagentLoot);
			}

			for (int i = Utility.Random(2, 4); i > 0; i--)
				DropItem(Loot.RandomPotion());

			if (0.75 > Utility.RandomDouble())
				for (int i = Utility.Random(15) + 1; i > 0; i--)
					DropItem(Loot.RandomScroll(0, 47, SpellbookType.Regular));

			if (0.75 > Utility.RandomDouble())
				for (int i = Utility.Random(12) + 1; i > 0; i--)
					DropItem(Loot.RandomGem());

			int itemCount = Utility.RandomMinMax(10, 15);
			for (int i = 0; i < itemCount; i++)
			{
				switch (Utility.Random(13))
				{
					case 0: DropItem(new Arrow(Utility.RandomMinMax(25, 50))); break;
					case 1: DropItem(new Bolt(Utility.RandomMinMax(25, 50))); break;
					case 2: DropItem(Loot.RandomWeapon()); break;
					case 3: DropItem(Loot.RandomArmor()); break;
					case 4: DropItem(Loot.RandomClothing()); break;
					case 5: DropItem(Loot.RandomHat()); break;
					case 6: DropItem(Loot.RandomJewelry()); break;
					case 7: DropItem(Loot.RandomPotion()); break;
					case 8: DropItem(Loot.RandomScroll(0, 47, SpellbookType.Regular)); break;
					case 9: DropItem(Loot.RandomReagent()); break;
					case 10: DropItem(Loot.RandomArmorOrShieldOrWeapon()); break;
					case 11: DropItem(Loot.RandomShield()); break;
					case 12: DropItem(Loot.RandomInstrument()); break;
				}
			}

			if (Utility.RandomDouble() < 0.35) // Augmenté à 35%
				DropItem(Loot.RandomStealableArtifact());
		}

		private void GenerateLevel4Loot()
		{
			DropItem(new BlankScroll(Utility.Random(2, 6)));
			DropItem(new DragonBlood(Utility.Random(3, 6)));

			for (int i = Utility.Random(3, 6); i > 0; i--)
			{
				Item ReagentLoot = Loot.RandomReagent();
				ReagentLoot.Amount = Utility.Random(15, 20);
				DropItem(ReagentLoot);
			}

			for (int i = Utility.Random(3, 6); i > 0; i--)
				DropItem(Loot.RandomPotion());

			if (0.85 > Utility.RandomDouble())
				for (int i = Utility.RandomMinMax(12, 20); i > 0; i--)
					DropItem(Loot.RandomScroll(0, 47, SpellbookType.Regular));

			if (0.85 > Utility.RandomDouble())
				for (int i = Utility.RandomMinMax(10, 16) + 1; i > 0; i--)
					DropItem(Loot.RandomGem());

			int itemCount = Utility.RandomMinMax(10, 15);
			for (int i = 0; i < itemCount; i++)
			{
				switch (Utility.Random(15))
				{
					case 0: DropItem(new Arrow(Utility.RandomMinMax(40, 80))); break;
					case 1: DropItem(new Bolt(Utility.RandomMinMax(40, 80))); break;
					case 2: DropItem(Loot.RandomWeapon()); break;
					case 3: DropItem(Loot.RandomArmor()); break;
					case 4: DropItem(Loot.RandomClothing()); break;
					case 5: DropItem(Loot.RandomHat()); break;
					case 6: DropItem(Loot.RandomJewelry()); break;
					case 7: DropItem(Loot.RandomPotion()); break;
					case 8: DropItem(Loot.RandomScroll(0, 47, SpellbookType.Regular)); break;
					case 9: DropItem(Loot.RandomReagent()); break;
					case 10: DropItem(Loot.RandomArmorOrShieldOrWeapon()); break;
					case 11: DropItem(Loot.RandomShield()); break;
					case 12: DropItem(Loot.RandomInstrument()); break;
					case 13: DropItem(new Bandage(Utility.RandomMinMax(10, 25))); break;
				}
			}

			if (Utility.RandomDouble() < 0.50) // Augmenté à 50%
				DropItem(Loot.RandomStealableArtifact());
		}

		private void GenerateLevel5Loot()
		{
			DropItem(new BlankScroll(Utility.Random(4, 8)));
			DropItem(new DragonBlood(Utility.Random(5, 10)));

			for (int i = Utility.Random(4, 8); i > 0; i--)
			{
				Item ReagentLoot = Loot.RandomReagent();
				ReagentLoot.Amount = Utility.Random(15, 30);
				DropItem(ReagentLoot);
			}

			for (int i = Utility.Random(4, 8); i > 0; i--)
				DropItem(Loot.RandomPotion());

			if (0.90 > Utility.RandomDouble())
				for (int i = Utility.RandomMinMax(15, 25); i > 0; i--)
					DropItem(Loot.RandomScroll(0, 63, SpellbookType.Regular));

			if (0.90 > Utility.RandomDouble())
				for (int i = Utility.RandomMinMax(15, 25); i > 0; i--)
					DropItem(Loot.RandomGem());

			int itemCount = Utility.RandomMinMax(15, 20);
			for (int i = 0; i < itemCount; i++)
			{
				switch (Utility.Random(18))
				{
					case 0: DropItem(new Arrow(Utility.RandomMinMax(50, 100))); break;
					case 1: DropItem(new Bolt(Utility.RandomMinMax(50, 100))); break;
					case 2: DropItem(Loot.RandomWeapon()); break;
					case 3: DropItem(Loot.RandomArmor()); break;
					case 4: DropItem(Loot.RandomClothing()); break;
					case 5: DropItem(Loot.RandomHat()); break;
					case 6: DropItem(Loot.RandomJewelry()); break;
					case 7: DropItem(Loot.RandomPotion()); break;
					case 8: DropItem(Loot.RandomScroll(0, 63, SpellbookType.Regular)); break;
					case 9: DropItem(Loot.RandomReagent()); break;
					case 10: DropItem(Loot.RandomArmorOrShieldOrWeapon()); break;
					case 11: DropItem(Loot.RandomShield()); break;
					case 12: DropItem(Loot.RandomInstrument()); break;
					case 13: DropItem(new Bandage(Utility.RandomMinMax(20, 40))); break;
				
				}
			}

			if (Utility.RandomDouble() < 0.75) // 75% de chance
				DropItem(Loot.RandomStealableArtifact());
		}

		private void GenerateLevel6Loot()
		{
			DropItem(new BlankScroll(Utility.Random(6, 12)));
			DropItem(new DragonBlood(Utility.Random(8, 15)));

			for (int i = Utility.Random(6, 10); i > 0; i--)
			{
				Item ReagentLoot = Loot.RandomReagent();
				ReagentLoot.Amount = Utility.Random(20, 40);
				DropItem(ReagentLoot);
			}

			for (int i = Utility.Random(6, 10); i > 0; i--)
				DropItem(Loot.RandomPotion());

			if (0.95 > Utility.RandomDouble())
				for (int i = Utility.RandomMinMax(20, 30); i > 0; i--)
					DropItem(Loot.RandomScroll(0, 71, SpellbookType.Regular));

			if (0.95 > Utility.RandomDouble())
				for (int i = Utility.RandomMinMax(20, 30); i > 0; i--)
					DropItem(Loot.RandomGem());

			int itemCount = Utility.RandomMinMax(20, 25);
			for (int i = 0; i < itemCount; i++)
			{
				switch (Utility.Random(20))
				{
					case 0: DropItem(new Arrow(Utility.RandomMinMax(70, 150))); break;
					case 1: DropItem(new Bolt(Utility.RandomMinMax(70, 150))); break;
					case 2: DropItem(Loot.RandomWeapon()); break;
					case 3: DropItem(Loot.RandomArmor()); break;
					case 4: DropItem(Loot.RandomClothing()); break;
					case 5: DropItem(Loot.RandomHat()); break;
					case 6: DropItem(Loot.RandomJewelry()); break;
					case 7: DropItem(Loot.RandomPotion()); break;
					case 8: DropItem(Loot.RandomScroll(0, 71, SpellbookType.Regular)); break;
					case 9: DropItem(Loot.RandomReagent()); break;
					case 10: DropItem(Loot.RandomArmorOrShieldOrWeapon()); break;
					case 11: DropItem(Loot.RandomShield()); break;
					case 12: DropItem(Loot.RandomInstrument()); break;
					case 13: DropItem(new Bandage(Utility.RandomMinMax(30, 60))); break;
					case 14: DropItem(Loot.RandomWand()); break;
					
				}
			}

			if (Utility.RandomDouble() < 1.00) // 100% de chance
				DropItem(Loot.RandomStealableArtifact());
		}

		private void StartResetTimer()
        {
            if (m_ResetTimer == null)
                m_ResetTimer = new TreasureResetTimer(this);
            else
                m_ResetTimer.Delay = TimeSpan.FromMinutes(Utility.Random(m_MinSpawnTime, m_MaxSpawnTime));

            m_ResetTimer.Start();
        }

        private class TreasureResetTimer : Timer
        {
            private readonly BaseTreasureChest m_Chest;
            public TreasureResetTimer(BaseTreasureChest chest)
                : base(TimeSpan.FromMinutes(Utility.Random(chest.MinSpawnTime, chest.MaxSpawnTime)))
            {
                m_Chest = chest;
                Priority = TimerPriority.OneMinute;
            }

            protected override void OnTick()
            {
                m_Chest.Reset();
            }
        }
        ;
    }
}
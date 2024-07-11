namespace Server.Items
{
    public class TreasureLevel1 : BaseDungeonChest
    {
        public override int DefaultGumpID => 0x49;

		public virtual int Level => 1;

		[Constructable]
        public TreasureLevel1() : base(Utility.RandomList(0xE3C, 0xE3E, 0x9a9)) // Large, Medium and Small Crate
        {
            RequiredSkill = 20;
            LockLevel = RequiredSkill - Utility.Random(1, 10);
            MaxLockLevel = RequiredSkill;
            TrapType = TrapType.ExplosionTrap;
            TrapPower = 1 * Utility.Random(35, 45);

					//      DropItem(new Gold(30, 100));
					DropItem(new Bolt(Utility.RandomMinMax(5, 15)));
					DropItem(new Arrow(Utility.RandomMinMax(5, 15)));
					DropItem(new Tourmaline(Utility.RandomMinMax(1, 4)));
					DropItem(new Ambre(Utility.RandomMinMax(1, 3)));
					DropItem(new Citrine(Utility.RandomMinMax(2, 6)));

				
			int itemCount = Utility.RandomMinMax(4, 8);
			for (int i = 0; i < itemCount; i++)
			{
				switch (Utility.Random(8))
				{
					case 0: DropItem(new Bandage(Utility.RandomMinMax(5, 10))); break;
					case 1: DropItem(Loot.RandomPotion()); break;
					case 2: DropItem(Loot.RandomReagent()); break;
					case 3: DropItem(Loot.RandomGem()); break;
					case 4: DropItem(Loot.RandomWeapon()); break;
					case 5: DropItem(Loot.RandomScroll(0, 39, SpellbookType.Regular)); break;
					case 6: DropItem(Loot.RandomArmorOrShieldOrWeaponOrJewelry()); break;
					case 7: DropItem(new Bottle()); break;
				}
			}

			// 1% de chance d'obtenir un item Stealable
			if (Utility.RandomDouble() < 0.01)
				DropItem(Loot.RandomStealableArtifact());
		}
	

        public TreasureLevel1(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class TreasureLevel2 : BaseDungeonChest
    {
		public virtual int Level => 2;

		[Constructable]
        public TreasureLevel2() : base(Utility.RandomList(0xe3c, 0xE3E, 0x9a9, 0xe42, 0x9ab, 0xe40, 0xe7f, 0xe77)) // various container IDs
        {
            RequiredSkill = 40;
            LockLevel = RequiredSkill - Utility.Random(1, 10);
            MaxLockLevel = RequiredSkill;
            TrapType = TrapType.ExplosionTrap;
            TrapPower = 2 * Utility.Random(30, 50);

			//		DropItem(new Gold(50, 75));
					DropItem(new Tourmaline(Utility.Random(3)));
					DropItem(new Ambre(Utility.Random(2)));
					DropItem(new Citrine(Utility.Random(5)));
					DropItem(new DragonBlood(Utility.Random(0, 1)));


					DropItem(new Bolt(Utility.RandomMinMax(10, 25)));
					DropItem(new Arrow(Utility.RandomMinMax(10, 25)));


					DropItem(Loot.RandomPotion());
					for (int i = Utility.Random(1, 2); i > 1; i--)
					{
						Item ReagentLoot = Loot.RandomReagent();
						ReagentLoot.Amount = Utility.Random(1, 2);
						DropItem(ReagentLoot);
					}
					if (Utility.RandomBool()) //50% chance
						for (int i = Utility.Random(8) + 1; i > 0; i--)
							DropItem(Loot.RandomScroll(0, 39, SpellbookType.Regular));

					if (Utility.RandomBool()) //50% chance
						for (int i = Utility.Random(6) + 1; i > 0; i--)
							DropItem(Loot.RandomGem());
				

			int itemCount = Utility.RandomMinMax(2, 4);
			for (int i = 0; i < itemCount; i++)
			{
				switch (Utility.Random(10))
				{
					case 0: DropItem(new Arrow(Utility.RandomMinMax(10, 25))); break;
					case 1: DropItem(new Bolt(Utility.RandomMinMax(10, 25))); break;
					case 2: DropItem(Loot.RandomWeapon()); break;
					case 3: DropItem(Loot.RandomArmor()); break;
					case 4: DropItem(Loot.RandomClothing()); break;
					case 5: DropItem(Loot.RandomHat()); break;
					case 6: DropItem(Loot.RandomJewelry()); break;
					case 7: DropItem(Loot.RandomPotion()); break;
					case 8: DropItem(Loot.RandomScroll(0, 39, SpellbookType.Regular)); break;
					case 9: DropItem(Loot.RandomReagent()); break;
				}
			}

			// 3% de chance d'obtenir un item Stealable
			if (Utility.RandomDouble() < 0.03)
				DropItem(Loot.RandomStealableArtifact());
		}

		public TreasureLevel2(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class TreasureLevel3 : BaseDungeonChest
    {
		public virtual int Level => 3;
		public override int DefaultGumpID => 0x4A;

        [Constructable]
        public TreasureLevel3() : base(Utility.RandomList(0x9ab, 0xe40, 0xe42)) // Wooden, Metal and Metal Golden Chest
        {
            RequiredSkill = 60;
            LockLevel = RequiredSkill - Utility.Random(1, 10);
            MaxLockLevel = RequiredSkill;
            TrapType = TrapType.ExplosionTrap;
            TrapPower = 3 * Utility.Random(30, 40);
			DropItem(new DragonBlood(Utility.Random(1, 2)));

				//		DropItem(new Gold(200, 500));



						DropItem(new Arrow(10));

						for (int i = Utility.Random(1, 3); i > 1; i--)
						{
							Item ReagentLoot = Loot.RandomReagent();
							ReagentLoot.Amount = Utility.Random(1, 9);
							DropItem(ReagentLoot);
						}

						for (int i = Utility.Random(1, 3); i > 1; i--)
							DropItem(Loot.RandomPotion());

						if (0.67 > Utility.RandomDouble()) //67% chance = 2/3
							for (int i = Utility.Random(12) + 1; i > 0; i--)
								DropItem(Loot.RandomScroll(0, 47, SpellbookType.Regular));

						if (0.67 > Utility.RandomDouble()) //67% chance = 2/3
							for (int i = Utility.Random(9) + 1; i > 0; i--)
								DropItem(Loot.RandomGem());



			int itemCount = Utility.RandomMinMax(4, 6);
			for (int i = 0; i < itemCount; i++)
			{
				switch (Utility.Random(12))
				{
					case 0: DropItem(new Arrow(Utility.RandomMinMax(20, 40))); break;
					case 1: DropItem(new Bolt(Utility.RandomMinMax(20, 40))); break;
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
				}
			}

			// 5% de chance d'obtenir un item Stealable
			if (Utility.RandomDouble() < 0.05)
				DropItem(Loot.RandomStealableArtifact());
		}

		public TreasureLevel3(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class TreasureLevel4 : BaseDungeonChest
    {
		public virtual int Level => 4;

		[Constructable]
        public TreasureLevel4() : base(Utility.RandomList(0xe40, 0xe42, 0x9ab)) // Wooden, Metal and Metal Golden Chest
        {
            RequiredSkill = 80;
            LockLevel = RequiredSkill - Utility.Random(6, 8);
            MaxLockLevel = RequiredSkill;
            TrapType = TrapType.ExplosionTrap;
            TrapPower = 4 * Utility.Random(25, 35);

			 //       DropItem(new Gold(350, 700));
					DropItem(new BlankScroll(Utility.Random(1, 4)));
					DropItem(new DragonBlood(Utility.Random(1, 3)));

					for (int i = Utility.Random(1, 4); i > 1; i--)
					{
						Item ReagentLoot = Loot.RandomReagent();
						ReagentLoot.Amount = Utility.Random(6, 12);
						DropItem(ReagentLoot);
					}

					for (int i = Utility.Random(1, 4); i > 1; i--)
						DropItem(Loot.RandomPotion());

					if (0.75 > Utility.RandomDouble()) //75% chance = 3/4
						for (int i = Utility.RandomMinMax(8, 16); i > 0; i--)
							DropItem(Loot.RandomScroll(0, 47, SpellbookType.Regular));

					if (0.75 > Utility.RandomDouble()) //75% chance = 3/4
						for (int i = Utility.RandomMinMax(6, 12) + 1; i > 0; i--)
							DropItem(Loot.RandomGem());

				

			int itemCount = Utility.RandomMinMax(8, 10);
			for (int i = 0; i < itemCount; i++)
			{
				switch (Utility.Random(14))
				{
					case 0: DropItem(new Arrow(Utility.RandomMinMax(30, 60))); break;
					case 1: DropItem(new Bolt(Utility.RandomMinMax(30, 60))); break;
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
					case 13: DropItem(new Bandage(Utility.RandomMinMax(5, 15))); break;
				}
			}

			// 10% de chance d'obtenir un item Stealable
			if (Utility.RandomDouble() < 0.10)
				DropItem(Loot.RandomStealableArtifact());
		}

		public TreasureLevel4(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

}

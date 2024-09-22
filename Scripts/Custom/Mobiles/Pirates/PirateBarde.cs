using Server.Items;

namespace Server.Mobiles
{
    public class PirateBarde : PirateBase
	{

	    public override bool AllFemale => true;
        public override bool AllowCharge => false;

   		public override bool BardImmune => true;
		public override bool Unprovokable => true;
		public override bool Uncalmable => true;



        [Constructable]
        public PirateBarde()
            : this(0)
        {

           
        }

        [Constructable]
        public PirateBarde(int PirateBoatId)
		   : base(AIType.AI_Archer, FightMode.Closest, 10, 1, 0.05, 0.2, PirateBoatId)
		{
       
          	SetHits(351, 450);

            SetStr(226, 325);
            SetDex(201, 305);
            SetInt(151, 165);

            SetDamage(10, 20);

            SetSkill(SkillName.Anatomy, 105.0, 120.0);
            SetSkill(SkillName.MagicResist, 80.0, 100.0);
            SetSkill(SkillName.Tactics, 115.0, 130.0);
            SetSkill(SkillName.Musicianship, 95.0, 120.0);
            SetSkill(SkillName.Discordance, 95.0, 120.0);
            SetSkill(SkillName.Provocation, 95.0, 120.0);
            SetSkill(SkillName.Peacemaking, 95.0, 120.0);
            SetSkill(SkillName.Archery, 95.0, 120.0);

            Fame = 1000;
            Karma = 1000;

			AddItem(new Bow());
		}

		public override bool CanDiscord => true;
        public override bool CanPeace => true;
        public override bool CanProvoke => true;


		public override void GenerateLoot()
        {
            AddLoot(LootPack.RandomLootItem(new System.Type[] { typeof(Harp), typeof(Lute), typeof(Drums), typeof(Tambourine) }));
            AddLoot(LootPack.LootItem<Longsword>(true));
            AddLoot(LootPack.LootItem<Bow>(true));
            AddLoot(LootPack.LootItem<Arrow>(100, true));
			AddLoot(LootPack.Average);
            base.GenerateLoot();
		}


        public override void FemaleCloth()
		{
			


			
				// haut // robe
				switch (Utility.Random(5))
				{
					
					case 1:
						AddItem(new Robe15(GetPirateBoat().MainHue));
						break;
					case 2:
						AddItem(new Robe6(GetPirateBoat().MainHue));
						break;
					case 3:

                        SoutienGorgeTissu soutien = new SoutienGorgeTissu(GetPirateBoat().MainHue);
                        soutien.Layer = Layer.OuterTorso;

						AddItem(soutien);
						break;
                    case 4:
						AddItem(new CorsetTissus(GetPirateBoat().MainHue));
						break;
					default:
                        AddItem(new Robe15(GetPirateBoat().MainHue));
					break;
				}

				// bas 
				switch (Utility.Random(2))
				{
					case 0:
						AddItem(new Pantalon7(GetPirateBoat().AltHue));
						break;
					case 1:
						AddItem(new ShortPants(GetPirateBoat().AltHue));
						break;			
					default:
						AddItem(new ShortPants(GetPirateBoat().AltHue));
					break;
				}
			

	


		}

        public PirateBarde(Serial serial)
            : base(serial)
        {
        }

        public override bool ClickTitle => false;
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0);// version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}

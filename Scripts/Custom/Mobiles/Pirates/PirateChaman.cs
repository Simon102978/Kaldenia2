using System;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName("Le corps d'un Pirate")]
	public class PirateChaman : PirateBase
	{

	    public override bool AllowCharge => false;

		 [Constructable]
        public PirateChaman()
            : this(0)
        {

           
        }


		[Constructable]
		public PirateChaman(int PirateBoatId) : base(AIType.AI_Mage, FightMode.Aggressor, 10, 1, 0.4, 0.2, PirateBoatId)
		{
		
		

			SetStr(271, 350);
			SetDex(126, 145);
			SetInt(300, 465);

			SetHits(303, 420);
			SetMana(600, 800);
			SetDamage(10, 15);

			SetDamageType(ResistanceType.Physical, 50);
			SetDamageType(ResistanceType.Energy, 50);

			SetResistance(ResistanceType.Physical, 50, 60);
			SetResistance(ResistanceType.Fire, 30, 50);
			SetResistance(ResistanceType.Cold, 50, 60);
			SetResistance(ResistanceType.Poison, 50, 60);
			SetResistance(ResistanceType.Energy, 50, 60);

			SetSkill(SkillName.MagicResist, 125, 140);
			SetSkill(SkillName.Tactics, 70, 120);
			SetSkill(SkillName.Wrestling, 80, 130);
			SetSkill(SkillName.Magery, 100, 120);
			SetSkill(SkillName.EvalInt, 100, 120);




		}

		public override void GenerateChapeau()
		{
			 AddItem(new WizardsHat(GetPirateBoat().AltHue));
		}

		
		public override TribeType Tribe => TribeType.Pirate;
		public override void GenerateLoot()
		{
			AddLoot(LootPack.Rich);
			AddLoot(LootPack.Others, Utility.RandomMinMax(1, 2));
			AddLoot(LootPack.MedScrolls);
			AddLoot(LootPack.MageryRegs, 15);
			AddLoot(LootPack.Potions, Utility.RandomMinMax(1, 2));
			base.GenerateLoot();

		}

		public PirateChaman(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
}
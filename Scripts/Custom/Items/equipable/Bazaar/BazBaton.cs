namespace Server.Items
{

	public class BazBaton1 : BaseStaff  // QuarterStaff
	{
		[Constructable]
		public BazBaton1()
			: base(0xA4AF)
		{
			Weight = 4.0;
			Name = "Baton";
			Layer = Layer.TwoHanded;
		}

		public BazBaton1(Serial serial)
			: base(serial)
		{
		}

		public override WeaponAbility PrimaryAbility => WeaponAbility.DefenseMastery;
		public override WeaponAbility SecondaryAbility => WeaponAbility.InfectiousStrike;
		public override int StrengthReq => 30;
		public override int MinDamage => 8;
		public override int MaxDamage => 10;

		public override float Speed => 2.25f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 60;
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

	public class BazBaton2 : BaseStaff  // GnarledStaff
	{
		[Constructable]
		public BazBaton2()
			: base(0xA4B3)
		{
			Weight = 3.0;
			Name = "Baton";
		}

		public BazBaton2(Serial serial)
			: base(serial)
		{
		}

		public override WeaponAbility PrimaryAbility => WeaponAbility.ParalyzingBlow;
		public override WeaponAbility SecondaryAbility => WeaponAbility.Dismount;
		public override int StrengthReq => 20;
		public override int MinDamage => 11;
		public override int MaxDamage => 13;
		public override float Speed => 3.25f;
		public override int InitMinHits => 31;
		public override int InitMaxHits => 50;
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




	
	public class BazBaton3 : BaseStaff // BlackStaff
	{
        [Constructable]
        public BazBaton3()
            : base(0xA4B8)
        {
            Weight = 6.0;
			Name = "Baton";
		}

        public BazBaton3(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.Block;
        public override WeaponAbility SecondaryAbility => WeaponAbility.ForceOfNature;
        public override int StrengthReq => 35;
        public override int MinDamage => 9;
        public override int MaxDamage => 11;
        public override float Speed => 2.75f;

		public override int InitMinHits => 31;
        public override int InitMaxHits => 70;
		
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

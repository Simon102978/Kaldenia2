using System;

namespace Server.Items
{
    public class Percemurs : BaseCrossbow
	{
		public override int EffectID => 0x1BFE;
		public override Type AmmoType => typeof(Bolt);
		public override Item Ammo => new Bolt();
		public override WeaponAbility PrimaryAbility => WeaponAbility.ShadowStrike;
		public override WeaponAbility SecondaryAbility => WeaponAbility.Disarm;
		public override int StrengthReq => 35;
		public override int MinDamage => 18;
		public override int MaxDamage => 22;
		public override float Speed => 4.50f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 80;


		[Constructable]
        public Percemurs()
            : base(41552)  
        {
            Weight = 7.0;
            Layer = Layer.TwoHanded;
            Name = "Percemurs";
        }

        public Percemurs(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

	public class Arbavive : BaseCrossbow
	{

		public override int EffectID => 0x1BFE;
		public override Type AmmoType => typeof(Bolt);
		public override Item Ammo => new Bolt();
		public override WeaponAbility PrimaryAbility => WeaponAbility.ParalyzingBlow;
		public override WeaponAbility SecondaryAbility => WeaponAbility.Block;
		public override int StrengthReq => 35;
		public override int MinDamage => 18;
		public override int MaxDamage => 22;
		public override float Speed => 4.50f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 80;
		[Constructable]
		public Arbavive()
			: base(41551) 
		{
			Weight = 7.0;
			Layer = Layer.TwoHanded;
			Name = "Arbavive";
		}

		public Arbavive(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}

	public class Lumitrait : BaseCrossbow
	{
		public override int EffectID => 0x1BFE;
		public override Type AmmoType => typeof(Bolt);
		public override Item Ammo => new Bolt();
		public override WeaponAbility PrimaryAbility => WeaponAbility.SerpentArrow;
		public override WeaponAbility SecondaryAbility => WeaponAbility.ForceArrow;
		public override int StrengthReq => 80;
		public override int MinDamage => 20;
		public override int MaxDamage => 24;
		public override float Speed => 5.00f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 100;

		[Constructable]
		public Lumitrait()
			: base(41550) 
		{
			Weight = 7.0;
			Layer = Layer.TwoHanded;
			Name = "Lumitrait";
		}

		public Lumitrait(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}

	public class ArbaletteChasse : BaseCrossbow
	{
		public override int EffectID => 0x1BFE;
		public override Type AmmoType => typeof(Bolt);
		public override Item Ammo => new Bolt();
		public override WeaponAbility PrimaryAbility => WeaponAbility.MovingShot;
		public override WeaponAbility SecondaryAbility => WeaponAbility.PsychicAttack;
		public override int StrengthReq => 80;
		public override int MinDamage => 20;
		public override int MaxDamage => 24;
		public override float Speed => 5.00f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 100;

		[Constructable]
		public ArbaletteChasse()
			: base(41553) 
		{
			Weight = 7.0;
			Layer = Layer.TwoHanded;
			Name = "Arbaletes de chasse";
		}

		public ArbaletteChasse(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}

	public class Arbalete : BaseCrossbow
	{

		public override int EffectID => 0x1BFE;
		public override Type AmmoType => typeof(Bolt);
		public override Item Ammo => new Bolt();
		public override WeaponAbility PrimaryAbility => WeaponAbility.ParalyzingBlow;
		public override WeaponAbility SecondaryAbility => WeaponAbility.Block;
		public override int StrengthReq => 35;
		public override int MinDamage => 18;
		public override int MaxDamage => 22;
		public override float Speed => 4.50f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 80;
		[Constructable]
		public Arbalete()
			: base(0xA419)
		{
			Weight = 7.0;
			Layer = Layer.TwoHanded;
			Name = "Arbalète";
		}

		public Arbalete(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}

	public class ArbalettePistolet : BaseCrossbow
	{

		public override int EffectID => 0x1BFE;
		public override Type AmmoType => typeof(Bolt);
		public override Item Ammo => new Bolt();
		public override WeaponAbility PrimaryAbility => WeaponAbility.ParalyzingBlow;
		public override WeaponAbility SecondaryAbility => WeaponAbility.Block;
		public override int StrengthReq => 35;
		public override int MinDamage => 18;
		public override int MaxDamage => 22;
		public override float Speed => 4.50f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 80;
		[Constructable]

		public ArbalettePistolet()
			: base(0xA41A)
		{
			Weight = 7.0;
			Layer = Layer.TwoHanded;
			Name = "Arbalète à Main";
		}

		public ArbalettePistolet(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}

	public class ArbaletteRepetition : BaseCrossbow
	{

		public override int EffectID => 0x1BFE;
		public override Type AmmoType => typeof(Bolt);
		public override Item Ammo => new Bolt();
		public override WeaponAbility PrimaryAbility => WeaponAbility.ParalyzingBlow;
		public override WeaponAbility SecondaryAbility => WeaponAbility.Block;
		public override int StrengthReq => 35;
		public override int MinDamage => 18;
		public override int MaxDamage => 22;
		public override float Speed => 4.50f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 80;
		[Constructable]

		public ArbaletteRepetition()
			: base(0xA41C)
		{
			Weight = 7.0;
			Layer = Layer.TwoHanded;
			Name = "Arbalète à Répétition";
		}

		public ArbaletteRepetition(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}

	public class ArbaletteLourde : BaseCrossbow
	{

		public override int EffectID => 0x1BFE;
		public override Type AmmoType => typeof(Bolt);
		public override Item Ammo => new Bolt();
		public override WeaponAbility PrimaryAbility => WeaponAbility.ParalyzingBlow;
		public override WeaponAbility SecondaryAbility => WeaponAbility.Block;
		public override int StrengthReq => 35;
		public override int MinDamage => 18;
		public override int MaxDamage => 22;
		public override float Speed => 4.50f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 80;
		[Constructable]

		public ArbaletteLourde()
			: base(0xA41B)
		{
			Weight = 7.0;
			Layer = Layer.TwoHanded;
			Name = "Arbalete à Mecanisme";
		}

		public ArbaletteLourde(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}



}

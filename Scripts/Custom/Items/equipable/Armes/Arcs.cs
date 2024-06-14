using System;

namespace Server.Items
{
    public class Legarc : BaseBow
    {
		public override int EffectID => 0xF42;
		public override Type AmmoType => typeof(Arrow);
		public override Item Ammo => new Arrow();
		public override WeaponAbility PrimaryAbility => WeaponAbility.MovingShot;
		public override WeaponAbility SecondaryAbility => WeaponAbility.DoubleShot;
		public override int StrengthReq => 30;
		public override int MinDamage => 16;
		public override int MaxDamage => 20;
		public override float Speed => 4.00f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 60;
		public override WeaponAnimation DefAnimation => WeaponAnimation.ShootBow;

		[Constructable]
		public Legarc() : base(41554) // 0x299F - 10655
        {
			Weight = 6.0;
			Layer = Layer.TwoHanded;
            Name = "Legarc";
		}

        public Legarc(Serial serial)
            : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
    }
    public class Tarkarc : BaseBow
	{
		public override int EffectID => 0xF42;
		public override Type AmmoType => typeof(Arrow);
		public override Item Ammo => new Arrow();
		public override WeaponAbility PrimaryAbility => WeaponAbility.ArmorIgnore;
		public override WeaponAbility SecondaryAbility => WeaponAbility.MortalStrike;
		public override int StrengthReq => 30;
		public override int MinDamage => 16;
		public override int MaxDamage => 20;
		public override float Speed => 4.00f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 60;
		public override WeaponAnimation DefAnimation => WeaponAnimation.ShootBow;

		[Constructable]
        public Tarkarc()
            : base(41555) // 0x299E  - 10654
        {
            Weight = 6.0;
            Layer = Layer.TwoHanded;
            Name = "Arc court renforcit";
        }

        public Tarkarc(Serial serial)
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
    public class Souplecorde : BaseBow
	{
		public override int EffectID => 0xF42;
		public override Type AmmoType => typeof(Arrow);
		public override Item Ammo => new Arrow();
		public override WeaponAbility PrimaryAbility => WeaponAbility.ArmorIgnore;
		public override WeaponAbility SecondaryAbility => WeaponAbility.MortalStrike;
		public override int StrengthReq => 45;
		public override int MinDamage => 16;
		public override int MaxDamage => 20;
		public override float Speed => 4.00f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 70;
		public override WeaponAnimation DefAnimation => WeaponAnimation.ShootBow;

		[Constructable]
        public Souplecorde()
            : base(41556) //0x299D - 10653
		{
            Weight = 6.0;
            Layer = Layer.TwoHanded;
            Name = "Souplecorde";
        }

        public Souplecorde(Serial serial)
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
    public class Sombrevent : BaseBow
	{
		public override int EffectID => 0xF42;
		public override Type AmmoType => typeof(Arrow);
		public override Item Ammo => new Arrow();
		public override WeaponAbility PrimaryAbility => WeaponAbility.ParalyzingBlow;
		public override WeaponAbility SecondaryAbility => WeaponAbility.CrushingBlow;
		public override int StrengthReq => 45;
		public override int MinDamage => 16;
		public override int MaxDamage => 20;
		public override float Speed => 4.00f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 70;
		public override WeaponAnimation DefAnimation => WeaponAnimation.ShootBow;

		[Constructable]
        public Sombrevent()
            : base(41557) // 0x299C - 10652
        {
            Weight = 6.0;
            Layer = Layer.TwoHanded;
            Name = "Sombrevent";
        }

        public Sombrevent(Serial serial)
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


	public class Blancorde : BaseBow
	{
		public override int EffectID => 0xF42;
		public override Type AmmoType => typeof(Arrow);
		public override Item Ammo => new Arrow();
		public override WeaponAbility PrimaryAbility => WeaponAbility.ParalyzingBlow;
		public override WeaponAbility SecondaryAbility => WeaponAbility.CrushingBlow;
		public override int StrengthReq => 45;
		public override int MinDamage => 16;
		public override int MaxDamage => 20;
		public override float Speed => 4.00f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 70;
		public override WeaponAnimation DefAnimation => WeaponAnimation.ShootBow;

		[Constructable]
		public Blancorde()
			: base(0xA41D) // 0x299C - 10652
		{
			Weight = 6.0;
			Layer = Layer.TwoHanded;
			Name = "Blancorde";
		}

		public Blancorde(Serial serial)
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

	public class Glaciale : BaseBow
	{
		public override int EffectID => 0xF42;
		public override Type AmmoType => typeof(Arrow);
		public override Item Ammo => new Arrow();
		public override WeaponAbility PrimaryAbility => WeaponAbility.ParalyzingBlow;
		public override WeaponAbility SecondaryAbility => WeaponAbility.CrushingBlow;
		public override int StrengthReq => 45;
		public override int MinDamage => 16;
		public override int MaxDamage => 20;
		public override float Speed => 4.00f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 70;
		public override WeaponAnimation DefAnimation => WeaponAnimation.ShootBow;

		[Constructable]
		public Glaciale()
			: base(0xA41E) // 0x299C - 10652
		{
			Weight = 6.0;
			Layer = Layer.TwoHanded;
			Name = "Glaciale";
		}

		public Glaciale(Serial serial)
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

	public class Chantefleche : BaseBow
	{
		public override int EffectID => 0xF42;
		public override Type AmmoType => typeof(Arrow);
		public override Item Ammo => new Arrow();
		public override WeaponAbility PrimaryAbility => WeaponAbility.ParalyzingBlow;
		public override WeaponAbility SecondaryAbility => WeaponAbility.CrushingBlow;
		public override int StrengthReq => 45;
		public override int MinDamage => 16;
		public override int MaxDamage => 20;
		public override float Speed => 4.00f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 70;
		public override WeaponAnimation DefAnimation => WeaponAnimation.ShootBow;

		[Constructable]
		public Chantefleche()
			: base(0xA41F) // 0x299C - 10652
		{
			Weight = 6.0;
			Layer = Layer.TwoHanded;
			Name = "Chantefleche";
		}

		public Chantefleche(Serial serial)
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

	public class Barbatrine : BaseBow
	{
		public override int EffectID => 0xF42;
		public override Type AmmoType => typeof(Arrow);
		public override Item Ammo => new Arrow();
		public override WeaponAbility PrimaryAbility => WeaponAbility.ParalyzingBlow;
		public override WeaponAbility SecondaryAbility => WeaponAbility.CrushingBlow;
		public override int StrengthReq => 45;
		public override int MinDamage => 16;
		public override int MaxDamage => 20;
		public override float Speed => 4.00f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 70;
		public override WeaponAnimation DefAnimation => WeaponAnimation.ShootBow;

		[Constructable]
		public Barbatrine()
			: base(0xA420) // 0x299C - 10652
		{
			Weight = 6.0;
			Layer = Layer.TwoHanded;
			Name = "Barbatrine";
		}

		public Barbatrine(Serial serial)
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

	public class Mirka : BaseBow
	{
		public override int EffectID => 0xF42;
		public override Type AmmoType => typeof(Arrow);
		public override Item Ammo => new Arrow();
		public override WeaponAbility PrimaryAbility => WeaponAbility.ParalyzingBlow;
		public override WeaponAbility SecondaryAbility => WeaponAbility.CrushingBlow;
		public override int StrengthReq => 45;
		public override int MinDamage => 16;
		public override int MaxDamage => 20;
		public override float Speed => 4.00f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 70;
		public override WeaponAnimation DefAnimation => WeaponAnimation.ShootBow;

		[Constructable]
		public Mirka()
			: base(0xA421) // 0x299C - 10652
		{
			Weight = 6.0;
			Layer = Layer.TwoHanded;
			Name = "Mirka";
		}

		public Mirka(Serial serial)
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

	public class Ebonie : BaseBow
	{
		public override int EffectID => 0xF42;
		public override Type AmmoType => typeof(Arrow);
		public override Item Ammo => new Arrow();
		public override WeaponAbility PrimaryAbility => WeaponAbility.ParalyzingBlow;
		public override WeaponAbility SecondaryAbility => WeaponAbility.CrushingBlow;
		public override int StrengthReq => 45;
		public override int MinDamage => 16;
		public override int MaxDamage => 20;
		public override float Speed => 4.00f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 70;
		public override WeaponAnimation DefAnimation => WeaponAnimation.ShootBow;

		[Constructable]
		public Ebonie()
			: base(0xA422) // 0x299C - 10652
		{
			Weight = 6.0;
			Layer = Layer.TwoHanded;
			Name = "Ebonie";
		}

		public Ebonie(Serial serial)
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

	public class Mirielle : BaseBow
	{
		public override int EffectID => 0xF42;
		public override Type AmmoType => typeof(Arrow);
		public override Item Ammo => new Arrow();
		public override WeaponAbility PrimaryAbility => WeaponAbility.ParalyzingBlow;
		public override WeaponAbility SecondaryAbility => WeaponAbility.CrushingBlow;
		public override int StrengthReq => 45;
		public override int MinDamage => 16;
		public override int MaxDamage => 20;
		public override float Speed => 4.00f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 70;
		public override WeaponAnimation DefAnimation => WeaponAnimation.ShootBow;

		[Constructable]
		public Mirielle()
			: base(0xA423) // 0x299C - 10652
		{
			Weight = 6.0;
			Layer = Layer.TwoHanded;
			Name = "Mirielle";
		}

		public Mirielle(Serial serial)
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

	public class Vigne : BaseBow
	{
		public override int EffectID => 0xF42;
		public override Type AmmoType => typeof(Arrow);
		public override Item Ammo => new Arrow();
		public override WeaponAbility PrimaryAbility => WeaponAbility.ParalyzingBlow;
		public override WeaponAbility SecondaryAbility => WeaponAbility.CrushingBlow;
		public override int StrengthReq => 45;
		public override int MinDamage => 16;
		public override int MaxDamage => 20;
		public override float Speed => 4.00f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 70;
		public override WeaponAnimation DefAnimation => WeaponAnimation.ShootBow;

		[Constructable]
		public Vigne()
			: base(0xA424) // 0x299C - 10652
		{
			Weight = 6.0;
			Layer = Layer.TwoHanded;
			Name = "Vigne";
		}

		public Vigne(Serial serial)
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

	public class Maegie : BaseBow
	{
		public override int EffectID => 0xF42;
		public override Type AmmoType => typeof(Arrow);
		public override Item Ammo => new Arrow();
		public override WeaponAbility PrimaryAbility => WeaponAbility.ParalyzingBlow;
		public override WeaponAbility SecondaryAbility => WeaponAbility.CrushingBlow;
		public override int StrengthReq => 45;
		public override int MinDamage => 16;
		public override int MaxDamage => 20;
		public override float Speed => 4.00f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 70;
		public override WeaponAnimation DefAnimation => WeaponAnimation.ShootBow;

		[Constructable]
		public Maegie()
			: base(0xA425) // 0x299C - 10652
		{
			Weight = 6.0;
			Layer = Layer.TwoHanded;
			Name = "Maegie";
		}

		public Maegie(Serial serial)
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

	public class Foliere : BaseBow
	{
		public override int EffectID => 0xF42;
		public override Type AmmoType => typeof(Arrow);
		public override Item Ammo => new Arrow();
		public override WeaponAbility PrimaryAbility => WeaponAbility.ParalyzingBlow;
		public override WeaponAbility SecondaryAbility => WeaponAbility.CrushingBlow;
		public override int StrengthReq => 45;
		public override int MinDamage => 16;
		public override int MaxDamage => 20;
		public override float Speed => 4.00f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 70;
		public override WeaponAnimation DefAnimation => WeaponAnimation.ShootBow;

		[Constructable]
		public Foliere()
			: base(0xA426) // 0x299C - 10652
		{
			Weight = 6.0;
			Layer = Layer.TwoHanded;
			Name = "Foliere";
		}

		public Foliere(Serial serial)
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

	public class Sifflecrin : BaseBow
	{
		public override int EffectID => 0xF42;
		public override Type AmmoType => typeof(Arrow);
		public override Item Ammo => new Arrow();
		public override WeaponAbility PrimaryAbility => WeaponAbility.ParalyzingBlow;
		public override WeaponAbility SecondaryAbility => WeaponAbility.CrushingBlow;
		public override int StrengthReq => 45;
		public override int MinDamage => 16;
		public override int MaxDamage => 20;
		public override float Speed => 4.00f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 70;
		public override WeaponAnimation DefAnimation => WeaponAnimation.ShootBow;

		[Constructable]
		public Sifflecrin()
			: base(0xA42B) // 0x299C - 10652
		{
			Weight = 6.0;
			Layer = Layer.TwoHanded;
			Name = "Sifflecrin";
		}

		public Sifflecrin(Serial serial)
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
	public class Pieuse : BaseBow
	{
		public override int EffectID => 0xF42;
		public override Type AmmoType => typeof(Arrow);
		public override Item Ammo => new Arrow();
		public override WeaponAbility PrimaryAbility => WeaponAbility.ParalyzingBlow;
		public override WeaponAbility SecondaryAbility => WeaponAbility.CrushingBlow;
		public override int StrengthReq => 45;
		public override int MinDamage => 16;
		public override int MaxDamage => 20;
		public override float Speed => 4.00f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 70;
		public override WeaponAnimation DefAnimation => WeaponAnimation.ShootBow;

		[Constructable]
		public Pieuse()
			: base(0xA42A) // 0x299C - 10652
		{
			Weight = 6.0;
			Layer = Layer.TwoHanded;
			Name = "Pieuse";
		}

		public Pieuse(Serial serial)
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
	public class Composite : BaseBow
	{
		public override int EffectID => 0xF42;
		public override Type AmmoType => typeof(Arrow);
		public override Item Ammo => new Arrow();
		public override WeaponAbility PrimaryAbility => WeaponAbility.ParalyzingBlow;
		public override WeaponAbility SecondaryAbility => WeaponAbility.CrushingBlow;
		public override int StrengthReq => 45;
		public override int MinDamage => 16;
		public override int MaxDamage => 20;
		public override float Speed => 4.00f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 70;
		public override WeaponAnimation DefAnimation => WeaponAnimation.ShootBow;

		[Constructable]
		public Composite()
			: base(0xA428) // 0x299C - 10652
		{
			Weight = 6.0;
			Layer = Layer.TwoHanded;
			Name = "Composite";
		}

		public Composite(Serial serial)
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
	public class Flamfleche : BaseBow
	{
		public override int EffectID => 0xF42;
		public override Type AmmoType => typeof(Arrow);
		public override Item Ammo => new Arrow();
		public override WeaponAbility PrimaryAbility => WeaponAbility.ParalyzingBlow;
		public override WeaponAbility SecondaryAbility => WeaponAbility.CrushingBlow;
		public override int StrengthReq => 45;
		public override int MinDamage => 16;
		public override int MaxDamage => 20;
		public override float Speed => 4.00f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 70;
		public override WeaponAnimation DefAnimation => WeaponAnimation.ShootBow;

		[Constructable]
		public Flamfleche()
			: base(0xA427) // 0x299C - 10652
		{
			Weight = 6.0;
			Layer = Layer.TwoHanded;
			Name = "Flamfleche";
		}

		public Flamfleche(Serial serial)
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
	public class Foudre : BaseBow
	{
		public override int EffectID => 0xF42;
		public override Type AmmoType => typeof(Arrow);
		public override Item Ammo => new Arrow();
		public override WeaponAbility PrimaryAbility => WeaponAbility.ParalyzingBlow;
		public override WeaponAbility SecondaryAbility => WeaponAbility.CrushingBlow;
		public override int StrengthReq => 45;
		public override int MinDamage => 16;
		public override int MaxDamage => 20;
		public override float Speed => 4.00f;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 70;
		public override WeaponAnimation DefAnimation => WeaponAnimation.ShootBow;

		[Constructable]
		public Foudre()
			: base(0xA42A) // 0x299C - 10652
		{
			Weight = 6.0;
			Layer = Layer.TwoHanded;
			Name = "Foudre";
		}

		public Foudre(Serial serial)
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

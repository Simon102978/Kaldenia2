using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public abstract class BaseGolemAsh : Item, ICommodity
	{
		public abstract string AshName { get; }
		public abstract int AshHue { get; }

		[Constructable]
		public BaseGolemAsh() : this(1)
		{
		}

		[Constructable]
		public BaseGolemAsh(int amount) : base(0x0F7C)
		{
			Name = AshName;
			Stackable = true;
			Amount = amount;
			Hue = AshHue;
			Weight = 0.1;
		}

		public BaseGolemAsh(Serial serial) : base(serial)
		{
		}

		TextDefinition ICommodity.Description => LabelNumber;
		bool ICommodity.IsDeedable => true;

		public override void AddNameProperty(ObjectPropertyList list)
		{
			if (Amount > 1)
				list.Add(1050039, "{0}\t{1}", Amount.ToString(), AshName); // ~1_NUMBER~ ~2_ITEMNAME~
			else
				list.Add(AshName);
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);
			list.Add($"Type: {GetType().Name.Replace("GolemCendre", "")}");
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

	public class GolemCendreFeu : BaseGolemAsh
	{
		public override string AshName => "Cendres Élémentaires de Feu";
		public override int AshHue => 1161;

		[Constructable]
		public GolemCendreFeu() : base() { }

		[Constructable]
		public GolemCendreFeu(int amount) : base(amount) { }

		public GolemCendreFeu(Serial serial) : base(serial) { }
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

	public class GolemCendreEau : BaseGolemAsh
	{
		public override string AshName => "Cendres Élémentaires d'Eau";
		public override int AshHue => 1156;

		[Constructable]
		public GolemCendreEau() : base() { }

		[Constructable]
		public GolemCendreEau(int amount) : base(amount) { }

		public GolemCendreEau(Serial serial) : base(serial) { }
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

	public class GolemCendreGlace : BaseGolemAsh
	{
		public override string AshName => "Cendres Élémentaires de Glace";
		public override int AshHue => 1152;

		[Constructable]
		public GolemCendreGlace() : base() { }

		[Constructable]
		public GolemCendreGlace(int amount) : base(amount) { }

		public GolemCendreGlace(Serial serial) : base(serial) { }
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

	public class GolemCendrePoison : BaseGolemAsh
	{
		public override string AshName => "Cendres Élémentaires de Poison";
		public override int AshHue => 1193;

		[Constructable]
		public GolemCendrePoison() : base() { }

		[Constructable]
		public GolemCendrePoison(int amount) : base(amount) { }

		public GolemCendrePoison(Serial serial) : base(serial) { }
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

	public class GolemCendreSang : BaseGolemAsh
	{
		public override string AshName => "Cendres Élémentaires de Sang";
		public override int AshHue => 1194;

		[Constructable]
		public GolemCendreSang() : base() { }

		[Constructable]
		public GolemCendreSang(int amount) : base(amount) { }

		public GolemCendreSang(Serial serial) : base(serial) { }
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

	public class GolemCendreSylvestre : BaseGolemAsh
	{
		public override string AshName => "Cendres Élémentaires Sylvestres";
		public override int AshHue => 1190;

		[Constructable]
		public GolemCendreSylvestre() : base() { }

		[Constructable]
		public GolemCendreSylvestre(int amount) : base(amount) { }

		public GolemCendreSylvestre(Serial serial) : base(serial) { }
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

	public class GolemCendreTerre : BaseGolemAsh
	{
		public override string AshName => "Cendres Élémentaires de Terre";
		public override int AshHue => 0;

		[Constructable]
		public GolemCendreTerre() : base() { }

		[Constructable]
		public GolemCendreTerre(int amount) : base(amount) { }

		public GolemCendreTerre(Serial serial) : base(serial) { }
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

	public class GolemCendreVent : BaseGolemAsh
	{
		public override string AshName => "Cendres Élémentaires de Vent";
		public override int AshHue => 2834;

		[Constructable]
		public GolemCendreVent() : base() { }

		[Constructable]
		public GolemCendreVent(int amount) : base(amount) { }

		public GolemCendreVent(Serial serial) : base(serial) { }
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
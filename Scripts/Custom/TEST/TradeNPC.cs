#region References
using Server.Gumps;
using Server.Items;
using Server.Network;
using System;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Targeting;
#endregion

namespace Server.Mobiles
{
	public class TradeNPC : BaseVendor
	{
		private readonly List<SBInfo> m_SBInfos = new List<SBInfo>();
		private Type _requiredResource; // Type of resource required for trade
		private int _requiredQuantity; // Quantity of required resource

		private Type _offeredResource1;
		private Type _offeredResource2;
		private Type _offeredResource3;
		private Type _offeredResource4;
		private Type _offeredResource5;

		private int _offeredQuantity1;
		private int _offeredQuantity2;
		private int _offeredQuantity3;
		private int _offeredQuantity4;
		private int _offeredQuantity5;

		[Constructable]
		public TradeNPC() : base("Marchand itinérant")
		{
			_requiredResource = typeof(Log);          // Default: Log
			_requiredQuantity = 20;                   // Default: 20

			Hue = 0x83EA;
			Body = 0x190; // Male human
			Name = "John le marchand";
		}

		public TradeNPC(Serial serial) : base(serial) { }

		public override bool ClickTitle => false;
		public override bool IsActiveBuyer => false;
		public override bool IsActiveSeller => true;
		public override VendorShoeType ShoeType => Utility.RandomBool() ? VendorShoeType.Shoes : VendorShoeType.Sandals;
		protected override List<SBInfo> SBInfos => m_SBInfos;

		public override bool OnBuyItems(Mobile buyer, List<BuyItemResponse> list)
		{
			return false;
		}

		public override void VendorBuy(Mobile from)
		{
			from.SendGump(new TradeNPCGump((PlayerMobile)from, this, _requiredResource, _requiredQuantity,
				new List<(Type, int)>
				{
					(_offeredResource1, _offeredQuantity1),
					(_offeredResource2, _offeredQuantity2),
					(_offeredResource3, _offeredQuantity3),
					(_offeredResource4, _offeredQuantity4),
					(_offeredResource5, _offeredQuantity5)
				}));
		}

		public override int GetHairHue()
		{
			return Utility.RandomBrightHue();
		}

		public override void InitOutfit()
		{
			base.InitOutfit();
			AddItem(new Robe(Utility.RandomPinkHue()));
		}

		public override bool CheckVendorAccess(Mobile from)
		{
			return base.CheckVendorAccess(from);
		}

		public override void InitSBInfo()
		{
			// No SBInfo needed for this vendor
		}

		[CommandProperty(AccessLevel.Seer)]
		public string RequiredResource
		{
			get { return _requiredResource?.Name; }
			set { _requiredResource = ScriptCompiler.FindTypeByName(value, false); }
		}

		[CommandProperty(AccessLevel.Seer)]
		public int RequiredQuantity
		{
			get { return _requiredQuantity; }
			set { _requiredQuantity = value; }
		}

		[CommandProperty(AccessLevel.Seer)]
		public string OfferedResource1
		{
			get { return _offeredResource1?.Name; }
			set { _offeredResource1 = ScriptCompiler.FindTypeByName(value, false); }
		}

		[CommandProperty(AccessLevel.Seer)]
		public int OfferedQuantity1
		{
			get { return _offeredQuantity1; }
			set { _offeredQuantity1 = value; }
		}

		[CommandProperty(AccessLevel.Seer)]
		public string OfferedResource2
		{
			get { return _offeredResource2?.Name; }
			set { _offeredResource2 = ScriptCompiler.FindTypeByName(value, false); }
		}

		[CommandProperty(AccessLevel.Seer)]
		public int OfferedQuantity2
		{
			get { return _offeredQuantity2; }
			set { _offeredQuantity2 = value; }
		}

		[CommandProperty(AccessLevel.Seer)]
		public string OfferedResource3
		{
			get { return _offeredResource3?.Name; }
			set { _offeredResource3 = ScriptCompiler.FindTypeByName(value, false); }
		}

		[CommandProperty(AccessLevel.Seer)]
		public int OfferedQuantity3
		{
			get { return _offeredQuantity3; }
			set { _offeredQuantity3 = value; }
		}

		[CommandProperty(AccessLevel.Seer)]
		public string OfferedResource4
		{
			get { return _offeredResource4?.Name; }
			set { _offeredResource4 = ScriptCompiler.FindTypeByName(value, false); }
		}

		[CommandProperty(AccessLevel.Seer)]
		public int OfferedQuantity4
		{
			get { return _offeredQuantity4; }
			set { _offeredQuantity4 = value; }
		}

		[CommandProperty(AccessLevel.Seer)]
		public string OfferedResource5
		{
			get { return _offeredResource5?.Name; }
			set { _offeredResource5 = ScriptCompiler.FindTypeByName(value, false); }
		}

		[CommandProperty(AccessLevel.Seer)]
		public int OfferedQuantity5
		{
			get { return _offeredQuantity5; }
			set { _offeredQuantity5 = value; }
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // Version

			// Write trade details
			writer.Write(_requiredResource?.FullName);
			writer.Write(_requiredQuantity);

			writer.Write(_offeredResource1?.FullName);
			writer.Write(_offeredQuantity1);

			writer.Write(_offeredResource2?.FullName);
			writer.Write(_offeredQuantity2);

			writer.Write(_offeredResource3?.FullName);
			writer.Write(_offeredQuantity3);

			writer.Write(_offeredResource4?.FullName);
			writer.Write(_offeredQuantity4);

			writer.Write(_offeredResource5?.FullName);
			writer.Write(_offeredQuantity5);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			// Read trade details
			_requiredResource = ScriptCompiler.FindTypeByName(reader.ReadString(), false);
			_requiredQuantity = reader.ReadInt();

			_offeredResource1 = ScriptCompiler.FindTypeByName(reader.ReadString(), false);
			_offeredQuantity1 = reader.ReadInt();

			_offeredResource2 = ScriptCompiler.FindTypeByName(reader.ReadString(), false);
			_offeredQuantity2 = reader.ReadInt();

			_offeredResource3 = ScriptCompiler.FindTypeByName(reader.ReadString(), false);
			_offeredQuantity3 = reader.ReadInt();

			_offeredResource4 = ScriptCompiler.FindTypeByName(reader.ReadString(), false);
			_offeredQuantity4 = reader.ReadInt();

			_offeredResource5 = ScriptCompiler.FindTypeByName(reader.ReadString(), false);
			_offeredQuantity5 = reader.ReadInt();
		}
	}
}

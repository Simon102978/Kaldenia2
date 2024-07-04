using System;
using System.Collections.Generic;
using Server.Gumps;
using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
	public class BazaarNPC : BaseVendor
	{
		private Type _requiredResource;
		private int _requiredQuantity;
		private int _requiredResourceArtID;
		private string _requiredResourceName;

		private readonly List<SBInfo> m_SBInfos = new List<SBInfo>();

		private List<(Type, int, int, string)> _offeredResources;

		[CommandProperty(AccessLevel.GameMaster)]
		public Type RequiredResource
		{
			get => _requiredResource;
			set => _requiredResource = value;
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int RequiredQuantity
		{
			get => _requiredQuantity;
			set => _requiredQuantity = value;
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int RequiredResourceArtID
		{
			get => _requiredResourceArtID;
			set => _requiredResourceArtID = value;
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public string RequiredResourceName
		{
			get => _requiredResourceName;
			set => _requiredResourceName = value;
		}

		// Properties for each offered resource
		[CommandProperty(AccessLevel.GameMaster)]
		public string OfferedResourceType1
		{
			get => _offeredResources[0].Item1?.FullName;
			set
			{
				var type = Type.GetType(value);
				if (type != null)
				{
					_offeredResources[0] = (type, _offeredResources[0].Item2, _offeredResources[0].Item3, _offeredResources[0].Item4);
				}
			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int OfferedResourceQuantity1
		{
			get => _offeredResources[0].Item2;
			set => _offeredResources[0] = (_offeredResources[0].Item1, value, _offeredResources[0].Item3, _offeredResources[0].Item4);
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int OfferedResourceArtID1
		{
			get => _offeredResources[0].Item3;
			set => _offeredResources[0] = (_offeredResources[0].Item1, _offeredResources[0].Item2, value, _offeredResources[0].Item4);
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public string OfferedResourceName1
		{
			get => _offeredResources[0].Item4;
			set => _offeredResources[0] = (_offeredResources[0].Item1, _offeredResources[0].Item2, _offeredResources[0].Item3, value);
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public string OfferedResourceType2
		{
			get => _offeredResources[1].Item1?.FullName;
			set
			{
				var type = Type.GetType(value);
				if (type != null)
				{
					_offeredResources[1] = (type, _offeredResources[1].Item2, _offeredResources[1].Item3, _offeredResources[1].Item4);
				}
			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int OfferedResourceQuantity2
		{
			get => _offeredResources[1].Item2;
			set => _offeredResources[1] = (_offeredResources[1].Item1, value, _offeredResources[1].Item3, _offeredResources[1].Item4);
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int OfferedResourceArtID2
		{
			get => _offeredResources[1].Item3;
			set => _offeredResources[1] = (_offeredResources[1].Item1, _offeredResources[1].Item2, value, _offeredResources[1].Item4);
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public string OfferedResourceName2
		{
			get => _offeredResources[1].Item4;
			set => _offeredResources[1] = (_offeredResources[1].Item1, _offeredResources[1].Item2, _offeredResources[1].Item3, value);
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public string OfferedResourceType3
		{
			get => _offeredResources[2].Item1?.FullName;
			set
			{
				var type = Type.GetType(value);
				if (type != null)
				{
					_offeredResources[2] = (type, _offeredResources[2].Item2, _offeredResources[2].Item3, _offeredResources[2].Item4);
				}
			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int OfferedResourceQuantity3
		{
			get => _offeredResources[2].Item2;
			set => _offeredResources[2] = (_offeredResources[2].Item1, value, _offeredResources[2].Item3, _offeredResources[2].Item4);
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int OfferedResourceArtID3
		{
			get => _offeredResources[2].Item3;
			set => _offeredResources[2] = (_offeredResources[2].Item1, _offeredResources[2].Item2, value, _offeredResources[2].Item4);
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public string OfferedResourceName3
		{
			get => _offeredResources[2].Item4;
			set => _offeredResources[2] = (_offeredResources[2].Item1, _offeredResources[2].Item2, _offeredResources[2].Item3, value);
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public string OfferedResourceType4
		{
			get => _offeredResources[3].Item1?.FullName;
			set
			{
				var type = Type.GetType(value);
				if (type != null)
				{
					_offeredResources[3] = (type, _offeredResources[3].Item2, _offeredResources[3].Item3, _offeredResources[3].Item4);
				}
			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int OfferedResourceQuantity4
		{
			get => _offeredResources[3].Item2;
			set => _offeredResources[3] = (_offeredResources[3].Item1, value, _offeredResources[3].Item3, _offeredResources[3].Item4);
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int OfferedResourceArtID4
		{
			get => _offeredResources[3].Item3;
			set => _offeredResources[3] = (_offeredResources[3].Item1, _offeredResources[3].Item2, value, _offeredResources[3].Item4);
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public string OfferedResourceName4
		{
			get => _offeredResources[3].Item4;
			set => _offeredResources[3] = (_offeredResources[3].Item1, _offeredResources[3].Item2, _offeredResources[3].Item3, value);
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public string OfferedResourceType5
		{
			get => _offeredResources[4].Item1?.FullName;
			set
			{
				var type = Type.GetType(value);
				if (type != null)
				{
					_offeredResources[4] = (type, _offeredResources[4].Item2, _offeredResources[4].Item3, _offeredResources[4].Item4);
				}
			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int OfferedResourceQuantity5
		{
			get => _offeredResources[4].Item2;
			set => _offeredResources[4] = (_offeredResources[4].Item1, value, _offeredResources[4].Item3, _offeredResources[4].Item4);
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int OfferedResourceArtID5
		{
			get => _offeredResources[4].Item3;
			set => _offeredResources[4] = (_offeredResources[4].Item1, _offeredResources[4].Item2, value, _offeredResources[4].Item4);
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public string OfferedResourceName5
		{
			get => _offeredResources[4].Item4;
			set => _offeredResources[4] = (_offeredResources[4].Item1, _offeredResources[4].Item2, _offeredResources[4].Item3, value);
		}

		[Constructable]
		public BazaarNPC() : base("Marchand itinérant")
		{
			_requiredResource = typeof(Log);
			_requiredQuantity = 20;
			_requiredResourceArtID = 0x1BDD;
			_requiredResourceName = "Buche";

			_offeredResources = new List<(Type, int, int, string)>
			{
				(typeof(Log), 10, 0x1, "Resource1"),
				(typeof(Log), 20, 0x2, "Resource2"),
				(typeof(Log), 30, 0x3, "Resource3"),
				(typeof(Log), 40, 0x4, "Resource4"),
				(typeof(Log), 50, 0x5, "Resource5")
			};

			Hue = 0x83EA;
			Body = 0x190;
			Name = Utility.RandomList(Name);
			Title = "Marchand du Bazaar";
			CantWalk = true;
		}

		public BazaarNPC(Serial serial) : base(serial) { }

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
			List<(Type, int, int, string)> offers = new List<(Type, int, int, string)>(_offeredResources);

			from.SendGump(new BazaarNPCGump((PlayerMobile)from, this, _requiredResource, _requiredQuantity, _requiredResourceArtID, _requiredResourceName, offers));
		}

		public override int GetHairHue()
		{
			return Utility.RandomAnimalHue();
		}

		public override void InitOutfit()
		{
			base.InitOutfit();
			AddItem(new Robe(Utility.RandomBirdHue()));
		}

		public override bool CheckVendorAccess(Mobile from)
		{
			return base.CheckVendorAccess(from);
		}

		public override void InitSBInfo()
		{
			// Aucune SBInfo nécessaire pour ce vendeur
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version 
			writer.Write(_requiredResource?.FullName);
			writer.Write(_requiredQuantity);
			writer.Write(_requiredResourceArtID);
			writer.Write(_requiredResourceName);

			writer.Write(_offeredResources.Count);
			foreach (var offer in _offeredResources)
			{
				writer.Write(offer.Item1?.FullName); // Serialize type name
				writer.Write(offer.Item2);
				writer.Write(offer.Item3);
				writer.Write(offer.Item4);
			}
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
			_requiredResource = Type.GetType(reader.ReadString());
			_requiredQuantity = reader.ReadInt();
			_requiredResourceArtID = reader.ReadInt();
			_requiredResourceName = reader.ReadString();

			int count = reader.ReadInt();
			_offeredResources = new List<(Type, int, int, string)>();
			for (int i = 0; i < count; i++)
			{
				Type type = Type.GetType(reader.ReadString());
				int quantity = reader.ReadInt();
				int artID = reader.ReadInt();
				string name = reader.ReadString();
				_offeredResources.Add((type, quantity, artID, name));
			}
		}
	}
}

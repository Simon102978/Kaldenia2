using System;
using System.Collections.Generic;
using Server.Gumps;
using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
	public class TradeNPC : BaseVendor
	{
		private readonly List<SBInfo> m_SBInfos = new List<SBInfo>();
		private Type _requiredResource; // Type de ressource requise pour le commerce
		private int _requiredQuantity; // Quantité de ressource requise
		private int _requiredResourceArtID; // ID d'art de la ressource requise
		private string _requiredResourceName; // Nom personnalisé pour la ressource requise

		private List<(Type, int, int, string)> _offeredResources; // Type, quantité, ID d'art, nom personnalisé

		[Constructable]
		public TradeNPC() : base("Marchand itinérant")
		{
			_requiredResource = typeof(Log);          // Par défaut: Log
			_requiredQuantity = 20;                   // Par défaut: 20
			_requiredResourceArtID = 0x1BDD;          // ID d'art par défaut pour Log
			_requiredResourceName = "Buche";            // Nom personnalisé par défaut pour la ressource requise

			_offeredResources = new List<(Type, int, int, string)>
			{
				(null, 0, 0, null),
				(null, 0, 0, null),
				(null, 0, 0, null),
				(null, 0, 0, null),
				(null, 0, 0, null)
			};

			Hue = 0x83EA;
			Body = 0x190; // Corps humain masculin
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
			List<(Type, int, int, string)> offers = new List<(Type, int, int, string)>();

			// Ajouter chaque offre basée sur les ressources proposées
			for (int i = 0; i < 5; i++)
			{
				offers.Add((_offeredResources[i].Item1, _offeredResources[i].Item2, _offeredResources[i].Item3, _offeredResources[i].Item4));
			}

			from.SendGump(new TradeNPCGump((PlayerMobile)from, this, _requiredResource, _requiredQuantity, _requiredResourceArtID, _requiredResourceName, offers));
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
			// Aucune SBInfo nécessaire pour ce vendeur
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
		public int RequiredResourceArtID
		{
			get { return _requiredResourceArtID; }
			set { _requiredResourceArtID = value; }
		}

		[CommandProperty(AccessLevel.Seer)]
		public string RequiredResourceName
		{
			get { return _requiredResourceName; }
			set { _requiredResourceName = value; }
		}

		[CommandProperty(AccessLevel.Seer)]
		public string OfferedResource1
		{
			get { return _offeredResources[0].Item1?.Name; }
			set { _offeredResources[0] = (ScriptCompiler.FindTypeByName(value, false), _offeredResources[0].Item2, _offeredResources[0].Item3, value); }
		}

		[CommandProperty(AccessLevel.Seer)]
		public int OfferedQuantity1
		{
			get { return _offeredResources[0].Item2; }
			set { _offeredResources[0] = (_offeredResources[0].Item1, value, _offeredResources[0].Item3, _offeredResources[0].Item4); }
		}

		[CommandProperty(AccessLevel.Seer)]
		public int OfferedResourceArtID1
		{
			get { return _offeredResources[0].Item3; }
			set { _offeredResources[0] = (_offeredResources[0].Item1, _offeredResources[0].Item2, value, _offeredResources[0].Item4); }
		}

		[CommandProperty(AccessLevel.Seer)]
		public string OfferedResourceName1
		{
			get { return _offeredResources[0].Item4; }
			set { _offeredResources[0] = (_offeredResources[0].Item1, _offeredResources[0].Item2, _offeredResources[0].Item3, value); }
		}

		[CommandProperty(AccessLevel.Seer)]
		public string OfferedResource2
		{
			get { return _offeredResources[1].Item1?.Name; }
			set { _offeredResources[1] = (ScriptCompiler.FindTypeByName(value, false), _offeredResources[1].Item2, _offeredResources[1].Item3, value); }
		}

		[CommandProperty(AccessLevel.Seer)]
		public int OfferedQuantity2
		{
			get { return _offeredResources[1].Item2; }
			set { _offeredResources[1] = (_offeredResources[1].Item1, value, _offeredResources[1].Item3, _offeredResources[1].Item4); }
		}

		[CommandProperty(AccessLevel.Seer)]
		public int OfferedResourceArtID2
		{
			get { return _offeredResources[1].Item3; }
			set { _offeredResources[1] = (_offeredResources[1].Item1, _offeredResources[1].Item2, value, _offeredResources[1].Item4); }
		}

		[CommandProperty(AccessLevel.Seer)]
		public string OfferedResourceName2
		{
			get { return _offeredResources[1].Item4; }
			set { _offeredResources[1] = (_offeredResources[1].Item1, _offeredResources[1].Item2, _offeredResources[1].Item3, value); }
		}

		[CommandProperty(AccessLevel.Seer)]
		public string OfferedResource3
		{
			get { return _offeredResources[2].Item1?.Name; }
			set { _offeredResources[2] = (ScriptCompiler.FindTypeByName(value, false), _offeredResources[2].Item2, _offeredResources[2].Item3, value); }
		}

		[CommandProperty(AccessLevel.Seer)]
		public int OfferedQuantity3
		{
			get { return _offeredResources[2].Item2; }
			set { _offeredResources[2] = (_offeredResources[2].Item1, value, _offeredResources[2].Item3, _offeredResources[2].Item4); }
		}

		[CommandProperty(AccessLevel.Seer)]
		public string OfferedResourceName3
		{
			get { return _offeredResources[2].Item4; }
			set { _offeredResources[2] = (_offeredResources[2].Item1, _offeredResources[2].Item2, _offeredResources[2].Item3, value); }
		}

		[CommandProperty(AccessLevel.Seer)]
		public string OfferedResource4
		{
			get { return _offeredResources[3].Item1?.Name; }
			set { _offeredResources[3] = (ScriptCompiler.FindTypeByName(value, false), _offeredResources[3].Item2, _offeredResources[3].Item3, value); }
		}

		[CommandProperty(AccessLevel.Seer)]
		public int OfferedQuantity4
		{
			get { return _offeredResources[3].Item2; }
			set { _offeredResources[3] = (_offeredResources[3].Item1, value, _offeredResources[3].Item3, _offeredResources[3].Item4); }
		}

		[CommandProperty(AccessLevel.Seer)]
		public int OfferedResourceArtID4
		{
			get { return _offeredResources[3].Item3; }
			set { _offeredResources[3] = (_offeredResources[3].Item1, _offeredResources[3].Item2, value, _offeredResources[3].Item4); }
		}

		[CommandProperty(AccessLevel.Seer)]
		public string OfferedResourceName4
		{
			get { return _offeredResources[3].Item4; }
			set { _offeredResources[3] = (_offeredResources[3].Item1, _offeredResources[3].Item2, _offeredResources[3].Item3, value); }
		}

		[CommandProperty(AccessLevel.Seer)]
		public string OfferedResource5
		{
			get { return _offeredResources[4].Item1?.Name; }
			set { _offeredResources[4] = (ScriptCompiler.FindTypeByName(value, false), _offeredResources[4].Item2, _offeredResources[4].Item3, value); }
		}

		[CommandProperty(AccessLevel.Seer)]
		public int OfferedQuantity5
		{
			get { return _offeredResources[4].Item2; }
			set { _offeredResources[4] = (_offeredResources[4].Item1, value, _offeredResources[4].Item3, _offeredResources[4].Item4); }
		}

		[CommandProperty(AccessLevel.Seer)]
		public int OfferedResourceArtID5
		{
			get { return _offeredResources[4].Item3; }
			set { _offeredResources[4] = (_offeredResources[4].Item1, _offeredResources[4].Item2, value, _offeredResources[4].Item4); }
		}

		[CommandProperty(AccessLevel.Seer)]
		public string OfferedResourceName5
		{
			get { return _offeredResources[4].Item4; }
			set { _offeredResources[4] = (_offeredResources[4].Item1, _offeredResources[4].Item2, _offeredResources[4].Item3, value); }
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // Version

			// Write trade details
			writer.Write(_requiredResource?.FullName ?? string.Empty);
			writer.Write(_requiredQuantity);
			writer.Write(_requiredResourceArtID);
			writer.Write(_requiredResourceName);

			for (int i = 0; i < 5; i++)
			{
				writer.Write(_offeredResources[i].Item1?.FullName ?? string.Empty); // Type.FullName for item type
				writer.Write(_offeredResources[i].Item2); // Quantity
				writer.Write(_offeredResources[i].Item3); // ArtID
				writer.Write(_offeredResources[i].Item4 ?? string.Empty); // Name
			}
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			// Read trade details
			string requiredResourceName = reader.ReadString();
			_requiredResource = !string.IsNullOrEmpty(requiredResourceName) ? ScriptCompiler.FindTypeByName(requiredResourceName, false) : null;
			_requiredQuantity = reader.ReadInt();
			_requiredResourceArtID = reader.ReadInt();
			_requiredResourceName = reader.ReadString();

			_offeredResources = new List<(Type, int, int, string)>();

			for (int i = 0; i < 5; i++)
			{
				string resourceTypeName = reader.ReadString();
				Type resourceType = !string.IsNullOrEmpty(resourceTypeName) ? ScriptCompiler.FindTypeByName(resourceTypeName, false) : null;
				int quantity = reader.ReadInt();
				int artID = reader.ReadInt();
				string resourceName = reader.ReadString();

				_offeredResources.Add((resourceType, quantity, artID, resourceName));

			}
		}
	}
}

using System;
using System.Linq;
using System.Runtime.InteropServices;
using Server.Items;
using System.Collections.Generic;

namespace Server.Misc
{
 

 		
        class Klyx : BaseRace
        {

		public override string Background => "Parler de brouhahas et de Casanovas nous mène inévitablement à penser aux Klyx, ce peuple aux mœurs légères qui s’empresse de courtiser leur énième future partenaire… ou les richesses de ce monde. L’amour comme l’or est cueilli comme des fruits à portée de toute main, sans lendemain. La jouissance par le touché, surtout l’or, est chose commune chez les Klyx. \n\nInséparables de la poussière des mines et de ses merveilles, les Klyx préfèrent s’en tenir aux forges et les secrets des minerais plutôt que le bien-vivre de la société. Si la plupart des peuples envient leurs brillantes parures, c’est dû à leurs remarquables connaissent des pierres précieuses qui regorgent de magie. Les Klyx les portent avec assurances dans chacun de leurs bijoux.\n\nAvares de tout l'or du monde, les Klyx s’emportent facilement contre toute atteinte à leur richesse matérielle. Ils préfèrent être exploité et même s’amputer pour honorer une dette. Chaque chaîne d'or, bijou, pierre sertie et canne d'appui en or massive est porté comme un morceau de vêtement. Il n’est pas rare d’observer des Klyx confectionner des chapeaux robustes pour y assortir des diamants ou tourmalines.\n\nToutes les méthodes sont bonnes pour séduire, mais amadouer est grande favorite. Si les compliments sont insuffisants, les Klyx se rabattent alors sur leur riche dialecte difficilement résistible. Ce pouvoir de séduction leur permette de s’enrichir aux dépens des autres.\n\n''Yé suis el meilleur, ma bella, si tout lés étoiles pourré être comme toé, yé les regarderé sans cesse''\n\nInspiration  - Spanius (Merci Profania) - Nain";
		 
		public override int[] SkinHues => new int[] { 1051, 1055, 1052, 1056, 1053, 1057, 1054, 1058 };

		//2102
		
        public static void Configure()
            {
                /* Here we configure all races. Some notes:
                * 
                * 1) The first 32 races are reserved for core use.
                * 2) Race 0x7F is reserved for core use.
                * 3) Race 0xFF is reserved for core use.
                * 4) Changing or removing any predefined races may cause server instability.
                */
               RegisterRace(new Klyx(6,6));
            }

        // 	protected Race(int raceID,  int raceIndex, string name,  string pluralName, int maleBody, int femaleBody, int maleGhostBody,  int femaleGhostBody)

        public Klyx(int raceID, int raceIndex)
                : base(raceID, raceIndex, "Klyx", "Klyx", 400, 401, 402, 403)
            {
            }


        public override bool ValidateEquipment(Item item)
            {
            return true;
            }

		public override BaseRaceGumps GetCorps(int hue)
		{
			int itemId = 41509;

			switch (hue)
			{
				case 1051:
					itemId = 41509;
					break;
				case 1055:
					itemId = 41509;
					break;
				case 1052:
					itemId = 41505;
					break;
				case 1053:
					itemId = 41505;
					break;
				case 1056:
					itemId = 41503;
					break;
				case 1057:
					itemId = 41503;
					break;
				case 1054:
					itemId = 41504; //
					break;
				case 1058:
					itemId = 41504;
					break;
				default:
					break;
			}
			return new CorpsKlyx(itemId, hue);
		}

		public override int GetGump(bool female, int hue)
		{
			int gumpid = 52090;

			switch (hue)
			{
				case 1051:
					gumpid = 52090;
					break;
				case 1055:
					gumpid = 52090;
					break;
				case 1052:
					gumpid = 52086;
					break;
				case 1053:
					gumpid = 52086;
					break;
				case 1056:
					gumpid = 52084;
					break;
				case 1057:
					gumpid = 52084;
					break;
				case 1054:
					gumpid = 52085; //
					break;
				case 1058:
					gumpid = 52085;
					break;
				default:
					break;
			}

			if (female)
			{
				gumpid += 10000;
			}

			return gumpid;
		}



	}

    
    
}

namespace Server.Items
{
    public class CorpsKlyx : BaseRaceGumps
    {
        [Constructable]
        public CorpsKlyx()
            : this(0)
        {
        }

        [Constructable]
        public CorpsKlyx(int Id, int hue)
            : base(Id, hue)
        {
            Name = "Klyx";
        }

        public CorpsKlyx(Serial serial)
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


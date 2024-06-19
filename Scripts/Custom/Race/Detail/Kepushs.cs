using System;
using System.Linq;
using System.Runtime.InteropServices;
using Server.Items;
using System.Collections.Generic;

namespace Server.Misc
{
	
        class Kepushs : BaseRace
        {

		public override bool Restriction => true;


		public override string Background => "Parmi toutes les expériences macabres de sacrifice et de démembrement, seule la sordide tribu des Kepushs les élève en célébrations. Tout ce qui provient de la terre est exploitée et honorée. En chaque chose contient la création vitale qui se doit d’être glorifiée par une cérémonie. Par offrande, les Kepushs donnent leur sang pour garantir l’équilibre de Kaldenia, encore fragile après la « Marche des Titans » repoussant les continents et agitant les mers avec eux. \n\nAu service de l’œil unique – communément appelé Keos, l’œil du Père Créateur –, la tribu Kepush louange son héritage et sa richesse autant dans la nuit que le jour. Lorsque l’œil unique assombrie les terres de Kaldenia, les Kepushs peuvent être entendus par leurs cris hystériques : ils se purifient en expulsant leurs démons intérieurs au travers de paroles extatiques et étranges. Une fois le voile sur Kaldenia levé, l’hystérie du prophète nocturne cède sa place au sage alchimiste qui réserve son temps à s’enjouer et étudier les plantes pour concocter des antidotes contre les nombreuses menaces.\n\nLa parure Kepush se démarque par la peinture corporelle, la fourrure, le cuir et le plumage fièrement portée. Considérant les textiles inutiles et irrespectueux envers l’œil unique, les Kepushs voient plutôt les dons de l’œil unique comme une richesse à préserver dans leur forme. La transformer est une souillure des plus insultantes.";

		public override int[] SkinHues => new int[] { 1747, 1752, 1748, 1753, 1749, 1754, 1751, 1750 };

        public static void Configure()
            {
                /* Here we configure all races. Some notes:
                * 
                * 1) The first 32 races are reserved for core use.
                * 2) Race 0x7F is reserved for core use.
                * 3) Race 0xFF is reserved for core use.
                * 4) Changing or removing any predefined races may cause server instability.
                */
               RegisterRace(new Kepushs(7,7));
            }

        // 	protected Race(int raceID,  int raceIndex, string name,  string pluralName, int maleBody, int femaleBody, int maleGhostBody,  int femaleGhostBody)

        public Kepushs(int raceID, int raceIndex)
                : base(raceID, raceIndex, "Kepush", "Kepushs", 400, 401, 402, 403)
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
				case 1747:
					itemId = 41509;
					break;
				case 1752:
					itemId = 41509;
					break;
				case 1748:
					itemId = 41505;
					break;
				case 1753:
					itemId = 41505;
					break;
				case 1749:
					itemId = 41503;
					break;
				case 1754:
					itemId = 41503;
					break;
				case 1751:
					itemId = 41504; //
					break;
				case 1750:
					itemId = 41504;
					break;
				default:
					break;
			}
			return new CorpsKepush(itemId, hue);
		}

		public override int GetGump(bool female, int hue)
		{
			int gumpid = 52090;

			switch (hue)
			{
				case 1747:
					gumpid = 52090;
					break;
				case 1752:
					gumpid = 52090;
					break;
				case 1748:
					gumpid = 52086;
					break;
				case 1753:
					gumpid = 52086;
					break;
				case 1749:
					gumpid = 52084;
					break;
				case 1754:
					gumpid = 52084;
					break;
				case 1751:
					gumpid = 52085; //
					break;
				case 1750:
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
    public class CorpsKepush : BaseRaceGumps
    {
        [Constructable]
        public CorpsKepush()
            : this(0)
        {
        }

        [Constructable]
        public CorpsKepush(int Id, int hue)
            : base(Id, hue)
        {
            Name = "Kepush";
        }

        public CorpsKepush(Serial serial)
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


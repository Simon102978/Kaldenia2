using System;
using System.Linq;
using System.Runtime.InteropServices;
using Server.Items;
using System.Collections.Generic;

namespace Server.Misc
{
 

 		
        class Kuya : BaseRace
        {

		public override string Background => "Fier représentant des montagnes l'autre côté de la mer, les Kuyas sont considéré comme un peuple nomade et discipliné a cheval. Souvent symboliser comme ‘’Invasion Barbare‘’, les Kuyas ne se gênent pas pour démontrer leur talent à cheval, pour eux une vie sur terre sans galoper ne mérite pas d'être vécu.\n\nLa pire chose que vous pouvez faire à un Kuya est de le laisser en cage ou de le priver de son cheval.\n\nUne nation reconnue non seulement pour son effet nomade mais également pour sa musique. Les Kuyas sont des passionnés du son. Pour eux la musique est le centre même d'un bon divertissement, selon certains écrits Kuyas. Ils auraient eu des faveurs divines grâce à leur sonorité recherchée.\n\nLes Kuyas ne sont pas des êtres à prendre à la légère, malgré le fait de ne démontrer aucune émotion face à l'adversité, parfois considérer comme trop terne. les Kuyas gardent leur aise avec ceux qu'ils apprécient, telle une horde de Loup. \n\nHabitué à galoper sur de longues distances, les Kuyas ont été habitués à pister et survivre dans plusieurs conditions différentes. Leur habillement différencie de la météo auquel ils doivent faire face, cela peut aller de la grosse fourrure double épaisseur au petit vêtement fin et léger. Leur couleur représentative de ces va et bien est l'orangée et le brun. \n\nQue vaut l'amour chez les Kuyas ? Malheureusement ils sont des êtres en mouvement constant, pour eux se rattacher à quelqu'un est signe de rester fixe à un endroit. Il existe parfois quelques exceptions, si la destinée d'un Kuya viens a celle d'un autre alors voyageons ensemble. \n\nQu'en est-il de la croyance majeure des Kuyas ? Pour eux, la croyance est qu'ils pourront un jour galoper à l'ouest et faire trembler les océans. Pour eux le culte des cinq n'est qu'une illusion formée par l'empire pour établir leur totale domination sur les autres peuples. \n\nLe langage Kuyas est souvent associé entre des mots saccader et couper en deux. Ils prennent leur temps pour socialiser et développer leur dialecte vu qu'ils sont rarement en contact avec des gens, pour eux prendre le temps de s'asseoir pour parler longtemps est primordial afin de conclure une discussion. \n\n''J'aimerais payer une bière au légionnaire par la bas'' Dialecte Korain \n\n''J'aime-rais. Pa.. Payer une... Bi-Hier O Légi - Naire par l-Bas ‘’ - Dialecte Kuya \n\nInspiration du peuple Kuya - Mongols et Doth-Raki";
		 
		public override int[] SkinHues => new int[] { 1118, 1122, 1119, 1123, 1120, 1124, 1121, 1125 };

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
               RegisterRace(new Kuya(5,5));
            }

        // 	protected Race(int raceID,  int raceIndex, string name,  string pluralName, int maleBody, int femaleBody, int maleGhostBody,  int femaleGhostBody)

        public Kuya(int raceID, int raceIndex)
                : base(raceID, raceIndex, "Kuya", "Kuyas", 400, 401, 402, 403)
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
				case 1118:
					itemId = 41509;
					break;
				case 1122:
					itemId = 41509;
					break;
				case 1119:
					itemId = 41505;
					break;
				case 1123:
					itemId = 41505;
					break;
				case 1120:
					itemId = 41503;
					break;
				case 1124:
					itemId = 41503;
					break;
				case 1121:
					itemId = 41504; //
					break;
				case 1125:
					itemId = 41504;
					break;
				default:
					break;
			}
			return new CorpsKuya(itemId, hue);
		}

		public override int GetGump(bool female, int hue)
		{
			int gumpid = 52090;

			switch (hue)
			{
				case 1118:
					gumpid = 52090;
					break;
				case 1122:
					gumpid = 52090;
					break;
				case 1119:
					gumpid = 52086;
					break;
				case 1123:
					gumpid = 52086;
					break;
				case 1120:
					gumpid = 52084;
					break;
				case 1124:
					gumpid = 52084;
					break;
				case 1121:
					gumpid = 52085; //
					break;
				case 1125:
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
    public class CorpsKuya : BaseRaceGumps
    {
        [Constructable]
        public CorpsKuya()
            : this(0)
        {
        }

        [Constructable]
        public CorpsKuya(int Id, int hue)
            : base(Id, hue)
        {
            Name = "Kuya";
        }

        public CorpsKuya(Serial serial)
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


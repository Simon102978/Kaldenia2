using System;
using Server;
using Server.Mobiles;
using Server.Gumps;
using System.Collections.Generic;
using Server.Engines.Craft;

namespace Server.Items
{
	
    public abstract class Peintures : Item, ICraftable
	{
		private string m_Title;
		private Dictionary<int, string> m_Description = new Dictionary<int, string>();
		private Mobile m_Crafter;
		private string m_CrafterName;
		private bool m_Finish;

		[CommandProperty(AccessLevel.GameMaster)]
		public string CrafterName { get => m_CrafterName; set => m_CrafterName = value; }

		[CommandProperty(AccessLevel.GameMaster)]
		public bool Finish { get => m_Finish; set => m_Finish = value; }

		[CommandProperty(AccessLevel.GameMaster)]
		public string Title { get => m_Title; set => m_Title = value; }

		[CommandProperty(AccessLevel.GameMaster)]
		public Dictionary<int, string> Description { get => m_Description; set => m_Description = value; }

		[CommandProperty(AccessLevel.GameMaster)]
		public Mobile Crafter { get => m_Crafter; set => m_Crafter = value; }

		public Peintures(int itemID)
            : this(itemID, 0)
        {
        }

        public Peintures(int itemID, int hue)
            : base(itemID)
        {
        }

        public Peintures(Serial serial)
            : base(serial)
        {
        }

		public override void OnDoubleClick(Mobile from)
		{
			if (from is CustomPlayerMobile )
			{
				CustomPlayerMobile cp = (CustomPlayerMobile)from;

				if (Finish || from == m_Crafter || from.AccessLevel > AccessLevel.Player)
				{			
					from.SendGump(new PeinturesGump(cp, this));
				}
				else 
				{
					from.SendMessage("Seul le créateur de la toile peut la terminer.");
				}
			
			}
		}

		public override void OnDoubleClickNotAccessible(Mobile from)
		{
			if (from is CustomPlayerMobile)
			{
				CustomPlayerMobile cp = (CustomPlayerMobile)from;

				if (Finish || from == m_Crafter || from.AccessLevel > AccessLevel.Player)
				{
					from.SendGump(new PeinturesGump(cp, this));
				}
				else
				{
					from.SendMessage("Seul le créateur de la toile peut la terminer.");
				}

			}
		}

		public override bool IsAccessibleTo(Mobile check)
		{
			return true;
		}

		public string GetContenu(int Entry)
		{
			if (Description.ContainsKey(Entry))
			{
				return Description[Entry];
			}
			else
			{
				return "";
			}


		}


		public void ApplyPeinture(PeintureEnCours p)
		{
			m_Finish = true;
			m_Title = p.Title;
			m_CrafterName = p.Name;

	//		Name = m_Title;

			foreach (KeyValuePair<int,string> item in p.Description)
			{
				addContenu(item.Key, item.Value);
			}
		}

		public void addContenu(int Entry, string value)
		{
			if (m_Description.ContainsKey(Entry))
			{
				m_Description[Entry] = value;
			}
			else
			{
				m_Description.Add(Entry, value);
			}
		}

		public override void GetProperties(ObjectPropertyList list)
        {
            if (!Finish)
                list.Add("Toile à complèter");
			else
			{
				list.Add(m_Title);
				list.Add("Par: " + m_CrafterName);
			}
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version
			writer.Write(m_CrafterName);
			writer.Write(m_Finish);
			writer.Write(m_Crafter);
			writer.Write(Description.Count);

			foreach (KeyValuePair<int, string> item in Description)
			{
				writer.Write(item.Key);
				writer.Write(item.Value);
			}

			writer.Write((string)m_Title);
		}

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();


			switch (version)
			{
				case 1:
					{
						m_CrafterName = reader.ReadString();
						m_Finish = reader.ReadBool();
						m_Crafter = reader.ReadMobile();

						int count = (int)reader.ReadInt();

						for (int i = 0; i < count; i++)
						{
							Description.Add(reader.ReadInt(), reader.ReadString());
						}

						goto case 0;
					}
				case 0:
					{
						m_Title = reader.ReadString();

						break;
					}
			}

		
        }

		public int OnCraft(int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, ITool tool, CraftItem craftItem, int resHue)
		{
		
			

			Crafter = from;


			return quality;

		}
	}
	
	public class PortraitSud01 : Peintures
	{
		[Constructable]
		public PortraitSud01() : this( 0 )
		{
		}

		[Constructable]
		public PortraitSud01(int hue) : base( 3750, hue )
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitSud01( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PortraitSud02 : Peintures
	{
		[Constructable]
		public PortraitSud02() : this( 0 )
		{
		}

		[Constructable]
		public PortraitSud02(int hue) : base( 3751 , hue)
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitSud02( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PortraitSud03 : Peintures
	{
		[Constructable]
		public PortraitSud03() : this( 0 )
		{
		}

		[Constructable]
		public PortraitSud03(int hue) : base( 3815, hue )
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitSud03( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
	public class PortraitSud04 : Peintures
	{
		[Constructable]
		public PortraitSud04() : this( 0 )
		{
		}

		[Constructable]
		public PortraitSud04(int hue) : base( 3743 , hue)
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitSud04( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PortraitSud05 : Peintures
	{
		[Constructable]
		public PortraitSud05() : this( 0 )
		{
		}

		[Constructable]
		public PortraitSud05(int hue) : base( 3747, hue )
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitSud05( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PortraitEst01 : Peintures
	{
        [Constructable]
        public PortraitEst01()
            : this(0)
        {
        }

        [Constructable]
        public PortraitEst01(int hue)
            : base(3749, hue)
        {
			Weight = 1.0;
            Name = "Portrait";
        }
		
		public PortraitEst01( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PortraitEst02 : Peintures
	{
		[Constructable]
		public PortraitEst02() : this( 0 )
		{
		}

		[Constructable]
		public PortraitEst02(int hue) : base( 3752, hue )
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitEst02( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PortraitEst03 : Peintures
	{
		[Constructable]
		public PortraitEst03() : this( 0 )
		{
		}

		[Constructable]
		public PortraitEst03(int hue) : base( 3785, hue )
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitEst03( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PortraitEst04 : Peintures
	{
		[Constructable]
		public PortraitEst04() : this( 0 )
		{
		}

		[Constructable]
		public PortraitEst04(int hue) : base( 3784, hue )
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitEst04( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PortraitEst05 : Peintures
	{
		[Constructable]
		public PortraitEst05() : this( 0 )
		{
		}

		[Constructable]
		public PortraitEst05(int hue) : base( 3748, hue )
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitEst05( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PortraitHomme : Peintures
	{
		[Constructable]
		public PortraitHomme() : this( 0 )
		{
		}

		[Constructable]
		public PortraitHomme(int hue) : base( 10905, hue )
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitHomme( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PortraitSud06 : Peintures
	{
		[Constructable]
		public PortraitSud06() : this( 0 )
		{
		}

		[Constructable]
		public PortraitSud06(int hue) : base( 10845, hue )
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitSud06( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PortraitSud07 : Peintures
	{
		[Constructable]
		public PortraitSud07() : this( 0 )
		{
		}

		[Constructable]
		public PortraitSud07(int hue) : base( 10846, hue )
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitSud07( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PortraitSud08 : Peintures
	{
		[Constructable]
		public PortraitSud08() : this( 0 )
		{
		}

		[Constructable]
		public PortraitSud08(int hue) : base( 10847, hue )
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitSud08( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PortraitSud09 : Peintures
	{
		[Constructable]
		public PortraitSud09() : this( 0 )
		{
		}

		[Constructable]
		public PortraitSud09(int hue) : base( 10848, hue )
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitSud09( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PortraitEst06 : Peintures
	{
		[Constructable]
		public PortraitEst06() : this( 0 )
		{
		}

		[Constructable]
		public PortraitEst06(int hue) : base( 10849, hue )
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitEst06( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PortraitEst07 : Peintures
	{
		[Constructable]
		public PortraitEst07() : this( 0 )
		{
		}

		[Constructable]
		public PortraitEst07(int hue) : base( 10850, hue )
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitEst07( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PortraitEst08 : Peintures
	{
		[Constructable]
		public PortraitEst08() : this( 0 )
		{
		}

		[Constructable]
		public PortraitEst08(int hue) : base( 10851 , hue)
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitEst08( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PortraitEst09 : Peintures
	{
		[Constructable]
		public PortraitEst09() : this( 0 )
		{
		}

		[Constructable]
		public PortraitEst09(int hue) : base( 10852, hue )
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitEst09( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureSud01 : Peintures
	{
		[Constructable]
		public PeintureSud01() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud01(int hue) : base( 11634 , hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud01( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureEst01 : Peintures
	{
		[Constructable]
		public PeintureEst01() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst01(int hue) : base( 11633, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst01( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PortraitFemme : Peintures
	{
		[Constructable]
		public PortraitFemme() : this( 0 )
		{
		}

		[Constructable]
		public PortraitFemme(int hue) : base( 3744 , hue)
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitFemme( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PortraitSud10 : Peintures
	{
		[Constructable]
		public PortraitSud10() : this( 0 )
		{
		}

		[Constructable]
		public PortraitSud10(int hue) : base(0x0C2C, hue )
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitSud10( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PortraitSud11 : Peintures
	{
		[Constructable]
		public PortraitSud11() : this( 0 )
		{
		}

		[Constructable]
		public PortraitSud11(int hue) : base(0x0EE7, hue )
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitSud11( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PortraitSud12 : Peintures
	{
		[Constructable]
		public PortraitSud12() : this( 0 )
		{
		}

		[Constructable]
		public PortraitSud12(int hue) : base( 10854, hue )
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitSud12( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PortraitSud13 : Peintures
	{
		[Constructable]
		public PortraitSud13() : this( 0 )
		{
		}

		[Constructable]
		public PortraitSud13(int hue) : base( 10857, hue )
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitSud13( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PortraitSud14 : Peintures
	{
		[Constructable]
		public PortraitSud14() : this( 0 )
		{
		}

		[Constructable]
		public PortraitSud14(int hue) : base( 10858 , hue)
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitSud14( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PortraitSud15 : Peintures
	{
		[Constructable]
		public PortraitSud15() : this( 0 )
		{
		}

		[Constructable]
		public PortraitSud15(int hue) : base( 10859, hue )
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitSud15( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PortraitSud16 : Peintures
	{
		[Constructable]
		public PortraitSud16() : this( 0 )
		{
		}

		[Constructable]
		public PortraitSud16(int hue) : base( 10860, hue )
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitSud16( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PortraitEst10 : Peintures
	{
		[Constructable]
		public PortraitEst10() : this( 0 )
		{
		}

		[Constructable]
		public PortraitEst10(int hue) : base(0x2D71, hue )
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitEst10( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PortraitEst11 : Peintures
	{
		[Constructable]
		public PortraitEst11() : this( 0 )
		{
		}

		[Constructable]
		public PortraitEst11(int hue) : base( 10855, hue )
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitEst11( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PortraitEst12 : Peintures
	{
		[Constructable]
		public PortraitEst12() : this( 0 )
		{
		}

		[Constructable]
		public PortraitEst12(int hue) : base( 10856, hue )
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitEst12( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PortraitEst13 : Peintures
	{
		[Constructable]
		public PortraitEst13() : this( 0 )
		{
		}

		[Constructable]
		public PortraitEst13(int hue) : base( 10861, hue )
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitEst13( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PortraitEst14 : Peintures
	{
		[Constructable]
		public PortraitEst14() : this( 0 )
		{
		}

		[Constructable]
		public PortraitEst14(int hue) : base( 10862, hue )
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitEst14( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PortraitEst15 : Peintures
	{
		[Constructable]
		public PortraitEst15() : this( 0 )
		{
		}

		[Constructable]
		public PortraitEst15(int hue) : base( 10863, hue )
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitEst15( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PortraitEst16 : Peintures
	{
		[Constructable]
		public PortraitEst16() : this( 0 )
		{
		}

		[Constructable]
		public PortraitEst16(int hue) : base( 10864, hue )
		{
			Weight = 1.0;
			
            Name = "Portrait";
		}

		public PortraitEst16( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureSud : Peintures
	{
		[Constructable]
		public PeintureSud() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud(int hue) : base(0x2D70, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureSud02 : Peintures
	{
		[Constructable]
		public PeintureSud02() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud02(int hue) : base(0x240D, hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud02( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureSud03 : Peintures
	{
		[Constructable]
		public PeintureSud03() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud03(int hue) : base(0x240F, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud03( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureSud04 : Peintures
	{
		[Constructable]
		public PeintureSud04() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud04(int hue) : base(0x2411, hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud04( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureSud05 : Peintures
	{
		[Constructable]
		public PeintureSud05() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud05(int hue) : base(0x2413, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud05( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureSud06 : Peintures
	{
		[Constructable]
		public PeintureSud06() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud06(int hue) : base(0x2415, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud06( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureSud07 : Peintures
	{
		[Constructable]
		public PeintureSud07() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud07(int hue) : base(0x2417, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud07( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureSud08 : Peintures
	{
		[Constructable]
		public PeintureSud08() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud08(int hue) : base(0x2887, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud08( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureSud09 : Peintures
	{
		[Constructable]
		public PeintureSud09() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud09(int hue) : base(0x403D, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud09( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureSud10 : Peintures
	{
		[Constructable]
		public PeintureSud10() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud10(int hue) : base(0x4C20, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud10( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureSud11 : Peintures
	{
		[Constructable]
		public PeintureSud11() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud11(int hue) : base(0x4C22, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud11( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureSud12 : Peintures
	{
		[Constructable]
		public PeintureSud12() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud12(int hue) : base(0x4C26, hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud12( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureSud13 : Peintures
	{
		[Constructable]
		public PeintureSud13() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud13(int hue) : base(0x4C28, hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud13( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureEst02 : Peintures
	{
		[Constructable]
		public PeintureEst02() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst02(int hue) : base(0x240E, hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst02( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureEst03 : Peintures
	{
		[Constructable]
		public PeintureEst03() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst03(int hue) : base(0x2410, hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst03( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureEst04 : Peintures
	{
		[Constructable]
		public PeintureEst04() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst04(int hue) : base(0x2412, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst04( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureEst05 : Peintures
	{
		[Constructable]
		public PeintureEst05() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst05(int hue) : base(0x2414, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst05( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureEst06 : Peintures
	{
		[Constructable]
		public PeintureEst06() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst06(int hue) : base(0x2416, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst06( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureEst07 : Peintures
	{
		[Constructable]
		public PeintureEst07() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst07(int hue) : base(0x2418, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst07( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureEst08 : Peintures
	{
		[Constructable]
		public PeintureEst08() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst08(int hue) : base(0x2886, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst08( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureEst09 : Peintures
	{
		[Constructable]
		public PeintureEst09() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst09(int hue) : base(0x403E, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst09( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureEst10 : Peintures
	{
		[Constructable]
		public PeintureEst10() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst10(int hue) : base(0x4C21, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst10( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureEst11 : Peintures
	{
		[Constructable]
		public PeintureEst11() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst11(int hue) : base(0x4C23, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst11( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureEst12 : Peintures
	{
		[Constructable]
		public PeintureEst12() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst12(int hue) : base(0x4C27, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst12( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureEst13 : Peintures
	{
		[Constructable]
		public PeintureEst13() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst13(int hue) : base(0x4C29, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst13( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureSud14 : Peintures
	{
		[Constructable]
		public PeintureSud14() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud14(int hue) : base(0x4C35, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud14( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureSud15 : Peintures
	{
		[Constructable]
		public PeintureSud15() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud15(int hue) : base(0x4C36, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud15( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureSud16 : Peintures
	{
		[Constructable]
		public PeintureSud16() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud16(int hue) : base(0x4C37, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud16( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureSud17 : Peintures
	{
		[Constructable]
		public PeintureSud17() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud17(int hue) : base(0x9CA8, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud17( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureSud18 : Peintures
	{
		[Constructable]
		public PeintureSud18() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud18(int hue) : base(0x9E2D, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud18( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureSud19 : Peintures
	{
		[Constructable]
		public PeintureSud19() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud19(int hue) : base(0x9E2F, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud19( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureSud20 : Peintures
	{
		[Constructable]
		public PeintureSud20() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud20(int hue) : base(0x9E31, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud20( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureSud21 : Peintures
	{
		[Constructable]
		public PeintureSud21() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud21(int hue) : base(0x9D53, hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud21( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureSud22 : Peintures
	{
		[Constructable]
		public PeintureSud22() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud22(int hue) : base(0x9D55, hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud22( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureSud23 : Peintures
	{
		[Constructable]
		public PeintureSud23() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud23(int hue) : base(0x9D57, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud23( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureSud24 : Peintures
	{
		[Constructable]
		public PeintureSud24() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud24(int hue) : base(0x9D59, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud24( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureSud25 : Peintures
	{
		[Constructable]
		public PeintureSud25() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud25(int hue) : base(0x9D5B, hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud25( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureSud26 : Peintures
	{
		[Constructable]
		public PeintureSud26() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud26(int hue) : base(0x9E33, hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud26( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureEst14 : Peintures
	{
		[Constructable]
		public PeintureEst14() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst14(int hue) : base(0x4C32, hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst14( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureEst15 : Peintures
	{
		[Constructable]
		public PeintureEst15() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst15(int hue) : base(0x4C33, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst15( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureEst16 : Peintures
	{
		[Constructable]
		public PeintureEst16() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst16(int hue) : base(0x4C34, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst16( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureEst17 : Peintures
	{
		[Constructable]
		public PeintureEst17() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst17(int hue) : base(0x9CA9, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst17( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureEst18 : Peintures
	{
		[Constructable]
		public PeintureEst18() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst18(int hue) : base(0x9E2E, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst18( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureEst19 : Peintures
	{
		[Constructable]
		public PeintureEst19() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst19(int hue) : base(0x9E30, hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst19( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureEst20 : Peintures
	{
		[Constructable]
		public PeintureEst20() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst20(int hue) : base(0x9E32, hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst20( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureEst21 : Peintures
	{
		[Constructable]
		public PeintureEst21() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst21(int hue) : base(0x9E34, hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst21( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureEst22 : Peintures
	{
		[Constructable]
		public PeintureEst22() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst22(int hue) : base(0x9D54, hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst22( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureEst23 : Peintures
	{
		[Constructable]
		public PeintureEst23() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst23(int hue) : base(0x9D56, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst23( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureEst24 : Peintures
	{
		[Constructable]
		public PeintureEst24() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst24(int hue) : base(0x9D58, hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst24( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureEst25 : Peintures
	{
		[Constructable]
		public PeintureEst25() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst25(int hue) : base(0x9D5A, hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst25( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureEst26 : Peintures
	{
		[Constructable]
		public PeintureEst26() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst26(int hue) : base(0x9D5C, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst26( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureSud27 : Peintures
	{
		[Constructable]
		public PeintureSud27() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud27(int hue) : base(0x2D74, hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud27( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureSud28 : Peintures
	{
		[Constructable]
		public PeintureSud28() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud28(int hue) : base(0x9D3C, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud28( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureSud29 : Peintures
	{
		[Constructable]
		public PeintureSud29() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud29(int hue) : base(0x9D5D, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud29( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureSud30 : Peintures
	{
		[Constructable]
		public PeintureSud30() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud30(int hue) : base(0x99A8, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud30( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureSud31 : Peintures
	{
		[Constructable]
		public PeintureSud31() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud31(int hue) : base(0x99AA, hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud31( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureSud32 : Peintures
	{
		[Constructable]
		public PeintureSud32() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud32(int hue) : base(0x99AC, hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud32( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureSud33 : Peintures
	{
		[Constructable]
		public PeintureSud33() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud33(int hue) : base(0x99AE, hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud33( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureSud34 : Peintures
	{
		[Constructable]
		public PeintureSud34() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud34(int hue) : base(0x99B0, hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud34( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureSud35 : Peintures
	{
		[Constructable]
		public PeintureSud35() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud35(int hue) : base(0x99B2, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud35( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();
		}
	}
	public class PeintureSud36 : Peintures
	{
		[Constructable]
		public PeintureSud36() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud36(int hue) : base(0x99B4, hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud36( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureSud37 : Peintures
	{
		[Constructable]
		public PeintureSud37() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud37(int hue) : base(0x99B6, hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud37( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureSud38 : Peintures
	{
		[Constructable]
		public PeintureSud38() : this( 0 )
		{
		}

		[Constructable]
		public PeintureSud38(int hue) : base(0x99B8, hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureSud38( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}

	public class PeintureSud39 : Peintures
	{
		[Constructable]
		public PeintureSud39() : this(0)
		{
		}

		[Constructable]
		public PeintureSud39(int hue) : base(0x99BA, hue)
		{
			Weight = 1.0;

			Name = "Peinture";
		}

		public PeintureSud39(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

		}
	}

	public class PeintureSud40 : Peintures
	{
		[Constructable]
		public PeintureSud40() : this(0)
		{
		}

		[Constructable]
		public PeintureSud40(int hue) : base(0x99BC, hue)
		{
			Weight = 1.0;

			Name = "Peinture";
		}

		public PeintureSud40(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

		}
	}

	public class PeintureSud41 : Peintures
	{
		[Constructable]
		public PeintureSud41() : this(0)
		{
		}

		[Constructable]
		public PeintureSud41(int hue) : base(0x99BE, hue)
		{
			Weight = 1.0;

			Name = "Peinture";
		}

		public PeintureSud41(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

		}
	}

	public class PeintureSud42 : Peintures
	{
		[Constructable]
		public PeintureSud42() : this(0)
		{
		}

		[Constructable]
		public PeintureSud42(int hue) : base(0x99C0, hue)
		{
			Weight = 1.0;

			Name = "Peinture";
		}

		public PeintureSud42(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

		}
	}
	public class PeintureSud43 : Peintures
	{
		[Constructable]
		public PeintureSud43() : this(0)
		{
		}

		[Constructable]
		public PeintureSud43(int hue) : base(0x99C2, hue)
		{
			Weight = 1.0;

			Name = "Peinture";
		}

		public PeintureSud43(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

		}
	}
	public class PeintureSud44 : Peintures
	{
		[Constructable]
		public PeintureSud44() : this(0)
		{
		}

		[Constructable]
		public PeintureSud44(int hue) : base(0x99C4, hue)
		{
			Weight = 1.0;

			Name = "Peinture";
		}

		public PeintureSud44(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

		}
	}
	public class PeintureSud45 : Peintures
	{
		[Constructable]
		public PeintureSud45() : this(0)
		{
		}

		[Constructable]
		public PeintureSud45(int hue) : base(0x99C6, hue)
		{
			Weight = 1.0;

			Name = "Peinture";
		}

		public PeintureSud45(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

		}
	}
	public class PeintureEst27 : Peintures
	{
		[Constructable]
		public PeintureEst27() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst27(int hue) : base(0x9D5E, hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst27( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureEst28 : Peintures
	{
		[Constructable]
		public PeintureEst28() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst28(int hue) : base(0x2D74, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst28( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureEst29 : Peintures
	{
		[Constructable]
		public PeintureEst29() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst29(int hue) : base(0x9D3D, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst29( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureEst30 : Peintures
	{
		[Constructable]
		public PeintureEst30() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst30(int hue) : base(0x99A7, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst30( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureEst31 : Peintures
	{
		[Constructable]
		public PeintureEst31() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst31(int hue) : base(0x99A9, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst31( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureEst32 : Peintures
	{
		[Constructable]
		public PeintureEst32() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst32(int hue) : base(0x99AB, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst32( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureEst33 : Peintures
	{
		[Constructable]
		public PeintureEst33() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst33(int hue) : base(0x99AD, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst33( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();


		}
	}
	public class PeintureEst34 : Peintures
	{
		[Constructable]
		public PeintureEst34() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst34(int hue) : base(0x99AF, hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst34( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureEst35 : Peintures
	{
		[Constructable]
		public PeintureEst35() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst35(int hue) : base(0x99B1, hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst35( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureEst36 : Peintures
	{
		[Constructable]
		public PeintureEst36() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst36(int hue) : base(0x99B3, hue)
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst36( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureEst37 : Peintures
	{
		[Constructable]
		public PeintureEst37() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst37(int hue) : base(0x99B5, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst37( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}
	public class PeintureEst38 : Peintures
	{
		[Constructable]
		public PeintureEst38() : this( 0 )
		{
		}

		[Constructable]
		public PeintureEst38(int hue) : base(0x99B7, hue )
		{
			Weight = 1.0;
			
            Name = "Peinture";
		}

		public PeintureEst38( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

            int version = reader.ReadInt();

		}
	}

	public class PeintureEst39 : Peintures
	{
		[Constructable]
		public PeintureEst39() : this(0)
		{
		}

		[Constructable]
		public PeintureEst39(int hue) : base(0x99BB, hue)
		{
			Weight = 1.0;

			Name = "Peinture";
		}

		public PeintureEst39(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

		}
	}

	public class PeintureEst40 : Peintures
	{
		[Constructable]
		public PeintureEst40() : this(0)
		{
		}

		[Constructable]
		public PeintureEst40(int hue) : base(0x99BD, hue)
		{
			Weight = 1.0;

			Name = "Peinture";
		}

		public PeintureEst40(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

		}
	}

	public class PeintureEst41 : Peintures
	{
		[Constructable]
		public PeintureEst41() : this(0)
		{
		}

		[Constructable]
		public PeintureEst41(int hue) : base(0x99BF, hue)
		{
			Weight = 1.0;

			Name = "Peinture";
		}

		public PeintureEst41(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

		}
	}

	public class PeintureEst42 : Peintures
	{
		[Constructable]
		public PeintureEst42() : this(0)
		{
		}

		[Constructable]
		public PeintureEst42(int hue) : base(0x99C1, hue)
		{
			Weight = 1.0;

			Name = "Peinture";
		}

		public PeintureEst42(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

		}
	}

	public class PeintureEst43 : Peintures
	{
		[Constructable]
		public PeintureEst43() : this(0)
		{
		}

		[Constructable]
		public PeintureEst43(int hue) : base(0x99C3, hue)
		{
			Weight = 1.0;

			Name = "Peinture";
		}

		public PeintureEst43(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

		}
	}

	public class PeintureEst44 : Peintures
	{
		[Constructable]
		public PeintureEst44() : this(0)
		{
		}

		[Constructable]
		public PeintureEst44(int hue) : base(0x99C5, hue)
		{
			Weight = 1.0;

			Name = "Peinture";
		}

		public PeintureEst44(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

		}
	}

	
	[Flipable(0xA565, 0xA566)]
	public class ToileKershe : Peintures
	{
		[Constructable]
		public ToileKershe() : this(0)
		{
		}

		[Constructable]
		public ToileKershe(int hue) : base(0xA565, hue)
		{
			Weight = 1.0;

			Name = "Peinture";
		}

		public ToileKershe(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

		}
	}
	[Flipable(0xA567, 0xA568)]
	public class ToileChameau : Peintures
	{
		[Constructable]
		public ToileChameau() : this(0)
		{
		}

		[Constructable]
		public ToileChameau(int hue) : base(0xA567, hue)
		{
			Weight = 1.0;

			Name = "Peinture";
		}

		public ToileChameau(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

		}
	}
}
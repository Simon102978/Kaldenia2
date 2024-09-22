using Server.Mobiles;
using System.Collections.Generic;
using System.IO;
using System;

namespace Server.Custom
{
    public static class CustomPersistence
    {
        public static string FilePath = Path.Combine("Saves/", "CustomPersistence.bin");

        public static DateTime Ouverture { get; set; }
		public static int TaxesMoney { get; set; }
		public static int Salaire { get; set; }

		public static int SlaveSell { get; set; }

		public static DateTime ProchainePay { get; set; }

		public static Dictionary<string, double> SellItems = new Dictionary<string, double>();

		public static int Location { get; set; }
		public static int LocationJardin { get; set; }




		public static bool m_PirateJailActive;

		public static List<Point3D> m_PirateLanding = new List<Point3D>();


		[CommandProperty(AccessLevel.GameMaster)]
    	public static bool PirateJailActive
        {
            get
            {
				if (m_PirateLanding.Count == 0)
				{
					return false;
				}


                return m_PirateJailActive;
            }
            set
            {
					if (m_PirateLanding.Count > 0 )
					{
						 m_PirateJailActive = value;     
					}
					else
					{
						m_PirateJailActive = false;
					}
					 
            }
        }


      [CommandProperty(AccessLevel.GameMaster)]
    	public static List<Point3D> PirateLanding
        {
            get
            {
                return m_PirateLanding;
            }
            set
            {
                m_PirateLanding = value;
            }
        }



 	public static void PirateJail(CustomPlayerMobile from)
    {
      
      if (!PirateJailActive)
      {   
        return;
      }
      else if(PirateLanding.Count == 0)
      {
       
        return;
      } 

      Point3D loc = GetLocation();

 
      from.MoveToWorld(loc, from.Map);	

	  if (from.BaseName == "Vesper")
	  {
			  World.Broadcast(37, false, AccessLevel.Counselor, $"Oui, les pirates ont encore capturé Vesper... Elle se trouve à {loc.X},{loc.Y},{loc.Z}");
	  }
	  else
	  {
			  World.Broadcast(37, false, AccessLevel.Counselor, $"Les pirates ont capturé {from.BaseName}, {(from.Female ? "Elle" : "Il") } se trouve à {loc.X},{loc.Y},{loc.Z}");
	  }
	  







    }

	public static Point3D GetLocation()
    {

      for (int i = 0; i < 25; i++)
      {
        Point3D p = m_PirateLanding[Utility.Random(m_PirateLanding.Count)];

        if (Map.Felucca.CanSpawnMobile(p))
        {
          return p;
        }       
      }
        return m_PirateLanding[Utility.Random(m_PirateLanding.Count)];
    }




    public static bool AddPirateJailLocation(Point3D location)
    {
      if (m_PirateLanding.Contains(location))
      {
        return false;
      }
      else
      {
        m_PirateLanding.Add(location);
        return true;
      }
    }

    public static void DeletePirateJailLocation(int location)
    {
      int n = 0;

      List<Point3D> newLanding = new List<Point3D>();

      foreach (Point3D item in m_PirateLanding)
      {
        if (location != n)
        {
          newLanding.Add(item);
        }
        n++;
      }
      m_PirateLanding = newLanding;
      
    }


		public static void AddSellItem(string items, double value)
		{
			if (SellItems.ContainsKey(items))
			{
				SellItems[items] += value;
			}
			else
			{
				SellItems.Add(items, value);
			}


		}


		public static void SellingLog(CustomPlayerMobile player,bool contrebandier, string item, int amount, int pricebyitem)
		{
		

			if (player != null && player.Account != null)
			{
				string path = "Logs/SellLog/";
				string fileName = path + "SellItem.csv";

				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);

					using (StreamWriter sw = new StreamWriter(fileName, true))
						sw.WriteLine("Date;Nom;Account;Contrebandier;Item;Prix;Qte;Total");  // CSV fIle type..
				}
					

				using (StreamWriter sw = new StreamWriter(fileName, true))
					sw.WriteLine(DateTime.Now.ToString() + ";" + player.GetBaseName() + ";" + player.Account.Username + ";" + contrebandier.ToString() + ";" + item + ";" + pricebyitem.ToString() + ";" + amount.ToString() + ";" + (amount * pricebyitem).ToString());  // CSV fIle type..
			}
		}






		public static void Configure()
        {
            EventSink.WorldSave += OnSave;
            EventSink.WorldLoad += OnLoad;

			Ouverture = DateTime.Now;
			ProchainePay = Ouverture.AddDays(7);
			TaxesMoney = 0;
			Salaire = 0;

		}

        public static void OnSave(WorldSaveEventArgs e)
        {
            Persistence.Serialize(
                FilePath,
                writer =>
                {
                    writer.Write(7);

					writer.Write(m_PirateJailActive);

					writer.Write(m_PirateLanding.Count);

					foreach (Point3D item in m_PirateLanding)
					{
						writer.Write(item);
					}

					writer.Write(LocationJardin);

					writer.Write(SlaveSell);

					writer.Write(Location);

					writer.Write(SellItems.Count);

					foreach (KeyValuePair<string, double> item in SellItems)
					{
						writer.Write(item.Key);
						writer.Write(item.Value);
					}

					writer.Write(ProchainePay);
					writer.Write(Salaire);
					writer.Write(TaxesMoney);

					writer.Write(Ouverture);
                });
        }

        public static void OnLoad()
        {
            Persistence.Deserialize(
                FilePath,
                reader =>
                {
                    int version = reader.ReadInt();

					switch (version)
					{
						case 7:
						{
							m_PirateJailActive = reader.ReadBool();
							int n = reader.ReadInt();

							for (int i = 0; i < n; i++)
							{
								m_PirateLanding.Add(reader.ReadPoint3D());
							}
							goto case 6;
						}
						case 6:
						{
							LocationJardin = reader.ReadInt();
							goto case 5;
						}
						case 5:
							{
								SlaveSell = reader.ReadInt();
								goto case 4;
							}
						case 4:
							{
								Location = reader.ReadInt();
								goto case 3;
							}
						case 3:
							{
								int count = reader.ReadInt();

								for (int i = 0; i < count; i++)
								{
									SellItems.Add(reader.ReadString(), reader.ReadDouble());
								}
								goto case 2;
							}

						case 2:
							{
								ProchainePay = reader.ReadDateTime();
								goto case 1;
							}
						case 1:
							{
								Salaire = reader.ReadInt();
								TaxesMoney = reader.ReadInt();
								goto case 0;
							}
						case 0:
							{
								Ouverture = reader.ReadDateTime();
								break;
							}
					}


				});
        }
    }
}

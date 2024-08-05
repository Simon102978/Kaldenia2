using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;

public class TamableCreaturesGump : Gump
{
	private const int EntriesPerPage = 20;
	private int m_Page;

	public TamableCreaturesGump(int page = 0) : base(50, 50)
	{
		m_Page = page;

		AddPage(0);
		AddBackground(0, 0, 600, 450, 9270);
		AddAlphaRegion(10, 10, 580, 430);

		AddHtml(10, 10, 580, 20, "<CENTER><BASEFONT COLOR=#FFFFFF>Liste des Créatures Apprivoisables</CENTER>", false, false);

		AddButton(550, 10, 4014, 4016, 0, GumpButtonType.Page, m_Page + 1);
		AddButton(525, 10, 4014, 4016, 0, GumpButtonType.Page, m_Page - 1);

		var tamableCreatures = GetTamableCreatures();
		var pageEntries = tamableCreatures.Skip(m_Page * EntriesPerPage).Take(EntriesPerPage).ToList();

		AddHtml(10, 40, 200, 20, "<BASEFONT COLOR=#FFFFFF>Nom", false, false);
		AddHtml(210, 40, 100, 20, "<BASEFONT COLOR=#FFFFFF>Min Taming", false, false);
		AddHtml(310, 40, 100, 20, "<BASEFONT COLOR=#FFFFFF>Followers", false, false);

		for (int i = 0; i < pageEntries.Count; i++)
		{
			var entry = pageEntries[i];
			int y = 60 + (i * 20);

			AddHtml(10, y, 200, 20, $"<BASEFONT COLOR=#FFFFFF>{entry.Name}", false, false);
			AddHtml(210, y, 100, 20, $"<BASEFONT COLOR=#FFFFFF>{entry.MinTaming}", false, false);
			AddHtml(310, y, 100, 20, $"<BASEFONT COLOR=#FFFFFF>{entry.ControlSlots}", false, false);
		}
	}

	  private List<TamableCreatureInfo> GetTamableCreatures()
	{
		var creatures = new List<TamableCreatureInfo>();

		// Obtenez tous les types dans l'assembly actuel
		var types = Assembly.GetExecutingAssembly().GetTypes();

		foreach (var type in types)
		{
			// Vérifiez si le type est une sous-classe non abstraite de BaseCreature
			if (type.IsSubclassOf(typeof(BaseCreature)) && !type.IsAbstract)
			{
				try
				{
					var instance = Activator.CreateInstance(type) as BaseCreature;

					if (instance != null && instance.Tamable)
					{
						creatures.Add(new TamableCreatureInfo
						{
							Name = instance.Name,
							MinTaming = instance.MinTameSkill,
							ControlSlots = instance.ControlSlots
						});
					}

					instance?.Delete();
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Erreur lors de la création de l'instance de {type.Name}: {ex.Message}");
				}
			}
		}

		return creatures.OrderBy(c => c.MinTaming).ToList();
	}


	private class TamableCreatureInfo
	{
		public string Name { get; set; }
		public double MinTaming { get; set; }
		public int ControlSlots { get; set; }
	}
}
namespace Server.Scripts.Commands
{
	public class TamableCreaturesCommand
{
	public static void Initialize()
	{
		CommandSystem.Register("TamableCreatures", AccessLevel.GameMaster, new CommandEventHandler(TamableCreatures_OnCommand));
	}

		[Usage("TamableCreatures")]
		[Description("Affiche un tableau des créatures apprivoisables.")]
		public static void TamableCreatures_OnCommand(CommandEventArgs e)
	{
		e.Mobile.SendGump(new TamableCreaturesGump());
	}
	}
}


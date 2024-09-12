using System;
using Server;
using Server.Commands;
using Server.Gumps;
using Server.Mobiles;
using Server.Items;
using Server.Targeting;
using Server.Network;

namespace Server.Scripts.Commands
{
    public class Marque
    {
        public static void Initialize()
        {
			CommandSystem.Register("Marque", AccessLevel.Player, new CommandEventHandler(Marque_OnCommand));
		}

		public static void Marque_OnCommand(CommandEventArgs e)
		{
            string name = e.ArgString.Trim();

            if (name.Length > 0)
                e.Mobile.Target = new MarqueTarget(name);
            else
                e.Mobile.SendMessage("La marque doit avoir au moins un caractère.");
        }

        private class MarqueTarget : Target
        {
            private string m_Name;
			private static readonly string[] ForbiddenWords = { "Épique", "Epique", "Légendaire", "Legendaire", "Exceptionnelle" };

			public MarqueTarget(string name)  : base(1, false, TargetFlags.None)
            {
				m_Name = CleanAndValidateName(name);
			}

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is Item item )
                {

						if (item is BaseJewel || item is BaseBracelet || item is BaseEarrings || item is BaseRing || item is BaseNecklace)
						{
							if (!item.IsChildOf(from.Backpack))
							{
								from.SendMessage("L'item doit être dans votre sac.");
								return;
							}
							else
							{
								item.Description = m_Name;
								from.SendMessage("Vous avez marqué le bijou avec succès.");
								return;
							}
						}

						if (item.Createur != from)
					{
						from.SendMessage("Vous devez avoir créer l'objet pour mettre votre marque.");
						return;
					}
					else if (!item.IsChildOf(from.Backpack))
					{
						from.SendMessage("L'item doit être dans votre sac.");
						return;
					}
					else
					{
						item.Description = m_Name;
					}					
				}
				else
                {
                    from.SendMessage("Vous devez choisir un Item.");
                }



				
            }
			private string CleanAndValidateName(string input)
			{
				// Supprime les balises BASEFONT
				string cleanedName = RemoveBaseFontTags(input);

				// Vérifie si le nom contient un mot interdit
				foreach (string word in ForbiddenWords)
				{
					if (cleanedName.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)
					{
						// Si un mot interdit est trouvé, retourne une chaîne vide ou un message d'erreur
						return "Nom non autorisé";
					}
				}

				// Si aucun mot interdit n'est trouvé, retourne le nom nettoyé
				return cleanedName;
			}

			private string RemoveBaseFontTags(string input)
			{
				// Supprime toutes les balises BASEFONT, quelle que soit la casse
				string pattern = @"</?BASEFONT[^>]*>";
				string cleanedName = System.Text.RegularExpressions.Regex.Replace(input, pattern, "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

				// Supprime les espaces en début et fin de chaîne
				return cleanedName.Trim();
			}
		}
	}
}



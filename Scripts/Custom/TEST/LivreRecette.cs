using System;
using System.Collections.Generic;
using Server;
using Server.Commands;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Custom
{
	public class CustomRecipe
	{
		public string Name { get; set; }
		public List<Item> Ingredients { get; set; }
		public int FinalItemID { get; set; }

		public CustomRecipe(string name, List<Item> ingredients, int finalItemID)
		{
			Name = name;
			Ingredients = ingredients;
			FinalItemID = finalItemID;
		}

		public CustomRecipe(GenericReader reader)
		{
			Deserialize(reader);
		}

		public void Serialize(GenericWriter writer)
		{
			writer.Write(Name);
			writer.Write(Ingredients.Count);
			foreach (Item ingredient in Ingredients)
			{
				writer.Write(ingredient);
			}
			writer.Write(FinalItemID);
		}

		private void Deserialize(GenericReader reader)
		{
			Name = reader.ReadString();
			int count = reader.ReadInt();
			Ingredients = new List<Item>();
			for (int i = 0; i < count; i++)
			{
				Item ingredient = reader.ReadItem();
				if (ingredient != null)
				{
					Ingredients.Add(ingredient);
				}
			}
			FinalItemID = reader.ReadInt();
		}
	}

	public class CustomRecipeBook : Item
	{
		private List<CustomRecipe> m_Recipes;

		[Constructable]
		public CustomRecipeBook() : base(0xFEF)
		{
			Name = "Livre de recettes personnalisées";
			m_Recipes = new List<CustomRecipe>();
		}

		public CustomRecipeBook(Serial serial) : base(serial)
		{
		}

		public List<CustomRecipe> Recipes { get { return m_Recipes; } }

		public void AddRecipe(CustomRecipe recipe)
		{
			if (!m_Recipes.Contains(recipe))
			{
				m_Recipes.Add(recipe);
			}
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (from is PlayerMobile player && IsChildOf(player.Backpack))
			{
				player.SendGump(new CustomRecipeBookGump(this, player));
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(0); // version

			writer.Write(m_Recipes.Count);
			foreach (CustomRecipe recipe in m_Recipes)
			{
				recipe.Serialize(writer);
			}
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			int count = reader.ReadInt();
			m_Recipes = new List<CustomRecipe>();
			for (int i = 0; i < count; i++)
			{
				CustomRecipe recipe = new CustomRecipe(reader);
				m_Recipes.Add(recipe);
			}
		}
	}

	public class CustomRecipeBookGump : Gump
	{
		private CustomRecipeBook m_Book;
		private PlayerMobile m_From;

		public CustomRecipeBookGump(CustomRecipeBook book, PlayerMobile from) : base(50, 50)
		{
			m_Book = book;
			m_From = from;

			AddPage(0);
			AddBackground(0, 0, 300, 400, 9380);
			AddLabel(100, 10, 0, "Livre de recettes");

			AddButton(20, 40, 4005, 4007, 1, GumpButtonType.Reply, 0);
			AddLabel(55, 40, 0, "Créer une nouvelle recette");

			int y = 80;
			for (int i = 0; i < book.Recipes.Count; i++)
			{
				AddButton(20, y, 2103, 2104, 100 + i, GumpButtonType.Reply, 0);
				AddLabel(55, y, 0, book.Recipes[i].Name);
				y += 30;
			}
		}

		public override void OnResponse(NetState sender, RelayInfo info)
		{
			if (info.ButtonID == 1)
			{
				m_From.SendGump(new CreateCustomRecipeGump(m_Book, m_From));
			}
			else if (info.ButtonID >= 100)
			{
				int index = info.ButtonID - 100;
				if (index < m_Book.Recipes.Count)
				{
					CustomRecipe recipe = m_Book.Recipes[index];
					m_From.SendGump(new ConfirmCraftGump(recipe, m_From, m_Book));
				}
			}
		}
	}

	public class CreateCustomRecipeGump : Gump
	{
		private CustomRecipeBook m_Book;
		private PlayerMobile m_From;
		private List<Item> m_Ingredients;
		private Item m_FinalItem;
		private string m_RecipeName;

		public CreateCustomRecipeGump(CustomRecipeBook book, PlayerMobile from) : base(50, 50)
		{
			m_Book = book;
			m_From = from;
			m_Ingredients = new List<Item>();
			m_RecipeName = "";

			AddPage(0);
			AddBackground(0, 0, 300, 300, 9380);
			AddLabel(100, 10, 0, "Créer une recette");

			AddLabel(20, 40, 0, "Nom de la recette:");
			AddBackground(20, 60, 260, 20, 9350);
			AddTextEntry(22, 60, 256, 20, 0, 1, m_RecipeName);

			AddButton(20, 90, 4005, 4007, 1, GumpButtonType.Reply, 0);
			AddLabel(55, 90, 0, $"Cibler les ingrédients ({m_Ingredients.Count}/5)");

			AddButton(20, 120, 4005, 4007, 2, GumpButtonType.Reply, 0);
			AddLabel(55, 120, 0, "Cibler l'item final");

			AddButton(100, 250, 4005, 4007, 3, GumpButtonType.Reply, 0);
			AddLabel(135, 250, 0, "Créer la recette");

			AddButton(20, 250, 4005, 4007, 0, GumpButtonType.Reply, 0);
			AddLabel(55, 250, 0, "Annuler");
		}

		public override void OnResponse(NetState sender, RelayInfo info)
		{
			m_RecipeName = info.GetTextEntry(1)?.Text.Trim() ?? "";

			switch (info.ButtonID)
			{
				case 0: // Annuler
					m_From.SendGump(new CustomRecipeBookGump(m_Book, m_From));
					break;
				case 1: // Cibler les ingrédients
					if (m_Ingredients.Count < 5)
					{
						m_From.Target = new InternalTarget(this, true);
					}
					else
					{
						m_From.SendMessage("Vous avez déjà sélectionné 5 ingrédients.");
						m_From.SendGump(this);
					}
					break;
				case 2: // Cibler l'item final
					m_From.Target = new InternalTarget(this, false);
					break;
				case 3: // Créer la recette
					if (!string.IsNullOrEmpty(m_RecipeName) && m_Ingredients.Count >= 2 && m_FinalItem != null)
					{
						CustomRecipe recipe = new CustomRecipe(m_RecipeName, m_Ingredients, m_FinalItem.ItemID);
						m_Book.AddRecipe(recipe);
						m_From.SendMessage("Recette créée avec succès !");
						m_From.SendGump(new CustomRecipeBookGump(m_Book, m_From));
					}
					else
					{
						m_From.SendMessage("Veuillez remplir tous les champs et sélectionner au moins 2 ingrédients et l'item final.");
						m_From.SendGump(this);
					}
					break;
				default:
					m_From.SendGump(this);
					break;
			}
		}

		private class InternalTarget : Target
		{
			private CreateCustomRecipeGump m_Gump;
			private bool m_TargetingIngredient;

			public InternalTarget(CreateCustomRecipeGump gump, bool targetingIngredient) : base(-1, false, TargetFlags.None)
			{
				m_Gump = gump;
				m_TargetingIngredient = targetingIngredient;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (targeted is Item item)
				{
					if (m_TargetingIngredient)
					{
						m_Gump.m_Ingredients.Add(item);
						from.SendMessage("Ingrédient ajouté : " + item.Name);
					}
					else
					{
						m_Gump.m_FinalItem = item;
						from.SendMessage("Item final sélectionné : " + item.Name);
					}
				}
				else
				{
					from.SendMessage("Veuillez cibler un objet.");
				}

				from.SendGump(m_Gump);
			}
		}
	}

	public class ConfirmCraftGump : Gump
	{
		private CustomRecipe m_Recipe;
		private PlayerMobile m_From;
		private CustomRecipeBook m_Book;

		public ConfirmCraftGump(CustomRecipe recipe, PlayerMobile from, CustomRecipeBook book) : base(50, 50)
		{
			m_Recipe = recipe;
			m_From = from;
			m_Book = book;

			AddPage(0);
			AddBackground(0, 0, 300, 250, 9380);
			AddLabel(100, 10, 0, "Confirmer le craft");

			AddLabel(20, 40, 0, "Recette : " + recipe.Name);
			AddLabel(20, 70, 0, "Ingrédients nécessaires :");

			int y = 90;
			foreach (Item ingredient in recipe.Ingredients)
			{
				AddLabel(40, y, 0, ingredient.Name);
				y += 20;
			}

			AddButton(40, 200, 4005, 4007, 1, GumpButtonType.Reply, 0);
			AddLabel(75, 200, 0, "Craft");

			AddButton(140, 200, 4005, 4007, 2, GumpButtonType.Reply, 0);
			AddLabel(175, 200, 0, "Copier");

			AddButton(240, 200, 4005, 4007, 0, GumpButtonType.Reply, 0);
			AddLabel(275, 200, 0, "Annuler");
		}

		public override void OnResponse(NetState sender, RelayInfo info)
		{
			switch (info.ButtonID)
			{
				case 1: // Craft
					if (HasIngredients())
					{
						ConsumeIngredients();
						CreateFinalItem();
						m_From.SendMessage("Vous avez créé " + m_Recipe.Name + " avec succès !");
					}
					else
					{
						m_From.SendMessage("Vous n'avez pas tous les ingrédients nécessaires.");
					}
					break;
				case 2: // Copier
					CreateRecipeScroll();
					break;
				case 0: // Annuler
					m_From.SendGump(new CustomRecipeBookGump(m_Book, m_From));
					break;
			}
		}

		private bool HasIngredients()
		{
			foreach (Item ingredient in m_Recipe.Ingredients)
			{
				if (!m_From.Backpack.ConsumeTotal(ingredient.GetType(), 1, false))
				{
					return false;
				}
			}
			return true;
		}

		private void ConsumeIngredients()
		{
			foreach (Item ingredient in m_Recipe.Ingredients)
			{
				m_From.Backpack.ConsumeTotal(ingredient.GetType(), 1);
			}
		}

		private void CreateFinalItem()
		{
			CustomFoodItem finalItem = new CustomFoodItem(m_Recipe.FinalItemID, m_Recipe.Name);
			m_From.AddToBackpack(finalItem);
		}

		private void CreateRecipeScroll()
		{
			RecipeScroll scroll = new RecipeScroll(m_Recipe);
			m_From.AddToBackpack(scroll);
			m_From.SendMessage("Vous avez créé un parchemin de recette pour " + m_Recipe.Name);
		}
	}

	public class CustomFoodItem : Item
	{
		[Constructable]
		public CustomFoodItem(int itemID, string name) : base(itemID)
		{
			Name = name;
			Weight = 1.0;
			Stackable = false;
		}

		public CustomFoodItem(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (!IsChildOf(from.Backpack))
			{
				from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
				return;
			}

			from.PlaySound(0x3A);
			from.SendMessage("Vous consommez " + Name + ".");
			Delete();
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}

	public class RecipeScroll : Item
	{
		private CustomRecipe m_Recipe;

		[Constructable]
		public RecipeScroll(CustomRecipe recipe) : base(0xE34)
		{
			m_Recipe = recipe;
			Name = "Parchemin de recette: " + recipe.Name;
			Weight = 1.0;
		}

		public RecipeScroll(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (from is PlayerMobile player)
			{
				Item book = player.Backpack.FindItemByType(typeof(CustomRecipeBook));
				if (book != null && book is CustomRecipeBook recipeBook)
				{
					recipeBook.AddRecipe(m_Recipe);
					player.SendMessage("La recette a été ajoutée à votre livre de recettes.");
					Delete();
				}
				else
				{
					player.SendMessage("Vous avez besoin d'un livre de recettes dans votre sac pour utiliser ce parchemin.");
				}
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(0); // version
			m_Recipe.Serialize(writer);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			m_Recipe = new CustomRecipe(reader);
		}
	}
}


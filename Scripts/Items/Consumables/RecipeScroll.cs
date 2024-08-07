using Server.Engines.Craft;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class RecipeScroll : Item
	{
		private int m_RecipeID;

		public RecipeScroll(Recipe r)
			: this(r.ID)
		{
		}

		[Constructable]
		public RecipeScroll(int recipeID)
			: base(0x2831)
		{
			Name = "Recette Secrète";
			m_RecipeID = recipeID;
			SetHueBasedOnRecipeID();
		}

		public RecipeScroll(Serial serial)
			: base(serial)
		{
		}

		private void SetHueBasedOnRecipeID()
		{
			if (m_RecipeID >= 10000 && m_RecipeID < 20000)
				Hue = 1173; // Couleur pour 10000-19999
			else if (m_RecipeID >= 20000 && m_RecipeID < 30000)
				Hue = 1174; // Couleur pour 20000-29999
			else if (m_RecipeID >= 30000 && m_RecipeID < 40000)
				Hue = 1175; // Couleur pour 30000-39999
			else if (m_RecipeID >= 40000 && m_RecipeID < 50000)
				Hue = 1176; // Couleur pour 40000-49999
			else if (m_RecipeID >= 50000 && m_RecipeID < 60000)
				Hue = 1177; // Couleur pour 50000-59999
			else if (m_RecipeID >= 60000 && m_RecipeID < 70000)
				Hue = 1178; // Couleur pour 60000-69999
			else if (m_RecipeID >= 70000 && m_RecipeID < 80000)
				Hue = 1179; // Couleur pour 70000-79999
			else if (m_RecipeID >= 80000 && m_RecipeID < 90000)
				Hue = 1180; // Couleur pour 80000-89999
			else if (m_RecipeID >= 90000 && m_RecipeID < 100000)
				Hue = 1181; // Couleur pour 90000-99999
			else
				Hue = 1182; // Couleur par défaut pour les autres ID
		}

		public override int LabelNumber => 1074560;// recipe scroll

		[CommandProperty(AccessLevel.GameMaster)]
		public int RecipeID
		{
			get
			{
				return m_RecipeID;
			}
			set
			{
				m_RecipeID = value;
				SetHueBasedOnRecipeID();
				InvalidateProperties();
			}
		}
		public Recipe Recipe
        {
            get
            {
                if (Recipe.Recipes.ContainsKey(m_RecipeID))
                    return Recipe.Recipes[m_RecipeID];

                return null;
            }
        }
        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            Recipe r = Recipe;

            if (r != null)
                list.Add(1049644, r.TextDefinition.ToString()); // [~1_stuff~]
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.InRange(GetWorldLocation(), 2))
            {
                from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1019045); // I can't reach that.
                return;
            }

            Recipe r = Recipe;

            if (r != null && from is PlayerMobile)
            {
                PlayerMobile pm = from as PlayerMobile;

                if (!pm.HasRecipe(r))
                {
                    bool allRequiredSkills = true;
                    double chance = r.CraftItem.GetSuccessChance(from, null, r.CraftSystem, false, ref allRequiredSkills);

                    if (allRequiredSkills && chance >= 0.0)
                    {
                        pm.SendMessage("Vous venez d'apprendre la recette : " +  r.TextDefinition.ToString()); // You have learned a new recipe: ~1_RECIPE~
                        pm.AcquireRecipe(r);
                        Delete();
                    }
                    else
                    {
                        pm.SendLocalizedMessage(1044153); // You don't have the required skills to attempt this item.
                    }
                }
                else
                {
                    pm.SendLocalizedMessage(1073427); // You already know this recipe.
                }
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version

            writer.Write(m_RecipeID);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_RecipeID = reader.ReadInt();

                        break;
                    }
            }
        }
    }

	/*   public class DoomRecipeScroll : RecipeScroll
	   {
		   [Constructable]
		   public DoomRecipeScroll()
			   : base(Utility.RandomList(355, 356, 456, 585))
		   {
		   }

		   public DoomRecipeScroll(Serial serial)
			   : base(serial)
		   {
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

	   public class SmallElegantAquariumRecipeScroll : RecipeScroll
	   {
		   [Constructable]
		   public SmallElegantAquariumRecipeScroll()
			   : base(153)
		   {
		   }

		   public SmallElegantAquariumRecipeScroll(Serial serial)
			   : base(serial)
		   {
		   }

		   public override void Serialize(GenericWriter writer)
		   {
			   base.Serialize(writer);
			   writer.Write(0); // version
		   }

		   public override void Deserialize(GenericReader reader)
		   {
			   base.Deserialize(reader);
			   reader.ReadInt();
		   }
	   }

	   public class WallMountedAquariumRecipeScroll : RecipeScroll
	   {
		   [Constructable]
		   public WallMountedAquariumRecipeScroll()
			   : base(154)
		   {
		   }

		   public WallMountedAquariumRecipeScroll(Serial serial)
			   : base(serial)
		   {
		   }

		   public override void Serialize(GenericWriter writer)
		   {
			   base.Serialize(writer);
			   writer.Write(0); // version
		   }

		   public override void Deserialize(GenericReader reader)
		   {
			   base.Deserialize(reader);
			   reader.ReadInt();
		   }
	   }

	   public class LargeElegantAquariumRecipeScroll : RecipeScroll
	   {
		   [Constructable]
		   public LargeElegantAquariumRecipeScroll()
			   : base(155)
		   {
		   }

		   public LargeElegantAquariumRecipeScroll(Serial serial)
			   : base(serial)
		   {
		   }

		   public override void Serialize(GenericWriter writer)
		   {
			   base.Serialize(writer);
			   writer.Write(0); // version
		   }

		   public override void Deserialize(GenericReader reader)
		   {
			   base.Deserialize(reader);
			   reader.ReadInt();
		   }*/

    }


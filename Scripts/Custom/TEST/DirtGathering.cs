using System;
using Server;
using Server.Commands;
using Server.Targeting;
using Server.Items;
using Server.Mobiles;
using System.Linq;

namespace Server.Engines.Harvest
{
	public class DirtHarvesting
	{
		private static Timer _harvestTimer;
		public static void Initialize()
		{
			CommandSystem.Register("RecolterTerre", AccessLevel.Player, new CommandEventHandler(OnCommand));
		}

		public static int[] DirtTiles = new int[]
		{
			0x071, 0x07C,
			0x165, 0x174,
			0x1DC, 0x1EF,
			0x306, 0x31F,
			0x08D, 0x0A7,
			0x2E5, 0x305,
			0x777, 0x791,
			0x98C, 0x9BF,
		};

		public static int[] GroundTiles = new int[]
		{
			0x003, 0x006,
			0x033, 0x03E,
			0x078, 0x08C,
			0x0AC, 0x0DB,
			0x108, 0x10B,
			0x14C, 0x174,
			0x1A4, 0x1A7,
			0x1B1, 0x1B2,
			0x26E, 0x281,
			0x292, 0x295,
			0x355, 0x37E,
			0x3CB, 0x3CE,
			0x547, 0x5A6,
			0x5E3, 0x618,
			0x66B, 0x66E,
			0x6A1, 0x6C2,
			0x6DE, 0x6E1,
			0x73F, 0x742,
		};

		[Usage("RecolterTerre")]
		[Description("Permet de récolter de la terre fertile avec une pelle.")]
		public static void OnCommand(CommandEventArgs e)
		{
			Mobile from = e.Mobile;

			if (!HasShovelEquipped(from))
			{
				from.SendMessage("Vous devez être équipé d'une pelle pour récolter de la terre.");
				return;
			}

			from.SendMessage("Où voulez-vous récolter de la terre ?");
			from.Target = new InternalTarget();
		}

		private static bool HasShovelEquipped(Mobile from)
		{
			Item handOne = from.FindItemOnLayer(Layer.OneHanded);
			Item handTwo = from.FindItemOnLayer(Layer.TwoHanded);

			return (handOne is Shovel) || (handTwo is Shovel);
		}

		public static bool ValidateDirt(Map map, int x, int y)
		{
			int tileID = map.Tiles.GetLandTile(x, y).ID & 0x3FFF;
			bool ground = false;
			for (int i = 0; !ground && i < DirtTiles.Length; i += 2)
				ground = (tileID >= DirtTiles[i] && tileID <= DirtTiles[i + 1]);
			return ground;
		}

		public static bool ValidateGround(Map map, int x, int y)
		{
			int tileID = map.Tiles.GetLandTile(x, y).ID & 0x3FFF;
			bool ground = false;
			for (int i = 0; !ground && i < GroundTiles.Length; i += 2)
				ground = (tileID >= GroundTiles[i] && tileID <= GroundTiles[i + 1]);
			return ground;
		}

		private class InternalTarget : Target
		{
			public InternalTarget() : base(2, true, TargetFlags.None)
			{
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (_harvestTimer != null && _harvestTimer.Running)
				{
					from.SendMessage("Vous devez attendre avant de pouvoir récolter à nouveau.");
					return;
				}

				if (!HasShovelEquipped(from))
				{
					from.SendMessage("Vous devez être équipé d'une pelle pour récolter de la terre.");
					return;
				}
				if (targeted is LandTarget landTarget)
				{
					if (ValidateDirt(from.Map, landTarget.X, landTarget.Y) || ValidateGround(from.Map, landTarget.X, landTarget.Y))
					{
						HarvestDirt(from);
					}
					else
					{
						from.SendMessage("Vous ne pouvez pas récolter de la terre ici.");
					}
				}
				else if (targeted is StaticTarget staticTarget)
				{
					int tileID = staticTarget.ItemID;
					if (DirtTiles.Contains(tileID) || GroundTiles.Contains(tileID))
					{
						HarvestDirt(from);
					}
					else
					{
						from.SendMessage("Vous ne pouvez pas récolter de la terre ici.");
					}
				}
				else
				{
					from.SendMessage("Vous devez cibler le sol.");
				}
			}

			private void HarvestDirt(Mobile from)
			{
				Shovel shovel = GetEquippedShovel(from);
				if (shovel == null)
				{
					from.SendMessage("Votre pelle s'est cassée.");
					return;
				}

				// Utiliser un nombre aléatoire au lieu de CheckSkill pour déterminer le succès
				if (from.Skills[SkillName.Botanique].Value >= Utility.Random(100))
				{
					// Déterminer la quantité de FertileDirt à récolter (entre 1 et 5)
					int amount = Utility.RandomMinMax(1, 5);

					for (int i = 0; i < amount; i++)
					{
						Item fertileDirt = new FertileDirt();
						from.AddToBackpack(fertileDirt);
					}

					from.SendMessage($"Vous avez récolté {amount} terre{(amount > 1 ? "s" : "")} fertile{(amount > 1 ? "s" : "")}.");
					from.PlaySound(0x125);
					from.Animate(32, 5, 1, true, false, 0);

					// Réduire l'utilisation de la pelle
					shovel.UsesRemaining--;
					if (shovel.UsesRemaining <= 0)
					{
						shovel.Delete();
						from.SendMessage("Votre pelle s'est cassée.");
					}
				}
				else
				{
					from.SendMessage("Vous n'avez pas réussi à récolter de la terre fertile.");
				}

				// Démarrer le timer
				_harvestTimer = Timer.DelayCall(TimeSpan.FromSeconds(5), () => _harvestTimer = null);
			}


			private Shovel GetEquippedShovel(Mobile from)
			{
				Item handOne = from.FindItemOnLayer(Layer.OneHanded);
				Item handTwo = from.FindItemOnLayer(Layer.TwoHanded);

				return (handOne as Shovel) ?? (handTwo as Shovel);
			}
		}
	}
}

using System;
using Server.Mobiles;
using Server.Spells;

namespace Server.Items
{
	public class ExtremeStrengthPotion : BaseStrengthPotion
	{
		[Constructable]
		public ExtremeStrengthPotion()
			: base(PotionEffect.ExtremeStrength)
		{
			Name = "Potion de force extrême";
		}

		public ExtremeStrengthPotion(Serial serial)
			: base(serial)
		{
		}

		public override int StrOffset => 200;
		public override TimeSpan Duration => TimeSpan.FromMinutes(5.0);

		private double _originalMagicResist;

		public override void Drink(Mobile from)
		{
			if (from.BeginAction(typeof(ExtremeStrengthPotion)))
			{
				base.Drink(from);

				if (from is PlayerMobile player)
				{
					ReduceResistances(player);
					ReduceMagicResist(player);
				}

				Timer.DelayCall(Duration, () =>
				{
					from.EndAction(typeof(ExtremeStrengthPotion));
				});
			}
			else
			{
				from.SendLocalizedMessage(502173); // You are already under a similar effect.
			}
		}

		private void ReduceResistances(PlayerMobile player)
		{
			ResistanceMod[] mods = new ResistanceMod[]
			{
				new ResistanceMod(ResistanceType.Physical, -40),
				new ResistanceMod(ResistanceType.Fire, -35),
				new ResistanceMod(ResistanceType.Cold, -35),
				new ResistanceMod(ResistanceType.Poison, -35),
				new ResistanceMod(ResistanceType.Energy, -35)
			};

			foreach (var mod in mods)
			{
				player.AddResistanceMod(mod);
			}

			Timer.DelayCall(Duration, () =>
			{
				foreach (var mod in mods)
				{
					player.RemoveResistanceMod(mod);
				}
			});
		}

		private void ReduceMagicResist(PlayerMobile player)
		{
			_originalMagicResist = player.Skills.MagicResist.Base;
			player.Skills.MagicResist.Base = 0;

			Timer.DelayCall(Duration, () =>
			{
				player.Skills.MagicResist.Base = _originalMagicResist;
			});
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
}

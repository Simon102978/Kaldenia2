using System;
using Server;
using Server.Mobiles;

namespace Custom.Jerbal.Jako
{
	public class JakoHitsAttribute : JakoBaseAttribute
	{
		public JakoHitsAttribute()
		{
		}

		public override uint Cap
		{
			get { return 550; }
		}

		public override double CapScale
		{
			get { return 1.25; }
		}

		public override uint GetStat(BaseCreature bc)
		{
			return (uint)bc.HitsMax;
		}

		protected override void SetStat(BaseCreature bc, uint toThis)
		{
			// Ensure we never decrease HitsMaxSeed
			if (toThis > bc.HitsMaxSeed)
			{
				bc.HitsMaxSeed = (int)toThis;

				// Trigger HitsMax recalculation in BaseCreature
				bc.Delta(MobileDelta.Hits);
			}
		}

		public override uint AttributesGiven
		{
			get { return 2; }
		}

		public override uint PointsTaken
		{
			get { return 1; }
		}

		public override string ToString()
		{
			return "Hits";
		}

		public override uint MaxBonus(BaseCreature bc)
		{
			uint baseMax = (uint)((bc.HitsMaxSeed - IncreasedBy) * CapScale);
			return Math.Min(baseMax, Cap);
		}

		public override bool SetBonus(BaseCreature bc, uint toThis)
		{
			if (toThis > MaxBonus(bc))
				return false;

			uint oldTraits = TraitsGiven;
			uint increase = toThis > GetStat(bc) ? toThis - GetStat(bc) : 0;
			TraitsGiven += (increase / AttributesGiven) * PointsTaken;
			bc.Traits -= TraitsGiven - oldTraits;
			SetStat(bc, toThis);
			return true;
		}
	}
}

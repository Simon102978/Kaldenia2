using Server.Engines.Craft;

namespace Server.Items
{
    [Alterable(typeof(DefBlacksmithy), typeof(GargishWarFork))]
    [Flipable(0x1405, 0x1404)]
    public class WarFork : BaseSpear
    {
        [Constructable]
        public WarFork()
            : base(0x1405)
        {
            Weight = 9.0;
        }

        public WarFork(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.BleedAttack;
        public override WeaponAbility SecondaryAbility => WeaponAbility.Disarm;
	
		public override int StrengthReq => 50;
		public override int MinDamage => 6;
		public override int MaxDamage => 8;
		public override float Speed => 2.75f;
		public override int DefMaxRange => 2;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 80;
		public override WeaponAnimation DefAnimation => WeaponAnimation.Pierce1H;
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
using Server.Engines.Craft;

namespace Server.Items
{
    [Alterable(typeof(DefBlacksmithy), typeof(DualPointedSpear))]
    [Flipable(0x26BF, 0x26C9)]
    public class DoubleBladedStaff : BaseSpear
    {
        [Constructable]
        public DoubleBladedStaff()
            : base(0x26BF)
        {
            Weight = 2.0;
        }

        public DoubleBladedStaff(Serial serial)
            : base(serial)
        {
        }

		public override WeaponAbility PrimaryAbility => WeaponAbility.WhirlwindAttack;
		public override WeaponAbility SecondaryAbility => WeaponAbility.ConcussionBlow;
		public override int StrengthReq => 50;
		public override int MinDamage => 13;
		public override int MaxDamage => 15;
		public override float Speed => 4.00f;

		public override int DefMaxRange => 2;

		public override int InitMinHits => 31;
		public override int InitMaxHits => 80;
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
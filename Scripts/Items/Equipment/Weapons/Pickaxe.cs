using Server.Engines.Craft;
using Server.Engines.Harvest;

namespace Server.Items
{
	[Flipable(0xE86, 0xE85)]
    public class Pickaxe : BaseAxe, IUsesRemaining, IHarvestTool
    {
        [Constructable]
        public Pickaxe() : base(0xE86)
        {
            Weight = 11.0;
            UsesRemaining = 50;
            ShowUsesRemaining = true;
			Name = "Pioche";
			Layer = Layer.TwoHanded;

		}
		public override void OnDoubleClick(Mobile from)
		{
				if (HarvestSystem == null || Deleted)
				return;

			if (IsChildOf(from.Backpack) || Parent == from)
			{

				bool pickaxe = this is Pickaxe;

				if (pickaxe && Parent != from)
				{
					from.SendMessage("Vous devez avoir l'outil en main pour l'utiliser."); // That must be in your pack for you to use it.
					return;
				}
			}


			Point3D loc = GetWorldLocation();

			if (!from.InLOS(loc) || !from.InRange(loc, 2))
			{
				from.LocalOverheadMessage(Network.MessageType.Regular, 0x3E9, 1019045); // I can't reach that
				return;
			}
			else if (!IsAccessibleTo(from))
			{
				PublicOverheadMessage(Network.MessageType.Regular, 0x3E9, 1061637); // You are not allowed to access 
				return;
			}

			if (!(HarvestSystem is Mining || HarvestSystem is CustomMining))
				from.SendLocalizedMessage(1010018); // What do you want to use this item on?

			HarvestSystem.BeginHarvesting(from, this);
		}
		public Pickaxe(Serial serial) : base(serial)
        {
        }

		public override HarvestSystem HarvestSystem { get { return CustomMining.GetSystem(this); } }

		public override WeaponAbility PrimaryAbility => WeaponAbility.DoubleStrike;
        public override WeaponAbility SecondaryAbility => WeaponAbility.Disarm;
        public override int StrengthReq => 25;
        public override int MinDamage => 6;
        public override int MaxDamage => 10;
        public override float Speed => 3.00f;
        public override int InitMinHits => 31;
        public override int InitMaxHits => 60;
        public override WeaponAnimation DefAnimation => WeaponAnimation.Bash1H;
        public override SkillName DefSkill => SkillName.Macing;

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

namespace Server.Items
{
    public class SsinsChest : LeatherChest
    {
        public override bool IsArtifact => true;
        [Constructable]
        public SsinsChest()
            : base()
        {
			Name = "Plastron du roi Ssins";
			Hue = 2833;
			SetHue = 2833;

			Attributes.BonusStam = 2;
            Attributes.WeaponSpeed = 5;

            SetSkillBonuses.SetValues(0, SkillName.Hiding, 30);

            SetSelfRepair = 3;

            SetAttributes.BonusDex = 12;

            SetPhysicalBonus = 5;
            SetFireBonus = 4;
            SetColdBonus = 3;
            SetPoisonBonus = 4;
            SetEnergyBonus = 4;
        }

        public SsinsChest(Serial serial)
            : base(serial)
        {
        }

        public override void ResetArmor()
        {
         	Name = "Plastron du roi Ssins";
			Hue = 2833;
			SetHue = 2833;

			Attributes.BonusStam = 2;
            Attributes.WeaponSpeed = 5;

            SetSkillBonuses.SetValues(0, SkillName.Hiding, 30);

            SetSelfRepair = 3;

            SetAttributes.BonusDex = 12;

            SetPhysicalBonus = 5;
            SetFireBonus = 4;
            SetColdBonus = 3;
            SetPoisonBonus = 4;
            SetEnergyBonus = 4;
        }

     //   public override int LabelNumber => 1074304;// Assassin Armor
        public override SetItem SetID => SetItem.Assassin;
        public override int Pieces => 5;
        public override int BasePhysicalResistance => 3;
        public override int BaseFireResistance => 2;
        public override int BaseColdResistance => 3;
        public override int BasePoisonResistance => 4;
        public override int BaseEnergyResistance => 4;
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(1); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {   
                case 1:
                {
                    break;
                } 
                case 0:
                {
                    
                    if (Parent is Mobile m)
                    {
                        m.PlaceInBackpack(this);
                    }

                    Layer = Layer.InnerTorso;

                    break;
                }
               
               
            }
        }
    }
}
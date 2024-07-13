using System.Collections.Generic;
using Server.Custom.System;

namespace Server.Mobiles
{
    public class ClergeMage : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new List<SBInfo>();


    	private GuildRecruter m_guild;


        [CommandProperty(AccessLevel.GameMaster)]
		public  GuildRecruter NewGuild
		{
			get => m_guild;
			set
			{
				m_guild = value;
			}

		}






        [Constructable]
        public ClergeMage()
            : base("the mage")
        {
            SetSkill(SkillName.EvalInt, 65.0, 88.0);
            SetSkill(SkillName.Inscribe, 60.0, 83.0);
            SetSkill(SkillName.Magery, 64.0, 100.0);
            SetSkill(SkillName.Meditation, 60.0, 83.0);
            SetSkill(SkillName.MagicResist, 65.0, 88.0);
            SetSkill(SkillName.Wrestling, 36.0, 68.0);
        }

        public ClergeMage(Serial serial)
            : base(serial)
        {
        }

		public override StatutSocialEnum MinBuyClasse => StatutSocialEnum.Equite;

		public override NpcGuild NpcGuild => NpcGuild.MagesGuild;
        public override VendorShoeType ShoeType => Utility.RandomBool() ? VendorShoeType.Shoes : VendorShoeType.Sandals;
        protected override List<SBInfo> SBInfos => m_SBInfos;
        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBMage());
        }

        public override bool CanBuyPlayer(CustomPlayerMobile cm)
        {
                if (NewGuild != null && NewGuild.VerifyMemberShip(cm))
                {
                    CustomGuildMember cgm = NewGuild.GetMobileInfo(cm);

                    if (cgm.CustomRank > 0)
                    {
                        return true;
                    }      
                }

                return base.CanBuyPlayer(cm);

        }

        public override void InitOutfit()
        {
            base.InitOutfit();

            AddItem(new Items.Robe(Utility.RandomBlueHue()));
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
            writer.Write(NewGuild);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {   
                case 0:
                {
                    NewGuild = (GuildRecruter)reader.ReadMobile();
                    break;
                }
                
            }
        }
    }
}
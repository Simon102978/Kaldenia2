using Server.Network;

namespace Server.Mobiles
{
    [CorpseName("Cheveux")]
    public class CheveuxRoux : BaseCreature
    {
        [Constructable]
        public CheveuxRoux()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Amas de cheveux roux";
            Body = 51;
            BaseSoundID = 456;

            Hue = RouxCouleur();

            SetStr(22, 34);
            SetDex(16, 21);
            SetInt(16, 20);

            SetHits(15, 19);

            SetMana(1000);

            SetDamage(1, 5);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 5, 10);
            SetResistance(ResistanceType.Poison, 10, 20);

	        SetSkill(SkillName.MagicResist, 125, 140);
			SetSkill(SkillName.Tactics, 50, 120);
			SetSkill(SkillName.Wrestling, 80, 130);
			SetSkill(SkillName.Magery, 70, 100);
			SetSkill(SkillName.EvalInt, 70, 100);

            Fame = 300;
            Karma = -300;
        }

        public CheveuxRoux(Serial serial)
            : base(serial)
        {
        }
		public override TribeType Tribe => TribeType.Titusien;
		public override Poison PoisonImmune => Poison.Lethal;
        public override Poison HitPoison => Poison.Lethal;
        public override FoodType FavoriteFood => FoodType.Meat | FoodType.Fish | FoodType.FruitsAndVegies | FoodType.GrainsAndHay | FoodType.Eggs;

		public int RouxCouleur()
		{
			var roux = 1602;

			switch (Utility.Random(3))
			{
				case 0:
					Utility.Random(1602, 52);
					break;
				case 1:
					Utility.Random(1502, 31);
					break;
				case 2:
					Utility.Random(1202, 23);
					break;

				default:
					break;
			}

			return roux;
		}

        public override void OnKill(Mobile m)
        {
            if (m is CustomPlayerMobile cp && !cp.CheckRoux())
            {
                Roucifier(m);
                
            }


        }
        public virtual void Roucifier(Mobile m)
		{
			if (m is CustomPlayerMobile cp && !cp.CheckRoux())
			{
				int couleur = RouxCouleur();
			    cp.HairHue = couleur;

		        if (!cp.Female)
				{
				  cp.FacialHairHue = couleur;
				}
						 
				PublicOverheadMessage(MessageType.Emote, 0x3B2,false, $"*Les cheveux de {cp.Name} devient roux.*"); 

			}

		}




        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(1);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}

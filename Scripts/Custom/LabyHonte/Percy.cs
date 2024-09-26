using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un gorille")]
    public class Percy : BaseCreature
    {

		public DateTime m_LastParole = DateTime.MinValue;

		public static List<string> ParoleProcheListe = new List<string>()
		{
			"*Percy à la patte baladeuse.*",
			"*Tente de le prendre dans une etreinte*",
			"*Se frotte auprès de sa victime.*",
		};

	    public static List<string> ParoleLoinListe = new List<string>()
		{
			"*Percy secoue son membre dans toute les directions*",
			"*Tente de prendre une posture imposante*",
			"*Emet un crie louche.*",
        	"*Passe sa patte sur son corps.*",
		};


		private WallControlerStone m_Stone;

        [CommandProperty(AccessLevel.GameMaster)]
        public WallControlerStone Stone
        {
            get
            {
                return m_Stone;
            }
            set
            {
                m_Stone = value;
                
               
            }
        }








        [Constructable]
        public Percy()
            : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "Percy";
            Title = "Gorille en ruth";
            Body = 0x1D;
            BaseSoundID = 0x9E;
            Hue = 2155;

            SetStr(33, 55);
            SetDex(36, 55);
            SetInt(36, 60);

            SetHits(50, 60);
            SetMana(0);

            SetDamage(1, 2);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 10, 20);
            SetResistance(ResistanceType.Fire, 5, 10);
            SetResistance(ResistanceType.Cold, 10, 15);

            SetSkill(SkillName.MagicResist, 45.1, 60.0);
            SetSkill(SkillName.Tactics, 43.3, 58.0);
            SetSkill(SkillName.Wrestling, 23.3, 30.0);

            Fame = 450;
            Karma = 0;


        }

        protected override void OnCreate()
        {
           
            base.OnCreate();
             CheckWallControler();

        }

        public void CheckWallControler()
        {

           IPooledEnumerable eable = Map.GetItemsInRange(this.Location, 10);

            foreach (Item item in eable)
            {
                if (item is WallControlerStone wc)
                {
                    m_Stone = wc;
                    break;
                }
            }
            eable.Free();

            if (m_Stone != null)
            {
                m_Stone.MobActif = true;
            }


        }

        public override void OnDeath(Container c)
		{
            if (m_Stone != null)
            {
                m_Stone.MobActif = false;
            }

			base.OnDeath(c);
		

		}

        public override void OnThink()
		{

			base.OnThink();

			Parole();
		
		}

        public override void AlterMeleeDamageTo(Mobile to, ref int damage)
		{
			Parole();
			base.AlterMeleeDamageTo(to, ref damage);
		}
       	public override void Attack(IDamageable e)
		{
			Parole();
			base.Attack(e);
		}

       	public void Parole()
		{
			if (m_LastParole < DateTime.Now && Combatant != null)
			{	

                if (InRange(Combatant.Location,3))
                {
                    Say(ParoleProcheListe[Utility.Random(ParoleProcheListe.Count)]);
                }
                else
                {
                    Say(ParoleLoinListe[Utility.Random(ParoleLoinListe.Count)]);
                }



				
			
				m_LastParole = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(60, 90));
			}
		}
       public override void OnDamage(int amount, Mobile from, bool willKill)
		{
           
			base.OnDamage(amount, from, willKill);
			Parole();
		}





		public override bool CanBeParagon => false;
       	public override bool CanReveal => false;
		public Percy(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => Utility.RandomMinMax(1 ,3);
        public override int Hides => Utility.RandomMinMax(3, 6);
		public override FoodType FavoriteFood => FoodType.FruitsAndVegies | FoodType.GrainsAndHay;
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
            writer.Write(m_Stone);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            m_Stone = (WallControlerStone)reader.ReadItem();
        }
    }
}

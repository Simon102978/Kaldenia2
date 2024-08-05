using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("le corps d'un aigle")]
    public class AigleCaucase : BaseCreature
    {
        public DateTime DelayHurlement;
		public DateTime DelayCoupVent;
		public DateTime DelayAttraction;
		public DateTime TuerSummoneur;
		private DateTime m_GlobalTimer;

        [Constructable]
        public AigleCaucase()
           : base(AIType.AI_Melee, FightMode.Closest, 10, 1, .2, .4)
        {
            Name = "Aigle du Caucase";
            Body = 5;
            BaseSoundID = 0x2EE;
            Hue = 2065;


            SetStr(73, 115);
            SetDex(76, 95);
            SetInt(16, 30);

            SetHits(100, 150);
            SetMana(0);

            SetDamage(7, 13);

            SetDamageType(ResistanceType.Physical, 60);
            SetDamageType(ResistanceType.Energy, 40);



            SetResistance(ResistanceType.Physical, 20, 25);
            SetResistance(ResistanceType.Fire, 10, 15);
            SetResistance(ResistanceType.Cold, 20, 25);
            SetResistance(ResistanceType.Poison, 5, 10);
            SetResistance(ResistanceType.Energy, 40, 50);


            SetSkill(SkillName.MagicResist, 30.1, 35.0);
            SetSkill(SkillName.Tactics, 60.3, 75.0);
            SetSkill(SkillName.Wrestling, 70.3, 80.0);

            Fame = 2000;
            Karma = -2000;

        }

        public override void OnThink()
		{



			base.OnThink();

			if (Combatant != null)
			{
				if (m_GlobalTimer < DateTime.UtcNow)
				{

					if (InLOS(Combatant.Location))
					{
						switch (Utility.Random(3))
						{
							case 0:
								Hurlement();
								break;
							case 1:
								CoupVent();
								break;
							case 2:
								Attraction();
								break;
							default:
								break;
						}
					}
					else
					{
						AntiSummon();
					}

					m_GlobalTimer = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(2, 5));
				}
			

			}
		}

        		public void Hurlement()
		{


			if (DelayHurlement < DateTime.UtcNow)
			{
					int Range = 10;
					List<Mobile> targets = new List<Mobile>();

					IPooledEnumerable eable = this.GetMobilesInRange(Range);

					foreach (Mobile m in eable)
					{
						if (this != m && !(m is Harpy) && !(m is StoneHarpy) && !m.IsStaff())
						{


							if (Core.AOS && !InLOS(m))
								continue;

							targets.Add(m);
						}
					}

					eable.Free();

					Effects.PlaySound(this, this.Map, 0x1FB);
					Effects.PlaySound(this, this.Map, 0x10B);
					Effects.SendLocationParticles(EffectItem.Create(this.Location, this.Map, EffectItem.DefaultDuration), 0x37CC, 1, 15, 97, 3, 9917, 0);

					Emote("*Lance un hurlement effroyable*");

					if (targets.Count > 0)
					{

			



						for (int i = 0; i < targets.Count; ++i)
						{
							Mobile m = targets[i];


							DoHarmful(m);

							m.Paralyze(TimeSpan.FromSeconds(10));

						}
					}

				DelayHurlement = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(10, 50));

			}
		}

		public void AntiSummon()
		{
			if (TuerSummoneur < DateTime.UtcNow)
			{
				if (Combatant is BaseCreature bc)
				{
					if (bc.Summoned)
					{
						if (bc.ControlMaster is CustomPlayerMobile cp)
						{
							Combatant = cp;

							Emote($"*se met à voler et plonge en piquer sur {cp.Name}*");

							cp.Damage(10);

							cp.Freeze(TimeSpan.FromSeconds(3));

							this.Location = cp.Location;

						}
					}
				}
				TuerSummoneur = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(10, 50));
			}
		}

		public void CoupVent()
		{

			if (DelayCoupVent < DateTime.UtcNow) 
			{
				int dmg = 35;

				Mobile target = Combatant as Mobile;

				Combatant.FixedParticles(0x374A, 10, 30, 5013, 1153, 2, EffectLayer.Waist);

				AOS.Damage(Combatant, this, dmg, 100, 0, 0, 0, 0); // C'est un coup de vent, donc rien d'electrique...

				Emote($"*Attire une bourasque provenant de {Combatant.Name}*");

				KnockBack(this.Location, target, -5);

				Combatant = target;



				DelayCoupVent = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(10, 60));
			}




		}

		public void Attraction()
		{
			if (DelayAttraction < DateTime.UtcNow)
			{
				int Range = 10;
				List<Mobile> targets = new List<Mobile>();

				IPooledEnumerable eable = this.GetMobilesInRange(Range);

				foreach (Mobile m in eable)
				{
					if (this != m && !m.IsStaff())
					{


						if (Core.AOS && !InLOS(m))
							continue;

						targets.Add(m);
					}
				}

				eable.Free();

				Effects.PlaySound(this, this.Map, 0x1FB);
				Effects.PlaySound(this, this.Map, 0x10B);
				Effects.SendLocationParticles(EffectItem.Create(this.Location, this.Map, EffectItem.DefaultDuration), 0x37CC, 1, 15, 97, 3, 9917, 0);

				Emote("*Aspire l'air autour d'elle*");

				if (targets.Count > 0)
				{

					int dmg = 15;

					Mobile target = Combatant as Mobile;


					for (int i = 0; i < targets.Count; ++i)
					{
						Mobile m = targets[i];


						DoHarmful(m);
						AOS.Damage(m, this, dmg, 100, 0, 0, 0, 0); // C'est un coup de vent, donc rien d'electrique...

						int Distance = 3;


						if (m.GetDistanceToSqrt(this.Location) < Distance)
						{
							Distance = (int)m.GetDistanceToSqrt(this.Location);
						}


						KnockBack(this.Location, m, Distance * -1); // Si sur le centre de la tornade...
					}

					Combatant = target;
				}


				DelayAttraction = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(10, 120));

			}

			
		}

		public void KnockBack(Point3D destination, Mobile target, int Distance)
		{



			if (target.Alive)
			{

				int Distance2 = Distance;
				int Neg = 1;

				if (Distance < 0)
				{
					Distance2 = -Distance;   // Sert a faire en sorte, que si c'est négative, ca va avancer.
					Neg = -1;
				}




				for (int i = 0; i < Distance2; i++)  // Le script valide toute les tiles jusqu'a la distance maximum. Si une d'entre elle est bloquer, il revient a la précédente (ou player location du départ) et stun la target.
				{
					Point3D point = KnockBackCalculation(destination, target, i * Neg);

					if (!target.Map.CanFit(point, 16, false, false) && i != Distance2)
					{
						target.Paralyze(TimeSpan.FromSeconds((Distance2 - i + 1)));
						break;
					}
					else
					{
						target.MoveToWorld(point, target.Map);
					}
				}
			}
		}
		public Point3D KnockBackCalculation(Point3D Loc, Mobile target, int Distance)
		{

			return KnockBackCalculation(Loc, new Point3D(target.Location), Distance);



		}

		public Point3D KnockBackCalculation(Point3D Loc, Point3D point, int Distance)
		{

			Direction d = Utility.GetDirection(point, Loc);

			switch (d)
			{
				case (Direction)0x0: case (Direction)0x80: point.Y += Distance; break; //North
				case (Direction)0x1: case (Direction)0x81: { point.X -= Distance; point.Y += Distance; break; } //Right
				case (Direction)0x2: case (Direction)0x82: point.X -= Distance; break; //East
				case (Direction)0x3: case (Direction)0x83: { point.X -= Distance; point.Y -= Distance; break; } //Down
				case (Direction)0x4: case (Direction)0x84: point.Y -= Distance; break; //South
				case (Direction)0x5: case (Direction)0x85: { point.X += Distance; point.Y -= Distance; break; } //Left
				case (Direction)0x6: case (Direction)0x86: point.X += Distance; break; //West
				case (Direction)0x7: case (Direction)0x87: { point.X += Distance; point.Y += Distance; break; } //Up
				default: { break; }
			}
			return point;
		}





        public AigleCaucase(Serial serial)
            : base(serial)
        {
        }

		public override bool CanBeParagon => false;

		public override int Meat => Utility.RandomMinMax(1, 3);
        public override MeatType MeatType => MeatType.Bird;
        public override int Feathers => Utility.RandomMinMax(10, 35);
        public override FoodType FavoriteFood => FoodType.Meat | FoodType.Fish;
        public override bool CanFly => true;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.LootItem<PlumesAigle>(3, 7));
            AddLoot(LootPack.Average);     
			AddLoot(LootPack.Others, Utility.RandomMinMax(0, 2));

		}

	


		public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}

using Server.Mobiles;
using System;

namespace Server.Items
{
    public abstract class BasePerfumePotion : BasePotion
    {

        public virtual Perfume Perfume { get { return new Perfume(); } }


        [Constructable]
        public BasePerfumePotion()
            : base(0xF06, PotionEffect.Perfume)
        {
            Hue = Perfume.Hue;
            Name = Perfume.Nom;
        }

        public BasePerfumePotion(Serial serial)
            : base(serial)
        {
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


        public override void Drink(Mobile from)
        {

            if (from is CustomPlayerMobile spm)
            {
                spm.AddPerfume(Perfume);

                from.FixedParticles(0x376A, 9, 32, 5007, EffectLayer.Waist);
                from.PlaySound(0x1E3);

                PlayDrinkEffect(from);
                Consume();
            }          
        }
    }

    public class PerfumGrisPotion : BasePerfumePotion
    {

        public override Perfume Perfume
        {
            get
            {
                return new Perfume("Sel de mer", 993, "#808080", TimeSpan.FromDays(7)); } }

            [Constructable]
            public PerfumGrisPotion()
                : base()
            {



            }

            public PerfumGrisPotion(Serial serial)
            : base(serial)
            {
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

    public class PerfumKakiPotion : BasePerfumePotion
    {

        public override Perfume Perfume
        {
            get
            {
                return new Perfume("Fromage vieillit", 552, "#608000", TimeSpan.FromDays(7)); }
            }

                [Constructable]
                public PerfumKakiPotion()
                    : base()
                {



                }

                public PerfumKakiPotion(Serial serial)
                    : base(serial)
                {
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

    public class PerfumBrunPotion : BasePerfumePotion
    {

        public override Perfume Perfume
        {
            get
            {
                return new Perfume("Vieux musk", 842, "#5c5c3d", TimeSpan.FromDays(7)); }
            }

                [Constructable]
                public PerfumBrunPotion()
                    : base()
                {



                }

                public PerfumBrunPotion(Serial serial)
                    : base(serial)
                {
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

    public class PerfumJaunePotion : BasePerfumePotion
    {

        public override Perfume Perfume
        {
            get
            {
                return new Perfume("Agrume en folie", 53, "#ffff00", TimeSpan.FromDays(7)); } }

                [Constructable]
                public PerfumJaunePotion()
                    : base()
                {



                }

                public PerfumJaunePotion(Serial serial)
                    : base(serial)
                {
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

    public class PerfumBleuClairPotion : BasePerfumePotion
    {

        public override Perfume Perfume
        {
            get
            {
                return new Perfume("Brise d’océan", 93, "#1a8cff", TimeSpan.FromDays(7)); }
            }

                [Constructable]
                public PerfumBleuClairPotion()
                    : base()
                {



                }

                public PerfumBleuClairPotion(Serial serial)
                    : base(serial)
                {
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

    public class PerfumMauvePotion : BasePerfumePotion
    {

        public override Perfume Perfume
        {
            get
            {
                return new Perfume("Souffle magique", 14, "#ac00e6", TimeSpan.FromDays(7)); }
            }

            [Constructable]
            public PerfumMauvePotion()
            : base()
            {
            }

            public PerfumMauvePotion(Serial serial)
            : base(serial)
            {
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

    public class PerfumRougevifPotion : BasePerfumePotion
    {

        public override Perfume Perfume
        {
        get
        {
            return new Perfume("Désir et passion", 38, "#ff0000", TimeSpan.FromDays(7)); } }

            [Constructable]
            public PerfumRougevifPotion()
                : base()
            {



            }

            public PerfumRougevifPotion(Serial serial)
                : base(serial)
            {
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

    public class PerfumRosePotion : BasePerfumePotion
    {

        public override Perfume Perfume
        {
            get
            {
                return new Perfume("Synergie féérique", 31, "#ff99c2", TimeSpan.FromDays(7)); }
            }

            [Constructable]
            public PerfumRosePotion()
            : base()
            {
            }

            public PerfumRosePotion(Serial serial)
                    : base(serial)
            {
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

    public class PerfumTurquoisePotion : BasePerfumePotion
    {

        public override Perfume Perfume
        {
            get
            {
                return new Perfume("Menthe polaire", 85, "#80ffd4", TimeSpan.FromDays(7)); } }

                [Constructable]
                public PerfumTurquoisePotion()
                    : base()
                {



                }

                public PerfumTurquoisePotion(Serial serial)
                    : base(serial)
                {
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

    public class PerfumOrangePotion : BasePerfumePotion
    {

        public override Perfume Perfume
        {
            get
            {
                return new Perfume("Effervescence fruitée", 44, "#ff8000", TimeSpan.FromDays(7)); } }

                [Constructable]
                public PerfumOrangePotion()
                    : base()
                {



                }

                public PerfumOrangePotion(Serial serial)
                    : base(serial)
                {
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

    public class PerfumMarinePotion : BasePerfumePotion
    {

        public override Perfume Perfume
        {
            get
            {
                return new Perfume("Frisson obscure", 302, "#003d99", TimeSpan.FromDays(7)); }
            }

            [Constructable]
            public PerfumMarinePotion()
                    : base()
            {
            }

        public PerfumMarinePotion(Serial serial)
        : base(serial)
        {
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

    public class PerfumVioletPotion : BasePerfumePotion
    {

        public override Perfume Perfume
        {
            get
            {
                return new Perfume("Escapade florale", 19, "#cc00cc", TimeSpan.FromDays(7)); } }

                [Constructable]
                public PerfumVioletPotion()
                    : base()
                {



                }

                public PerfumVioletPotion(Serial serial)
                    : base(serial)
                {
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

    public class PerfumVertpommePotion : BasePerfumePotion
    {

        public override Perfume Perfume
        {
            get
            {
                return new Perfume("Régénérescence", 58, "#99ff33", TimeSpan.FromDays(7)); }
            }

            [Constructable]
            public PerfumVertpommePotion()
                    : base()
            {
            }

            public PerfumVertpommePotion(Serial serial)
                    : base(serial)
            {
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

    public class PerfumRougevinPotion : BasePerfumePotion
    {

        public override Perfume Perfume
        {
            get
            {
                return new Perfume("Volupté charnelle", 337, "#990000", TimeSpan.FromDays(7)); }
            }

            [Constructable]
            public PerfumRougevinPotion()
               : base()
            {
            }
            public PerfumRougevinPotion(Serial serial)
                    : base(serial)
            {
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

    public class PerfumOcrePotion : BasePerfumePotion
    {

        public override Perfume Perfume
        {
            get
            {
                return new Perfume("Opulence", 49, "#ffd11a", TimeSpan.FromDays(7)); } }

                [Constructable]
                public PerfumOcrePotion()
                    : base()
                {



                }

                public PerfumOcrePotion(Serial serial)
                    : base(serial)
                {
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

    public class PerfumVertforêtPotion : BasePerfumePotion
    {

        public override Perfume Perfume
        {
            get
            {
                return new Perfume("Bois de santal", 567, "#009933", TimeSpan.FromDays(7)); } }

                [Constructable]
                public PerfumVertforêtPotion()
                    : base()
                {



                }

                public PerfumVertforêtPotion(Serial serial)
                    : base(serial)
                {
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

    public class PerfumNoirPotion : BasePerfumePotion
    {

        public override Perfume Perfume
        {
            get
            {
                return new Perfume("La fin des temps", 1, "#000000", TimeSpan.FromDays(7)); } }

                [Constructable]
                public PerfumNoirPotion()
                    : base()
                {



                }

                public PerfumNoirPotion(Serial serial)
                    : base(serial)
                {
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

using System;
using Server.Engines.Craft;
using Server.Mobiles;
using static Server.Mobiles.CustomPlayerMobile;

namespace Server.Items
{
    [FlipableAttribute(0xFBE, 0xFBD)]
	public abstract class BaseLivreOubli : Item
	{


       	public virtual bool Classe => true;
		 
  	    public virtual int Puissance => 0;

		
   
        public BaseLivreOubli() : base(0xFBE)
        {
            Name = "livre de l'oubli";
            Weight = 2.0;
        }


        public BaseLivreOubli(Serial serial) : base(serial)
        {
        }
        public override void OnDoubleClick(Mobile from)
        {
            if (from is CustomPlayerMobile pm) 
            {

                if (!IsChildOf(pm.Backpack))
                {
                    pm.SendMessage("Le livre doit être dans votre sac.");
                    return;
                }

                if (Classe)
                {
                    if (Puissance < pm.Classe.ClasseLvl)
                    {
                        pm.SendMessage("Ce livre n'est pas capable de vous faire oublier votre classe."); return;
                    }
                    else if(pm.Classe.ClasseID == 0)
                    {
                        pm.SendMessage("Vous n'avez pas de classe."); return;
                    }
                    else
                    {
                        pm.Classe =  Server.Classe.GetClasse(0);
                        pm.SendMessage("Vous oubliez votre classe.");
                         this.Delete();
                    }

                }
                else
                {
                    if (Puissance < pm.Metier.MetierLvl)
                    {
                        pm.SendMessage("Ce livre n'est pas capable de vous faire oublier votre métier."); return;
                    }
                    else if(pm.Metier.MetierID == 0)
                    {
                        pm.SendMessage("Vous n'avez pas de métier."); return;
                    }
                    else
                    {
                        pm.Metier =  Server.Metier.GetMetier(0);
                        pm.SendMessage("Vous oubliez votre métier.");
                        this.Delete();
                    }

                }
            }
        }
        public override void GetProperties(ObjectPropertyList list)
        {
                base.GetProperties(list); 
				list.Add(String.Format(Classe ? "Classe": "Métier"));
				list.Add(String.Format($"Puissance: {Puissance.ToString()}"));
			
        }

		public override void Serialize( GenericWriter writer )
		{
            base.Serialize(writer);

            writer.Write((int)0); // version

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

          
            
        }


	}

    [FlipableAttribute(0xFBE, 0xFBD)]
	public class LivreClasse0Oubli : BaseLivreOubli
	{


       	public override bool Classe => true;
		 
  	    public override int Puissance => 0;

		
        [Constructable]
        public LivreClasse0Oubli(): base()
        {
            Name = "livre de l'oubli";
            Weight = 2.0;
        }


        public LivreClasse0Oubli(Serial serial) : base(serial)
        {
        }
      	public override void Serialize( GenericWriter writer )
		{
            base.Serialize(writer);

            writer.Write((int)0); // version

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

          
            
        }
	}

         [FlipableAttribute(0xFBE, 0xFBD)]
	public class LivreClasse1Oubli : BaseLivreOubli
	{


       	public override bool Classe => true;
		 
  	    public override int Puissance => 1;

		
        [Constructable]
        public LivreClasse1Oubli() : base()
        {
            Name = "livre de l'oubli";
            Weight = 2.0;
        }


        public LivreClasse1Oubli(Serial serial) : base(serial)
        {
        }
      	public override void Serialize( GenericWriter writer )
		{
            base.Serialize(writer);

            writer.Write((int)0); // version

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

          
            
        }
	}
    [FlipableAttribute(0xFBE, 0xFBD)]
	public class LivreClasse2Oubli : BaseLivreOubli
	{


       	public override bool Classe => true;
		 
  	    public override int Puissance => 2;

		
        [Constructable]
        public LivreClasse2Oubli() : base()
        {
            Name = "livre de l'oubli";
            Weight = 2.0;
        }


        public LivreClasse2Oubli (Serial serial) : base(serial)
        {
        }
      	public override void Serialize( GenericWriter writer )
		{
            base.Serialize(writer);

            writer.Write((int)0); // version

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

          
            
        }
	}

    [FlipableAttribute(0xFBE, 0xFBD)]
	public class LivreMetier0Oubli : BaseLivreOubli
	{


       	public override bool Classe => false;
		 
  	    public override int Puissance => 0;

		
        [Constructable]
        public LivreMetier0Oubli() : base()
        {
            Name = "livre de l'oubli";
            Weight = 2.0;
        }


        public LivreMetier0Oubli(Serial serial) : base(serial)
        {
        }
      	public override void Serialize( GenericWriter writer )
		{
            base.Serialize(writer);

            writer.Write((int)0); // version

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

          
            
        }
	}
        [FlipableAttribute(0xFBE, 0xFBD)]
	public class LivreMetier1Oubli : BaseLivreOubli
	{

       	public override bool Classe => false;
		 
  	    public override int Puissance => 1;

		
        [Constructable]
        public LivreMetier1Oubli() : base()
        {
            Name = "livre de l'oubli";
            Weight = 2.0;
        }


        public LivreMetier1Oubli(Serial serial) : base(serial)
        {
        }
      	public override void Serialize( GenericWriter writer )
		{
            base.Serialize(writer);

            writer.Write((int)0); // version

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

          
            
        }
	}
    [FlipableAttribute(0xFBE, 0xFBD)]
	public class LivreMetier2Oubli : BaseLivreOubli
	{

       	public override bool Classe => false;
		 
  	    public override int Puissance => 2;

	    [Constructable]
        public LivreMetier2Oubli() : base()
        {
            Name = "livre de l'oubli";
            Weight = 2.0;
        }


        public LivreMetier2Oubli(Serial serial) : base(serial)
        {
        }
      	public override void Serialize( GenericWriter writer )
		{
            base.Serialize(writer);

            writer.Write((int)0); // version

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

          
            
        }
	}
}
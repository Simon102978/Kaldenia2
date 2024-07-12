using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using Server.Gumps;
using Server.Mobiles;
using System.Collections.Generic;


namespace Server.Mobiles
{
	public class Joueur : BaseVendor
	{

		private readonly List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos => m_SBInfos;
		public override bool ClickTitle{ get{ return false; } }
				
        public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBJoueur() );
		}


    public int m_MiseMax;

		[CommandProperty( AccessLevel.GameMaster )]
		public int MiseMax
		{
			get{ return m_MiseMax; }
			set{ m_MiseMax = value; }
		}		
 
 		[Constructable]
		public Joueur() : base("Barde")
		{
			Name = "Ali";
		}         

		public override void OnDoubleClick(Mobile m_Mobile)
		{
          if( !( m_Mobile is PlayerMobile ) )
					return;
					
					if( m_Mobile == null )
					return;
		
				PlayerMobile mobile = (PlayerMobile) m_Mobile;
				{
					if ( ! mobile.HasGump( typeof( GambleGump ) ) )
					{						   
          mobile.SendGump( new GambleGump( mobile, this ));		
					}
        }
    }
        			

		public void gamble( Mobile from, int tip, int Mise )
		{
  		if( Mise > 5000)	
  		{
      Say("hmm, Tu mises pas un peu trop haut mon ami ? Pas question !");
      return;
      }
  		else if( m_MiseMax < (Mise * 4))	
  		{
      Say("Je n'ai pas eu de chance aujourd'hui, je n'ai plus assez de pièces pour ta mise...");
      return;
      }	
 			
 			if(tip < 1 || tip > 6)
 			{
 			Say("Un dé a seulement six faces mon ami... raisonne un peu!");
      return;
      }	
 			
      if ( Mise > 0 )
  		{
  		Container pack = from.Backpack; 
  		
      	if ( pack != null && pack.ConsumeTotal( typeof( Gold ), Mise ) )
    		{
    		string MiseDonner = "Vous me donnez " + Mise + " pièces";
        from.SendMessage(MiseDonner);

        int Profit = 0;		
    		this.Emote( "*S'empare d'un petit sac d'or provenant de sa veste*" ); 
    		int Des1 = Utility.Random( 6 ) + 1;
    		int Des2 = Utility.Random( 6 ) + 1;

        string Ensemble = "Ta mise est de " + Mise + " et ton nombre le " + tip;
    		this.Say(Ensemble);
           		 
    		string Combinaison = "*Je roule uuuun " + Des1 + " et uuuun " + Des2 + "*";
    		this.Say(Combinaison);
                
        string Resultat;
                if(tip == Des1 && tip == Des2)
                {
                Profit = Mise * 4;
                Resultat = "Peste, vous avez gagné un doublé! un simple coup de chance... " + Profit + " pièces d'or!";
                }
                else if(tip == Des1 || tip == Des2)
                {
                Profit = Mise * 2;
                Resultat = "Mmh, vous avez quand même mérité un petit quelque chose... " + Profit + " pièces d'or!";
                }
    			   		else
    			   		{
                Resultat = "Ah, Ah! Ton or est à moi!";
                Profit = 0;
                }
                Say(Resultat);
                 
                if(Profit > 0)
                {
                from.AddToBackpack( new Gold(Profit) );
                m_MiseMax =  m_MiseMax - Profit;
                }
                else
                {
                m_MiseMax = m_MiseMax + Mise;
                }
   					 
    			
    		}	
        else
        {
           this.Say("Tu ne sembles pas avoir assez de pièces mon ami...");
        }	
    	}			
    	else
    	{
        this.Say("Cela me semble bien peu...");
    	}
		}

		



		public Joueur( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( m_MiseMax );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
      m_MiseMax = reader.ReadInt();
			int version = reader.ReadInt();
		}
	}
}
//Gump

namespace Server.Gumps
{
  public class GambleGump : Server.Gumps.Gump
  {
    private Mobile m_mobile;
    private Joueur m_Joueur;
    
    public GambleGump( Mobile mobile, Joueur Joueur) : base(0, 0)
    {
    m_mobile = mobile;
    m_Joueur = Joueur;
    
    this.Closable=true;
    this.Disposable=true;
    this.Dragable=true;
    this.Resizable=false;
    this.AddPage(0);
    this.AddBackground(50, 28, 500, 390, 9380);
    this.AddButton(438, 351, 247, 248, 1, GumpButtonType.Reply, 0); //Okay
    this.AddImage(107, 118, 62);
    this.AddImage(106, 79, 62);
    this.AddImageTiled(200, 184, 35, 32, 3004);
    this.AddItem(139, 178, 1175);
    this.AddImage(155, 195, 2362);
    this.AddImage(204, 188, 2362);
    this.AddImage(218, 200, 2362);
    this.AddImage(438, 305, 2443);
    this.AddImage(336, 270, 2440);
    this.AddLabel(338, 250, 0, @"Votre mise en pièces :");
    this.AddLabel(338, 305, 0, @"Votre nombre :");
    this.AddTextEntry(397, 272, 43, 20, 0, 1, @"100");
    this.AddTextEntry(463, 306, 21, 20, 0, 2, @"6");
    this.AddHtml( 303, 68, 210, 172, @"Gagner jusqu'à 4 fois votre mise!<br>Misez et choisissez un nombre de 1 à 6. Si votre nombre est sur 1 des deux dés, vous gagner 2 fois la mise. S'il est sur les deux dés, vous gagner 4 fois la mise!<br><br>Sinon, votre or est à moi!", (bool)true, (bool)true);
    }
    
    private int Mise;
    private int tip;
    
    public override void OnResponse( NetState state, RelayInfo info ) 
    { 
         Mobile from = state.Mobile; 
          
         switch ( info.ButtonID ) 
         { 
            case 0: 
            {
              break;          
            }
            case 1: 
            { 
               TextRelay text_Mise = info.GetTextEntry(1);
               TextRelay text_tip = info.GetTextEntry(2);//
               
               if ( text_Mise != null && text_tip != null)
						   {
  						   try
  						   {
  							 Mise = Convert.ToInt32( text_Mise.Text );
  							 tip = Convert.ToInt32( text_tip.Text );
                 m_Joueur.gamble(from, tip, Mise);
  							 }
  							 catch
  							 {
                 Console.WriteLine("Araden note : Crash du au script du Joueur");
                 }
               }
               break; 
            } 
            default:
            {
              break;
            }

        }
    }  

  }
}		 		
			
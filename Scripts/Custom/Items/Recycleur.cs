using System;
using Server;
using Server.Engines.Craft;
using Server.Targeting;
using Server.CustomScripts;

namespace Server.Items
{
	[FlipableAttribute(4787, 4787)]
	public class Recycleur : Item
	{
        private int m_UsesRemaining;

        [CommandProperty(AccessLevel.GameMaster)]
        public int UsesRemaining
        {
            get { return m_UsesRemaining; }
            set { m_UsesRemaining = value; InvalidateProperties(); }
        }

        public bool ShowUsesRemaining { get { return true; } set { } }

		[Constructable]
        public Recycleur() : base(4787)
		{
            Name = "Recycleur";
			Hue = 1109;
			Weight = 2.0;
            m_UsesRemaining = Utility.Random(50, 75);
		}

		public Recycleur( Serial serial ) : base( serial )
		{
		}

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add(String.Format("Utilisations restantes: {0}", m_UsesRemaining));
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.Backpack == null || this.Parent != from.Backpack)
            {
                from.SendMessage("Ce doit être dans votre sac.");
            }
            else
            {
                from.SendMessage("Sélectionner un objet à recycler.");
                from.Target = new InternalTarget(this);
            }
        }

        private class InternalTarget : Target
        {
            private Recycleur m_Recycleur;

            public InternalTarget(Recycleur recycleur) : base(1, false, TargetFlags.None)
            {
                m_Recycleur = recycleur;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                CraftSystem m_CraftSystem = null;
                CraftResource resource = CraftResource.None;

                if (targeted is BaseArmor)
                {
                    BaseArmor armor = (BaseArmor)targeted;
                    resource = armor.Resource;
                }
                else if (targeted is BaseWeapon)
                {
                    BaseWeapon weapon = (BaseWeapon)targeted;
                    resource = weapon.Resource;
                }
				else if (targeted is BaseRanged)
				{
					BaseRanged weapon = (BaseRanged)targeted;
					resource = weapon.Resource;
				}
				else if (targeted is BaseBow)
				{
					BaseBow weapon = (BaseBow)targeted;
					resource = weapon.Resource;
				}
				else if (targeted is BaseShoes)
                {
                    BaseShoes shoes = (BaseShoes)targeted;
                    resource = shoes.Resource;
                }
				else if (targeted is BaseJewel)
				{
					BaseJewel jewel = (BaseJewel)targeted;
					resource = jewel.Resource;
				}
				
				else
                {
                    from.SendMessage( "Cet article ne peut pas être recyclé.");
                }

				if (resource == CraftResource.None )
				{
                    from.SendMessage( "Vous ne pouvez pas recycler ceci. (Code: -1)");
                    return;
                }

                switch (resource)
				{
                    case CraftResource.Iron:             m_CraftSystem = DefBlacksmithy.CraftSystem; break;
                    case CraftResource.Bronze:                 m_CraftSystem = DefBlacksmithy.CraftSystem; break;
                    case CraftResource.Copper:		        m_CraftSystem = DefBlacksmithy.CraftSystem; break;
                    case CraftResource.Sonne:		        m_CraftSystem = DefBlacksmithy.CraftSystem; break;
					case CraftResource.Argent:			        m_CraftSystem = DefBlacksmithy.CraftSystem; break;
					case CraftResource.Boreale:			    m_CraftSystem = DefBlacksmithy.CraftSystem; break;
					case CraftResource.Chrysteliar:			    m_CraftSystem = DefBlacksmithy.CraftSystem; break;
					case CraftResource.Glacias:			    m_CraftSystem = DefBlacksmithy.CraftSystem; break;
					case CraftResource.Lithiar:		    m_CraftSystem = DefBlacksmithy.CraftSystem; break;
					case CraftResource.Acier:		    m_CraftSystem = DefBlacksmithy.CraftSystem; break;
					case CraftResource.Durian:			m_CraftSystem = DefBlacksmithy.CraftSystem; break;
					case CraftResource.Equilibrum:		m_CraftSystem = DefBlacksmithy.CraftSystem; break;
					case CraftResource.Gold:			m_CraftSystem = DefBlacksmithy.CraftSystem; break;
					case CraftResource.Jolinar:			m_CraftSystem = DefBlacksmithy.CraftSystem; break;
					case CraftResource.Justicium:		 m_CraftSystem = DefBlacksmithy.CraftSystem; break;
					case CraftResource.Abyssium:		m_CraftSystem = DefBlacksmithy.CraftSystem; break;
					case CraftResource.Bloodirium:		m_CraftSystem = DefBlacksmithy.CraftSystem; break;
					case CraftResource.Herbrosite:		m_CraftSystem = DefBlacksmithy.CraftSystem; break;
					case CraftResource.Khandarium:			m_CraftSystem = DefBlacksmithy.CraftSystem; break;
					case CraftResource.Mytheril:			m_CraftSystem = DefBlacksmithy.CraftSystem; break;
					case CraftResource.Sombralir:		m_CraftSystem = DefBlacksmithy.CraftSystem; break;
					case CraftResource.Draconyr:		m_CraftSystem = DefBlacksmithy.CraftSystem; break;
					case CraftResource.Heptazion:		 m_CraftSystem = DefBlacksmithy.CraftSystem; break;
					case CraftResource.Oceanis:			m_CraftSystem = DefBlacksmithy.CraftSystem; break;
					case CraftResource.Brazium:			m_CraftSystem = DefBlacksmithy.CraftSystem; break;
					case CraftResource.Lunerium:		m_CraftSystem = DefBlacksmithy.CraftSystem; break;
					case CraftResource.Marinar:			m_CraftSystem = DefBlacksmithy.CraftSystem; break;
					case CraftResource.Nostalgium:		m_CraftSystem = DefBlacksmithy.CraftSystem; break;



					case CraftResource.LupusLeather:     m_CraftSystem = DefTailoring.CraftSystem; break;
					case CraftResource.ReptilienLeather:	        m_CraftSystem = DefTailoring.CraftSystem; break;
					case CraftResource.GeantLeather:	    m_CraftSystem = DefTailoring.CraftSystem; break;
					case CraftResource.OphidienLeather:	    m_CraftSystem = DefTailoring.CraftSystem; break;
                    case CraftResource.ArachnideLeather:         m_CraftSystem = DefTailoring.CraftSystem; break;
                    case CraftResource.DragoniqueLeather:       m_CraftSystem = DefTailoring.CraftSystem; break;
                    case CraftResource.DemoniaqueLeather:      m_CraftSystem = DefTailoring.CraftSystem; break;
                    case CraftResource.AncienLeather:        m_CraftSystem = DefTailoring.CraftSystem; break;
                    case CraftResource.RegularLeather:       m_CraftSystem = DefTailoring.CraftSystem; break;


					case CraftResource.RegularBone:        m_CraftSystem = DefBoneTailoring.CraftSystem; break;
					case CraftResource.LupusBone:	            m_CraftSystem = DefBoneTailoring.CraftSystem; break;
					case CraftResource.ReptilienBone:	        m_CraftSystem = DefBoneTailoring.CraftSystem; break;
					case CraftResource.GeantBone:	        m_CraftSystem = DefBoneTailoring.CraftSystem; break;
                    case CraftResource.OphidienBone:            m_CraftSystem = DefBoneTailoring.CraftSystem; break;
                    case CraftResource.ArachnideBone:          m_CraftSystem = DefBoneTailoring.CraftSystem; break;
                    case CraftResource.DragoniqueBone:         m_CraftSystem = DefBoneTailoring.CraftSystem; break;
                    case CraftResource.DemoniaqueBone:           m_CraftSystem = DefBoneTailoring.CraftSystem; break;
                    case CraftResource.AncienBone:          m_CraftSystem = DefBoneTailoring.CraftSystem; break;

                    case CraftResource.PalmierWood:          m_CraftSystem = DefCarpentry.CraftSystem; break;
					case CraftResource.ErableWood:	        m_CraftSystem = DefCarpentry.CraftSystem; break;
					case CraftResource.CheneWood:	    m_CraftSystem = DefCarpentry.CraftSystem; break;
                    case CraftResource.CedreWood:           m_CraftSystem = DefCarpentry.CraftSystem; break;
                    case CraftResource.CypresWood:        m_CraftSystem = DefCarpentry.CraftSystem; break;
                    case CraftResource.SauleWood:           m_CraftSystem = DefCarpentry.CraftSystem; break;
                    case CraftResource.AcajouWood:          m_CraftSystem = DefCarpentry.CraftSystem; break;
					case CraftResource.EbeneWood: m_CraftSystem = DefCarpentry.CraftSystem; break;
					case CraftResource.AmaranteWood: m_CraftSystem = DefCarpentry.CraftSystem; break;
					case CraftResource.PinWood: m_CraftSystem = DefCarpentry.CraftSystem; break;
					case CraftResource.AncienWood: m_CraftSystem = DefCarpentry.CraftSystem; break;


					case CraftResource.None: m_CraftSystem = DefTailoring.CraftSystem; break;

				}

				if (m_CraftSystem == null)
                {
                    from.SendMessage( "Vous ne pouvez pas recycler ceci. (Code: 0)");
                    return;
                }

                CraftResourceInfo info = CraftResources.GetInfo(resource);

				if ((info == null || info.ResourceTypes.Length == 0))
				{
                    from.SendMessage( "Vous ne pouvez pas recycler ceci. (Code: 1)");
                    return;
                }

                CraftItem craftItem = m_CraftSystem.CraftItems.SearchFor(targeted.GetType());

                if (craftItem == null || craftItem.Resources.Count == 0)
                {
                    switch (resource)
				    {
						case CraftResource.Iron: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.Bronze: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.Copper: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.Sonne: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.Argent: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.Boreale: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.Chrysteliar: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.Glacias: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.Lithiar: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.Acier: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.Durian: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.Equilibrum: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.Gold: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.Jolinar: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.Justicium: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.Abyssium: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.Bloodirium: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.Herbrosite: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.Khandarium: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.Mytheril: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.Sombralir: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.Draconyr: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.Heptazion: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.Oceanis: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.Brazium: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.Lunerium: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.Marinar: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.Nostalgium: m_CraftSystem = DefTinkering.CraftSystem; break;




						case CraftResource.LupusLeather: m_CraftSystem = DefLeatherArmor.CraftSystem; break;
						case CraftResource.ReptilienLeather: m_CraftSystem = DefLeatherArmor.CraftSystem; break;
						case CraftResource.GeantLeather: m_CraftSystem = DefLeatherArmor.CraftSystem; break;
						case CraftResource.OphidienLeather: m_CraftSystem = DefLeatherArmor.CraftSystem; break;
						case CraftResource.ArachnideLeather: m_CraftSystem = DefLeatherArmor.CraftSystem; break;
						case CraftResource.DragoniqueLeather: m_CraftSystem = DefLeatherArmor.CraftSystem; break;
						case CraftResource.DemoniaqueLeather: m_CraftSystem = DefLeatherArmor.CraftSystem; break;
						case CraftResource.AncienLeather: m_CraftSystem = DefLeatherArmor.CraftSystem; break;
						case CraftResource.RegularLeather: m_CraftSystem = DefLeatherArmor.CraftSystem; break;



						case CraftResource.PalmierWood: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.ErableWood: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.CheneWood: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.CedreWood: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.CypresWood: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.SauleWood: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.AcajouWood: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.EbeneWood: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.AmaranteWood: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.PinWood: m_CraftSystem = DefTinkering.CraftSystem; break;
						case CraftResource.AncienWood: m_CraftSystem = DefTinkering.CraftSystem; break;
					}

                    craftItem = m_CraftSystem.CraftItems.SearchFor(targeted.GetType());
                }

                if (craftItem == null || craftItem.Resources.Count == 0)
                {
                    from.SendMessage( "Vous ne pouvez pas recycler ceci. (Code: 2)");
                    return;
                }

                CraftRes craftResource = craftItem.Resources.GetAt(0);

                if (craftResource.Amount < 2)
                {
                    from.SendMessage( "Cet objet ne contient pas suffisamment de ressources pour être recyclé.");
                    return;
                }

                Type resourceType = info.ResourceTypes[0];
                Item resItem = (Item)Activator.CreateInstance(resourceType);

                if (resItem is BaseIngot)
                {
                    switch (resource)
                    {

						case CraftResource.Iron: resItem = new IronIngot(); break;
						case CraftResource.Bronze: resItem = new BronzeIngot(); break;
						case CraftResource.Copper: resItem = new CopperIngot(); break;
						case CraftResource.Sonne: resItem = new SonneIngot(); break;
						case CraftResource.Argent: resItem = new ArgentIngot(); break;
						case CraftResource.Boreale: resItem = new BorealeIngot(); break;
						case CraftResource.Chrysteliar: resItem = new ChrysteliarIngot(); break;
						case CraftResource.Glacias: resItem = new GlaciasIngot(); break;
						case CraftResource.Lithiar: resItem = new LithiarIngot(); break;
						case CraftResource.Acier: resItem = new AcierIngot(); break;
						case CraftResource.Durian: resItem = new DurianIngot(); break;
						case CraftResource.Equilibrum: resItem = new EquilibrumIngot(); break;
						case CraftResource.Gold: resItem = new GoldIngot(); break;
						case CraftResource.Jolinar: resItem = new JolinarIngot(); break;
						case CraftResource.Justicium: resItem = new JusticiumIngot(); break;
						case CraftResource.Abyssium: resItem = new AbyssiumIngot(); break;
						case CraftResource.Bloodirium: resItem = new BloodiriumIngot(); break;
						case CraftResource.Herbrosite: resItem = new HerbrositeIngot(); break;
						case CraftResource.Khandarium: resItem = new KhandariumIngot(); break;
						case CraftResource.Mytheril: resItem = new MytherilIngot(); break;
						case CraftResource.Sombralir: resItem = new SombralirIngot(); break;
						case CraftResource.Draconyr: resItem = new DraconyrIngot(); break;
						case CraftResource.Heptazion: resItem = new HeptazionIngot(); break;
						case CraftResource.Oceanis: resItem = new OceanisIngot(); break;
						case CraftResource.Brazium: resItem = new BraziumIngot(); break;
						case CraftResource.Lunerium: resItem = new LuneriumIngot(); break;
						case CraftResource.Marinar: resItem = new MarinarIngot(); break;
						case CraftResource.Nostalgium: resItem = new NostalgiumIngot(); break;
						
                    }
                }
                else if (resItem is BaseLog)
                {
                    switch (resource)
                    {
                       
						case CraftResource.PalmierWood:  resItem = new PalmierBoard(); break;
						case CraftResource.ErableWood:  resItem = new ErableBoard(); break;
						case CraftResource.CheneWood:  resItem = new CheneBoard(); break;
						case CraftResource.CedreWood:  resItem = new CedreBoard(); break;
						case CraftResource.CypresWood:  resItem = new CypresBoard(); break;
						case CraftResource.SauleWood:  resItem = new SauleBoard(); break;
						case CraftResource.AcajouWood:  resItem = new AcajouBoard(); break;
						case CraftResource.EbeneWood:  resItem = new EbeneBoard(); break;
						case CraftResource.AmaranteWood:  resItem = new AmaranteBoard(); break;
						case CraftResource.PinWood:  resItem = new PinBoard(); break;
						case CraftResource.AncienWood:  resItem = new AncienBoard(); break;
					}
                }
                else if (resItem is BaseBone)
                {
                    switch (resource)
                    {
                        case CraftResource.RegularBone:  resItem = new Bone(); break;
                        case CraftResource.LupusBone:       resItem = new LupusBone(); break;
                        case CraftResource.ReptilienBone:     resItem = new ReptilienBone(); break;
                        case CraftResource.GeantBone:   resItem = new GeantBone(); break;
                        case CraftResource.OphidienBone:      resItem = new OphidienBone(); break;
                        case CraftResource.ArachnideBone:    resItem = new ArachnideBone(); break;
                        case CraftResource.DragoniqueBone:   resItem = new DragoniqueBone(); break;
                        case CraftResource.DemoniaqueBone:     resItem = new DemoniaqueBone(); break;
                        case CraftResource.AncienBone:    resItem = new AncienBone(); break;
                    }
                }
                
                else if (resItem is BaseHides)
                {
                    switch (resource)
                    {
                        case CraftResource.RegularLeather:  resItem = new Leather(); break;
                        case CraftResource.LupusLeather:       resItem = new LupusLeather(); break;
                        case CraftResource.ReptilienLeather:     resItem = new ReptilienLeather(); break;
                        case CraftResource.GeantLeather:   resItem = new GeantLeather(); break;
                        case CraftResource.OphidienLeather:      resItem = new OphidienLeather(); break;
                        case CraftResource.ArachnideLeather:    resItem = new ArachnideLeather(); break;
                        case CraftResource.DragoniqueLeather:   resItem = new DragoniqueLeather(); break;
                        case CraftResource.DemoniaqueLeather:     resItem = new DemoniaqueLeather(); break;
                        case CraftResource.AncienLeather:    resItem = new AncienLeather(); break;
                    }
                }

				


				int newAmount = (int)(craftResource.Amount * 0.5);
                
                if (newAmount < 1)
                    newAmount = 1;

				resItem.Amount = newAmount;


                from.AddToBackpack(resItem);

                ((Item)targeted).Delete();
                    
                m_Recycleur.UsesRemaining -= 1;

                if (m_Recycleur.UsesRemaining < 1)
                {
                    m_Recycleur.Delete();
                    from.SendMessage( "Vous brisez votre outil!");
                }

                from.PlaySound(0x5CA);
            }
		}
	


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

            writer.Write((int)m_UsesRemaining);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

            m_UsesRemaining = reader.ReadInt();
		}
	}
}
using Server.Engines.Craft;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
    public abstract class BaseOre : Item
    {
        protected virtual CraftResource DefaultResource => CraftResource.Iron;

        private CraftResource m_Resource;

        [CommandProperty(AccessLevel.GameMaster)]
        public CraftResource Resource
        {
            get
            {
                return m_Resource;
            }
            set
            {
                m_Resource = value;
                InvalidateProperties();
            }
        }

        public abstract BaseIngot GetIngot();

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(1); // version

            writer.Write((int)m_Resource);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 2: // Reset from Resource System
                    m_Resource = DefaultResource;
                    reader.ReadString();
                    break;
                case 1:
                    {
                        m_Resource = (CraftResource)reader.ReadInt();
                        break;
                    }
                case 0:
                    {
                        OreInfo info;

                        switch (reader.ReadInt())
                        {
                            case 0:
                                info = OreInfo.Iron;
                                break;
                            case 1:
                                info = OreInfo.DullCopper;
                                break;
                            case 2:
                                info = OreInfo.ShadowIron;
                                break;
                            case 3:
                                info = OreInfo.Copper;
                                break;
                            case 4:
                                info = OreInfo.Bronze;
                                break;
                            case 5:
                                info = OreInfo.Gold;
                                break;
                            case 6:
                                info = OreInfo.Agapite;
                                break;
                            case 7:
                                info = OreInfo.Verite;
                                break;
                            case 8:
                                info = OreInfo.Valorite;
                                break;
                            default:
                                info = null;
                                break;
                        }

                        m_Resource = CraftResources.GetFromOreInfo(info);
                        break;
                    }
            }
        }

        private static int RandomSize()
        {
            //double rand = Utility.RandomDouble();

            //if (rand < 0.12)
            //    return 0x19B7;
            //else if (rand < 0.18)
            //    return 0x19B8;
            //else if (rand < 0.25)
            //    return 0x19BA;
            //else
                return 0x19B9;
        }

        public BaseOre(CraftResource resource)
            : this(resource, 1)
        {
        }

        public BaseOre(CraftResource resource, int amount)
            : base(RandomSize())
        {
            Stackable = true;
            Amount = amount;
            Hue = CraftResources.GetHue(resource);

            m_Resource = resource;
        }

        public BaseOre(Serial serial)
            : base(serial)
        {
        }

        public override void AddNameProperty(ObjectPropertyList list)
        {
            if (Amount > 1)
                list.Add(1050039, "{0}\t{1}", Amount, "Minerais"); // ~1_NUMBER~ ~2_ITEMNAME~
            else
				list.Add(1050039, "{0}\t{1}", Amount, "Minerai"); // ore
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add(CraftResources.GetName(m_Resource));
        }

		public override double DefaultWeight => 2;

		public override int LabelNumber
        {
            get
            {
                if (m_Resource >= CraftResource.DullCopper && m_Resource <= CraftResource.Valorite)
                    return 1042845 + (m_Resource - CraftResource.DullCopper);

                return 1042853; // iron ore;
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
                return;

            if (RootParent is BaseCreature)
            {
                from.SendLocalizedMessage(500447); // That is not accessible
            }
            else if (from.InRange(GetWorldLocation(), 2))
            {
                from.SendLocalizedMessage(501971); // Select the forge on which to smelt the ore, or another pile of ore with which to combine it.
                from.Target = new InternalTarget(this);
            }
            else
            {
                from.SendLocalizedMessage(501976); // The ore is too far away.
            }
        }

        private class InternalTarget : Target
        {
            private readonly BaseOre m_Ore;

            public InternalTarget(BaseOre ore)
                : base(2, false, TargetFlags.None)
            {
                m_Ore = ore;
            }

            private bool IsForge(object obj)
            {
                if (obj is Mobile && ((Mobile)obj).IsDeadBondedPet)
                    return false;

                if (obj.GetType().IsDefined(typeof(ForgeAttribute), false))
                    return true;

                int itemID = 0;

                if (obj is Item)
                    itemID = ((Item)obj).ItemID;
                else if (obj is StaticTarget)
                    itemID = ((StaticTarget)obj).ItemID;

                return (itemID == 4017 || (itemID >= 6522 && itemID <= 6569));
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (m_Ore.Deleted)
                    return;

                if (!from.InRange(m_Ore.GetWorldLocation(), 2))
                {
                    from.SendLocalizedMessage(501976); // The ore is too far away.
                    return;
                }

                #region Combine Ore
                if (targeted is BaseOre)
                {
                    BaseOre ore = (BaseOre)targeted;

                    if (!ore.Movable)
                    {
                        return;
                    }
                    else if (m_Ore == ore)
                    {
                        from.SendLocalizedMessage(501972); // Select another pile or ore with which to combine 
                        from.Target = new InternalTarget(ore);
                        return;
                    }
                    else if (ore.Resource != m_Ore.Resource)
                    {
                        from.SendLocalizedMessage(501979); // You cannot combine ores of different metals.
                        return;
                    }

                    int worth = ore.Amount;

                    if (ore.ItemID == 0x19B9)
                        worth *= 8;
                    else if (ore.ItemID == 0x19B7)
                        worth *= 2;
                    else
                        worth *= 4;

                    int sourceWorth = m_Ore.Amount;

                    if (m_Ore.ItemID == 0x19B9)
                        sourceWorth *= 8;
                    else if (m_Ore.ItemID == 0x19B7)
                        sourceWorth *= 2;
                    else
                        sourceWorth *= 4;

                    worth += sourceWorth;

                    int plusWeight = 0;
                    int newID = ore.ItemID;

                    if (ore.DefaultWeight != m_Ore.DefaultWeight)
                    {
                        if (ore.ItemID == 0x19B7 || m_Ore.ItemID == 0x19B7)
                        {
                            newID = 0x19B7;
                        }
                        else if (ore.ItemID == 0x19B9)
                        {
                            newID = m_Ore.ItemID;
                            plusWeight = ore.Amount * 2;
                        }
                        else
                        {
                            plusWeight = m_Ore.Amount * 2;
                        }
                    }

                    if ((ore.ItemID == 0x19B9 && worth > 120000) || ((ore.ItemID == 0x19B8 || ore.ItemID == 0x19BA) && worth > 60000) || (ore.ItemID == 0x19B7 && worth > 30000))
                    {
                        from.SendLocalizedMessage(1062844); // There is too much ore to combine.
                        return;
                    }
                    else if (ore.RootParent is Mobile && (plusWeight + ((Mobile)ore.RootParent).Backpack.TotalWeight) > ((Mobile)ore.RootParent).Backpack.MaxWeight)
                    {
                        from.SendLocalizedMessage(501978); // The weight is too great to combine in a container.
                        return;
                    }

                    ore.ItemID = newID;

                    if (ore.ItemID == 0x19B9)
                        ore.Amount = worth / 8;
                    else if (ore.ItemID == 0x19B7)
                        ore.Amount = worth / 2;
                    else
                        ore.Amount = worth / 4;

                    m_Ore.Delete();
                    return;
                }
                #endregion

                if (IsForge(targeted))
                {
                    double difficulty;

                    #region Void Pool Rewards
                    bool talisman = false;
                    SmeltersTalisman t = from.FindItemOnLayer(Layer.Talisman) as SmeltersTalisman;
                    if (t != null && t.Resource == m_Ore.Resource)
                        talisman = true;
                    #endregion

                    switch (m_Ore.Resource)
                    {
                        default:
                            difficulty = 30.0;
                            break;
                        case CraftResource.DullCopper:
                            difficulty = 35.0;
                            break;
                        case CraftResource.ShadowIron:
                            difficulty = 40.0;
                            break;
                        case CraftResource.Agapite:
                            difficulty = 60.0;
                            break;
                        case CraftResource.Verite:
                            difficulty = 65.0;
                            break;
                        case CraftResource.Valorite:
                            difficulty = 70.0;
                            break;
						case CraftResource.Iron:
							difficulty = 0.0;
							break;
						case CraftResource.Bronze:
							difficulty = 0.0;
							break;
						case CraftResource.Copper:
							difficulty = 0.0;
							break;
						case CraftResource.Sonne:
							difficulty = 20.0;
							break;
						case CraftResource.Argent:
							difficulty = 20.0;
							break;
						case CraftResource.Boreale:
							difficulty = 20.0;
							break;
						case CraftResource.Chrysteliar:
							difficulty = 20.0;
							break;
						case CraftResource.Glacias:
							difficulty = 20.0;
							break;
						case CraftResource.Lithiar:
							difficulty = 20.0;
							break;
						case CraftResource.Acier:
							difficulty = 40.0;
							break;
						case CraftResource.Durian:
							difficulty = 40.0;
							break;
						case CraftResource.Equilibrum:
							difficulty = 40.0;
							break;
						case CraftResource.Justicium:
							difficulty = 40.0;
							break;
						case CraftResource.Gold:
							difficulty = 40.0;
							break;
						case CraftResource.Jolinar:
							difficulty = 40.0;
							break;
						case CraftResource.Abyssium:
							difficulty = 60.0;
							break;
						case CraftResource.Bloodirium:
							difficulty = 60.0;
							break;
						case CraftResource.Herbrosite:
							difficulty = 60.0;
							break;
						case CraftResource.Khandarium:
							difficulty = 60.0;
							break;
						case CraftResource.Mytheril:
							difficulty = 60.0;
							break;
						case CraftResource.Sombralir:
							difficulty = 60.0;
							break;
						case CraftResource.Draconyr:
							difficulty = 80.0;
							break;
						case CraftResource.Heptazion:
							difficulty = 80.0;
							break;
						case CraftResource.Oceanis:
							difficulty = 80.0;
							break;
						case CraftResource.Brazium:
							difficulty = 80.0;
							break;
						case CraftResource.Lunerium:
							difficulty = 80.0;
							break;
						case CraftResource.Marinar:
							difficulty = 80.0;
							break;
						case CraftResource.Nostalgium:
							difficulty = 100.0;
							break;
					}

                    double minSkill = difficulty - 25.0;
                    double maxSkill = difficulty + 25.0;

                    if (difficulty > 50.0 && difficulty > from.Skills[SkillName.Mining].Value && !talisman)
                    {
                        from.SendLocalizedMessage(501986); // You have no idea how to smelt this strange ore!
                        return;
                    }

                    if (m_Ore.ItemID == 0x19B7 && m_Ore.Amount < 2)
                    {
                        from.SendLocalizedMessage(501987); // There is not enough metal-bearing ore in this pile to make an ingot.
                        return;
                    }

                    if (talisman || from.CheckTargetSkill(SkillName.Mining, targeted, minSkill, maxSkill))
                    {
                        int toConsume = m_Ore.Amount;

                        if (toConsume <= 0)
                        {
                            from.SendLocalizedMessage(501987); // There is not enough metal-bearing ore in this pile to make an ingot.
                        }
                        else
                        {
                            if (toConsume > 30000)
                                toConsume = 30000;

                            int ingotAmount;

                            if (m_Ore.ItemID == 0x19B7)
                            {
                                ingotAmount = toConsume / 2;

                                if (toConsume % 2 != 0)
                                    --toConsume;
                            }
                            else if (m_Ore.ItemID == 0x19B9)
                            {
                                ingotAmount = toConsume * 2;
                            }
                            else
                            {
                                ingotAmount = toConsume;
                            }

                            BaseIngot ingot = m_Ore.GetIngot();
                            ingot.Amount = ingotAmount;

                            if (m_Ore.HasSocket<Caddellite>())
                            {
                                ingot.AttachSocket(new Caddellite());
                            }

                            m_Ore.Consume(toConsume);
                            from.AddToBackpack(ingot);
                            //from.PlaySound( 0x57 );

                            if (talisman && t != null)
                            {
                                t.UsesRemaining--;
                                from.SendLocalizedMessage(1152620); // The magic of your talisman guides your hands as you purify the metal. Success is ensured!
                            }
                            else
                                from.SendLocalizedMessage(501988); // You smelt the ore removing the impurities and put the metal in your backpack.
                        }
                    }
                    else
                    {
                        if (m_Ore.Amount < 2)
                        {
                            if (m_Ore.ItemID == 0x19B9)
                                m_Ore.ItemID = 0x19B8;
                            else
                                m_Ore.ItemID = 0x19B7;
                        }
                        else
                        {
                            m_Ore.Amount /= 2;
                        }

                        from.SendLocalizedMessage(501990); // You burn away the impurities but are left with less useable metal.
                    }
                }
            }
        }
    }

    public class IronOre : BaseOre
    {
        [Constructable]
        public IronOre()
            : this(1)
        {
        }

        [Constructable]
        public IronOre(int amount)
            : base(CraftResource.Iron, amount)
        {
        }

        public IronOre(bool fixedSize)
            : this(1)
        {
            if (fixedSize)
                ItemID = 0x19B8;
        }

        public IronOre(Serial serial)
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

        public override BaseIngot GetIngot()
        {
            return new IronIngot();
        }
    }

    public class DullCopperOre : BaseOre
    {
        protected override CraftResource DefaultResource => CraftResource.DullCopper;

        [Constructable]
        public DullCopperOre()
            : this(1)
        {
        }

        [Constructable]
        public DullCopperOre(int amount)
            : base(CraftResource.DullCopper, amount)
        {
        }

        public DullCopperOre(Serial serial)
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

        public override BaseIngot GetIngot()
        {
            return new DullCopperIngot();
        }
    }

    public class ShadowIronOre : BaseOre
    {
        protected override CraftResource DefaultResource => CraftResource.ShadowIron;

        [Constructable]
        public ShadowIronOre()
            : this(1)
        {
        }

        [Constructable]
        public ShadowIronOre(int amount)
            : base(CraftResource.ShadowIron, amount)
        {
        }

        public ShadowIronOre(Serial serial)
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

        public override BaseIngot GetIngot()
        {
            return new ShadowIronIngot();
        }
    }

    public class CopperOre : BaseOre
    {
        protected override CraftResource DefaultResource => CraftResource.Copper;

        [Constructable]
        public CopperOre()
            : this(1)
        {
        }

        [Constructable]
        public CopperOre(int amount)
            : base(CraftResource.Copper, amount)
        {
        }

        public CopperOre(Serial serial)
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

        public override BaseIngot GetIngot()
        {
            return new CopperIngot();
        }
    }

    public class BronzeOre : BaseOre
    {
        protected override CraftResource DefaultResource => CraftResource.Bronze;

        [Constructable]
        public BronzeOre()
            : this(1)
        {
        }

        [Constructable]
        public BronzeOre(int amount)
            : base(CraftResource.Bronze, amount)
        {
        }

        public BronzeOre(Serial serial)
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

        public override BaseIngot GetIngot()
        {
            return new BronzeIngot();
        }
    }

    public class GoldOre : BaseOre
    {
        protected override CraftResource DefaultResource => CraftResource.Gold;

        [Constructable]
        public GoldOre()
            : this(1)
        {
        }

        [Constructable]
        public GoldOre(int amount)
            : base(CraftResource.Gold, amount)
        {
        }

        public GoldOre(Serial serial)
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

        public override BaseIngot GetIngot()
        {
            return new GoldIngot();
        }
    }

    public class AgapiteOre : BaseOre
    {
        protected override CraftResource DefaultResource => CraftResource.Agapite;

        [Constructable]
        public AgapiteOre()
            : this(1)
        {
        }

        [Constructable]
        public AgapiteOre(int amount)
            : base(CraftResource.Agapite, amount)
        {
        }

        public AgapiteOre(Serial serial)
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

        public override BaseIngot GetIngot()
        {
            return new AgapiteIngot();
        }
    }

    public class VeriteOre : BaseOre
    {
        protected override CraftResource DefaultResource => CraftResource.Verite;

        [Constructable]
        public VeriteOre()
            : this(1)
        {
        }

        [Constructable]
        public VeriteOre(int amount)
            : base(CraftResource.Verite, amount)
        {
        }

        public VeriteOre(Serial serial)
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

        public override BaseIngot GetIngot()
        {
            return new VeriteIngot();
        }
    }

    public class ValoriteOre : BaseOre
    {
        protected override CraftResource DefaultResource => CraftResource.Valorite;

        [Constructable]
        public ValoriteOre()
            : this(1)
        {
        }

        [Constructable]
        public ValoriteOre(int amount)
            : base(CraftResource.Valorite, amount)
        {
        }

        public ValoriteOre(Serial serial)
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

        public override BaseIngot GetIngot()
        {
            return new ValoriteIngot();
        }
    }
	public class MytherilOre : BaseOre
	{
		[Constructable]
		public MytherilOre() : this(1)
		{
		}

		[Constructable]
		public MytherilOre(int amount) : base(CraftResource.Mytheril, amount)
		{
		}

		public MytherilOre(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new MytherilIngot();
		}
	}

	public class SonneOre : BaseOre
	{
		[Constructable]
		public SonneOre() : this(1)
		{
		}

		[Constructable]
		public SonneOre(int amount) : base(CraftResource.Sonne, amount)
		{
		}

		public SonneOre(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new SonneIngot();
		}
	}
	public class ArgentOre : BaseOre
	{
		[Constructable]
		public ArgentOre() : this(1)
		{
		}

		[Constructable]
		public ArgentOre(int amount) : base(CraftResource.Argent, amount)
		{
		}

		public ArgentOre(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new ArgentIngot();
		}
	}
	public class BorealeOre : BaseOre
	{
		[Constructable]
		public BorealeOre() : this(1)
		{
		}

		[Constructable]
		public BorealeOre(int amount) : base(CraftResource.Boreale, amount)
		{
		}

		public BorealeOre(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new BorealeIngot();
		}
	}
	public class ChrysteliarOre : BaseOre
	{
		[Constructable]
		public ChrysteliarOre() : this(1)
		{
		}

		[Constructable]
		public ChrysteliarOre(int amount) : base(CraftResource.Chrysteliar, amount)
		{
		}

		public ChrysteliarOre(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new ChrysteliarIngot();
		}
	}
	public class GlaciasOre : BaseOre
	{
		[Constructable]
		public GlaciasOre() : this(1)
		{
		}

		[Constructable]
		public GlaciasOre(int amount) : base(CraftResource.Glacias, amount)
		{
		}

		public GlaciasOre(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new GlaciasIngot();
		}
	}
	public class LithiarOre : BaseOre
	{
		[Constructable]
		public LithiarOre() : this(1)
		{
		}

		[Constructable]
		public LithiarOre(int amount) : base(CraftResource.Lithiar, amount)
		{
		}

		public LithiarOre(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new LithiarIngot();
		}
	}
	public class AcierOre : BaseOre
	{
		[Constructable]
		public AcierOre() : this(1)
		{
		}

		[Constructable]
		public AcierOre(int amount) : base(CraftResource.Acier, amount)
		{
		}

		public AcierOre(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new AcierIngot();
		}
	}
	public class DurianOre : BaseOre
	{
		[Constructable]
		public DurianOre() : this(1)
		{
		}

		[Constructable]
		public DurianOre(int amount) : base(CraftResource.Durian, amount)
		{
		}

		public DurianOre(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new DurianIngot();
		}
	}
	public class EquilibrumOre : BaseOre
	{
		[Constructable]
		public EquilibrumOre() : this(1)
		{
		}

		[Constructable]
		public EquilibrumOre(int amount) : base(CraftResource.Equilibrum, amount)
		{
		}

		public EquilibrumOre(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new EquilibrumIngot();
		}
	}
	public class JolinarOre : BaseOre
	{
		[Constructable]
		public JolinarOre() : this(1)
		{
		}

		[Constructable]
		public JolinarOre(int amount) : base(CraftResource.Jolinar, amount)
		{
		}

		public JolinarOre(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new JolinarIngot();
		}
	}
	public class JusticiumOre : BaseOre
	{
		[Constructable]
		public JusticiumOre() : this(1)
		{
		}

		[Constructable]
		public JusticiumOre(int amount) : base(CraftResource.Justicium, amount)
		{
		}

		public JusticiumOre(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new JusticiumIngot();
		}
	}
	public class AbyssiumOre : BaseOre
	{
		[Constructable]
		public AbyssiumOre() : this(1)
		{
		}

		[Constructable]
		public AbyssiumOre(int amount) : base(CraftResource.Abyssium, amount)
		{
		}

		public AbyssiumOre(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new AbyssiumIngot();
		}
	}
	public class BloodiriumOre : BaseOre
	{
		[Constructable]
		public BloodiriumOre() : this(1)
		{
		}

		[Constructable]
		public BloodiriumOre(int amount) : base(CraftResource.Bloodirium, amount)
		{
		}

		public BloodiriumOre(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new BloodiriumIngot();
		}
	}
	public class HerbrositeOre : BaseOre
	{
		[Constructable]
		public HerbrositeOre() : this(1)
		{
		}

		[Constructable]
		public HerbrositeOre(int amount) : base(CraftResource.Herbrosite, amount)
		{
		}

		public HerbrositeOre(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new HerbrositeIngot();
		}
	}
	public class KhandariumOre : BaseOre
	{
		[Constructable]
		public KhandariumOre() : this(1)
		{
		}

		[Constructable]
		public KhandariumOre(int amount) : base(CraftResource.Khandarium, amount)
		{
		}

		public KhandariumOre(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new KhandariumIngot();
		}
	}
	public class SombralirOre : BaseOre
	{
		[Constructable]
		public SombralirOre() : this(1)
		{
		}

		[Constructable]
		public SombralirOre(int amount) : base(CraftResource.Sombralir, amount)
		{
		}

		public SombralirOre(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new SombralirIngot();
		}
	}
	public class DraconyrOre : BaseOre
	{
		[Constructable]
		public DraconyrOre() : this(1)
		{
		}

		[Constructable]
		public DraconyrOre(int amount) : base(CraftResource.Draconyr, amount)
		{
		}

		public DraconyrOre(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new DraconyrIngot();
		}
	}
	public class HeptazionOre : BaseOre
	{
		[Constructable]
		public HeptazionOre() : this(1)
		{
		}

		[Constructable]
		public HeptazionOre(int amount) : base(CraftResource.Heptazion, amount)
		{
		}

		public HeptazionOre(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new HeptazionIngot();
		}
	}
	public class OceanisOre : BaseOre
	{
		[Constructable]
		public OceanisOre() : this(1)
		{
		}

		[Constructable]
		public OceanisOre(int amount) : base(CraftResource.Oceanis, amount)
		{
		}

		public OceanisOre(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new OceanisIngot();
		}
	}
	public class BraziumOre : BaseOre
	{
		[Constructable]
		public BraziumOre() : this(1)
		{
		}

		[Constructable]
		public BraziumOre(int amount) : base(CraftResource.Brazium, amount)
		{
		}

		public BraziumOre(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new BraziumIngot();
		}
	}
	public class LuneriumOre : BaseOre
	{
		[Constructable]
		public LuneriumOre() : this(1)
		{
		}

		[Constructable]
		public LuneriumOre(int amount) : base(CraftResource.Lunerium, amount)
		{
		}

		public LuneriumOre(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new LuneriumIngot();
		}
	}
	public class MarinarOre : BaseOre
	{
		[Constructable]
		public MarinarOre() : this(1)
		{
		}

		[Constructable]
		public MarinarOre(int amount) : base(CraftResource.Marinar, amount)
		{
		}

		public MarinarOre(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new MarinarIngot();
		}
	}
	public class NostalgiumOre : BaseOre
	{
		[Constructable]
		public NostalgiumOre() : this(1)
		{
		}

		[Constructable]
		public NostalgiumOre(int amount) : base(CraftResource.Nostalgium, amount)
		{
		}

		public NostalgiumOre(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}

		public override BaseIngot GetIngot()
		{
			return new NostalgiumIngot();
		}
	}
}

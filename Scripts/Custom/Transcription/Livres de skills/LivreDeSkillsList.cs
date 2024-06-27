namespace Server.Items
{
	public class Etude
	{

	}


    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsAlchemy : LivreSkills
    {
        [Constructable]
        public LivreSkillsAlchemy()
            : this(SkillName.Alchemy, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsAlchemy(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Alchemy";
        }

        public LivreSkillsAlchemy(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsAnatomy : LivreSkills
    {
        [Constructable]
        public LivreSkillsAnatomy()
            : this(SkillName.Anatomy, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsAnatomy(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Anatomy";
        }

        public LivreSkillsAnatomy(Serial serial)
            : base(serial)
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
    }

    //[FlipableAttribute(0xFBE, 0xFBD)]
    //public class LivreSkillsAnimalLore : LivreSkills
    //{
    //    [Constructable]
    //    public LivreSkillsAnimalLore()
    //        : this(SkillName.AnimalLore, 0.0, 0.0)
    //    {
    //    }

    //    [Constructable]
    //    public LivreSkillsAnimalLore(SkillName skill, double value, double growvalue)
    //        : base(skill, value, growvalue)
    //    {
    //        Name = "Étude : Animal Lore";
    //    }

    //    public LivreSkillsAnimalLore(Serial serial)
    //        : base(serial)
    //    {
    //    }

    //    public override void Serialize(GenericWriter writer)
    //    {
    //        base.Serialize(writer);

    //        writer.Write((int)0); // version
    //    }

    //    public override void Deserialize(GenericReader reader)
    //    {
    //        base.Deserialize(reader);

    //        int version = reader.ReadInt();
    //    }
    //}

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsItemID : LivreSkills
    {
        [Constructable]
        public LivreSkillsItemID()
            : this(SkillName.ItemID, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsItemID(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Item ID";
        }

        public LivreSkillsItemID(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsArmsLore : LivreSkills
    {
        [Constructable]
        public LivreSkillsArmsLore()
            : this(SkillName.ArmsLore, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsArmsLore(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Arms Lore";
        }

        public LivreSkillsArmsLore(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsParry : LivreSkills
    {
        [Constructable]
        public LivreSkillsParry()
            : this(SkillName.Parry, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsParry(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Parry";
        }

        public LivreSkillsParry(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsBegging : LivreSkills
    {
        [Constructable]
        public LivreSkillsBegging()
            : this(SkillName.Begging, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsBegging(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Begging";
        }

        public LivreSkillsBegging(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsBlacksmith : LivreSkills
    {
        [Constructable]
        public LivreSkillsBlacksmith()
            : this(SkillName.Blacksmith, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsBlacksmith(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Blacksmith";
        }

        public LivreSkillsBlacksmith(Serial serial)
            : base(serial)
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
    }

    //[FlipableAttribute(0xFBE, 0xFBD)]
    //public class LivreSkillsFletching : LivreSkills
    //{
    //    [Constructable]
    //    public LivreSkillsFletching()
    //        : this(SkillName.Fletching, 0.0, 0.0)
    //    {
    //    }

    //    [Constructable]
    //    public LivreSkillsFletching(SkillName skill, double value, double growvalue)
    //        : base(skill, value, growvalue)
    //    {
    //        Name = "Étude : Fletching";
    //    }

    //    public LivreSkillsFletching(Serial serial)
    //        : base(serial)
    //    {
    //    }

    //    public override void Serialize(GenericWriter writer)
    //    {
    //        base.Serialize(writer);

    //        writer.Write((int)0); // version
    //    }

    //    public override void Deserialize(GenericReader reader)
    //    {
    //        base.Deserialize(reader);

    //        int version = reader.ReadInt();
    //    }
    //}

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsPeacemaking : LivreSkills
    {
        [Constructable]
        public LivreSkillsPeacemaking()
            : this(SkillName.Peacemaking, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsPeacemaking(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Peacemaking";
        }

        public LivreSkillsPeacemaking(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsCamping : LivreSkills
    {
        [Constructable]
        public LivreSkillsCamping()
            : this(SkillName.Camping, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsCamping(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Camping";
        }

        public LivreSkillsCamping(Serial serial)
            : base(serial)
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
    }

    //[FlipableAttribute(0xFBE, 0xFBD)]
    //public class LivreSkillsCarpentry : LivreSkills
    //{
    //    [Constructable]
    //    public LivreSkillsCarpentry()
    //        : this(SkillName.Carpentry, 0.0, 0.0)
    //    {
    //    }

    //    [Constructable]
    //    public LivreSkillsCarpentry(SkillName skill, double value, double growvalue)
    //        : base(skill, value, growvalue)
    //    {
    //        Name = "Étude : Carpentry";
    //    }

    //    public LivreSkillsCarpentry(Serial serial)
    //        : base(serial)
    //    {
    //    }

    //    public override void Serialize(GenericWriter writer)
    //    {
    //        base.Serialize(writer);

    //        writer.Write((int)0); // version
    //    }

    //    public override void Deserialize(GenericReader reader)
    //    {
    //        base.Deserialize(reader);

    //        int version = reader.ReadInt();
    //    }
    //}

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsCartography : LivreSkills
    {
        [Constructable]
        public LivreSkillsCartography()
            : this(SkillName.Cartography, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsCartography(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Cartography";
        }

        public LivreSkillsCartography(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsCooking : LivreSkills
    {
        [Constructable]
        public LivreSkillsCooking()
            : this(SkillName.Cooking, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsCooking(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Cooking";
        }

        public LivreSkillsCooking(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsDetectHidden : LivreSkills
    {
        [Constructable]
        public LivreSkillsDetectHidden()
            : this(SkillName.DetectHidden, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsDetectHidden(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Detect Hidden";
        }

        public LivreSkillsDetectHidden(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsDiscordance : LivreSkills
    {
        [Constructable]
        public LivreSkillsDiscordance()
            : this(SkillName.Discordance, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsDiscordance(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Discordance";
        }

        public LivreSkillsDiscordance(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsEvalInt : LivreSkills
    {
        [Constructable]
        public LivreSkillsEvalInt()
            : this(SkillName.EvalInt, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsEvalInt(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Eval Int";
        }

        public LivreSkillsEvalInt(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsHealing : LivreSkills
    {
        [Constructable]
        public LivreSkillsHealing()
            : this(SkillName.Healing, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsHealing(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Healing";
        }

        public LivreSkillsHealing(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsFishing : LivreSkills
    {
        [Constructable]
        public LivreSkillsFishing()
            : this(SkillName.Fishing, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsFishing(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Fishing";
        }

        public LivreSkillsFishing(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsForensics : LivreSkills
    {
        [Constructable]
        public LivreSkillsForensics()
            : this(SkillName.Forensics, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsForensics(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Forensics";
        }

        public LivreSkillsForensics(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsHerding : LivreSkills
    {
        [Constructable]
        public LivreSkillsHerding()
            : this(SkillName.Herding, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsHerding(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Herding";
        }

        public LivreSkillsHerding(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsHiding : LivreSkills
    {
        [Constructable]
        public LivreSkillsHiding()
            : this(SkillName.Hiding, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsHiding(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Hiding";
        }

        public LivreSkillsHiding(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsProvocation : LivreSkills
    {
        [Constructable]
        public LivreSkillsProvocation()
            : this(SkillName.Provocation, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsProvocation(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Provocation";
        }

        public LivreSkillsProvocation(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsInscribe : LivreSkills
    {
        [Constructable]
        public LivreSkillsInscribe()
            : this(SkillName.Inscribe, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsInscribe(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Inscribe";
        }

        public LivreSkillsInscribe(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsLockpicking : LivreSkills
    {
        [Constructable]
        public LivreSkillsLockpicking()
            : this(SkillName.Lockpicking, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsLockpicking(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Lockpicking";
        }

        public LivreSkillsLockpicking(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsMagery : LivreSkills
    {
        [Constructable]
        public LivreSkillsMagery()
            : this(SkillName.Magery, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsMagery(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Magery";
        }

        public LivreSkillsMagery(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsMagicResist : LivreSkills
    {
        [Constructable]
        public LivreSkillsMagicResist()
            : this(SkillName.MagicResist, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsMagicResist(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Magic Resist";
        }

        public LivreSkillsMagicResist(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsTactics : LivreSkills
    {
        [Constructable]
        public LivreSkillsTactics()
            : this(SkillName.Tactics, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsTactics(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Tactics";
        }

        public LivreSkillsTactics(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsSnooping : LivreSkills
    {
        [Constructable]
        public LivreSkillsSnooping()
            : this(SkillName.Snooping, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsSnooping(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Snooping";
        }

        public LivreSkillsSnooping(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsMusicianship : LivreSkills
    {
        [Constructable]
        public LivreSkillsMusicianship()
            : this(SkillName.Musicianship, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsMusicianship(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Musicianship";
        }

        public LivreSkillsMusicianship(Serial serial)
            : base(serial)
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
    }
	[FlipableAttribute(0xFBE, 0xFBD)]
	public class LivreSkillsNecromancy : LivreSkills
	{
		[Constructable]
		public LivreSkillsNecromancy()
			: this(SkillName.Necromancy, 0.0, 0.0)
		{
		}

		[Constructable]
		public LivreSkillsNecromancy(SkillName skill, double value, double growvalue)
			: base(skill, value, growvalue)
		{
			Name = "Étude : Necromancy";
		}

		public LivreSkillsNecromancy(Serial serial)
			: base(serial)
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
	}

	[FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsPoisoning : LivreSkills
    {
        [Constructable]
        public LivreSkillsPoisoning()
            : this(SkillName.Poisoning, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsPoisoning(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Poisoning";
        }

        public LivreSkillsPoisoning(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsArchery : LivreSkills
    {
        [Constructable]
        public LivreSkillsArchery()
            : this(SkillName.Archery, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsArchery(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Archery";
        }

        public LivreSkillsArchery(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsSpiritSpeak : LivreSkills
    {
        [Constructable]
        public LivreSkillsSpiritSpeak()
            : this(SkillName.SpiritSpeak, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsSpiritSpeak(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Spirit Speak";
        }

        public LivreSkillsSpiritSpeak(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsStealing : LivreSkills
    {
        [Constructable]
        public LivreSkillsStealing()
            : this(SkillName.Stealing, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsStealing(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Stealing";
        }

        public LivreSkillsStealing(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsTailoring : LivreSkills
    {
        [Constructable]
        public LivreSkillsTailoring()
            : this(SkillName.Tailoring, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsTailoring(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Tailoring";
        }

        public LivreSkillsTailoring(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsAnimalTaming : LivreSkills
    {
        [Constructable]
        public LivreSkillsAnimalTaming()
            : this(SkillName.AnimalTaming, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsAnimalTaming(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Animal Taming";
        }

        public LivreSkillsAnimalTaming(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsTasteID : LivreSkills
    {
        [Constructable]
        public LivreSkillsTasteID()
            : this(SkillName.TasteID, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsTasteID(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Taste ID";
        }

        public LivreSkillsTasteID(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsTinkering : LivreSkills
    {
        [Constructable]
        public LivreSkillsTinkering()
            : this(SkillName.Tinkering, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsTinkering(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Tinkering";
        }

        public LivreSkillsTinkering(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsTracking : LivreSkills
    {
        [Constructable]
        public LivreSkillsTracking()
            : this(SkillName.Tracking, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsTracking(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Tracking";
        }

        public LivreSkillsTracking(Serial serial)
            : base(serial)
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
    }

    //[FlipableAttribute(0xFBE, 0xFBD)]
    //public class LivreSkillsVeterinary : LivreSkills
    //{
    //    [Constructable]
    //    public LivreSkillsVeterinary()
    //        : this(SkillName.Veterinary, 0.0, 0.0)
    //    {
    //    }

    //    [Constructable]
    //    public LivreSkillsVeterinary(SkillName skill, double value, double growvalue)
    //        : base(skill, value, growvalue)
    //    {
    //        Name = "Étude : Veterinary";
    //    }

    //    public LivreSkillsVeterinary(Serial serial)
    //        : base(serial)
    //    {
    //    }

    //    public override void Serialize(GenericWriter writer)
    //    {
    //        base.Serialize(writer);

    //        writer.Write((int)0); // version
    //    }

    //    public override void Deserialize(GenericReader reader)
    //    {
    //        base.Deserialize(reader);

    //        int version = reader.ReadInt();
    //    }
    //}

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsSwords : LivreSkills
    {
        [Constructable]
        public LivreSkillsSwords()
            : this(SkillName.Swords, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsSwords(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Swords";
        }

        public LivreSkillsSwords(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsMacing : LivreSkills
    {
        [Constructable]
        public LivreSkillsMacing()
            : this(SkillName.Macing, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsMacing(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Macing";
        }

        public LivreSkillsMacing(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsFencing : LivreSkills
    {
        [Constructable]
        public LivreSkillsFencing()
            : this(SkillName.Fencing, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsFencing(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Fencing";
        }

        public LivreSkillsFencing(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsWrestling : LivreSkills
    {
        [Constructable]
        public LivreSkillsWrestling()
            : this(SkillName.Wrestling, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsWrestling(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Wrestling";
        }

        public LivreSkillsWrestling(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsLumberjacking : LivreSkills
    {
        [Constructable]
        public LivreSkillsLumberjacking()
            : this(SkillName.Lumberjacking, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsLumberjacking(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Lumberjacking";
        }

        public LivreSkillsLumberjacking(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsMining : LivreSkills
    {
        [Constructable]
        public LivreSkillsMining()
            : this(SkillName.Mining, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsMining(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Mining";
        }

        public LivreSkillsMining(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsMeditation : LivreSkills
    {
        [Constructable]
        public LivreSkillsMeditation()
            : this(SkillName.Meditation, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsMeditation(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Meditation";
        }

        public LivreSkillsMeditation(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsStealth : LivreSkills
    {
        [Constructable]
        public LivreSkillsStealth()
            : this(SkillName.Stealth, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsStealth(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Equitation";
        }

        public LivreSkillsStealth(Serial serial)
            : base(serial)
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
    }

    [FlipableAttribute(0xFBE, 0xFBD)]
    public class LivreSkillsRemoveTrap : LivreSkills
    {
        [Constructable]
        public LivreSkillsRemoveTrap()
            : this(SkillName.RemoveTrap, 0.0, 0.0)
        {
        }

        [Constructable]
        public LivreSkillsRemoveTrap(SkillName skill, double value, double growvalue)
            : base(skill, value, growvalue)
        {
            Name = "Étude : Remove Trap";
        }

        public LivreSkillsRemoveTrap(Serial serial)
            : base(serial)
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
    }
}
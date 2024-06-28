using System;
using System.IO;
using Server.Commands;

namespace Server.Misc
{
    public class BookGenerator
    {
        public static void Initialize()
        {
			CommandSystem.Register("Bookgen", AccessLevel.Administrator, new CommandEventHandler(OnBookGen));
        }

        #region Template
        private static string m_TemplatePrefix = @"using System;
using Server;
using Server.Mobiles;
using Server.Engines.Craft;

namespace Server.Items
{
";            

                private static string m_Template = @"index = AddCraft(typeof(LivreSkills{skill}), ""Livre d'étude (skills)"", ""{skill}"", 0.0, 0.0, typeof(LivreVierge), ""Livre vierge"", 1, ""Vous n'avez pas de livre vierge."");
            AddAptitude(index, NAptitude.Etude, 1);
            AddAptitude(index, NAptitude.Ecriture, 1);

";

//        private static string m_Template = @"Add(new LivreEtudeBuyInfo(typeof(LivreSkills{skill}), SkillName.{skill}, 45.0, 350, Utility.RandomMinMax(8, 12), 0xFBE, 0));
//";

//        private static string m_Template = @"
//    [FlipableAttribute(0xFBE, 0xFBD)]
//	public class LivreSkills{skill} : LivreSkills
//	{
//        [Constructable]
//        public LivreSkills{skill}() : this(SkillName.{skill}, 0.0, 0.0)
//        {
//        }
//
//        [Constructable]
//        public LivreSkills{skill}(SkillName skill, double value, double growvalue) : base(skill, value, growvalue)
//        {
//            Name = ""Livre de compétences : {namer}"";
//        }
//
//        public LivreSkills{skill}(Serial serial) : base(serial)
//        {
//        }
//
//		public override void Serialize( GenericWriter writer )
//		{
//            base.Serialize(writer);
//
//            writer.Write((int)0); // version
//		}
//
//		public override void Deserialize( GenericReader reader )
//		{
//			base.Deserialize( reader );
//
//			int version = reader.ReadInt();
//        }
//	}
//";

        private static string m_TemplateSuffix = @"}";
        #endregion

        [Usage("BookGen"),
        Description("Brings up the item script generator gump.")]
        private static void OnBookGen(CommandEventArgs e)
        {
            string m_TotalTemplate = m_TemplatePrefix;

            for (int i = 0; i < 54; i++)
            {
                SkillName skill = (SkillName)i;
                string final = m_Template;

                final = final.Replace("{namer}", skill.ToString());
                final = final.Replace("{skill}", skill.ToString());;

                m_TotalTemplate = m_TotalTemplate + final;
            }

            m_TotalTemplate = m_TotalTemplate + m_TemplateSuffix;

            StreamWriter writer = null;

            string path = "TheBox\\" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString();

            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            path = Path.Combine(path, string.Format(@"{0}.cs", "LivreDeSkillsList"));
            writer = new StreamWriter(path, false);
            writer.Write(m_TotalTemplate);

            e.Mobile.SendMessage(0x40, "Skill list" + " saved to {0}", path);

            if (writer != null)
                writer.Close();
        }
    }
}
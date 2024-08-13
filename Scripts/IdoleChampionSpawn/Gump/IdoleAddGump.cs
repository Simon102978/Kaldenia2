using Server.Engines.Idole;
using Server.Mobiles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Server.Gumps
{
    public class IdoleAddGump : Gump
    {
        private readonly string m_SearchString;
        private readonly ArrayList m_SearchResults;
        private readonly int m_Page;
        private readonly int m_EntryIndex;

        private IdoleChampionSpawn m_Stone;

		private IdoleInfo m_IdoleInfo;

		private IdoleLevelInfo m_Niveau;


        public IdoleAddGump(CustomPlayerMobile from, string searchString, int page, ArrayList searchResults, bool explicitSearch, int entryindex, IdoleChampionSpawn stone, IdoleInfo info, IdoleLevelInfo level) : base(50, 50)
        {
	        m_Stone  = stone;
			m_IdoleInfo = info;
			m_Niveau = level;

            m_SearchString = searchString;
            m_SearchResults = searchResults;
            m_Page = page;

            m_EntryIndex = entryindex;

            from.CloseGump(typeof(IdoleAddGump));

            AddPage(0);


            AddBackground(0, 0, 420, 280, 5054);

            AddImageTiled(10, 10, 400, 20, 2624);
            AddAlphaRegion(10, 10, 400, 20);
            AddImageTiled(41, 11, 184, 18, 0xBBC);
            AddImageTiled(42, 12, 182, 16, 2624);
            AddAlphaRegion(42, 12, 182, 16);

            AddButton(10, 9, 4011, 4013, 1, GumpButtonType.Reply, 0);
            AddTextEntry(44, 10, 180, 20, 0x480, 0, searchString);

            AddHtmlLocalized(230, 10, 100, 20, 3010005, 0x7FFF, false, false);

            AddImageTiled(10, 40, 400, 200, 2624);
            AddAlphaRegion(10, 40, 400, 200);


            if (searchResults.Count > 0)
            {
                for (int i = (page * 10); i < ((page + 1) * 10) && i < searchResults.Count; ++i)
                {
                    int index = i % 10;

                    SearchEntry se = (SearchEntry)searchResults[i];

                    string labelstr = se.EntryType.Name;

                    if (se.Parameters.Length > 0)
                    {
                        for (int j = 0; j < se.Parameters.Length; j++)
                        {
                            labelstr += ", " + se.Parameters[j].Name;
                        }
                    }

                    AddLabel(44, 39 + (index * 20), 0x480, labelstr);
                    AddButton(10, 39 + (index * 20), 4023, 4025, 4 + i, GumpButtonType.Reply, 0);

                }
            }
            else
            {
                AddLabel(15, 44, 0x480, explicitSearch ? "Nothing matched your search terms." : "No results to display.");
            }

            AddImageTiled(10, 250, 400, 20, 2624);
            AddAlphaRegion(10, 250, 400, 20);

            if (m_Page > 0)
                AddButton(10, 249, 4014, 4016, 2, GumpButtonType.Reply, 0);
            else
                AddImage(10, 249, 4014);

            AddHtmlLocalized(44, 250, 170, 20, 1061028, m_Page > 0 ? 0x7FFF : 0x5EF7, false, false); // Previous page

            if (((m_Page + 1) * 10) < searchResults.Count)
                AddButton(210, 249, 4005, 4007, 3, GumpButtonType.Reply, 0);
            else
                AddImage(210, 249, 4005);

            AddHtmlLocalized(244, 250, 170, 20, 1061027, ((m_Page + 1) * 10) < searchResults.Count ? 0x7FFF : 0x5EF7, false, false); // Next page
        }

        private static readonly Type typeofItem = typeof(Item), typeofMobile = typeof(Mobile);

        private class SearchEntry
        {
            public Type EntryType;
            public ParameterInfo[] Parameters;
        }
        private static void Match(string match, IReadOnlyList<Type> types, IList results)
        {
            if (match.Length == 0)
                return;

            match = match.ToLower();

            for (int i = 0; i < types.Count; ++i)
            {
                Type t = types[i];

                if ((typeofMobile.IsAssignableFrom(t) || typeofItem.IsAssignableFrom(t)) && t.Name.ToLower().IndexOf(match) >= 0 && !results.Contains(t))
                {
                    ConstructorInfo[] ctors = t.GetConstructors();

                    for (int j = 0; j < ctors.Length; ++j)
                    {
                        if ( /*ctors[j].GetParameters().Length == 0 && */ ctors[j].IsDefined(typeof(ConstructableAttribute), false))
                        {
                            SearchEntry s = new SearchEntry
                            {
                                EntryType = t,
                                Parameters = ctors[j].GetParameters()
                            };
                            //results.Add( t );
                            results.Add(s);
                            //break;
                        }
                    }
                }
            }
        }

        public static ArrayList Match(string match)
        {
            ArrayList results = new ArrayList();
            Type[] types;

            Assembly[] asms = ScriptCompiler.Assemblies;

            for (int i = 0; i < asms.Length; ++i)
            {
                types = ScriptCompiler.GetTypeCache(asms[i]).Types;

                Match(match, types, results);
            }

            types = ScriptCompiler.GetTypeCache(Core.Assembly).Types;
            Match(match, types, results);

            results.Sort(new TypeNameComparer());

            return results;
        }

        private class TypeNameComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                SearchEntry a = x as SearchEntry;
                SearchEntry b = y as SearchEntry;

                return a.EntryType.Name.CompareTo(b.EntryType.Name);
            }
        }


        public override void OnResponse(Network.NetState sender, RelayInfo info)
        {

            if (sender.Mobile is CustomPlayerMobile from)
            {
                 switch (info.ButtonID)
                {
                    case 1: // Search
                        {
                            TextRelay te = info.GetTextEntry(0);
                            string match = (te == null ? "" : te.Text.Trim());

                            if (match.Length < 3)
                            {
                                from.SendMessage("Invalid search string.");
                                from.SendGump(new IdoleAddGump(from, match, m_Page, m_SearchResults, false, m_EntryIndex, m_Stone,m_IdoleInfo,m_Niveau));
                            }
                            else
                            {
                                from.SendGump(new IdoleAddGump(from, match, 0, Match(match), true, m_EntryIndex, m_Stone,m_IdoleInfo,m_Niveau));
                            }

                            break;
                        }
                    case 2: // Previous page
                        {
                            if (m_Page > 0)
                                from.SendGump(new IdoleAddGump(from, m_SearchString, m_Page - 1, m_SearchResults, true, m_EntryIndex, m_Stone,m_IdoleInfo,m_Niveau));

                            break;
                        }
                    case 3: // Next page
                        {
                            if ((m_Page + 1) * 10 < m_SearchResults.Count)
                                from.SendGump(new IdoleAddGump(from, m_SearchString, m_Page + 1, m_SearchResults, true, m_EntryIndex, m_Stone,m_IdoleInfo,m_Niveau));

                            break;
                        }
                    default:
                        {
                            int index = info.ButtonID - 4;

                             Type type = ((SearchEntry)m_SearchResults[index]).EntryType;

                             if (type.IsSubclassOf(typeof(Mobile)))
                             {

                                m_Niveau.Types[m_EntryIndex] = new IdoleTypeInfo(1, type) ;
                                from.SendGump(new IdoleNiveauGump(from, m_Stone,m_IdoleInfo,m_Niveau, 0));	
                                
                             }
                             else
                             {
                                from.SendMessage("Seulement les mobiles peuvent être selectionné.");
                                from.SendGump(new IdoleAddGump(from, m_SearchString, m_Page , m_SearchResults, true, m_EntryIndex, m_Stone,m_IdoleInfo,m_Niveau));
                             }



                            break;
                        }
                }
            }

           
        }
    }
}

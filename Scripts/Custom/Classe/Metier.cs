using System;
using System.Linq;
using System.Runtime.InteropServices;
using Server.Items;
using System.Collections.Generic;

namespace Server.Misc
{
 		
        class MetierInit 
        {
				public static void Configure()
				{
					Metier.RegisterMetier(new Metier(0, "Aucune",0,0,false, new List<int>(){1,2,3,4}, new Dictionary<SkillName, double>() { },new List<int>(){} )); 
					Metier.RegisterMetier(new Metier(1, "Historien",0,1,false, new List<int>(){5,6,7}, new Dictionary<SkillName, double>() { {SkillName.Cartography, 100}, {SkillName.Inscribe, 100}, },new List<int>(){1} )); 
					Metier.RegisterMetier(new Metier(2, "Ingénieur",0,1,false, new List<int>(){5,8,9}, new Dictionary<SkillName, double>() { {SkillName.Blacksmith, 100}, {SkillName.Tinkering, 100}, },new List<int>(){2} )); 
					Metier.RegisterMetier(new Metier(3, "Styliste",0,1,false, new List<int>(){6,8,10}, new Dictionary<SkillName, double>() { {SkillName.Tailoring, 100}, {SkillName.Carpentry, 100}, },new List<int>(){3} )); 
					Metier.RegisterMetier(new Metier(4, "Épicier",0,1,false, new List<int>(){7,9,10}, new Dictionary<SkillName, double>() { {SkillName.Alchemy, 100}, {SkillName.Cooking, 100}, },new List<int>(){4} )); 
					Metier.RegisterMetier(new Metier(5, "Historien Ingénieur",1,2,false, new List<int>(){11,12}, new Dictionary<SkillName, double>() { {SkillName.Blacksmith, 100}, {SkillName.Tinkering, 100}, {SkillName.Cartography, 100}, {SkillName.Inscribe, 100}, },new List<int>(){1,2} )); 
					Metier.RegisterMetier(new Metier(6, "Historien Styliste",1,2,false, new List<int>(){11,13}, new Dictionary<SkillName, double>() { {SkillName.Cartography, 100}, {SkillName.Inscribe, 100}, {SkillName.Tailoring, 100}, {SkillName.Carpentry, 100}, },new List<int>(){1,3} )); 
					Metier.RegisterMetier(new Metier(7, "Historien Épicier",1,2,false, new List<int>(){12,14}, new Dictionary<SkillName, double>() { {SkillName.Alchemy, 100}, {SkillName.Cooking, 100}, {SkillName.Cartography, 100}, {SkillName.Inscribe, 100}, },new List<int>(){1,4} )); 
					Metier.RegisterMetier(new Metier(8, "Ingénieur Styliste",1,2,false, new List<int>(){11,14}, new Dictionary<SkillName, double>() { {SkillName.Blacksmith, 100}, {SkillName.Tinkering, 100}, {SkillName.Tailoring, 100}, {SkillName.Carpentry, 100}, },new List<int>(){2,3} )); 
					Metier.RegisterMetier(new Metier(9, "Ingénieur Épicier",1,2,false, new List<int>(){12,14}, new Dictionary<SkillName, double>() { {SkillName.Alchemy, 100}, {SkillName.Cooking, 100}, {SkillName.Blacksmith, 100}, {SkillName.Tinkering, 100}, },new List<int>(){2,4} )); 
					Metier.RegisterMetier(new Metier(10, "Styliste Épicier",1,2,false, new List<int>(){}, new Dictionary<SkillName, double>() { {SkillName.Alchemy, 100}, {SkillName.Cooking, 100}, {SkillName.Tailoring, 100}, {SkillName.Carpentry, 100}, },new List<int>(){3,4} )); 
					Metier.RegisterMetier(new Metier(11, "Historien Ingénieur Styliste",2,3,false, new List<int>(){}, new Dictionary<SkillName, double>() { {SkillName.Blacksmith, 100}, {SkillName.Tinkering, 100}, {SkillName.Cartography, 100}, {SkillName.Inscribe, 100}, {SkillName.Tailoring, 100}, {SkillName.Carpentry, 100}, },new List<int>(){1,2,3} )); 
					Metier.RegisterMetier(new Metier(12, "Historien Ingénieur Épicier",2,3,false, new List<int>(){}, new Dictionary<SkillName, double>() { {SkillName.Alchemy, 100}, {SkillName.Cooking, 100}, {SkillName.Blacksmith, 100}, {SkillName.Tinkering, 100}, {SkillName.Cartography, 100}, {SkillName.Inscribe, 100}, },new List<int>(){1,2,4} )); 
					Metier.RegisterMetier(new Metier(13, "Historien Épicier Styliste",2,3,false, new List<int>(){}, new Dictionary<SkillName, double>() { {SkillName.Alchemy, 100}, {SkillName.Cooking, 100}, {SkillName.Cartography, 100}, {SkillName.Inscribe, 100}, {SkillName.Tailoring, 100}, {SkillName.Carpentry, 100}, },new List<int>(){1,4,3} )); 
					Metier.RegisterMetier(new Metier(14, "Ingénieur Styliste Épicier",2,3,false, new List<int>(){}, new Dictionary<SkillName, double>() { {SkillName.Alchemy, 100}, {SkillName.Cooking, 100}, {SkillName.Blacksmith, 100}, {SkillName.Tinkering, 100}, {SkillName.Tailoring, 100}, {SkillName.Carpentry, 100}, },new List<int>(){2,3,4} )); 
				}
		}
}

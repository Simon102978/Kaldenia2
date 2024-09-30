using System;
using System.Collections.Generic;

namespace Server.Engines.Craft
{
    public class CraftItemCol : System.Collections.CollectionBase
    {
        public int Add(CraftItem craftItem)
        {
            return List.Add(craftItem);
        }

        public void Remove(int index)
        {
            if (index > Count - 1 || index < 0)
            {
            }
            else
            {
                List.RemoveAt(index);
            }
        }

        public CraftItem GetAt(int index)
        {
            return (CraftItem)List[index];
        }

        public CraftItem SearchForSubclass(Type type)
        {
            for (int i = 0; i < List.Count; i++)
            {
                CraftItem craftItem = (CraftItem)List[i];

                if (craftItem.ItemType == type || type.IsSubclassOf(craftItem.ItemType))
                    return craftItem;
            }

            return null;
        }

        public CraftItem SearchFor(Type type)
        {
            for (int i = 0; i < List.Count; i++)
            {
                CraftItem craftItem = (CraftItem)List[i];
                if (craftItem.ItemType == type)
                {
                    return craftItem;
                }
            }
            return null;
        }
    
	public CraftItem GetPrevCraftItem(CraftItem currentItem)
	{
		int index = List.IndexOf(currentItem);
		if (index > 0)
		{
			return (CraftItem)List[index - 1];
		}
		else if (index == 0)
		{
			// Retourner le dernier élément si on est au début
			return (CraftItem)List[List.Count - 1];
		}
		return null;
	}

	public CraftItem GetNextCraftItem(CraftItem currentItem)
	{
		int index = List.IndexOf(currentItem);
		if (index < List.Count - 1)
		{
			return (CraftItem)List[index + 1];
		}
		else if (index == List.Count - 1)
		{
			// Retourner le premier élément si on est à la fin
			return (CraftItem)List[0];
		}
		return null;
	}
}
}


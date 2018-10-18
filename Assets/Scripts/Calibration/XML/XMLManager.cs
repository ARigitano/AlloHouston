using UnityEngine;

namespace VRCalibrationTool
{
    /// <summary>
    /// Stores or loads instantiable objects' coordinates for the auto calibration phase
    /// </summary>
    public class XMLManager : MonoBehaviour
    {
        private static XMLManager s_instance;
        public static XMLManager instance
        {
            get
            {
                return s_instance;
            }
        }

        public ItemDatabase itemDB;

        private void Awake()
        {
            if (s_instance == null)
            {
                s_instance = this;
            }
            else if (s_instance != this)
            {
                Destroy(this);
            }
            itemDB = ItemDatabase.Load();
        }
        
        public void SaveItems()
        {
            itemDB.Save();
        }
        
        public void InsertOrReplaceItem(ItemEntry itemEntry)
        {
            //Entering or updating those coordinates inside the XML file
            bool added = false;
            for (int i = 0; i < itemDB.list.Count; i++)
            {
                if (itemDB.list[i].type == itemEntry.type)
                {
#if UNITY_EDITOR
                    Debug.Log("Item entry modified in XML: " + itemEntry.type);
#endif
                    itemDB.list[i] = itemEntry;
                    added = true;
                }
            }
            if (!added)
            {
#if UNITY_EDITOR
                itemDB.list.Add(itemEntry);
#endif
                Debug.Log("Item entry created in XML: " + itemEntry.type);
            }
            SaveItems();
        }
    }
}



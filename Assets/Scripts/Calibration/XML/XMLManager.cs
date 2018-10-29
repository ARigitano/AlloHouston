using UnityEngine;

namespace CRI.HelloHouston.Calibration.XML
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

        public BlockDatabase blockDB;

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
            blockDB = BlockDatabase.Load();
        }

        public void SaveItems()
        {
            blockDB.Save();
        }

        public void InsertOrReplaceItem(BlockEntry blockEntry)
        {
            //Entering or updating those coordinates inside the XML file
            bool added = false;
            for (int i = 0; i < blockDB.list.Count; i++)
            {
                if (blockDB.list[i].index == blockEntry.index && blockDB.list[i].type == blockEntry.type)
                {
                    Debug.Log("Item entry modified in XML: " + blockEntry.type);
                    blockDB.list[i] = blockEntry;
                    added = true;
                }
            }
            if (!added)
            {
                blockDB.list.Add(blockEntry);
                Debug.Log("Item entry created in XML: " + blockEntry.type);
            }
            SaveItems();
        }
    }
}


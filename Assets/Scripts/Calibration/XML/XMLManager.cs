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

        public void InsertOrReplaceRoom(RoomEntry roomEntry)
        {
            //Entering or updating those coordinates inside the XML file
            bool added = false;
            for (int i = 0; i < blockDB.rooms.Count; i++)
            {
                if (blockDB.rooms[i].index == roomEntry.index)
                {
                    blockDB.rooms[i] = roomEntry;
                    added = true;
                }
            }
            if (!added)
            {
                blockDB.rooms.Add(roomEntry);
            }
            SaveItems();
        }
    }
}


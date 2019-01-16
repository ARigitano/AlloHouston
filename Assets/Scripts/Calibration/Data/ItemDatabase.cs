using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

namespace CRI.HelloHouston.Calibration.Data
{
    /// <summary>
    /// Serializable XML entry
    /// </summary>
    [Serializable]
    [CreateAssetMenu(fileName = "New ItemDatabase", menuName = "Calibration/ItemDatabase", order = 1)]
    public class ItemDatabase : ScriptableObject
    {
        public List<RoomEntry> rooms;

        public const string path = "CalibrationData/item_data.json";

        /// <summary>
        /// Save items' coordinates in an XML file at the default path.
        /// </summary>
        public void Save()
        {
            Save(path);
        }

        /// <summary>
        /// Save items' coordinates in an XML file at the location of the path.
        /// </summary>
        /// <param name="path">The path of the XML file</param>
        public void Save(string path)
        {
            string dataJson = JsonUtility.ToJson(this, true);
            string filePath = Path.Combine(Application.streamingAssetsPath, path);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            using (var streamWriter = new StreamWriter(fileStream))
            {
                streamWriter.Write(dataJson);
            }
        }

        /// <summary>
        /// Loads the information stored in the XML file located at the default path.
        /// </summary>
        /// <returns>An item database</returns>
        public static ItemDatabase Load()
        {
            return Load(path);
        }

        /// <summary>
        /// Loads the informations stored in an XML file
        /// </summary>
        /// <param name="path">Path of the xml file</param>
        /// <returns>An item database</returns>
        public static ItemDatabase Load(string path)
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, path);
            var res = CreateInstance<ItemDatabase>();
            if (File.Exists(filePath))
                JsonUtility.FromJsonOverwrite(File.ReadAllText(filePath), res);
            return res;
        }
    }
}
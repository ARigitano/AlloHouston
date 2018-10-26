using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

namespace VRCalibrationTool
{
    /// <summary>
    /// Serializable XML entry
    /// </summary>
    [System.Serializable]
    [XmlRoot(ElementName = "block_database")]
    public class BlockDatabase
    {
        [XmlArrayItem(typeof(Room), ElementName = "room")]
        [XmlArray("rooms")]
        public List<BlockEntry> list = new List<BlockEntry>();

        public const string path = "XML/block_data.xml";

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
            XmlSerializer serializer = new XmlSerializer(typeof(BlockDatabase));
            using (var stream = new FileStream(Path.Combine(Application.streamingAssetsPath, path), FileMode.Create))
            {
                serializer.Serialize(stream, this);
                stream.Close();
            }
        }

        /// <summary>
        /// Loads the information stored in the XML file located at the default path.
        /// </summary>
        /// <returns>An item database</returns>
        public static BlockDatabase Load()
        {
            return Load(path);
        }

        /// <summary>
        /// Loads the informations stored in an XML file
        /// </summary>
        /// <param name="path">Path of the xml file</param>
        /// <returns>An item database</returns>
        public static BlockDatabase Load(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BlockDatabase));
            string completePath = Path.Combine(Application.streamingAssetsPath, path);
            if (!File.Exists(path))
                return new BlockDatabase();
            using (var stream = new FileStream(Path.Combine(Application.streamingAssetsPath, path), FileMode.Open))
            {
                var itemDB = serializer.Deserialize(stream) as BlockDatabase;
                stream.Close();
                return itemDB;
            }
        }
    }
}
using System;
using System.Globalization;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
using VRCalibrationTool;

namespace CRI.HelloHouston.Calibration.XML
{
    /// <summary>
    /// An item entry.
    /// </summary>
    [System.Serializable]
    public class BlockEntry
    {
        /// <summary>
        /// Path of the ItemEntry prefab.
        /// </summary>
        public string prefabPath
        {
            get
            {
                return type + index.ToString();
            }
        }
        /// <summary>
        /// Index of ItemEntry.
        /// </summary>
        [XmlAttribute("index")]
        public int index;
        /// <summary>
        /// Type of ItemEntry.
        /// </summary>
        [XmlAttribute("type")]
        public BlockType type;
        /// <summary>
        /// Points of calibration.
        /// </summary>
        [XmlArrayItem(typeof(SerializableVector3), ElementName = "point")]
        [XmlArray("points")]
        public SerializableVector3[] serializablePoints;
        /// <summary>
        /// Date of the calibration of the RoomEntry.
        /// </summary>
        [XmlIgnore]
        public DateTime date
        {
            get
            {
                return DateTime.ParseExact(serializedDate, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            }
            set
            {
                serializedDate = value.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            }
        }
        /// <summary>
        /// Serialized version of the date of the calibration.
        /// </summary>
        [XmlAttribute("date")]
        public string serializedDate
        {
            get;
            private set;
        }
        /// <summary>
        /// Points of calibration
        /// </summary>
        [XmlIgnore]
        public Vector3[] points
        {
            get
            {
                return serializablePoints.Select(x => x.Vector3).ToArray();
            }
        }

        public BlockEntry()
        { }

        public BlockEntry(int index, BlockType type, PositionTag[] points, DateTime date)
        {
            this.index = index;
            this.type = type;
            this.date = date;
            serializablePoints = new SerializableVector3[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                serializablePoints[i] = new SerializableVector3(points[i].transform.position);
            }
        }
    }
}

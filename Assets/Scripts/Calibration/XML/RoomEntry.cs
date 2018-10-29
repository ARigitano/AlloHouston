using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
using VRCalibrationTool;

namespace CRI.HelloHouston.Calibration.XML
{
    /// <summary>
    /// A room.
    /// </summary>
    public class RoomEntry
    {
        /// <summary>
        /// Index of ItemEntry.
        /// </summary>
        [XmlAttribute("index")]
        public int index;
        /// <summary>
        /// List of items that made up the room.  
        /// </summary>
        [XmlArrayItem(typeof(BlockEntry), ElementName = "block")]
        [XmlArray("blocks")]
        public BlockEntry[] blocks;
        /// <summary>
        /// The table of the room.
        /// </summary>
        [XmlElement("table")]
        public BlockEntry table;
        /// <summary>
        /// Login of the person who calibrated the ItemEntry.
        /// </summary>
        [XmlAttribute("login")]
        public string login;
        /// <summary>
        /// Points of calibration.
        /// </summary>
        [XmlArrayItem(typeof(SerializableVector3), ElementName = "point")]
        [XmlArray("points")]
        public SerializableVector3[] serializablePoints;
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

        public RoomEntry(int index, BlockEntry[] blocks, BlockEntry table, PositionTag[] points, DateTime date)
        {
            this.index = index;
            this.blocks = blocks;
            this.table = table;
            this.date = date;
            serializablePoints = new SerializableVector3[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                serializablePoints[i] = new SerializableVector3(points[i].transform.position);
            }
        }
    }
}

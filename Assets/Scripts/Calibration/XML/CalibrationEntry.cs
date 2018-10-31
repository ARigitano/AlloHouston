using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;
using VRCalibrationTool;

namespace CRI.HelloHouston.Calibration.XML
{
    public abstract class ItemEntry
    {
        /// <summary>
        /// Name of the item entry
        /// </summary>
        [XmlIgnore]
        public abstract string name { get; }
        /// <summary>
        /// Index of the item entry.
        /// </summary>
        [XmlAttribute("index")]
        public int index;
        /// <summary>
        /// Date of the last calibration.
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

        public ItemEntry(int index, PositionTag[] points, DateTime date)
        {
            this.index = index;
            this.date = date;
            serializablePoints = new SerializableVector3[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                serializablePoints[i] = new SerializableVector3(points[i].transform.position);
            }
        }
    }
}
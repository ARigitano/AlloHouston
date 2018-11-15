using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Serialization;
using VRCalibrationTool;

namespace CRI.HelloHouston.Calibration.XML
{
    [Serializable]
    public abstract class ItemEntry
    {
        /// <summary>
        /// Name of the item entry
        /// </summary>
        public abstract string name { get; }
        /// <summary>
        /// Index of the item entry.
        /// </summary>
        public int index;
        /// <summary>
        /// Date of the last calibration.
        /// </summary>
        public DateTime date
        {
            get
            {
                return DateTime.ParseExact(_serializedDate, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            }
            set
            {
                _serializedDate = value.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            }
        }
        /// <summary>
        /// Serialized version of the date of the calibration.
        /// </summary>
        [SerializeField]
        private string _serializedDate;
        /// <summary>
        /// Points of calibration
        /// </summary>
        public Vector3[] points;

        public ItemEntry()
        {
            date = DateTime.Now;
            points = new Vector3[0];
        }

        public ItemEntry(int index, PositionTag[] points, DateTime date)
        {
            this.index = index;
            this.date = date;
            this.points = new Vector3[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                this.points[i] = points[i].transform.position;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;

namespace VRCalibrationTool
{
    /// <summary>
    /// A room.
    /// </summary>
    public class RoomEntry
    {
        /// <summary>
        /// List of items that made up the room.  
        /// </summary>
        [XmlArrayItem(typeof(ItemEntry), ElementName = "item_entry")]
        [XmlArray("room_items")]
        public List<ItemEntry> roomItems;
        /// <summary>
        /// The table of the room.
        /// </summary>
        [XmlElement("table")]
        public ItemEntry table;
        /// <summary>
        /// Login of the person who calibrated the ItemEntry.
        /// </summary>
        [XmlAttribute("login")]
        public string login;
        /// <summary>
        /// Date of the calibration of the ItemEntry.
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
    }
}

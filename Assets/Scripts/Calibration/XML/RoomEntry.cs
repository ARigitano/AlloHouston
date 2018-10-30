using System;
using System.Xml.Serialization;
using VRCalibrationTool;

namespace CRI.HelloHouston.Calibration.XML
{
    /// <summary>
    /// A room.
    /// </summary>
    public class RoomEntry : CalibrationEntry
    {
        /// <summary>
        /// Name of the room.
        /// </summary>
        public override string name
        {
            get
            {
                return "Room " + index;
            }
        }
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

        public RoomEntry(int index, BlockEntry[] blocks, BlockEntry table, PositionTag[] points, DateTime date) : base(index, points, date)
        {
            this.blocks = blocks;
            this.table = table;
        }
    }
}

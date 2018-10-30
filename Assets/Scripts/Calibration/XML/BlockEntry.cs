using System;
using System.Xml.Serialization;
using VRCalibrationTool;

namespace CRI.HelloHouston.Calibration.XML
{
    /// <summary>
    /// An item entry.
    /// </summary>
    [System.Serializable]
    public class BlockEntry : CalibrationEntry
    {
        /// <summary>
        /// Path of the ItemEntry prefab.
        /// </summary>
        [XmlIgnore]
        public override string name
        {
            get
            {
                return type.ToString() + " " + index.ToString();
            }
        }
        /// <summary>
        /// Type of Block Entry.
        /// </summary>
        [XmlAttribute("type")]
        public BlockType type;

        public BlockEntry(int index, BlockType type, PositionTag[] points, DateTime date) : base(index, points, date)
        {
            this.type = type;
        }
    }
}

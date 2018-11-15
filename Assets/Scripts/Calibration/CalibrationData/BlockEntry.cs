using System;
using System.Xml.Serialization;
using VRCalibrationTool;

namespace CRI.HelloHouston.Calibration.XML
{
    /// <summary>
    /// An item entry.
    /// </summary>
    [Serializable]
    public class BlockEntry : ItemEntry
    {
        /// <summary>
        /// Path of the ItemEntry prefab.
        /// </summary>
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
        public BlockType type;

        public BlockEntry() : base() { }

        public BlockEntry(int index, BlockType type, PositionTag[] points, DateTime date) : base(index, points, date)
        {
            this.type = type;
        }
    }
}

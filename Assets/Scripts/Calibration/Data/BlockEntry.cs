using System;
using VRCalibrationTool;

namespace CRI.HelloHouston.Calibration.Data
{
    /// <summary>
    /// A block entry, a data entity that contains the values of a virtual block.
    /// </summary>
    [Serializable]
    public class BlockEntry : ItemEntry
    {
        /// <summary>
        /// Path of the BlockEntry prefab.
        /// </summary>
        public override string name
        {
            get
            {
                return type.ToString() + " " + index.ToString();
            }
        }
        /// <summary>
        /// Type of BlockEntry.
        /// </summary>
        public BlockType type;

        public BlockEntry() : base() { }

        public BlockEntry(int index, BlockType type, PositionTag[] points, DateTime date) : base(index, points, date)
        {
            this.type = type;
        }
    }
}

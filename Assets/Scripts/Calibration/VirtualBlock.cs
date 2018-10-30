using CRI.HelloHouston.Calibration.XML;
using System;
using UnityEngine;
using VRCalibrationTool;

namespace CRI.HelloHouston.Calibration
{
    public class VirtualBlock : VirtualItem
    {
        /// <summary>
        /// Type of the virtual item.
        /// </summary>
        public override VirtualItemType virtualItemType
        {
            get
            {
                return VirtualItemType.Block;
            }
        }
        [Serializable]
        public struct BlockTypeIndex
        {
            /// <summary>
            /// Type of the block entry.
            /// </summary>
            [Tooltip("Type of the block entry.")]
            public BlockType type;
            /// <summary>
            /// Index of the block entry.
            /// </summary>
            [Tooltip("Index of the block entry.")]
            public int index;
        }
        /// <summary>
        /// Information about the corresponding block entry.
        /// </summary>
        [Tooltip("Information about the block entry.")]
        public BlockTypeIndex block;

        public void Init(BlockEntry block)
        {
            this.block.type = block.type;
            this.block.index = block.index;
            this.lastUpdate = block.date;
            if (block.points.Length >= 3)
                Calibrate(block.points);
        }

        public BlockEntry ToBlockEntry()
        {
            return new BlockEntry(block.index, block.type, virtualPositionTags, lastUpdate);
        }
    }
}

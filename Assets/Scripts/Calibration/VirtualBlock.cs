using CRI.HelloHouston.Calibration.XML;
using System;
using UnityEngine;
using VRCalibrationTool;

namespace CRI.HelloHouston.Calibration
{
    public class VirtualBlock : VirtualObject
    {
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
        /// <summary>
        /// Information about the last update.
        /// </summary>
        [Tooltip("Information about the last update.")]
        public DateTime lastUpdate;

        public void Init(BlockEntry block)
        {
            this.block.type = block.type;
            this.block.index = block.index;
            this.lastUpdate = block.date;
            Calibrate(block.points);
        }

        public BlockEntry ToBlockEntry()
        {
            return new BlockEntry(block.index, block.type, virtualPositionTags, lastUpdate);
        }

        /// <summary>
        /// Calibrate the objet to change its rotation, position and scale to match its position tags to the given positions tags.
        /// </summary>
        /// <param name="realPositions">Real positions.</param>
        public override void Calibrate(PositionTag[] realPositionTags)
        {
            lastUpdate = DateTime.Now;
            base.Calibrate(realPositionTags);
        }

        /// <summary>
        /// Calibrate the objet to change its rotation, position and scale to match its position tags to the given positions tags.
        /// </summary>
        /// <param name="realPositionTags">Real position tags.</param>
        public override void Calibrate(Vector3[] realPositions)
        {
            lastUpdate = DateTime.Now;
            base.Calibrate(realPositions);
        }
    }
}

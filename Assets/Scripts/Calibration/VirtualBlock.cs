using System;
using UnityEngine;

namespace VRCalibrationTool
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
        public BlockTypeIndex blockEntry;
    }
}

using CRI.HelloHouston.Calibration.XML;
using System;
using System.Linq;
using UnityEngine;

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
        /// <summary>
        /// Index of the block inside the room.
        /// </summary>
        [Tooltip("Index of the block inside the room.")]
        public int indexInRoom;
        /// <summary>
        /// All the zones inside the block.
        /// </summary>
        public VirtualZone[] zones;
        /// <summary>
        /// All the placeholders inside the zones inside the block.
        /// </summary>
        public VirtualPlaceholder[] placeholders
        {
            get
            {
                if (zones.Length == 0)
                    return new VirtualPlaceholder[0];
                return zones.SelectMany(x => x.placeholders).ToArray();
            }
        }

        public void Start()
        {
            zones = GetComponentsInChildren<VirtualZone>();
        }

        /// <summary>
        /// Initialization of the virtual block.
        /// </summary>
        /// <param name="block">A block entry</param>
        /// <param name="indexInRoom">The index of the block inside the room.</param>
        public void Init(BlockEntry block, int indexInRoom)
        {
            this.block.type = block.type;
            this.block.index = block.index;
            this.lastUpdate = block.date;
            this.indexInRoom = indexInRoom;
            if (block.points.Length >= 3)
                Calibrate(block.points);
        }

        /// <summary>
        /// Get all zones of a type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public VirtualZone[] GetZones(VirtualZoneType type)
        {
            if (zones.Length == 0)
                return new VirtualZone[0];
            return zones.Where(x => x.type == type).ToArray();
        }

        /// <summary>
        /// Gets a block entry out of the virtual block.
        /// </summary>
        /// <returns>An instance of BlockEntry</returns>
        public BlockEntry ToBlockEntry()
        {
            return new BlockEntry(block.index, block.type, virtualPositionTags, lastUpdate);
        }
    }
}

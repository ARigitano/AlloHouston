using CRI.HelloHouston.Calibration.XML;
using CRI.HelloHouston.Experience;
using System;
using System.Linq;
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
        /// <summary>
        /// Index of the block inside the room.
        /// </summary>
        [HideInInspector]
        public int indexInRoom;
        /// <summary>
        /// All the zones inside the block.
        /// </summary>
        public VirtualZone[] zones;
        /// <summary>
        /// All the placeholders inside the zones inside the block.
        /// </summary>
        public VirtualElement[] elements
        {
            get
            {
                if (zones.Length == 0)
                    return new VirtualElement[0];
                return zones.SelectMany(x => x.virtualElements).ToArray();
            }
        }

        private void Start()
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
            if (virtualPositionTags.Length == 0)
                calibrated = true;
            if (block.points.Length >= 3)
                Calibrate(block.points);
            if (!calibrated)
                gameObject.SetActive(false);
        }

        /// <summary>
        /// Get all zones of a type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public VirtualZone[] GetZones(ZoneType type)
        {
            if (zones.Length == 0)
                return new VirtualZone[0];
            return zones.Where(x => x.zoneType == type).ToArray();
        }

        /// <summary>
        /// Gets a block entry out of the virtual block.
        /// </summary>
        /// <returns>An instance of BlockEntry</returns>
        public BlockEntry ToBlockEntry()
        {
            return new BlockEntry(block.index, block.type, calibrated ? virtualPositionTags : new PositionTag[0], lastUpdate);
        }
    }
}

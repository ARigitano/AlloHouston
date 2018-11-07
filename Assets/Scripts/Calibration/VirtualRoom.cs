using CRI.HelloHouston.Calibration.XML;
using System.Linq;
using VRCalibrationTool;
using UnityEngine;

namespace CRI.HelloHouston.Calibration
{
    public class VirtualRoom : VirtualItem
    {
        /// <summary>
        /// Type of the virtual item.
        /// </summary>
        public override VirtualItemType virtualItemType
        {
            get
            {
                return VirtualItemType.Room;
            }
        }

        public override bool calibrated
        {
            get
            {
                return base.calibrated;
            }

            set
            {
                base.calibrated = value;
                SetAllBlocksAsChild(value);
            }
        }
        /// <summary>
        /// The block of the room.
        /// </summary>
        public VirtualBlock[] blocks;

        /// <summary>
        /// Init a VirtualRoom
        /// </summary>
        /// <param name="room">A Room Entry</param>
        /// <param name="calibrationManager">The calibration manager</param>
        public void Init(RoomEntry room)
        {
            this.index = room.index;
            this.lastUpdate = room.date;
            if (room.points.Length >= 3)
                Calibrate(room.points);
        }

        /// <summary>
        /// Set all blocks as child of the virtual block.
        /// </summary>
        /// <param name="set">If true, it will set all blocks as child of the virtual block. If false, the blocks' parent will be set up as null.</param>
        public void SetAllBlocksAsChild(bool set = true)
        {
            foreach (var block in blocks)
            {
                block.transform.SetParent(set ? transform : null);
            }
        }

        /// <summary>
        /// Get all the zones of a type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public VirtualZone[] GetZones(VirtualZoneType type)
        {
            return blocks.SelectMany(x => x.GetZones(type)).ToArray();
        }

        /// <summary>
        /// Gets the RoomEntry for the Virtual Room.
        /// </summary>
        /// <returns></returns>
        public RoomEntry ToRoomEntry()
        {
            return new RoomEntry(
                index,
                blocks.Select(x => x.ToBlockEntry()).ToArray(),
                calibrated ? virtualPositionTags : new PositionTag[0],
                lastUpdate
                );
        }

        /// <summary>
        /// Reset all tags.ssss
        /// </summary>
        public override void ResetAllTags()
        {
            base.ResetAllTags();
            foreach (var block in blocks)
            {
                block.ResetAllTags();
            }
        }
    }
}

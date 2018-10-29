using CRI.HelloHouston.Calibration.XML;
using System;
using System.Linq;
using UnityEngine;
using VRCalibrationTool;

namespace CRI.HelloHouston.Calibration
{
    public class VirtualRoom : VirtualObject
    {
        /// <summary>
        /// The block of the room.
        /// </summary>
        public VirtualBlock[] blocks;
        /// <summary>
        /// The block table.
        /// </summary>
        public VirtualBlock table;
        /// <summary>
        /// Index of the room.
        /// </summary>
        public int index;
        /// <summary>
        /// Date of the last calibration.
        /// </summary>
        public DateTime lastUpdate;

        /// <summary>
        /// Init a VirtualRoom
        /// </summary>
        /// <param name="room">A Room Entry</param>
        /// <param name="calibrationManager">The calibration manager</param>
        public void Init(RoomEntry room, CalibrationManager calibrationManager)
        {
            this.index = room.index;
            this.lastUpdate = room.date;
            Calibrate(room.points);
            table = Instantiate(calibrationManager.GetVirtualBlockPrefab(room.table), transform);
            table.Init(room.table);
            blocks = new VirtualBlock[room.blocks.Length];
            for (int i = 0; i < room.blocks.Length; i++)
            {
                blocks[i] = Instantiate(calibrationManager.GetVirtualBlockPrefab(room.blocks[i]), transform);
                blocks[i].Init(room.blocks[i]);
            }
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
                table.ToBlockEntry(),
                virtualPositionTags,
                lastUpdate
                );
        }

        /// <summary>
        /// Calibrate the objet to change its rotation, position and scale to match its position tags to the given positions tags.
        /// </summary>
        /// <param name="realPositionTags">Real position tags.</param>
        public override void Calibrate(PositionTag[] realPositionTags)
        {
            lastUpdate = DateTime.Now;
            base.Calibrate(realPositionTags);
        }

        /// <summary>
        /// Calibrate the objet to change its rotation, position and scale to match its position tags to the given positions tags.
        /// </summary>
        /// <param name="realPositions">Positions of the real tags.</param>
        public override void Calibrate(Vector3[] realPositions)
        {
            lastUpdate = DateTime.Now;
            base.Calibrate(realPositions);
        }
    }
}

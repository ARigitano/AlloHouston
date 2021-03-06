﻿using System;
using System.Collections.Generic;
using VRCalibrationTool;

namespace CRI.HelloHouston.Calibration.Data
{
    /// <summary>
    /// A block room, a data entity that contains the values of a virtual room.
    /// </summary>
    [Serializable]
    public class RoomEntry : ItemEntry
    {
        /// <summary>
        /// Name of the room.
        /// </summary>
        public override string name
        {
            get
            {
                return "Room " + index;
            }
        }
        /// <summary>
        /// List of items that made up the room.  
        /// </summary>
        public BlockEntry[] blocks;
        /// <summary>
        /// Login of the person who calibrated the ItemEntry.
        /// </summary>
        public string login;

        public List<string> checklist;

        public RoomEntry() : base() { }

        public RoomEntry(int index, BlockEntry[] blocks, PositionTag[] points, DateTime date, List<string> checklist) : base(index, points, date)
        {
            this.blocks = blocks;
            this.checklist = checklist;
        }
    }
}

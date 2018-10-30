using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using VRCalibrationTool;

namespace CRI.HelloHouston.Calibration
{
    public abstract class VirtualItem : VirtualObject
    {
        public enum VirtualItemType
        {
            Block,
            Room,
        }
        /// <summary>
        /// Index of the room.
        /// </summary>
        public int index;
        /// <summary>
        /// Date of the last calibration.
        /// </summary>
        public DateTime lastUpdate;
        /// <summary>
        /// The virtual item type.
        /// </summary>
        public abstract VirtualItemType virtualItemType { get; }

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

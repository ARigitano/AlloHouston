﻿using System;
using UnityEngine;
using VRCalibrationTool;

namespace CRI.HelloHouston.Calibration
{
    public abstract class VirtualItem : VirtualObject
    {
        public delegate void DateEvent(DateTime date);
        public delegate void BoolEvent(bool b);
        public event DateEvent onDateChange;
        public event BoolEvent onCalibratedChange;
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
        public DateTime lastUpdate
        {
            get
            {
                return _lastUpdate;
            }
            set
            {
                _lastUpdate = value;
                if (onDateChange != null)
                    onDateChange(value);
            }
        }
        /// <summary>
        /// Date of the last calibration.
        /// </summary>
        protected DateTime _lastUpdate;
        public virtual bool calibrated
        {
            get
            {
                return _calibrated;
            }
            set
            {
                _calibrated = value;
                if (onCalibratedChange != null)
                    onCalibratedChange(value);
                gameObject.SetActive(_calibrated);
            }
        }
        protected bool _calibrated;
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
            calibrated = true;
        }

        /// <summary>
        /// Calibrate the objet to change its rotation, position and scale to match its position tags to the given positions tags.
        /// </summary>
        /// <param name="realPositions">Positions of the real tags.</param>
        public override void Calibrate(Vector3[] realPositions)
        {
            lastUpdate = DateTime.Now;
            base.Calibrate(realPositions);
            calibrated = true;
        }
        /// <summary>
        /// Resets the position of all the tags.
        /// </summary>
        public virtual void ResetAllTags()
        {
            foreach (var positionTag in virtualPositionTags)
            {
                positionTag.ResetPosition();
            }
            calibrated = false;
        }
    }
}

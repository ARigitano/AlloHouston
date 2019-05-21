using CRI.HelloHouston.Audio;
using CRI.HelloHouston.Experience;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Calibration
{
    public class VirtualWallTopZone : VirtualZone
    {
        public override ZoneType zoneType
        {
            get
            {
                return ZoneType.WallTop;
            }
        }

        public override bool switchableZone
        {
            get
            {
                return true;
            }
        }

        public override VirtualElement[] virtualElements
        {
            get
            {
                return new VirtualElement[] { wallTopLeftVirtualElement, wallTopRightVirtualElement, wallTopTabletVirtualElement };
            }
        }

        public override XPZone xpZone
        {
            get
            {
                return xpWallTopZone;
            }
        }

        protected override void AddXPZone(XPZone xpZone, XPContext xpContext)
        {
            if (xpZone == null)
            {
                this.xpWallTopZone = null;
                wallTopLeftVirtualElement.Clean();
                wallTopLeftVirtualElement.PlaceObject(null, null);
                wallTopRightVirtualElement.PlaceObject(null, null);
                wallTopTabletVirtualElement.PlaceObject(null, null);
            }
            else
            {
                var xpWallTopZone = xpZone as XPWallTopZone;
                if (!xpWallTopZone)
                    throw new WrongZoneTypeException();
                this.xpWallTopZone = xpWallTopZone;
                wallTopLeftVirtualElement.PlaceObject(xpWallTopZone.elementLeftPrefab, xpContext);
                wallTopRightVirtualElement.PlaceObject(xpWallTopZone.elementRightPrefab, xpContext);
                wallTopTabletVirtualElement.PlaceObject(xpWallTopZone.elementTabletPrefab, xpContext);
            }
        }

        public override XPElement[] CleanAll()
        {
            var res = base.CleanAll();
            if (_leftSpeaker != null)
            _leftSpeaker.StopAll();
            if (_leftSpeaker != null)
            _rightSpeaker.StopAll();
            return res;
        }

        public VirtualElement wallTopLeftVirtualElement;
        public VirtualElement wallTopRightVirtualElement;
        public VirtualElement wallTopTabletVirtualElement;
        public XPWallTopZone xpWallTopZone { get; protected set; }
        /// <summary>
        /// The index of the wall top zone.
        /// </summary>
        [Tooltip("The index of the wall top zone.")]
        public int index;
        /// <summary>
        /// The left speaker of the wall top.
        /// </summary>
        [SerializeField]
        [Tooltip("The left speaker of the wall top.")]
        private SoundManager _leftSpeaker = null;
        /// <summary>
        /// The left speaker of the wall top.
        /// </summary>
        public SoundManager leftSpeaker { get { return _leftSpeaker; } }
        /// <summary>
        /// The right speaker of the wall top.
        /// </summary>
        [SerializeField]
        [Tooltip("The right speaker of the wall top.")]
        private SoundManager _rightSpeaker = null;
        /// <summary>
        /// The right speaker of the wall top.
        /// </summary>
        public SoundManager rightSpeaker { get { return _rightSpeaker; } }
    }
}

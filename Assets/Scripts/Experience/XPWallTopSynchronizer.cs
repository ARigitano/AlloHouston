using CRI.HelloHouston.Calibration;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    [System.Serializable]
    public class XPWallTopSynchronizer : XPSynchronizer
    {
        public override XPElement[] contents
        {
            get
            {
                var list = new List<XPElement>();
                if (contentLeft != null)
                    list.Add(contentLeft);
                if (contentRight != null)
                    list.Add(contentRight);
                if (contentTablet != null)
                    list.Add(contentTablet);
                return list.ToArray();
            }
        }
        /// <summary>
        /// The left side of the wall top.
        /// </summary>
        public XPElement contentLeft { get; protected set; }
        /// <summary>
        /// The right side of the wall top.
        /// </summary>
        public XPElement contentRight { get; protected set; }
        /// <summary>
        /// The tablet of the wall top.
        /// </summary>
        public XPElement contentTablet { get; protected set; }

        public void Init(XPWallTopZone zoneContent, VirtualWallTopZone virtualZone)
        {
            if (contentLeft != null)
            {
                contentLeft = Instantiate(zoneContent.elementLeftPrefab);
                virtualZone.wallTopLeftVirtualElement.PlaceObject(contentLeft);
            }
            if (contentRight != null)
            {
                contentRight = Instantiate(zoneContent.elementRightPrefab);
                virtualZone.wallTopRightVirtualElement.PlaceObject(contentRight);
            }
            if (contentTablet != null)
            {
                contentTablet = Instantiate(zoneContent.elementTabletPrefab);
                virtualZone.wallTopTabletVirtualElement.PlaceObject(contentTablet);
            }
        }
    }
}

using CRI.HelloHouston.Calibration;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    [System.Serializable]
    public class XPWallTopSynchronizer : XPSynchronizer
    {
        public override XPContent[] contents
        {
            get
            {
                var list = new List<XPContent>();
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
        public XPContent contentLeft { get; protected set; }
        /// <summary>
        /// The right side of the wall top.
        /// </summary>
        public XPContent contentRight { get; protected set; }
        /// <summary>
        /// The tablet of the wall top.
        /// </summary>
        public XPContent contentTablet { get; protected set; }

        public void Init(WallTopZoneContent zoneContent, VirtualWallTopZone virtualZone)
        {
            if (contentLeft != null)
            {
                contentLeft = Instantiate(zoneContent.contentLeftPrefab);
                virtualZone.wallTopLeftPlaceholder.PlaceObject(contentLeft);
            }
            if (contentRight != null)
            {
                contentRight = Instantiate(zoneContent.contentRightPrefab);
                virtualZone.wallTopRightPlaceholder.PlaceObject(contentRight);
            }
            if (contentTablet != null)
            {
                contentTablet = Instantiate(zoneContent.contentTabletPrefab);
                virtualZone.wallTopTabletPlaceholder.PlaceObject(contentTablet);
            }
        }
    }
}

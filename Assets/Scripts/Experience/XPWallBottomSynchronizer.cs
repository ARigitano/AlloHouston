using CRI.HelloHouston.Calibration;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    public class XPWallBottomSynchronizer : XPSynchronizer
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
                return list.ToArray();
            }
        }
        /// <summary>
        /// The left side of the wall bottom.
        /// </summary>
        public XPContent contentLeft { get; private set; }
        /// <summary>
        /// The right side of the wall bottom.
        /// </summary>
        public XPContent contentRight { get; private set; }

        public void Init(WallBottomZoneContent zoneContent, VirtualWallBottomZone virtualZone)
        {
            if (contentLeft != null)
            {
                contentLeft = Instantiate(zoneContent.contentLeftPrefab);
                virtualZone.wallBottomLeftPlaceholder.PlaceObject(contentLeft);
            }
            if (contentRight != null)
            {
                contentRight = Instantiate(zoneContent.contentRightPrefab);
                virtualZone.wallBottomRightPlaceholder.PlaceObject(contentRight);
            }
        }
    }
}
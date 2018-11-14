using CRI.HelloHouston.Calibration;
using System;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    public class XPCornerSynchronizer : XPSynchronizer
    {
        public override XPElement[] contents
        {
            get
            {
                if (cornerContent != null)
                    return new XPElement[] { cornerContent };
                return new XPElement[0];
            }
        }

        /// <summary>
        /// The corner content.
        /// </summary>
        public XPElement cornerContent { get; private set; }

        public void Init(XPCornerZone zoneContent, VirtualCornerZone virtualZone)
        {
            if (cornerContent != null)
            {
                cornerContent = Instantiate(zoneContent.cornerContentPrefab);
                virtualZone.wallCornerVirtualElement.PlaceObject(cornerContent);
            }
        }
    }
}
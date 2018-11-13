using CRI.HelloHouston.Calibration;
using System;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    public class XPCornerSynchronizer : XPSynchronizer
    {
        public override XPContent[] contents
        {
            get
            {
                if (cornerContent != null)
                    return new XPContent[] { cornerContent };
                return new XPContent[0];
            }
        }

        /// <summary>
        /// The corner content.
        /// </summary>
        public XPContent cornerContent { get; private set; }

        public void Init(CornerZoneContent zoneContent, VirtualCornerZone virtualZone)
        {
            if (cornerContent != null)
            {
                cornerContent = Instantiate(zoneContent.cornerContentPrefab);
                virtualZone.wallCornerPlaceholder.PlaceObject(cornerContent);
            }
        }
    }
}
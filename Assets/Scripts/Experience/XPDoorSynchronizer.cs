using CRI.HelloHouston.Calibration;
using System;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    [Serializable]
    public class XPDoorSynchronizer : XPSynchronizer
    {
        public override XPElement[] contents
        {
            get
            {
                if (doorContent != null)
                    return new XPElement[] { doorContent };
                return new XPElement[0];
            }
        }
        /// <summary>
        /// The door content.
        /// </summary>
        public XPElement doorContent { get; private set; }

        public void Init(XPDoorZone zoneContent, VirtualDoorZone virtualZone)
        {
            if (doorContent != null)
            {
                doorContent = Instantiate(zoneContent.doorContentPrefab);
                virtualZone.doorVirtualElement.PlaceObject(doorContent);
            }
        }
    }
}
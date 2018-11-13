using CRI.HelloHouston.Calibration;
using System;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    [Serializable]
    public class XPDoorSynchronizer : XPSynchronizer
    {
        public override XPContent[] contents
        {
            get
            {
                if (doorContent != null)
                    return new XPContent[] { doorContent };
                return new XPContent[0];
            }
        }
        /// <summary>
        /// The door content.
        /// </summary>
        public XPContent doorContent { get; private set; }

        public void Init(DoorZoneContent zoneContent, VirtualDoorZone virtualZone)
        {
            if (doorContent != null)
            {
                doorContent = Instantiate(zoneContent.doorContentPrefab);
                virtualZone.doorPlaceholder.PlaceObject(doorContent);
            }
        }
    }
}
using CRI.HelloHouston.Calibration;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    public class XPWallBottomSynchronizer : XPSynchronizer
    {
        public override XPElement[] contents
        {
            get
            {
                if (element != null)
                    return new XPElement[] { element };
                return new XPElement[0];
            }
        }
        /// <summary>
        /// The left side of the wall bottom.
        /// </summary>
        public XPElement element { get; private set; }

        public void Init(XPWallBottomZone xpZone, VirtualWallBottomZone virtualZone)
        {
            if (element != null)
            {
                element = Instantiate(xpZone.element);
                virtualZone.wallBottomVirtualElement.PlaceObject(element);
            }
        }
    }
}
using CRI.HelloHouston.Calibration;
using System;

namespace CRI.HelloHouston.Experience
{
    [Serializable]
    public struct RoomExperiencePair
    {
        /// <summary>
        /// The element as an experience.
        /// </summary>
        public XPElement xpElement;
        /// <summary>
        /// The element as a virtual element.
        /// </summary>
        public VirtualElement virtualElement;

        public RoomExperiencePair(XPElement xpElement, VirtualElement virtualElement)
        {
            this.xpElement = xpElement;
            this.virtualElement = virtualElement;
        }
    }
}

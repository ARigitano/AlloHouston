using CRI.HelloHouston.Experience;
using UnityEngine;

namespace CRI.HelloHouston.Calibration
{
    public abstract class VirtualZone : MonoBehaviour
    {
        /// <summary>
        /// Gets the type of virtual zone.
        /// </summary>
        public abstract ZoneType zoneType { get; }
        /// <summary>
        /// Gets and set an xpZone on a virtualZone.
        /// </summary>
        public abstract XPZone xpZone { get; }
        /// <summary>
        /// Gets all the virtual elements inside the zone.
        /// </summary>
        public abstract VirtualElement[] virtualElements { get; }
    }
}
using CRI.HelloHouston.Experience;
using System;
using UnityEngine;

namespace CRI.HelloHouston.Calibration
{
    public abstract class VirtualZone : MonoBehaviour
    {
        public class WrongZoneTypeException : Exception { }
        /// <summary>
        /// Gets the type of virtual zone.
        /// </summary>
        public abstract ZoneType zoneType { get; }
        /// <summary>
        /// Gets and set an xpZone on a virtualZone.
        /// </summary>
        public abstract XPZone xpZone { get;}
        /// <summary>
        /// Gets and set and xpContext on a virtualZone.
        /// </summary>
        public XPContext xpContext { get; protected set; }
        /// <summary>
        /// Gets all the virtual elements inside the zone.
        /// </summary>
        public abstract VirtualElement[] virtualElements { get; }

        public virtual void Place(XPZone xpZone, XPContext xpContext)
        {
            AddXPZone(xpZone);
            this.xpContext = xpContext;
        }

        protected abstract void AddXPZone(XPZone xpZone);
    }
}
using CRI.HelloHouston.Experience;
using System;
using System.Linq;
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
        /// Whether the zone is switchable after the initialization or not.
        /// </summary>
        public abstract bool switchableZone { get; }
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
            this.xpContext = xpContext;
            AddXPZone(xpZone, xpContext);
        }

        public virtual XPElement[] InitAll(XPSynchronizer xpSynchronizer)
        {
            XPElement[] res = virtualElements.Where(x => x.xpContext != null).Select(x => x.Init(xpSynchronizer)).ToArray();
            return res;
        }

        public virtual void CleanAll()
        {
            foreach (var virtualElement in virtualElements)
                virtualElement.Clean();
        }

        protected abstract void AddXPZone(XPZone xpZone, XPContext xpContext);
    }
}
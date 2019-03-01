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
        /// Gets the current xpZone on this virtualZone.
        /// </summary>
        public abstract XPZone xpZone { get; }
        /// <summary>
        /// Gets and set and xpContext on a virtualZone.
        /// </summary>
        public XPContext xpContext { get; protected set; }
        /// <summary>
        /// Gets all the virtual elements inside the zone.
        /// </summary>
        public abstract VirtualElement[] virtualElements { get; }
        
        /// <summary>
        /// Places an XPZone and its XPContext inside an VirtualZone and set in cascade all the XPElements inside the VirtualElements of the VirtualZone.
        /// </summary>
        /// <param name="xpZone"></param>
        /// <param name="xpContext"></param>
        public virtual void Place(XPZone xpZone, XPContext xpContext)
        {
            this.xpContext = xpContext;
            AddXPZone(xpZone, xpContext);
        }

        /// <summary>
        /// Initializes all the elements defined in the current XPZone.
        /// </summary>
        /// <returns>All the elements, initialized.</returns>
        public virtual XPElement[] InitAll(XPManager manager)
        {
            XPElement[] res = virtualElements.Where(x => x.xpContext != null).Select(x => x.Init(manager)).ToArray();
            return res;
        }

        /// <summary>
        /// Clean all virtual elements of this VirtualZone.
        /// </summary>
        public virtual void CleanAll()
        {
            foreach (var virtualElement in virtualElements)
                virtualElement.Clean();
        }

        protected abstract void AddXPZone(XPZone xpZone, XPContext xpContext);
    }
}
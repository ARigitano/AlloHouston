using CRI.HelloHouston.Experience;
using System;
using System.Collections.Generic;
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
        /// The manager currently on this virtualZone.
        /// </summary>
        public XPManager manager { get; protected set; }
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
            IEnumerable<XPElement> res = virtualElements.Where(x => x.xpContext != null).Select(x => x.Init(manager));
            this.manager = manager;
            return res.ToArray();
        }

        /// <summary>
        /// Clean all virtual elements of this VirtualZone.
        /// <return>All the cleaned elements.</return> 
        /// </summary>
        public virtual XPElement[] CleanAll()
        {
            this.manager = null;
            return virtualElements.Select(x => x.Clean()).ToArray();
        }

        protected abstract void AddXPZone(XPZone xpZone, XPContext xpContext);
    }
}
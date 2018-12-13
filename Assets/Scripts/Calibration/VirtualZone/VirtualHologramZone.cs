using System;
using CRI.HelloHouston.Experience;
using System.Collections.Generic;

namespace CRI.HelloHouston.Calibration
{
    public class VirtualHologramZone : VirtualZone
    {
        public override ZoneType zoneType
        {
            get
            {
                return ZoneType.Hologram;
            }
        }

        public override VirtualElement[] virtualElements
        {
            get
            {
                return hologramVirtualElements;
            }
        }

        public override XPZone xpZone
        {
            get
            {
                return null;
            }
        }

        protected override void AddXPZone(XPZone xpZone)
        {
            var hologramZone = xpZone as XPHologramZone;
            if (!hologramZone)
                throw new WrongZoneTypeException();
            hologramZones.Add(hologramZone);
        }

        public VirtualElement[] hologramVirtualElements;
        public List<XPHologramZone> hologramZones { get; protected set; }
    }
}

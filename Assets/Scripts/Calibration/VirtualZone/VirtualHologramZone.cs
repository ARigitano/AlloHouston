using CRI.HelloHouston.Experience;

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
                return new VirtualElement[] { hologramVirtualElement };
            }
        }

        public override XPZone xpZone
        {
            get
            {
                return xpHologramZone;
            }
        }

        public VirtualElement hologramVirtualElement;
        public XPHologramZone xpHologramZone;
    }
}

using CRI.HelloHouston.Experience;

namespace CRI.HelloHouston.Calibration
{
    public class VirtualCornerZone : VirtualZone
    {
        public override ZoneType zoneType
        {
            get
            {
                return ZoneType.Corner;
            }
        }
        public override VirtualElement[] virtualElements
        {
            get
            {
                return new VirtualElement[] { wallCornerVirtualElement };
            }
        }

        public override XPZone xpZone
        {
            get
            {
                return xpCornerZone;
            }
            protected set
            {
                xpCornerZone = (XPCornerZone)value;
            }
        }

        public VirtualElement wallCornerVirtualElement;
        public XPCornerZone xpCornerZone;
    }
}

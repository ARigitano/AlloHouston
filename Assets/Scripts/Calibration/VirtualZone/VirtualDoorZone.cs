using CRI.HelloHouston.Experience;

namespace CRI.HelloHouston.Calibration
{
    public class VirtualDoorZone : VirtualZone
    {
        public override ZoneType zoneType
        {
            get
            {
                return ZoneType.Door;
            }
        }
        public override VirtualElement[] virtualElements
        {
            get
            {
                return new VirtualElement[] { doorVirtualElement };
            }
        }

        public override XPZone xpZone
        {
            get
            {
                return xpDoorZone;
            }
            protected set
            {
                xpDoorZone = (XPDoorZone)value;
            }
        }

        public VirtualElement doorVirtualElement;
        public XPDoorZone xpDoorZone;
    }

}

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
        }

        protected override void AddXPZone(XPZone xpZone)
        {
            var xpDoorZone = xpZone as XPDoorZone;
            if (!xpDoorZone)
                throw new WrongZoneTypeException();
            this.xpDoorZone = xpDoorZone;
        }

        public VirtualElement doorVirtualElement;
        public XPDoorZone xpDoorZone { get; protected set; }
    }

}

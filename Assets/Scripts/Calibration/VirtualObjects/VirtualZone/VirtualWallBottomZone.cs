 using CRI.HelloHouston.Experience;

namespace CRI.HelloHouston.Calibration
{
    public class VirtualWallBottomZone : VirtualZone
    {
        public override ZoneType zoneType
        {
            get
            {
                return ZoneType.WallBottom;
            }
        }

        public override bool switchableZone
        {
            get
            {
                return false;
            }
        }

        public override VirtualElement[] virtualElements
        {
            get
            {
                return new VirtualElement[] { wallBottomVirtualElement };
            }
        }

        public override XPZone xpZone
        {
            get
            {
                return xpWallBottomZone;
            }
        }

        protected override void AddXPZone(XPZone xpZone, XPContext xpContext)
        {
            if (xpZone == null)
            {
                this.xpWallBottomZone = null;
                wallBottomVirtualElement.PlaceObject(null, null);
            }
            else
            {
                var xpWallBottomZone = xpZone as XPWallBottomZone;
                if (!xpWallBottomZone)
                    throw new WrongZoneTypeException();
                this.xpWallBottomZone = xpWallBottomZone;
                wallBottomVirtualElement.PlaceObject(xpWallBottomZone.element, xpContext);
            }
        }

        public VirtualElement wallBottomVirtualElement;
        public XPWallBottomZone xpWallBottomZone { get; protected set; }
    }
}

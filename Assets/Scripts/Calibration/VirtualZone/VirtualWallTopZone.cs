using CRI.HelloHouston.Experience;

namespace CRI.HelloHouston.Calibration
{
    public class VirtualWallTopZone : VirtualZone
    {
        public override ZoneType zoneType
        {
            get
            {
                return ZoneType.WallTop;
            }
        }

        public override bool switchableZone
        {
            get
            {
                return true;
            }
        }

        public override VirtualElement[] virtualElements
        {
            get
            {
                return new VirtualElement[] { wallTopLeftVirtualElement, wallTopRightVirtualElement, wallTopTabletVirtualElement };
            }
        }

        public override XPZone xpZone
        {
            get
            {
                return xpWallTopZone;
            }
        }

        protected override void AddXPZone(XPZone xpZone, XPContext xpContext)
        {
            var xpWallTopZone = xpZone as XPWallTopZone;
            if (!xpWallTopZone)
                throw new WrongZoneTypeException();
            this.xpWallTopZone = xpWallTopZone;
            wallTopLeftVirtualElement.PlaceObject(xpWallTopZone.elementLeftPrefab, xpContext);
            wallTopRightVirtualElement.PlaceObject(xpWallTopZone.elementRightPrefab, xpContext);
            wallTopTabletVirtualElement.PlaceObject(xpWallTopZone.elementTabletPrefab, xpContext);
        }

        public VirtualElement wallTopLeftVirtualElement;
        public VirtualElement wallTopRightVirtualElement;
        public VirtualElement wallTopTabletVirtualElement;
        public XPWallTopZone xpWallTopZone { get; protected set; }
    }

}

using CRI.HelloHouston.Calibration;

namespace CRI.HelloHouston.Experience
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

        public VirtualElement wallTopLeftVirtualElement;
        public VirtualElement wallTopRightVirtualElement;
        public VirtualElement wallTopTabletVirtualElement;

        public XPWallTopZone xpWallTopZone;
    }

}

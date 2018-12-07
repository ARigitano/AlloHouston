using CRI.HelloHouston.Calibration;

namespace CRI.HelloHouston.Experience
{
    public struct RoomXPPair
    {
        public VirtualRoom vroom;
        public XPContext[] xpContexts;

        public RoomXPPair(VirtualRoom vroom, XPContext[] xpContexts)
        {
            this.vroom = vroom;
            this.xpContexts = xpContexts;
        }
    }
}

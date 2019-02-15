using CRI.HelloHouston.Calibration;

namespace CRI.HelloHouston.Experience
{
    public struct RoomXPPair
    {
        public VirtualRoom vroom { get; private set; }
        public XPContext[] xpContexts { get; private set; }
        public bool[] starting { get; private set; }
        public int seed { get; set; }

        public RoomXPPair(VirtualRoom vroom, XPContext[] xpContexts, bool[] starting, int seed = -1)
        {
            this.vroom = vroom;
            this.xpContexts = xpContexts;
            this.starting = starting;
            this.seed = seed;
        }
    }
}

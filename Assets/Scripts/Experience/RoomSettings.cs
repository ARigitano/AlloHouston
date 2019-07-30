using CRI.HelloHouston.Calibration;
using System.Linq;

namespace CRI.HelloHouston.Experience
{
    public struct RoomSettings
    {
        public VirtualRoom vroom { get; private set; }
        public XPContext[] xpContexts { get; private set; }
        public bool[] starting { get; private set; }
        public int seed { get; set; }
        public int timeEstimate { get; set; }

        public RoomSettings(VirtualRoom vroom, XPContext[] xpContexts, bool[] starting, int timeEstimate, int seed)
        {
            this.vroom = vroom;
            this.xpContexts = xpContexts;
            this.starting = starting;
            this.seed = seed;
            this.timeEstimate = timeEstimate;
        }

        public RoomSettings(VirtualRoom vroom, XPContext[] xpContexts, bool[] starting, int seed = -1)
        {
            this.vroom = vroom;
            this.xpContexts = xpContexts;
            this.starting = starting;
            this.seed = seed;
            this.timeEstimate = xpContexts.Where(xpContext => xpContext.xpSettings != null).Sum(xpContext => xpContext.xpSettings.duration);
        }
    }
}

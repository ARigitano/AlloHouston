using CRI.HelloHouston.Experience;

namespace CRI.HelloHouston.Calibration
{
    internal struct ContextZone
    {
        public XPContext xpContext { get; private set; }
        public XPZone xpZone { get; private set; }
        public bool start { get; private set; }

        public ContextZone(XPContext xpContext, XPZone xpZone, bool start = false)
        {
            this.xpContext = xpContext;
            this.xpZone = xpZone;
            this.start = start;
        }
    }
}
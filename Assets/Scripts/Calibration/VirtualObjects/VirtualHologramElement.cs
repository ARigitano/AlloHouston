using CRI.HelloHouston.Experience;

namespace CRI.HelloHouston.Calibration
{
    public class VirtualHologramElement : VirtualElement
    {
        public VirtualHologramZone virtualHologramZone;

        public override XPElement Init(XPManager manager)
        {
            XPElement res = base.Init(manager);
            res.GetComponent<XPHologramElement>().hologramZone = virtualHologramZone;
            return res;
        }
    }
}
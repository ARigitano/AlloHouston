using CRI.HelloHouston;
using CRI.HelloHouston.Calibration;
using CRI.HelloHouston.Experience;

public class UIRoomPanel : UIPanel {
    private VirtualRoom _vroom;
    private XPContext[] _xpContexts;
    public override void Init(object obj)
    {
        var rxpp = (RoomXPPair)obj;
        _vroom = rxpp.vroom;
        _xpContexts = rxpp.xpContexts;
    }
}

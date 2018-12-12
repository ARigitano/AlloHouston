using CRI.HelloHouston;
using CRI.HelloHouston.Calibration;
using CRI.HelloHouston.Experience;
using System.Linq;
using UnityEngine;

namespace CRI.HelloHouston.Calibration.UI {
    public class UIRoomPanel : UIPanel {
        private VirtualRoom _vroom;
        private XPContext[] _xpContexts;
        private ZoneManager _zoneManager;

        public void Start()
        {
            VirtualRoom vroom = FindObjectOfType<VirtualRoom>();
            vroom.blocks = vroom.GetComponentsInChildren<VirtualBlock>();
            vroom.SetAllBlocksAsChild();
            XPContext[] xpContexts = Resources.LoadAll<XPContext>("AllExperiences/Electricity/XpContext");
            _zoneManager = new ZoneManager(FindObjectOfType<LaserClicker>());
            Init(new RoomXPPair(vroom, xpContexts));
        }

        public override void Init(object obj)
        {
            var rxpp = (RoomXPPair)obj;
            _vroom = rxpp.vroom;
            var zones = _vroom.GetZones();
            _xpContexts = rxpp.xpContexts;
            foreach (var zone in _vroom.GetZones())
            {
                UIZone UIZone = zone.GetComponent<UIZone>();
                if (UIZone != null)
                    UIZone.Init(_zoneManager);
            }
            _zoneManager.DistributeZones(zones, _xpContexts.SelectMany(xpContext => xpContext.zones.Select(xpZone => new ContextZone(xpContext, xpZone))).ToArray());
        }
    }
}

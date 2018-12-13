using CRI.HelloHouston;
using CRI.HelloHouston.Calibration;
using CRI.HelloHouston.Experience;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Calibration.UI {
    public class UIRoomPanel : UIPanel {
        /// <summary>
        /// The component that will be used to click on the different zones.
        /// </summary>
        [SerializeField]
        [Tooltip("The component that will be used to click on the different zones.")]
        private PointerClicker _laserClicker = null;
        /// <summary>
        /// Next button.
        /// </summary>
        [SerializeField]
        [Tooltip("Next button.")]
        private Button _nextButton = null;

        public override void Init(object obj)
        {
            var rxpp = (RoomXPPair)obj;
            var zoneManager = new ZoneManager(_laserClicker);
            VirtualRoom vroom = rxpp.vroom;
            XPContext[] xpContexts = rxpp.xpContexts;
            VirtualZone[] zones = vroom.GetZones();
            foreach (var zone in zones)
            {
                UIZone UIZone = zone.GetComponent<UIZone>();
                if (UIZone != null)
                    UIZone.Init(zoneManager);
            }
            zoneManager.DistributeZones(zones, xpContexts.SelectMany(xpContext => xpContext.zones.Select(xpZone => new ContextZone(xpContext, xpZone))).ToArray());
            _nextObject = rxpp;
            _nextButton.onClick.AddListener(Next);
        }
    }
}

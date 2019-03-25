using CRI.HelloHouston;
using CRI.HelloHouston.Calibration;
using CRI.HelloHouston.Experience;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

namespace CRI.HelloHouston.Calibration.UI {
    public class UIRoomPanel : UIPanel
    {
        /// <summary>
        /// The player gameobject.
        /// </summary>
        [SerializeField]
        [Tooltip("The player gameobject")]
        private VRTK_SDKManager _player = null;
        /// <summary>
        /// Layer setup for the player in game.
        /// </summary>
        [SerializeField]
        [Tooltip("Layer setup for the player in game.")]
        private LayerMask _roomLayerMask = new LayerMask();
        /// <summary>
        /// The component that will be used to click on the different zones.
        /// </summary>
        private PointerClicker _laserClicker = null;
        /// <summary>
        /// Next button.
        /// </summary>
        [SerializeField]
        [Tooltip("Next button.")]
        private Button _nextButton = null;

        public override void Init(object obj)
        {
            if (_player == null)
                _player = FindObjectOfType<VRTK_SDKManager>();
            VRTK_SDKSetup setup = _player.loadedSetup;
            if (setup != null)
            {
                var cameras = setup.actualHeadset.GetComponentsInChildren<Camera>();
                foreach (var camera in cameras)
                    camera.cullingMask = _roomLayerMask;
                _laserClicker = setup.actualLeftController.GetComponentInChildren<PointerClicker>(true);
                if (_laserClicker != null)
                {
                    if (_laserClicker.GetComponent<SteamVR_LaserPointer>())
                        _laserClicker.GetComponent<SteamVR_LaserPointer>().enabled = false;
                    _laserClicker.enabled = true;
                }
            }
            var rxpp = (RoomSettings)obj;
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
            _nextButton.onClick.AddListener(() =>
            {
                foreach (var zone in zones)
                {
                    Collider col = zone.GetComponent<Collider>();
                    if (col != null)
                        col.enabled = false;
                }
                Next();
            });
        }

        public override void Next()
        {
            if (_laserClicker != null)
            {
                if (_laserClicker.GetComponent<SteamVR_LaserPointer>())
                    _laserClicker.GetComponent<SteamVR_LaserPointer>().enabled = false;
                _laserClicker.enabled = false;
            }
            base.Next();
        }
    }
}

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

        private SteamVR_LaserPointer _laserPointer = null;
        [SerializeField]
        [Tooltip("The laser's layer mask for target detection.")]
        private LayerMask _laserLayerMask = new LayerMask();
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
            GameObject leftController = null;
            GameObject rightController = null;
            if (setup != null)
            {
                var cameras = setup.actualHeadset.GetComponentsInChildren<Camera>();
                foreach (var camera in cameras)
                    camera.cullingMask = _roomLayerMask;
                leftController = setup.actualLeftController;
                rightController = setup.actualRightController;
            }
            var rxpp = (RoomSettings)obj;
            var zoneManager = new ZoneManager(rightController, leftController);
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
            if (_laserClicker != null && _laserPointer != null)
            {
                Destroy(_laserPointer.holder);
                Destroy(_laserPointer.pointer);
                Destroy(_laserPointer);
                _laserPointer.enabled = false;
            }
            base.Next();
        }
    }
}

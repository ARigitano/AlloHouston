using CRI.HelloHouston.Calibration.UI;
using CRI.HelloHouston.Experience;
using CRI.HelloHouston.Experience.UI;
using System.Linq;
using UnityEngine;
using VRTK;

namespace CRI.HelloHouston.Calibration.UI
{
    internal class ZoneManager
    {
        private UIZone _currentSelectedUIZone;
        private GameObject _leftController;
        private GameObject _rightController;

        public void Select(object sender, ObjectInteractEventArgs e)
        {
            Debug.Log("Select");
            UIZone zone = e.target.GetComponent<UIZone>();
            if (zone != null)
            {
                if (!_currentSelectedUIZone)
                {
                    _currentSelectedUIZone = zone;
                    zone.Select();
                }
                else if (zone.zoneType == _currentSelectedUIZone.zoneType)
                {
                    XPZone xpZone = zone.xpZone;
                    XPContext xpContext = zone.xpContext;
                    zone.Place(_currentSelectedUIZone.xpZone, _currentSelectedUIZone.xpContext);
                    _currentSelectedUIZone.Place(xpZone, xpContext);
                    zone.Unselect();
                    Unselect(sender, e);
                }
            }
        }

        public void Unselect(object sender, ControllerInteractionEventArgs e)
        {
            if (_currentSelectedUIZone != null)
                _currentSelectedUIZone.Unselect();
            _currentSelectedUIZone = null;
        }

        public void Unselect(object sender, ObjectInteractEventArgs e)
        {
            if (_currentSelectedUIZone != null)
                _currentSelectedUIZone.Unselect();
            _currentSelectedUIZone = null;
        }

        public bool IsSelectable(UIZone zone)
        {
            return _currentSelectedUIZone == null || zone.zoneType == _currentSelectedUIZone.zoneType;
        }

        public void DistributeZones(VirtualZone[] virtualZones, ContextZone[] contextZones)
        {
            virtualZones = virtualZones.Shuffle().ToArray();
            foreach (VirtualZone virtualZone in virtualZones)
            {
                var ui = virtualZone.GetComponent<UIZone>();
                if (ui)
                    ui.enabled = true;
            }
            foreach (ContextZone contextZone in contextZones)
            {
                VirtualZone zone = virtualZones.FirstOrDefault(x => x.zoneType == contextZone.xpZone.zoneType && x.xpZone == null);
                if (zone != null && zone.GetComponent<UIZone>() != null)
                    zone.GetComponent<UIZone>().Place(contextZone.xpZone, contextZone.xpContext);
                if (zone != null)
                    zone.Place(contextZone.xpZone, contextZone.xpContext);
            }
        }

        public ZoneManager(GameObject rightController, GameObject leftController)
        {
            _rightController = rightController;
            _leftController = leftController;
            VRTK_InteractUse leftInteractUse = null;
            VRTK_InteractUse rightInteractUse = null;
            VRTK_ControllerEvents leftControllerEvents = null;
            VRTK_ControllerEvents rightControllerEvents = null;
            if (_rightController != null)
            {
                rightInteractUse = _rightController.GetComponentInChildren<VRTK_InteractUse>();
                rightControllerEvents = _rightController.GetComponentInChildren<VRTK_ControllerEvents>();
            }
            if (_leftController != null)
            {
                leftInteractUse = _leftController.GetComponentInChildren<VRTK_InteractUse>();
                leftControllerEvents = _leftController.GetComponentInChildren<VRTK_ControllerEvents>();
            }
            if (leftInteractUse != null)
                leftInteractUse.ControllerUseInteractableObject += Select;
            if (rightInteractUse != null)
                rightInteractUse.ControllerUseInteractableObject += Select;
            if (leftControllerEvents != null)
                leftControllerEvents.GripReleased += Unselect;
            if (rightControllerEvents != null)
                rightControllerEvents.GripReleased += Unselect;
        }

        ~ZoneManager()
        {
            VRTK_InteractUse leftInteractUse = null;
            VRTK_InteractUse rightInteractUse = null;
            VRTK_ControllerEvents leftControllerEvents = null;
            VRTK_ControllerEvents rightControllerEvents = null;
            if (_rightController != null)
            {
                rightInteractUse = _rightController.GetComponentInChildren<VRTK_InteractUse>();
                rightControllerEvents = _rightController.GetComponentInChildren<VRTK_ControllerEvents>();
            }
            if (_leftController != null)
            {
                leftInteractUse = _leftController.GetComponentInChildren<VRTK_InteractUse>();
                leftControllerEvents = _leftController.GetComponentInChildren<VRTK_ControllerEvents>();
            }
            if (leftInteractUse != null)
                leftInteractUse.ControllerUseInteractableObject -= Select;
            if (rightInteractUse != null)
                rightInteractUse.ControllerUseInteractableObject -= Select;
            if (leftControllerEvents != null)
                leftControllerEvents.GripReleased -= Unselect;
            if (rightControllerEvents != null)
                rightControllerEvents.GripReleased -= Unselect;
        }
    }
}
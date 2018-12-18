using CRI.HelloHouston.Calibration.UI;
using CRI.HelloHouston.Experience;
using CRI.HelloHouston.Experience.UI;
using System.Linq;
using UnityEngine;

namespace CRI.HelloHouston.Calibration.UI
{
    internal class ZoneManager
    {
        public UIZone _currentSelectedUIZone;

        public void Select(object sender, IPointerClickable clickable)
        {
            UIZone zone = clickable as UIZone;
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
                    Unselect();
                }
            }
        }

        public void Unselect()
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
            virtualZones.Shuffle();
            foreach (VirtualZone virtualZone in virtualZones)
            {
                var ui = virtualZone.GetComponent<UIZone>();
                if (ui)
                    ui.enabled = true;
            }
            foreach (ContextZone contextZone in contextZones)
            {
                Debug.Log(contextZone.xpZone);
                VirtualZone zone = virtualZones.FirstOrDefault(x => x.zoneType == contextZone.xpZone.zoneType && x.xpZone == null);
                if (zone != null && zone.GetComponent<UIZone>() != null)
                    zone.GetComponent<UIZone>().Place(contextZone.xpZone, contextZone.xpContext);
                if (zone != null)
                    zone.Place(contextZone.xpZone, contextZone.xpContext);
            }
        }

        public ZoneManager(PointerClicker laserClicker)
        {
            laserClicker.onGripClicked += Unselect;
            laserClicker.onHangleTriggerClicked += Select;
        }
    }
}
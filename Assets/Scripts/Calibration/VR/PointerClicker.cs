using CRI.HelloHouston.Calibration.UI;
using System;
using UnityEngine;
using Valve.VR.Extras;

namespace CRI.HelloHouston.Calibration
{
    public class PointerClicker : MonoBehaviour
    {
        public delegate void LaserClickerEvent(object sender, IPointerClickable target);
        private IPointer _pointer;
        private ITrackedController _trackedController;

        private IPointerClickable _currentClickable;

        private ZoneManager _zoneManager;

        public LaserClickerEvent onHandlePointerIn;
        public LaserClickerEvent onHanglePointerOut;
        public LaserClickerEvent onHangleTriggerClicked;
        public Action onGripClicked;

        private void OnEnable()
        {
            _pointer = GetComponent<IPointer>();
            _pointer.PointerIn -= HandlePointerIn;
            _pointer.PointerIn += HandlePointerIn;
            _pointer.PointerOut -= HandlePointerOut;
            _pointer.PointerOut += HandlePointerOut;

            _trackedController = GetComponent<ITrackedController>();
            if (_trackedController == null)
            {
                _trackedController = GetComponentInParent<ITrackedController>();
            }
            _trackedController.TriggerClicked -= HandleTriggerClicked;
            _trackedController.TriggerClicked += HandleTriggerClicked;
            _trackedController.Gripped -= GripClicked;
            _trackedController.Gripped += GripClicked;
        }

        private void GripClicked(object sender, ClickedEventArgs e)
        {
            if (onGripClicked != null)
                onGripClicked();
        }

        private void HandleTriggerClicked(object sender, ClickedEventArgs e)
        {
            if (_currentClickable != null)
            {
                _currentClickable.OnLaserClick();
            }
            if (onHangleTriggerClicked != null)
                onHangleTriggerClicked(this, _currentClickable);
        }

        private void HandlePointerIn(object sender, PointerEventArgs e)
        {
            var clickable = e.target.GetComponent<IPointerClickable>();
            if (clickable != null)
            {
                clickable.OnLaserEnter();
                if (onHandlePointerIn != null)
                    onHandlePointerIn(this, clickable);
                _currentClickable = clickable;
            }
        }

        private void HandlePointerOut(object sender, PointerEventArgs e)
        {
            var clickable = e.target.GetComponent<IPointerClickable>();
            if (clickable != null)
            {
                clickable.OnLaserExit();
                if (onHanglePointerOut != null)
                    onHanglePointerOut(this, clickable);
                _currentClickable = null;
            }
        }
    }
}

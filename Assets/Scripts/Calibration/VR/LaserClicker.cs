using System;
using UnityEngine;

namespace CRI.HelloHouston.Calibration
{
    /// <summary>
    /// Inputs linked to the Vive controller's laser
    /// </summary>
    public class LaserClicker : MonoBehaviour
    {
        public delegate void LaserClickerEvent(object sender, ILaserClickable target);
        private ILaserPointer _laserPointer;
        private ITrackedController _trackedController;

        private ILaserClickable _currentClickable;

        private ZoneManager _zoneManager;

        public LaserClickerEvent onHandlePointerIn;
        public LaserClickerEvent onHanglePointerOut;
        public LaserClickerEvent onHangleTriggerClicked;
        public Action onGripClicked;

        private void OnEnable()
        {
            _laserPointer = GetComponent<ILaserPointer>();
            _laserPointer.PointerIn -= HandlePointerIn;
            _laserPointer.PointerIn += HandlePointerIn;
            _laserPointer.PointerOut -= HandlePointerOut;
            _laserPointer.PointerOut += HandlePointerOut;

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
            var clickable = e.target.GetComponent<ILaserClickable>();
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
            var clickable = e.target.GetComponent<ILaserClickable>();
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calibration.VR
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    /// <summary>
    /// Inputs linked to the Vive controller's laser
    /// </summary>
    [RequireComponent(typeof(SteamVR_LaserPointer))]
    public class ViveLaserClicker : MonoBehaviour
    {
        private SteamVR_LaserPointer _laserPointer;
        private SteamVR_TrackedController _trackedController;

        private ILaserClickable _currentClickable;

        private void OnEnable()
        {
            _laserPointer = GetComponent<SteamVR_LaserPointer>();
            _laserPointer.PointerIn -= HandlePointerIn;
            _laserPointer.PointerIn += HandlePointerIn;
            _laserPointer.PointerOut -= HandlePointerOut;
            _laserPointer.PointerOut += HandlePointerOut;

            _trackedController = GetComponent<SteamVR_TrackedController>();
            if (_trackedController == null)
            {
                _trackedController = GetComponentInParent<SteamVR_TrackedController>();
            }
            _trackedController.TriggerClicked -= HandleTriggerClicked;
            _trackedController.TriggerClicked += HandleTriggerClicked;
        }

        private void HandleTriggerClicked(object sender, ClickedEventArgs e)
        {
            if (_currentClickable != null)
                _currentClickable.OnLaserClick();
        }

        private void HandlePointerIn(object sender, PointerEventArgs e)
        {
            var clickable = e.target.GetComponent<ILaserClickable>();
            if (clickable != null)
            {
                clickable.OnLaserEnter();
                _currentClickable = clickable;
            }
        }

        private void HandlePointerOut(object sender, PointerEventArgs e)
        {
            var clickable = e.target.GetComponent<ILaserClickable>();
            if (clickable != null)
            {
                clickable.OnLaserExit();
                _currentClickable = null;
            }
        }
    }
}

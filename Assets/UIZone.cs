using System;
using CRI.HelloHouston.Calibration;
using UnityEngine;

namespace CRI.HelloHouston.Calibration.UI
{
    public class UIZone : MonoBehaviour, ILaserClickable
    {
        /// <summary>
        /// An instance of virtual zone.
        /// </summary>
        [SerializeField]
        [Tooltip("An instance virtual zone.")]
        private VirtualZone _zone;

        void ILaserClickable.OnLaserClick()
        {
            // Send the XPContext to the Manager as the current selection
        }

        void ILaserClickable.OnLaserEnter()
        {
           // Selected
        }

        void ILaserClickable.OnLaserExit()
        {
            // Unselected
        }

        private void Reset()
        {
            _zone = GetComponent<VirtualZone>();
        }
    }
}

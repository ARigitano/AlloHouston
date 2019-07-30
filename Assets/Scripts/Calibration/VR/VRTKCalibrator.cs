using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

namespace CRI.HelloHouston.Calibration
{
    public class VRTKCalibrator : MonoBehaviour, ICalibrator
    {
        /// <summary>
        /// Tracked object.
        /// </summary>
        private VRTK_ControllerEvents events;
        /// <summary>
        /// The transform of the position on which the position tags will be created.
        /// </summary>
        [SerializeField]
        [Tooltip("The transform of the position on which the position tags will be created.")]
        private Transform _spawnPosition = null;

        private void Start()
        {
            events = GetComponentInChildren<VRTK_ControllerEvents>();
        }

        /// <summary>
        /// True if the calibration reset button is up.
        /// </summary>
        /// <returns></returns>
        public bool GetCalibrationResetUp()
        {
            return events.gripPressed;
        }
        /// <summary>
        /// True if the trigger button is up.
        /// </summary>
        /// <returns></returns>
        public bool GetCalibrationTriggerUp()
        {
            return events.triggerClicked;
        }

        public Vector3 GetCalibrationPosition()
        {
            return _spawnPosition.position;
        }
    }
}

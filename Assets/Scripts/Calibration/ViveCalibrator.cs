using System;
using UnityEngine;

namespace CRI.HelloHouston.Calibration
{
    [RequireComponent(typeof(SteamVR_TrackedObject))]
    public class ViveCalibrator : MonoBehaviour, ICalibrator
    {
        /// <summary>
        /// Tracked object.
        /// </summary>
        [SerializeField]
        [Tooltip("Tracked object.")]
        private SteamVR_TrackedObject _trackedObject;
        private SteamVR_Controller.Device _device;
        /// <summary>
        /// The transform of the position on which the position tags will be created.
        /// </summary>
        [SerializeField]
        [Tooltip("The transform of the position on which the position tags will be created.")]
        private Transform _spawnPosition = null;

        private void Reset()
        {
            _trackedObject = GetComponent<SteamVR_TrackedObject>();
        }

        private void Start()
        {
            _device = SteamVR_Controller.Input((int)_trackedObject.index);
        }

        public bool GetCalibrationResetUp()
        {
            return _device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger);
        }

        public bool GetCalibrationTriggerUp()
        {
            return _device.GetPressUp(SteamVR_Controller.ButtonMask.Grip);
        }

        public Vector3 GetCalibrationPosition()
        {
            return _spawnPosition.position;
        }
    }
}

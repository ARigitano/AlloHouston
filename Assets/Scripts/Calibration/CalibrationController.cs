using CRI.HelloHouston.Calibration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CRI.HelloHouston.Calibration
{
    [RequireComponent(typeof(SteamVR_TrackedObject))]
    public class CalibrationController : MonoBehaviour
    {
        /// <summary>
        /// A vive controller with a precision spike.
        /// </summary>
        [Tooltip("A vive controller with a precision spike.")]
        public SteamVR_TrackedObject trackedObject;
        /// <summary>
        /// The pointer of the controller.
        /// </summary>
        [Tooltip("The pointer of the controller.")]
        public VivePointer pointer;
        /// <summary>
        /// The transform of the position on which the position tags will be created.
        /// </summary>
        [Tooltip("The transform of the position on which the position tags will be created.")]
        public Transform spawnPosition;
        /// <summary>
        /// The calibration manager.
        /// </summary>
        [Tooltip("The calibration manager.")]
        public CalibrationManager calibrationManager;
        /// <summary>
        /// If true, the calibration has started.
        /// </summary>
        [SerializeField]
        [Tooltip("If true, the calibration has started.")]
        private bool _calibration = false;
        /// <summary>
        /// Cooldown between the creation of a position tag and its deletion.
        /// </summary>
        [SerializeField]
        [Tooltip("Cooldown between the creation of a position tag and its deletion")]
        private float _cooldownTime = 1.0f;
        
        private float _lastCreation = Time.time;

        private void Reset()
        {
            trackedObject = GetComponent<SteamVR_TrackedObject>();
            pointer = GetComponentInChildren<VivePointer>();
        }

        private void Start()
        {
            calibrationManager = FindObjectOfType<CalibrationManager>();
        }

        /// <summary>
        /// Starts the calibration.
        /// </summary>
        public void StartCalibration()
        {
            _calibration = true;
        }

        /// <summary>
        /// Stops the calibration.
        /// </summary>
        public void StopCalibration()
        {
            _calibration = false;
        }

        private void Update()
        {
            if (_calibration)
            {
                SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObject.index);
                if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
                {
                    //On trigger press: either create a position tag or instantiate an object
                    if (!pointer.isTouchingPoint)
                    {
                        if (calibrationManager.canCreatePositionTag)
                        {
                            calibrationManager.CreatePositionTag(spawnPosition.position);
                            _lastCreation = Time.time;
                        }
                        else
                            calibrationManager.CalibrateVR();
                    }
                    else if (pointer.incorrectPoint != null && Time.time - _lastCreation > _cooldownTime)
                    {
                        calibrationManager.RemovePositionTag(pointer.incorrectPoint);
                        pointer.ResetPointer();
                    }
                }
                else if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
                {
                    calibrationManager.ResetPositionTags();
                    pointer.ResetPointer();
                }
            }
        }
    }
}

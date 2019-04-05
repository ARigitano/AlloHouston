using UnityEngine;

namespace CRI.HelloHouston.Calibration
{
    public class CalibrationController : MonoBehaviour
    {
        /// <summary>
        /// A calibrator.
        /// </summary>
        [SerializeField]
        [Tooltip("A calibrator.")]
        private GameObject _calibrator;
        /// <summary>
        /// The pointer of the controller.
        /// </summary>
        [SerializeField]
        [Tooltip("The pointer of the controller.")]
        private CalibrationPointer _pointer;
        /// <summary>
        /// The calibration manager.
        /// </summary>
        private CalibrationManager _calibrationManager;
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
        /// <summary>
        /// The virtual item that will be calibrated.
        /// </summary>
        private VirtualItem _virtualItem;

        private float _lastCreation;

        private void OnValidate()
        {
            if (_calibrator != null && _calibrator.GetComponent<ICalibrator>() == null)
            {
                Debug.LogError("The calibrator needs to implement the ICalibrator interface.");
                _calibrator = null;
            }
        }

        private void Reset()
        {
            _pointer = GetComponentInChildren<CalibrationPointer>();
        }

        private void Start()
        {
            _lastCreation = Time.time;
            _calibrationManager = FindObjectOfType<CalibrationManager>();
        }

        /// <summary>
        /// Starts the calibration.
        /// </summary>
        public void StartCalibration()
        {
            _pointer.ResetPointer();
            _calibration = true;
        }

        /// <summary>
        /// Stops the calibration.
        /// </summary>
        public void StopCalibration()
        {
            _pointer.ResetPointer();
            _calibration = false;
        }

        private void Update()
        {
            if (_calibration && _calibrationManager)
            {
                if (_calibrator.GetComponent<ICalibrator>().GetCalibrationTriggerUp())
                {
                    Debug.Log("Up");
                    //On trigger press: either create a position tag or instantiate an object
                    if (!_pointer.isTouchingPoint)
                    {
                        if (_calibrationManager.canCreatePositionTag)
                        {
                            _calibrationManager.CreatePositionTag(_calibrator.GetComponent<ICalibrator>().GetCalibrationPosition());
                            _lastCreation = Time.time;
                        }
                        else
                        {
                            Debug.Log("Stop");
                            _calibrationManager.CalibrateCurrentVirtualItem();
                            _calibrationManager.StopObjectCalibration();
                        }
                    }
                    else if (_pointer.incorrectPoint != null && Time.time - _lastCreation > _cooldownTime)
                    {
                        _calibrationManager.RemovePositionTag(_pointer.incorrectPoint);
                        _pointer.ResetPointer();
                    }
                }
                else if (_calibrator.GetComponent<ICalibrator>().GetCalibrationResetUp())
                {
                    _calibrationManager.RemoveLastPositionTag();
                    _pointer.ResetPointer();
                }
            }
        }
    }
}

using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

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
        private Hand _device;
        [SerializeField]
        private SteamVR_Action_Boolean _grip;
        [SerializeField]
        private SteamVR_Action_Boolean _trigger;
        /// <summary>
        /// The transform of the position on which the position tags will be created.
        /// </summary>
        [SerializeField]
        [Tooltip("The transform of the position on which the position tags will be created.")]
        private Transform _spawnPosition = null;

        private void Reset()
        {
            _device = gameObject.GetComponent<Hand>();
        }

        /// <summary>
        /// True if the calibration reset button is up.
        /// </summary>
        /// <returns></returns>
        public bool GetCalibrationResetUp()
        {
            return _grip.stateUp;
        }
        /// <summary>
        /// True if the trigger button is up.
        /// </summary>
        /// <returns></returns>
        public bool GetCalibrationTriggerUp()
        {
            return _trigger.stateUp;
        }

        public Vector3 GetCalibrationPosition()
        {
            return _spawnPosition.position;
        }
    }
}

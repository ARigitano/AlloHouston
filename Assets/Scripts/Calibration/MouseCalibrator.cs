using UnityEngine;

namespace CRI.HelloHouston.Calibration
{
    public class MouseCalibrator : MonoBehaviour, ICalibrator
    {
        /// <summary>
        /// True if the calibration reset button is up.
        /// </summary>
        /// <returns></returns>
        public bool GetCalibrationResetUp()
        {
            return Input.GetMouseButtonUp(1);
        }
        /// <summary>
        /// True if the calibration trigger button is up.
        /// </summary>
        /// <returns></returns>
        public bool GetCalibrationTriggerUp()
        {
            return Input.GetMouseButtonUp(0);
        }
        /// <summary>
        /// Returns the calibration position.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetCalibrationPosition()
        {
            return Input.mousePosition;
        }
    }
}


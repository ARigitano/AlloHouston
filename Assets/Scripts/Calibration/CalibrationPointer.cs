using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRCalibrationTool;

namespace CRI.HelloHouston.Calibration
{
    /// <summary>
    /// Represents the 3D printed precision spike added to the Vive controller in order to instantiate the position tags more easily
    /// </summary>
    public class CalibrationPointer : MonoBehaviour
    {
        /// <summary>
        /// Is the precision spike touching an instantiated position tag?
        /// </summary>
        public bool isTouchingPoint { get; private set; }
        /// <summary>
        /// Is the precision spike touching a ViveTracker?
        /// </summary>
        public bool isTouchingTracker { get; private set; }
        /// <summary>
        /// Position tag considered as incorrectly positionned.
        /// </summary>
        public PositionTag incorrectPoint { get; private set; }

        public void ResetPointer()
        {
            isTouchingPoint = false;
            isTouchingTracker = false;
            incorrectPoint = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "PositionTag")
            {
                isTouchingPoint = true;
                incorrectPoint = other.gameObject.GetComponent<PositionTag>();
            }
            else if (other.tag == "ViveTracker")
            {
                isTouchingTracker = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "PositionTag")
            {
                isTouchingPoint = false;
                incorrectPoint = null;
            }
        }
    }
}

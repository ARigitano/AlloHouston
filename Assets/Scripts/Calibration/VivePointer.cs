using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRCalibrationTool;

namespace CRI.HelloHouston.Calibration
{
    /// <summary>
    /// Represents the 3D printed precision spike added to the Vive controller in order to instantiate the position tags more easily
    /// </summary>
    public class VivePointer : MonoBehaviour
    {
        [SerializeField]
        private ViveControllerManager _viveManager = null;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "PositionTag")
            {
                _viveManager._touchingPoint = true;
                _viveManager._incorrectPoint = other.gameObject.GetComponent<PositionTag>();
            }
            else if (other.tag == "ViveTracker")
            {
                _viveManager._touchingTracker = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "PositionTag")
            {
                _viveManager._touchingPoint = false;
                _viveManager._incorrectPoint = null;
            }
        }
    }
}

using System;
using UnityEngine;

namespace CRI.HelloHouston.Calibration
{
    public class MouseCalibrator : MonoBehaviour, ICalibrator
    {
        public bool GetCalibrationResetUp()
        {
            return Input.GetMouseButtonUp(1);
        }

        public bool GetCalibrationTriggerUp()
        {
            return Input.GetMouseButtonUp(0);
        }

        public Vector3 GetCalibrationPosition()
        {
            return Input.mousePosition;
        }
    }
}


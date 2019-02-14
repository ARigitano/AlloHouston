using UnityEngine;

namespace CRI.HelloHouston.Calibration
{
    public interface ICalibrator
    {
        bool GetCalibrationTriggerUp();
        bool GetCalibrationResetUp();
        Vector3 GetCalibrationPosition();
    }
}

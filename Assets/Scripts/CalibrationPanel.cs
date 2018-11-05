using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalibrationPanel : MonoBehaviour
{

    [SerializeField]
    private Text title, next, item, status, calibration, date;
    [SerializeField]
    private Translation translation;
    [SerializeField]
    private Language setLanguage;

   


    void OnEnable()
    {
        translation = setLanguage.translation;
        title.text = translation.calibrationTitle;
        next.text = translation.calibrationNext;
        item.text = translation.calibrationItem;
        status.text = translation.calibrationStatus;
        calibration.text = translation.calibrationCalibration;
        date.text = translation.calibrationDate;
    }
}

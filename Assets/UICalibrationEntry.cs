using CRI.HelloHouston.Calibration.XML;
using UnityEngine;
using UnityEngine.UI;
using VRCalibrationTool;

public class UICalibrationEntry : MonoBehaviour {
    /// <summary>
    /// Text field of the entry's name.
    /// </summary>
    [SerializeField]
    [Tooltip("Text field of the entry's name.")]
    private Text _nameText;
    /// <summary>
    /// Button to start the calibration of the entry.
    /// </summary>
    [SerializeField]
    [Tooltip("Button to start the calibration of the entry.")]
    private Button _calibrationButton;
    /// <summary>
    /// Image to tell if the calibration is successful.
    /// </summary>
    [SerializeField]
    [Tooltip("Image to tell if the calibration is successful")]
    private Image _calibrationSuccessfulImage;
    /// <summary>
    /// Text field of the date of the last calibration.
    /// </summary>
    [SerializeField]
    [Tooltip("Text field of the date of the last calibration.")]
    private Text _dateText;

    private int _calibrationEntryIndex;

    public void Init(VirtualObject virtualEntry, CalibrationEntry calibrationEntry)
    {
    }
}

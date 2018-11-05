using UnityEngine;
using UnityEngine.UI;

public class UICalibrationValidationButton : MonoBehaviour {
    /// <summary>
    /// Color when the calibration is valid.
    /// </summary>
    [SerializeField]
    [Tooltip("Color when the calibration is valid.")]
    private Color _validColor = Color.black;
    /// <summary>
    /// Color when the calibration is invalid.
    /// </summary>
    [SerializeField]
    [Tooltip("Color when the calibration is invalid.")]
    private Color _invalidColor = Color.black;
    /// <summary>
    /// Sprite when the calibration is valid.
    /// </summary>
    [SerializeField]
    [Tooltip("Sprite when the calibration is valid.")]
    private Sprite _validSprite = null;
    /// <summary>
    /// Sprite when the calibration is invalid.
    /// </summary>
    [SerializeField]
    [Tooltip("Sprite when the calibration is invalid.")]
    private Sprite _invalidSprite = null;
    /// <summary>
    /// Image of the validation.
    /// </summary>
    [SerializeField]
    [Tooltip("Image of the validation.")]
    private Image _image = null;
    /// <summary>
    /// Image of the background.
    /// </summary>
    [SerializeField]
    [Tooltip("Image of the background")]
    private Image _backgroundImage = null;

    /// <summary>
    /// Sets the validation of the button.
    /// </summary>
    public void SetValidation(bool validation)
    {
        _image.sprite = validation ? _validSprite : _invalidSprite;
        _backgroundImage.color = validation ? _validColor : _invalidColor;
    }
}

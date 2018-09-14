using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRCalibrationTool;

/// <summary>
/// One of the four colored button to press to solve the color experiment
/// </summary>
public class ColorButton : MonoBehaviour
{
    [SerializeField] private ColorXP _colorXP;  //The instance of color experiment that this button belongs to           
    public string _inputValue;                  //The value of the button unmodified by the state of ColorXP's led
    public string _inputValueSend;              //The value sent when pressing this button
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if(_colorXP._isLedOn)
        {
            _inputValueSend = _inputValue + "ok";
        }
        else
        {
            _inputValueSend = _inputValue;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ViveController" && !_colorXP._fixed) {
            _colorXP.Resolved(_inputValueSend);
            _colorXP._fixed = true;
		}
	}
}

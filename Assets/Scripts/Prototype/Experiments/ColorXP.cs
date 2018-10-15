using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRCalibrationTool;
using UnityEngine.UI;

/// <summary>
/// Experiment: the player must choose the right color button among four according to the error
/// code on the table, and place the tracked cube or not on the table depending on the color
/// of the LED.
/// </summary>
public class ColorXP : Experimentation {

    [SerializeField] private GameObject _led;       //LED must be on to clear incident
    [SerializeField] private Material[] _ledColor;  //LED matarials, 0 LED on, 1 LED off
    [SerializeField] private int _ledState;         //LED state picked randomly
    public bool _isLedOn;                           //Is the LED on?
    public string _ledMessage;                      //String sent to notify when LED is on

    // Use this for initialization
    private void Start ()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _ledState = Random.Range(0, 2);

        _error = Random.Range(0, 3);

        switch (_error)
        {
            case 0:
                _errorReference = "AB484";
                break;
            case 1:
                _errorReference = "KQ208";
                break;
            case 2:
                _errorReference = "CR101";
                break;
            case 3:
                _errorReference = "AH332";
                break;
            default:
                break;
        }

        _errorText = _errorReference+"ok";

        _table = GameObject.FindGameObjectWithTag("Table").GetComponent<Room>();

        if (_table != null && _table._canvas)
        {
            AttachPanel();
            _text.transform.GetComponent<Text>().text = _expNumber.text +": Error "+_errorReference;
        }
    }

    private void Update()
    {
        if (_ledState == 0)
        {
            if (_table.GetComponent<Table>()._trackedObject == "cube")
                _isLedOn = true;
            else
                _isLedOn = false;
        }
        else
        {
            if (_table.GetComponent<Table>()._trackedObject == "cube")
                _isLedOn = false;
            else
                _isLedOn = true;
        }

        if (_isLedOn)
        {
            _led.GetComponent<MeshRenderer>().material = _ledColor[0];
            _ledMessage = "ok";
        }
        else {
            _led.GetComponent<MeshRenderer>().material = _ledColor[1];
            _ledMessage = "";
        }



    }
}

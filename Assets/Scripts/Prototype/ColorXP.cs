//Experiment in which the player must press the right color button


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRCalibrationTool;
using UnityEngine.UI;


public class ColorXP : Experimentation {

    [SerializeField] private GameObject _led;
    [SerializeField] private Material[] _ledColor; //Materials if led is 0 on or 1-2 off
    [SerializeField] private int _ledState;
    public bool _isLedOn;
    public string _ledMessage;

    // Use this for initialization
    private void Start ()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _ledState = Random.Range(0, 2);

        

        error = Random.Range(0, 3);

        switch (error)
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

        _errorText = _errorReference;

        table = GameObject.FindGameObjectWithTag("Table").GetComponent<Room>();

        if (table != null && table.canvas)
        {
            AttachPanel();
            text.transform.GetComponent<Text>().text = _expNumber.text +": Error "+_errorReference;
        }
    }

    private void Update()
    {
        if (_ledState == 0)
        {
            if (table.GetComponent<Table>()._trackedObject == "cube")
                _isLedOn = true;
            else
                _isLedOn = false;
        }
        else
        {
            if (table.GetComponent<Table>()._trackedObject == "cube")
                _isLedOn = false;
            else
                _isLedOn = true;
        }

        if (_isLedOn)
        {
            _led.GetComponent<MeshRenderer>().material = _ledColor[0];
            _ledMessage = "";
        }
        else {
            _led.GetComponent<MeshRenderer>().material = _ledColor[1];
            _ledMessage = "";
        }



    }
}

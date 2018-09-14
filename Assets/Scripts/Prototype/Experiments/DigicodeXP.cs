using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRCalibrationTool;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Experiment: the player must modify the number displayer according to the error code on the 
/// table and then validate.
/// </summary>
public class DigicodeXP : Experimentation
{
    public int _number;                             //Number to modify to clear incident
    [SerializeField] public TextMeshPro _numberText;   //Shows that number
    

	// Use this for initialization
	void Start ()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _table = GameObject.FindGameObjectWithTag("Table").GetComponent<Room>();
        _number = Random.RandomRange(1, 99);
        _numberText.text = _number.ToString();

        if (_table != null && _table._canvas)
        {
            AttachPanel();
            _error = Random.Range(0, 5);

            switch (_error)
            {
                case 0:
                    _errorReference = "He";
                    _error = _number+5;
                    break;
                case 1:
                    _errorReference = "Au";
                    _error = _number*3;
                    break;
                case 2:
                    _errorReference = "Ag";
                    _error = (_number+7)*2;
                    break;
                case 3:
                    _errorReference = "K";
                    _error = _number * _number;
                    break;
                case 4:
                    _errorReference = "O";
                    _error = _number+_number*_number;
                    break;
                case 5:
                    _errorReference = "C";
                    _error = _number * _number + _number*3;
                    break;
                default:
                    break;
            }

            _errorText = _error.ToString();
            _text.transform.GetComponent<Text>().text = _expNumber.text + ": Error " + _errorReference;
        }
    }
}

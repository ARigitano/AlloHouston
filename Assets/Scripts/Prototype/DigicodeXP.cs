using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRCalibrationTool;
using UnityEngine.UI;

public class DigicodeXP : Experimentation
{
    public int _number; //Number to modify to clear incident
    [SerializeField] public TextMesh _numberText; //Shows that number
    

	// Use this for initialization
	void Start () {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        table = GameObject.FindGameObjectWithTag("Table").GetComponent<Room>();

        _number = Random.RandomRange(1, 99);
        _numberText.text = _number.ToString();

        if (table != null && table.canvas)
        {
            AttachPanel();
            error = Random.Range(0, 5);

            switch (error)
            {
                case 0:
                    _errorReference = "He";
                    error = _number+5;
                    break;
                case 1:
                    _errorReference = "Au";
                    error = _number*3;
                    break;
                case 2:
                    _errorReference = "Ag";
                    error = (_number+7)*2;
                    break;
                case 3:
                    _errorReference = "K";
                    error = _number * _number;
                    break;
                case 4:
                    _errorReference = "O";
                    error = _number+_number*_number;
                    break;
                case 5:
                    _errorReference = "C";
                    error = _number * _number + _number*3;
                    break;
                default:
                    break;
            }

            _errorText = error.ToString();

            text.transform.GetComponent<Text>().text = _expNumber.text + ": Error " + _errorReference;
        }

    }
}

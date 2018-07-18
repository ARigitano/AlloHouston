using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRCalibrationTool;
using UnityEngine.UI;

public class DigicodeXP : Experimentation
{

    [SerializeField] private TextMesh _numberText;
    private int _number;

	// Use this for initialization
	void Start () {
        _number = Random.RandomRange(1, 99);
        _numberText.text = _number.ToString();

        if (table != null && table.canvas)
        {
            AttachPanel();
            error = Random.Range(0, 3);

            switch (error)
            {

                case 0:
                    _errorReference = "AB484";
                    break;
                case 1:
                    _errorReference = "KJ208";
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

            text.transform.GetComponent<Text>().text = _expNumber.text + ": Error " + _errorReference;
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

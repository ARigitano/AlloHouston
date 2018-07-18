//Experiment in which the player must press the right color button


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRCalibrationTool;
using UnityEngine.UI;


public class ColorXP : Experimentation {

    // Use this for initialization
    void Start ()
    {
        

        if (table != null && table.canvas)
        {
            AttachPanel();
            error = Random.Range(0, 5);

            switch(error)
            {

                    case 0:
                    _errorReference = "He";
                    break;
                    case 1:
                    _errorReference = "Au";
                    break;
                    case 2:
                    _errorReference = "Ag";
                    break;
                case 3:
                    _errorReference = "K";
                    break;
                case 4:
                    _errorReference = "O";
                    break;
                case 5:
                    _errorReference = "C";
                    break;
                default:
                    break;
            }

            text.transform.GetComponent<Text>().text = _expNumber.text +": Error "+_errorReference;
        }
    }


}

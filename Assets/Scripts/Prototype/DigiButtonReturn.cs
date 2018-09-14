using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Button from the digicode experiment to delete the current number
/// </summary>
public class DigiButtonReturn : MonoBehaviour {

    [SerializeField] private DigicodeXP _digicodXP; //Digicode experiment linked with this button

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ViveController")
        {
            _digicodXP._number = 0;
            _digicodXP._numberText.text = _digicodXP._number.ToString();
        }
    }
}

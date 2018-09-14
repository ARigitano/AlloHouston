using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Button from the digicode experiment to send the final number in an attempt to clear the incident
public class DigiButtonValidation : MonoBehaviour {

    [SerializeField] private DigicodeXP _digicodXP; //Digicode experiment linked with this button

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ViveController" && !_digicodXP._fixed)
        {
            _digicodXP.Resolved(_digicodXP._number.ToString());
            _digicodXP._fixed = true;
        }
    }
}

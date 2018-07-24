using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigiButtonValidation : MonoBehaviour {

    [SerializeField] private DigicodeXP _digicodXP;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ViveController" && !_digicodXP._fixed)
        {
            _digicodXP.Resolved(_digicodXP._number.ToString());
            _digicodXP._fixed = true;
        }
    }

}

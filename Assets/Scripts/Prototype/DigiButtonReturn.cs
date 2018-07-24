using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DigiButtonReturn : MonoBehaviour {

    [SerializeField] private DigicodeXP _digicodXP; //Digicode experiment linked with this button

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {

            //_digicodXP._numberText.text = _digicodXP._numberText.text.Substring(0, _digicodXP._numberText.text.Length-1);
            //_digicodXP._number = Convert.ToInt32(_digicodXP._numberText.text);
            //Debug.Log(_digicodXP._number);
     
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ViveController")
        {
            _digicodXP._number = 0;
            _digicodXP._numberText.text = _digicodXP._number.ToString();
            Debug.Log(_digicodXP._number);
        }
    }
}

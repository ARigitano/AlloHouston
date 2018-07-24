using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DigiButton : MonoBehaviour
{
    [SerializeField] private int _value; //Value of this button
    [SerializeField] private DigicodeXP _digicodXP; //Digicode experiment linked with this button
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ViveController")
        {
            if(_digicodXP._number == 0)
            {
                _digicodXP._number = _value;
                _digicodXP._numberText.text = _digicodXP._number.ToString();
            } else
            {
                _digicodXP._numberText.text += _value.ToString();
                _digicodXP._number = Convert.ToInt32(_digicodXP._numberText.text);
            }

            
        }
    }
}

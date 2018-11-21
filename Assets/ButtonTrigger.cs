using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTrigger : MonoBehaviour {

    private bool _controllerIn = false;
    private bool _activated = false;

    private float _time;

	// Use this for initialization
	void Start ()
    {
        _time = Time.time;
        Debug.Log("Start");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Time.time - _time > 1.0f && !_activated && !_controllerIn)
        {
            Debug.Log("Activation");
            _activated = true;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ViveController" && !_activated && enabled)
        {
            _controllerIn = true;
            Debug.Log("already in");
        }
        else if (other.tag == "ViveController" && enabled)
        {
            this.gameObject.GetComponent<Button>().onClick.Invoke();
            Debug.Log("done2");
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ViveController" && enabled)
        {
            _controllerIn = false;
            Debug.Log("now ok");
        }
    }
}

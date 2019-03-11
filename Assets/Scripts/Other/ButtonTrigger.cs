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
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Time.time - _time > 0.5f && !_activated && !_controllerIn)
        {
            _activated = true;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "ViveTracker" || other.tag == "ViveController") && !_activated && enabled)
        {
            _controllerIn = true;
        }
        else if ((other.tag == "ViveTracker" || other.tag == "ViveController") && enabled)
        {
            this.gameObject.GetComponent<Button>().onClick.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.tag == "ViveTracker" || other.tag == "ViveController") && enabled)
        {
            _controllerIn = false;
        }
    }
}

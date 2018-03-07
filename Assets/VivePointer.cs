using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VRCalibrationTool
{
public class VivePointer : MonoBehaviour {

	
		[SerializeField] private ViveControllerManager viveManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "PositionTag") {
			viveManager.touchingPoint = true;
				viveManager.incorrectPoint = other.gameObject;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "PositionTag") {
				viveManager.touchingPoint = false;
				viveManager.incorrectPoint = null;
		}
	}
}
}

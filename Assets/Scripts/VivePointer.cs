using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VRCalibrationTool
{
	public class VivePointer : MonoBehaviour {

		[SerializeField] private ViveControllerManager viveManager;

		void OnTriggerEnter(Collider other) {
			if (other.tag == "PositionTag") {
				viveManager.touchingPoint = true;
				viveManager.incorrectPoint = other.gameObject;
			} else if (other.tag == "ViveTracker") {
				viveManager.touchingTracker = true;
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

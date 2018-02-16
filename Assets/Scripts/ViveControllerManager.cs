using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRCalibrationTool
{
	public class ViveControllerManager : MonoBehaviour {

		public SteamVR_TrackedObject trackedObj;

		// Use this for initialization
		void Start () {
		
		}
	
		// Update is called once per frame
		void Update ()
		{

			SteamVR_Controller.Device device = SteamVR_Controller.Input ((int)trackedObj.index);
			if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
				Debug.Log ("Trigger pressed");
				var controllerPosition = trackedObj.transform.position;
				MouseManager().CreatePositionTag cPT = new VRCalibrationTool.MouseManager().CreatePositionTag (controllerPosition);
			}
		
		}
	}
}

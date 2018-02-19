using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRCalibrationTool
{
	public class ViveControllerManager : MonoBehaviour
	{

		[SerializeField] private SteamVR_TrackedObject _trackedObj;
		[SerializeField] private PositionTag _positionTag;
		public PositionTag[] PositionTags;
		private int indexPositionTag = 0;

		// Use this for initialization
		void Start ()
		{
		
		}

		/// <summary>
		/// Creates a new position tag at the position of the controller.
		/// </summary>
		void CreatePositionTag ()
		{
			PositionTag pTag = (PositionTag)Instantiate (_positionTag, _trackedObj.transform.position, Quaternion.identity);
			pTag.GetComponent<PositionTag> ().positionTagIndex = indexPositionTag;
			PositionTags [indexPositionTag] = pTag;
			indexPositionTag++;
			Debug.Log ("New position tag created");
		}
	
		// Update is called once per frame
		void Update ()
		{
			SteamVR_Controller.Device device = SteamVR_Controller.Input ((int)_trackedObj.index);
			if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
				if (indexPositionTag < 3) {
					CreatePositionTag ();
				} else {
					GameObject VirtualObject = GameObject.Find ("Cube");
					if (VirtualObject.GetComponent<VirtualObject> () != null) {
						VirtualObject.GetComponent<VirtualObject> ().Calibrate (PositionTags);
					}
				}
			}
		
		}

	}
}

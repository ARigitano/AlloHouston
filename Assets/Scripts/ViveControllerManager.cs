using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRCalibrationTool
{
	public class ViveControllerManager : MonoBehaviour
	{

		[SerializeField] private SteamVR_TrackedObject _trackedObj;
		[SerializeField] private PositionTag _positionTag;
		[SerializeField] private Transform _spawnPosition;
		[SerializeField] private int _numberPoints;
		[SerializeField] private int indexPositionTag = 0;
		[SerializeField] private bool hasWaited = false;
		//private int temporaryIndex;
		public PositionTag[] PositionTags;
		public bool touchingPoint = false;
		public GameObject incorrectPoint;


		/// <summary>
		/// Creates a new position tag at the position of the controller.
		/// </summary>
		void CreatePositionTag ()
		{
			PositionTag pTag = (PositionTag)Instantiate (_positionTag, _spawnPosition.position, Quaternion.identity);
			for (int i = 0; i < PositionTags.Length; i++) {
				if (PositionTags [i] == null) {
					pTag.GetComponent<PositionTag> ().positionTagIndex = i;
					pTag.GetComponent<Renderer> ().material.color = new Color (i * 0.2f, i * 0.2f, i * 0.2f, 0.6f);
					PositionTags [i] = pTag;
					break;
				}
			}

			indexPositionTag++;
			Debug.Log ("New position tag created");
			StartCoroutine ("Waiting");

		}
			

		IEnumerator Waiting() {
			hasWaited = false;
			yield return new WaitForSeconds (1);
			hasWaited = true;
		}

		void RemovePositionTag(GameObject pointRemove) {
			Destroy (pointRemove);
			indexPositionTag--;
			touchingPoint = false;
			incorrectPoint = null;
			Debug.Log ("Position tag destroyed");
		}
	
		// Update is called once per frame
		void Update ()
		{
			SteamVR_Controller.Device device = SteamVR_Controller.Input ((int)_trackedObj.index);
			if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
				if (!touchingPoint) {
					if (indexPositionTag < _numberPoints) {
						CreatePositionTag ();
					} else {
						GameObject VirtualObject = GameObject.Find ("Cube");
						if (VirtualObject.GetComponent<VirtualObject> () != null) {
							VirtualObject.GetComponent<VirtualObject> ().Calibrate (PositionTags);
						}
					}
				} else if (incorrectPoint != null && hasWaited && touchingPoint) {
					RemovePositionTag (incorrectPoint);
				}
			}
		
		}

	}
}

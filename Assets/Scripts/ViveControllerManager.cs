using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
		[SerializeField] private string _objectName;
		[SerializeField] private Material[] _materialDistances;
		//private int temporaryIndex;
		public PositionTag[] PositionTags;
		public bool touchingPoint = false;
		public bool touchingTracker = false;
		public GameObject incorrectPoint;
		private GameObject virtualObject;
		private float[] distancePoints;
		private PositionTag[] orderedTags;

		void Start() {
			virtualObject = GameObject.Find (_objectName);
		}


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

		/// <summary>
		/// Calibrates the virtual object in VR.
		/// </summary>
		/// <param name="objectCalibrate">Vive Tracker used if the virtual object is movable.</param>
		void CalibrateVR(GameObject objectCalibrate) {
			objectCalibrate.GetComponent<VirtualObject> ().Calibrate (PositionTags);
			distancePoints = new float[_numberPoints];
			for (int i = 0; i < PositionTags.Length; i++) {
				distancePoints [i] = Vector3.Distance (PositionTags [i].gameObject.transform.position, virtualObject.GetComponent<VirtualObject> ().virtualPositionTags [i].transform.position); 
			}
				
			orderedTags = new PositionTag[_numberPoints];
			for (int i = 0; i < PositionTags.Length; i++) {
				if (i == 0 || distancePoints [i] >= distancePoints [i - 1]) {
					orderedTags[i] = PositionTags[i];
				} else if (distancePoints [i] < distancePoints [i-1]) {
					orderedTags[i] = orderedTags [i-1];
					orderedTags[i-1] = PositionTags[i];
				}
			}

			for (int i = 0; i < PositionTags.Length; i++) {
				orderedTags [i].gameObject.GetComponent<Renderer>().material = _materialDistances [i];
			}

			if (touchingTracker) {
				GameObject viveTracker = GameObject.FindGameObjectWithTag ("ViveTracker");
				objectCalibrate.transform.parent = viveTracker.transform; 
			}
		}
			
		/// <summary>
		/// Waits one second not to delete a position tag by mistake after it has been created.
		/// </summary>
		IEnumerator Waiting() {
			hasWaited = false;
			yield return new WaitForSeconds (1);
			hasWaited = true;
		}

		/// <summary>
		/// Removes the position tag which collider is being touched by the controller.
		/// </summary>
		/// <param name="pointRemove">The position tag to remove.</param>
		void RemovePositionTag(GameObject pointRemove) {
			Destroy (pointRemove);
			indexPositionTag--;
			touchingPoint = false;
			incorrectPoint = null;
			Debug.Log ("Position tag destroyed");
		}

		/// <summary>
		/// Deletes every position tags spawned.
		/// </summary>
		void ResetPositionTags() {
			for (int i = 0; i < PositionTags.Length; i++) {
				if (PositionTags [i] != null) {
					Destroy (PositionTags [i].gameObject);
					PositionTags [i] = null;
				}
			}
			indexPositionTag = 0;
			touchingTracker = false;
			virtualObject.transform.parent = null;
			Debug.Log("All position tags have been removed");
		}
	
		// Update is called once per frame
		void Update ()
		{
			SteamVR_Controller.Device device = SteamVR_Controller.Input ((int)_trackedObj.index);
			if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
				if (!touchingPoint) {
					if (indexPositionTag < _numberPoints) {
						CreatePositionTag ();
					} else if (virtualObject.GetComponent<VirtualObject> () != null) {
							CalibrateVR (virtualObject);
						}
				} else if (incorrectPoint != null && hasWaited && touchingPoint) {
					RemovePositionTag (incorrectPoint);
				}
			} else if (device.GetPressDown (SteamVR_Controller.ButtonMask.Grip)) {
				ResetPositionTags ();
			}
		
		}

	}
}

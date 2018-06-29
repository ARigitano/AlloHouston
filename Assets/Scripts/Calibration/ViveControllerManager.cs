// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

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
		[SerializeField] private int _indexPositionTag = 0;
		[SerializeField] private bool _hasWaited = false;
		[SerializeField] private string _objectName;
		[SerializeField] private Material[] _materialDistances;
		[SerializeField] private GameObject[] _objectsCollection;
		[SerializeField] private SteamVR_LaserPointer _laser;
		[SerializeField] private float[] _distancePoint;
		private PositionTag[] _orderedTags;
		private GameObject _virtualObject;
		public PositionTag[] _PositionTags;
		public bool _touchingPoint = false;
		public bool _touchingTracker = false;
		public GameObject _incorrectPoint;
		public int _objectNumber;

		/// <summary>
		/// Creates a new position tag at the position of the controller.
		/// </summary>
		public void CreatePositionTag ()
		{
			PositionTag pTag = (PositionTag)Instantiate (_positionTag, _spawnPosition.position, Quaternion.identity);
			for (int i = 0; i < _PositionTags.Length; i++) 
			{
				if (_PositionTags [i] == null) 
				{
					pTag.GetComponent<PositionTag> ().positionTagIndex = i;
					pTag.GetComponent<Renderer> ().material.color = new Color (i * 0.2f, i * 0.2f, i * 0.2f, 0.6f);
					_PositionTags [i] = pTag;
					break;
				}
			}

			_indexPositionTag++;
			Debug.Log ("New position tag created");
			StartCoroutine ("Waiting");

		}

		/// <summary>
		/// Calibrates the virtual object in VR.
		/// </summary>
		/// <param name="objectCalibrate">Vive Tracker used if the virtual object is movable.</param>
		void CalibrateVR(GameObject objectCalibrate) 
		{

			/*_PositionTags [0] = null;
			_PositionTags [1] = null;
			_PositionTags [2] = null;*/
			Debug.Log ("YOOOOHIII");

			objectCalibrate.GetComponent<VirtualObject> ().Calibrate (_PositionTags);

			ItemEntry calObj = new ItemEntry();
			calObj.type = _objectsCollection[_objectNumber].name;

			calObj.point1 = new SerializableVector3();
			calObj.point1.X = _PositionTags [0].transform.position.x;
			calObj.point1.Y = _PositionTags [0].transform.position.y;
			calObj.point1.Z = _PositionTags [0].transform.position.z;

			calObj.point2 = new SerializableVector3();
			calObj.point2.X = _PositionTags [1].transform.position.x;
			calObj.point2.Y = _PositionTags [1].transform.position.y;
			calObj.point2.Z = _PositionTags [1].transform.position.z;

			calObj.point3 = new SerializableVector3();
			calObj.point3.X = _PositionTags [2].transform.position.x;
			calObj.point3.Y = _PositionTags [2].transform.position.y;
			calObj.point3.Z = _PositionTags [2].transform.position.z;


			if (XMLManager.ins.itemDB.list.Count != 0) 
			{
				for (int i = 0; i < XMLManager.ins.itemDB.list.Count; i++) 
				{

					if (XMLManager.ins.itemDB.list [i].type == _objectsCollection [_objectNumber].name) 
					{
						XMLManager.ins.itemDB.list [i].type = _objectsCollection [_objectNumber].name;

						XMLManager.ins.itemDB.list [i].point1 = new SerializableVector3 ();
						XMLManager.ins.itemDB.list [i].point1.X = _PositionTags [0].transform.position.x;
						XMLManager.ins.itemDB.list [i].point1.Y = _PositionTags [0].transform.position.y;
						XMLManager.ins.itemDB.list [i].point1.Z = _PositionTags [0].transform.position.z;

						XMLManager.ins.itemDB.list [i].point2 = new SerializableVector3 ();
						XMLManager.ins.itemDB.list [i].point2.X = _PositionTags [1].transform.position.x;
						XMLManager.ins.itemDB.list [i].point2.Y = _PositionTags [1].transform.position.y;
						XMLManager.ins.itemDB.list [i].point2.Z = _PositionTags [1].transform.position.z;

						XMLManager.ins.itemDB.list [i].point3 = new SerializableVector3 ();
						XMLManager.ins.itemDB.list [i].point3.X = _PositionTags [2].transform.position.x;
						XMLManager.ins.itemDB.list [i].point3.Y = _PositionTags [2].transform.position.y;
						XMLManager.ins.itemDB.list [i].point3.Z = _PositionTags [2].transform.position.z;

						Debug.Log ("Item entry modified in XML: "+calObj.type);

						break;
					} 
					else if (i == XMLManager.ins.itemDB.list.Count - 1) 
					{
						XMLManager.ins.itemDB.list.Add(calObj);
						Debug.Log ("Item entry created in XML: "+calObj.type);
					} 
					else
						Debug.Log ("Error");
				}
			} 
			else 
			{
				XMLManager.ins.itemDB.list.Add(calObj);
				Debug.Log ("Item entry created in XML: "+calObj.type);
			}

			XMLManager.ins.SaveItems ();

			_distancePoint = new float[_numberPoints];
			Color cStart = Color.red;
			Color cEnd = Color.white;
			for (int i = 0; i < _PositionTags.Length; i++) 
			{
				_distancePoint[i] = Vector3.Distance (_PositionTags [i].gameObject.transform.position, objectCalibrate.GetComponent<VirtualObject> ().virtualPositionTags [i].transform.position);
				_PositionTags [i].GetComponent<Renderer> ().material.color = new Color (_distancePoint[i]*5, 0f, 0f, 0.6f);
			}


				
			/*orderedTags = new PositionTag[_numberPoints];

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
			}*/

            /*if (objectCalibrate == _objectsCollection [2]) {
				transformTracker (objectCalibrate);
			}*/

            if (_touchingTracker)
            {
                GameObject viveTracker = GameObject.FindGameObjectWithTag("ViveTracker");
                objectCalibrate.transform.parent = viveTracker.transform;
                transformTracker(viveTracker);
            }


        }

        /// <summary>
        /// Waits one second not to delete a position tag by mistake after it has been created.
        /// </summary>
        private IEnumerator Waiting()
        {
            _hasWaited = false;
            yield return new WaitForSeconds(1);
            _hasWaited = true;
        }

        /// <summary>
        /// Removes the position tag which collider is being touched by the controller.
        /// </summary>
        /// <param name="pointRemove">The position tag to remove.</param>
        private void RemovePositionTag(GameObject pointRemove)
        {
            Destroy(pointRemove);
            _indexPositionTag--;
            _touchingPoint = false;
            _incorrectPoint = null;
            Debug.Log("Position tag destroyed");
        }

        /// <summary>
        /// Deletes every position tags spawned.
        /// </summary>
        private void ResetPositionTags()
        {
            for (int i = 0; i < _PositionTags.Length; i++)
            {
                if (_PositionTags[i] != null)
                {
                    Destroy(_PositionTags[i].gameObject);
                    _PositionTags[i] = null;
                }
            }
            _indexPositionTag = 0;
            _touchingTracker = false;
            //_objectsCollection[objectNumber].transform.parent = null;
            Debug.Log("All position tags have been removed");
        }

        private void transformTracker(GameObject tracker)
        {
            if (tracker.GetComponent<SteamVR_TrackedObject>() == null)
            {
                tracker.AddComponent<SteamVR_TrackedObject>();
                tracker.GetComponent<SteamVR_TrackedObject>().index = SteamVR_TrackedObject.EIndex.Device2;
            }

            SteamVR_ControllerManager cameraVive = GameObject.Find("[CameraRig]").GetComponent<SteamVR_ControllerManager>();
            cameraVive.objects[2] = tracker;
        }

        // Update is called once per frame
        private void Update()
        {
            SteamVR_Controller.Device device = SteamVR_Controller.Input((int)_trackedObj.index);
            if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                if (!_touchingPoint)
                {
                    if (_indexPositionTag < _numberPoints)
                    {
                        CreatePositionTag();
                    }
                    else if (_objectsCollection[_objectNumber].GetComponent<VirtualObject>() != null)
                    {
						CalibrateVR(Instantiate(_objectsCollection[_objectNumber]));
                    }
                }
                else if (_incorrectPoint != null && _hasWaited && _touchingPoint)
                {
                    RemovePositionTag(_incorrectPoint);
                }
            }
            else if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
            {
                ResetPositionTags();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace VRCalibrationTool
{
    /// <summary>
    /// Manages the use of the Vive controllers during the calibratiion process
    /// </summary>
	public class ViveControllerManager : MonoBehaviour
    {
        [SerializeField]
        private SteamVR_TrackedObject _trackedObj = null; //The controller with the precision spike
        [SerializeField]
        private PositionTag _positionTagPrefab = null;          //Prefab of a position tag
        [SerializeField]
        private Transform _spawnPositionTransform = null;          //Position at the tip of the precision spike on which the position tags will spawn
        [SerializeField]
        private int _indexPositionTag = 0;         //Number of instantiated position tags    
        [SerializeField]
        private bool _hasWaited = false;           //Security stop to prevent removing position tags by mistake
        [SerializeField]
        private VirtualObject[] _virtualObjectPrefabs;   //Contains all the objects that can be instantiated during the calibration phase

        public VirtualObject[] virtualObjectPrefabs
        {
            get
            {
                return _virtualObjectPrefabs;
            }
        }
        [SerializeField]
        private float[] _distancePoint;            //Distance between an instantiated position tag and the position where it should be on the instantiated object
        [SerializeField]
        private bool mouseMode;
        public List<PositionTag> _positionTags;                         //Contains the instantiated position tags
        public List<PositionTag> positionTag
        {
            get
            {
                return _positionTags;
            }
        }
        public bool _touchingPoint = false;                         //Is the precision spike touching an instantiated position tag?
        public bool _touchingTracker = false;                       //Is the precision spike touching a ViveTracker?
        public PositionTag _incorrectPoint;                          //Position tag considered as incorrectly positionned
        public int _virtualObjectPrefabIndex;                                   //Index of an object inside the instatiable objects collection
        [SerializeField]
        private GameManager _gameManager;

        public VirtualObject currentVirtualObjectPrefab
        {
            get
            {
                if (_virtualObjectPrefabs != null && _virtualObjectPrefabIndex >= 0 && _virtualObjectPrefabIndex < _virtualObjectPrefabs.Length)
                    return _virtualObjectPrefabs[_virtualObjectPrefabIndex];
                return null;
            }
        }

        private void Start()
        {
            _virtualObjectPrefabs = Resources.LoadAll<VirtualObject>("VirtualObjects/");
            if (_spawnPositionTransform == null)
                _spawnPositionTransform = GameObject.FindWithTag("SpawnPosition").transform;
        }

        /// <summary>
        /// Creates n position tags.
        /// </summary>
        /// <param name="n">The number of position tags</param>
        public void CreatePositionTag(int n)
        {
            for (int i = 0; i < n; i++)
                CreatePositionTag(_spawnPositionTransform.position);
        }

        /// <summary>
        /// Creates a new position tag at the position of the controller.
        /// </summary>
        public void CreatePositionTag(Vector3 position)
        {
            int count = _positionTags.Count;
            PositionTag positionTag = (PositionTag)Instantiate(_positionTagPrefab, position, Quaternion.identity);
            positionTag.positionTagIndex = count;
            positionTag.GetComponent<Renderer>().material.color = new Color(count * 0.2f, count * 0.2f, count * 0.2f, 0.6f);
            _positionTags.Add(positionTag);
            _indexPositionTag++;
            Debug.Log("New position tag created");
            StartCoroutine(Waiting());
        }

        /// <summary>
        /// Instantiate and calibrate a virtual object.
        /// </summary>
        /// <param name="itemType">Name of the type of item that will be instantiated and calibrated.</param>
        public void CalibrateVR(string itemType)
        {
            //Storing the coordinates of the position tags used to instantiate the object
            VirtualObject virtualObject = Instantiate(_virtualObjectPrefabs.First(x => x.name == itemType));
            virtualObject.Calibrate(_positionTags.ToArray());

            XMLManager.instance.InsertOrReplaceItem(new ItemEntry(0, itemType, _positionTags.ToArray()));

            //Coloring the position tags according to their distance to their theoretical positions
            _distancePoint = new float[_positionTags.Count];
            Color cStart = Color.red;
            Color cEnd = Color.white;
            for (int i = 0; i < _positionTags.Count; i++)
            {
                _distancePoint[i] = Vector3.Distance(_positionTags[i].gameObject.transform.position, virtualObject.GetComponent<VirtualObject>().virtualPositionTags[i].transform.position);
                _positionTags[i].GetComponent<Renderer>().material.color = new Color(_distancePoint[i] * 5, 0f, 0f, 0.6f);
            }
            //Should the object be tracked?
            if (_touchingTracker)
            {
                GameObject viveTracker = GameObject.FindGameObjectWithTag("ViveTracker");
                virtualObject.transform.parent = viveTracker.transform;
                TransformTracker(viveTracker);
            }
        }

        /// <summary>
        /// Waits one second not to delete a position tag by mistake after it has been created.
        /// </summary>
        private IEnumerator Waiting(int time = 1)
        {
            _hasWaited = false;
            yield return new WaitForSeconds(time);
            _hasWaited = true;
        }

        /// <summary>
        /// Removes the position tag which collider is being touched by the controller.
        /// </summary>
        /// <param name="pointRemove">The position tag to remove.</param>
        private void RemovePositionTag(PositionTag pointRemove)
        {
            Destroy(pointRemove.gameObject);
            Destroy(pointRemove);
            _indexPositionTag--;
            _touchingPoint = false;
            _incorrectPoint = null;
            Debug.Log("Position tag destroyed");
        }

        /// <summary>
        /// Deletes every position tags spawned.
        /// </summary>
        public void ResetPositionTags()
        {
            foreach (var positionTag in _positionTags)
            {
                RemovePositionTag(positionTag);
            }
            _positionTags.Clear();
            _indexPositionTag = 0;
            _touchingTracker = false;
            Debug.Log("All position tags have been removed");
        }

        /// <summary>
        /// Turns the instantiated object into a tracked object
        /// </summary>
        /// <param name="tracker">The ViveTracker attached to the object</param>
        private void TransformTracker(GameObject tracker)
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
            //On trigger press: either create a position tag or instantiate an object
            SteamVR_Controller.Device device = SteamVR_Controller.Input((int)_trackedObj.index);
            if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) && !_gameManager._gameStarted
#if UNITY_EDITOR
|| mouseMode && Input.GetMouseButtonUp(1)
#endif
            )
            {
                if (!_touchingPoint)
                {
                    if (_indexPositionTag < currentVirtualObjectPrefab.virtualPositionTags.Length)
                    {
                        CreatePositionTag(mouseMode ? Input.mousePosition : _spawnPositionTransform.position);
                    }
                    else
                    {
                        CalibrateVR(currentVirtualObjectPrefab.name);
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
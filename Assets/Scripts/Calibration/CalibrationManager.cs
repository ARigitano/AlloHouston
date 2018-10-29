using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using VRCalibrationTool;
using CRI.HelloHouston.Calibration.XML;
using System;

namespace CRI.HelloHouston.Calibration
{
    /// <summary>
    /// Manages the use of the Vive controllers during the calibratiion process
    /// </summary>
	public class CalibrationManager : MonoBehaviour
    {
        /// <summary>
        /// Prefab of a position tag
        /// </summary>
        [SerializeField]
        [Tooltip("Prefab of a position tag.")]
        private PositionTag _positionTagPrefab = null;
        /// <summary>
        /// A list of position tags.
        /// </summary>
        [Tooltip("A list of position tags.")]
        private List<PositionTag> _positionTags = new List<PositionTag>();
        /// <summary>
        /// Contains all the blocks that can be instantiated during the calibration.
        /// </summary>
        [SerializeField]
        [Tooltip("Contains all the blocks that can be instantiated during the calibration.")]
        private VirtualBlock[] _virtualBlockPrefabs;
        /// <summary>
        /// Contains all the rooms that can be instantiated during the calibration.
        /// </summary>
        [SerializeField]
        [Tooltip("Contains all the rooms that can be instantiated during the calibration.")]
        private VirtualRoom[] _virtualRoomPrefabs;
        /// <summary>
        /// True if the calibration manager can create a position tag.
        /// </summary>
        public bool canCreatePositionTag
        {
            get
            {
                return currentVirtualBlockPrefab != null && _positionTags.Count < currentVirtualBlockPrefab.virtualPositionTags.Length;
            }
        }

        public int _virtualObjectPrefabIndex;                                   //Index of an object inside the instatiable objects collection

        public VirtualBlock currentVirtualBlockPrefab
        {
            get
            {
                if (_virtualBlockPrefabs != null && _virtualObjectPrefabIndex >= 0 && _virtualObjectPrefabIndex < _virtualBlockPrefabs.Length)
                    return _virtualBlockPrefabs[_virtualObjectPrefabIndex];
                return null;
            }
        }

        private void Start()
        {
            _virtualBlockPrefabs = Resources.LoadAll<VirtualBlock>("VirtualObjects/");
            _virtualRoomPrefabs = Resources.LoadAll<VirtualRoom>("VirtualObjects/");
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
        }

        public void CalibrateVR()
        {
            if (currentVirtualBlockPrefab != null)
            {
                CalibrateVR(currentVirtualBlockPrefab.block.index, currentVirtualBlockPrefab.block.type);
            }
            else
            {
                throw new Exception("No prefab available for calibration.");
            }
        }

        /// <summary>
        /// Instantiate and calibrate a virtual object.
        /// </summary>
        /// <param name="blockType">Name of the type of item that will be instantiated and calibrated.</param>
        public void CalibrateVR(int blockIndex, BlockType blockType)
        {
            //Storing the coordinates of the position tags used to instantiate the object
            VirtualBlock virtualBlock = Instantiate(GetVirtualBlockPrefab(blockIndex, blockType));
            virtualBlock.Calibrate(_positionTags.ToArray());

            XMLManager.instance.InsertOrReplaceItem(virtualBlock.ToBlockEntry());

            //Coloring the position tags according to their distance to their theoretical positions
            float[] _distancePoint = new float[_positionTags.Count];
            for (int i = 0; i < _positionTags.Count; i++)
            {
                _distancePoint[i] = Vector3.Distance(_positionTags[i].gameObject.transform.position, virtualBlock.GetComponent<VirtualObject>().virtualPositionTags[i].transform.position);
                _positionTags[i].GetComponent<Renderer>().material.color = new Color(_distancePoint[i] * 5, 0f, 0f, 0.6f);
            }
        }

        /// <summary>
        /// Get the prefab of a virtual block.
        /// </summary>
        /// <param name="blockEntry">A block entry.</param>
        /// <returns>The prefab of the virtual block.</returns>
        public VirtualBlock GetVirtualBlockPrefab(BlockEntry blockEntry)
        {
            return GetVirtualBlockPrefab(blockEntry.index, blockEntry.type);
        }

        /// <summary>
        /// Gets all the virtual block prefabs.
        /// </summary>
        /// <returns>An array of VirtualBlock</returns>
        public VirtualBlock[] GetAllVirtualBlockPrefabs()
        {
            return _virtualBlockPrefabs;
        }
        
        /// <summary>
        /// Gets the prefab of a virtual block.
        /// </summary>
        /// <param name="blockIndex">The index of the virtual block.</param>
        /// <param name="blockType">The type of the virtual block.</param>
        /// <returns>The prefab of a virtual block.</returns>
        public VirtualBlock GetVirtualBlockPrefab(int blockIndex, BlockType blockType)
        {
            return _virtualBlockPrefabs.First(x => x.block.type == blockType && x.block.index == blockIndex);
        }

        /// <summary>
        /// Gets the prefab of a virtual room.
        /// </summary>
        /// <param name="room">A room entry.</param>
        /// <returns>A VirtualRoom</returns>
        public VirtualRoom GetVirtualRoomPrefab(RoomEntry room)
        {
            return GetVirtualRoomPrefab(room.index);
        }

        /// <summary>
        /// Gets the prefab of a virtual room.
        /// </summary>
        /// <param name="index">The index of the virtual room.</param>
        /// <returns>A VirtualRoom</returns>
        public VirtualRoom GetVirtualRoomPrefab(int index)
        {
            return _virtualRoomPrefabs.First(x => x.index == index);
        }

        /// <summary>
        /// Gets all the prefabs of the virtual rooms.
        /// </summary>
        /// <returns>An array of VirtualRoom</returns>
        public VirtualRoom[] GetAllVirtualRooms()
        {
            return _virtualRoomPrefabs;
        }

        /// <summary>
        /// Removes the position tag which collider is being touched by the controller.
        /// </summary>
        /// <param name="pointRemove">The position tag to remove.</param>
        internal void RemovePositionTag(PositionTag pointRemove)
        {
            Destroy(pointRemove.gameObject);
            Destroy(pointRemove);
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
    }
}
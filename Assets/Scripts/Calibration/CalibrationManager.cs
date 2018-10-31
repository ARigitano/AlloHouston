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

        private VirtualItem _currentVirtualItem;

        private CalibrationController _controller;

        /// <summary>
        /// True if the calibration manager can create a position tag.
        /// </summary>
        public bool canCreatePositionTag
        {
            get
            {
                return _currentVirtualItem && _positionTags.Count < _currentVirtualItem.virtualPositionTags.Length;
            }
        }
        

        private void Start()
        {
            _virtualBlockPrefabs = Resources.LoadAll<VirtualBlock>("VirtualObjects/");
            _virtualRoomPrefabs = Resources.LoadAll<VirtualRoom>("VirtualObjects/");
            _controller = FindObjectOfType<CalibrationController>();
        }

        /// <summary>
        /// Creates a new position tag at the position of the controller.
        /// </summary>
        public void CreatePositionTag(Vector3 position)
        {
            int count = _positionTags.Count;
            PositionTag positionTag = (PositionTag)Instantiate(_positionTagPrefab, position, Quaternion.identity);
            positionTag.index = count;
            positionTag.GetComponent<Renderer>().material.color = new Color(count * 0.2f, count * 0.2f, count * 0.2f, 0.6f);
            _positionTags.Add(positionTag);
        }

        public void CalibrateCurrentVirtualItem()
        {
            if (_currentVirtualItem != null)
            {
                _currentVirtualItem.Calibrate(_positionTags.ToArray());
                _currentVirtualItem.lastUpdate = DateTime.Now;
                if (_currentVirtualItem.virtualItemType == VirtualItem.VirtualItemType.Room)
                    XMLManager.instance.InsertOrReplace(((VirtualRoom)_currentVirtualItem).ToRoomEntry());
                else if (_currentVirtualItem.virtualItemType == VirtualItem.VirtualItemType.Block)
                    XMLManager.instance.InsertOrReplace(_currentVirtualItem.GetComponentInParent<VirtualRoom>().ToRoomEntry());
            }
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
            return GetAllVirtualBlockPrefabs().First(x => x.block.type == blockType && x.block.index == blockIndex);
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
            return GetAllVirtualRooms().First(x => x.index == index);
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
        public void RemovePositionTag(PositionTag pointRemove, bool reindex = true)
        {
            _positionTags.Remove(pointRemove);
            Destroy(pointRemove.gameObject);
            Destroy(pointRemove);
            for (int i = 0; i < _positionTags.Count && reindex; i++)
            {
                _positionTags[i].index = i;
            }
        }

        /// <summary>
        /// Remove the latest position tag.
        /// </summary>
        public void RemoveLastPositionTag()
        {
            if (_positionTags.Count > 0)
                RemovePositionTag(_positionTags.Last(), false);
        }

        /// <summary>
        /// Deletes every position tags spawned.
        /// </summary>
        public void ResetPositionTags()
        {
            foreach (var positionTag in _positionTags)
            {
                RemovePositionTag(positionTag, false);
            }
            _positionTags.Clear();
        }

        public void StartCalibration(VirtualItem virtualItem)
        {
            ResetPositionTags();
            _currentVirtualItem = virtualItem;
            _controller.StartCalibration();
        }

        public void StopCalibration()
        {
            ResetPositionTags();
            _currentVirtualItem = null;
            _controller.StopCalibration();
        }

        /// <summary>
        /// Instantiates and inits a VirtualRoom from a RoomEntry.
        /// </summary>
        /// <param name="roomEntry">The RoomEntry that describes the VirtualRoom</param>
        /// <returns>An instance of VirtualRoom</returns>
        public VirtualRoom CreateRoom(RoomEntry roomEntry)
        {
            VirtualRoom vroom = Instantiate(GetVirtualRoomPrefab(roomEntry));
            vroom.Init(roomEntry);
            VirtualBlock vtable = Instantiate(GetVirtualBlockPrefab(roomEntry.table), transform);
            vtable.Init(roomEntry.table);
            vroom.table = vtable;
            vroom.blocks = new VirtualBlock[vroom.blocks.Length];
            for (int i = 0; i < vroom.blocks.Length; i++)
            {
                vroom.blocks[i] = Instantiate(GetVirtualBlockPrefab(roomEntry.blocks[i]), transform);
                vroom.blocks[i].Init(roomEntry.blocks[i]);
            }
            return vroom;
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
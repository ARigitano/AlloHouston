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
        public class VirtualBlockIndex
        {
            public VirtualBlock virtualBlock;
            public int calibrationIndex;
        }

        public class VirtualRoomIndex
        {
            public VirtualRoom virtualRoom;
            public int calibrationIndex;
        }
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
        private VirtualItem[] _virtualItemPrefabs;

        /// <summary>
        /// List of instantiated blocks and their corresponding calibration index.
        /// </summary>
        private List<VirtualBlockIndex> _instantiatedBlocks;
        /// <summary>
        /// Instantiated room and its corresponding calibration index.
        /// </summary>
        private VirtualRoomIndex _instantiatedRoom;
        /// <summary>
        /// True if the calibration manager can create a position tag.
        /// </summary>
        public bool canCreatePositionTag
        {
            get
            {
                return currentVirtualItemPrefab != null && _positionTags.Count < currentVirtualItemPrefab.virtualPositionTags.Length;
            }
        }

        public int _calibrationIndex;

        public int _currentVirtualItemPrefabIndex;

        public VirtualItem currentVirtualItemPrefab
        {
            get
            {
                if (_currentVirtualItemPrefabIndex > _virtualItemPrefabs.Length)
                    return null;
                return _virtualItemPrefabs[_currentVirtualItemPrefabIndex];
            }
        }
        private void Start()
        {
            _virtualItemPrefabs = Resources.LoadAll<VirtualItem>("VirtualObjects/");
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
            if (currentVirtualItemPrefab != null)
            {
                VirtualItem virtualItem = Instantiate(currentVirtualItemPrefab);
                virtualItem.Calibrate(_positionTags.ToArray());
            }
            else
            {
                throw new Exception("No prefab available for calibration.");
            }
        }


        /// <summary>
        /// Gets all the virtual block prefabs.
        /// </summary>
        /// <returns>An array of VirtualBlock</returns>
        public VirtualBlock[] GetAllVirtualBlockPrefabs()
        {
            return _virtualItemPrefabs.Where(x => x.virtualItemType == VirtualItem.VirtualItemType.Block).Select(x => (VirtualBlock)x).ToArray();
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
            return _virtualItemPrefabs.Where(x => x.virtualItemType == VirtualItem.VirtualItemType.Room).Select(x => (VirtualRoom)x).ToArray();
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

        public void StartCalibration(int index, int currentVirtualItemPrefabIndex)
        {
            _calibrationIndex = index;
            _currentVirtualItemPrefabIndex = currentVirtualItemPrefabIndex;
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
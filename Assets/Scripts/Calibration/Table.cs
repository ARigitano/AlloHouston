using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRCalibrationTool
{
    /// <summary>
    /// Control table
    /// </summary>
    public class Table : Room
    {

        private RoomManager _roomManager;
        public string _trackedObject; //Type of tracked object that got placed on the tactile table

        // Use this for initialization
        void Start()
        {
            _roomManager = GameObject.Find("RoomManager").GetComponent<RoomManager>();
            _roomManager._table = gameObject;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "TrackedObject")
            {
                _trackedObject = other.GetComponent<TrackedObject>()._type;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.tag == "TrackedObject")
            {
                _trackedObject = "";
            }
        }
    }
}

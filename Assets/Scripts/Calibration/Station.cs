using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRCalibrationTool
{
    public class Station : Room
    {
        private RoomManager _roomManager;

        // Use this for initialization
        void Start()
        {
            _roomManager = GameObject.Find("RoomManager").GetComponent<RoomManager>();

            _roomManager._room = gameObject;

            int placeholderCounter = 0;
            foreach (Transform child in transform)
            {
                if (child.tag == "Placeholder")
                {
                        _roomManager._placeholdersRoom[placeholderCounter] = child.gameObject;
                        //_roomManager.CreateButton (child.gameObject.name);
                        _roomManager._wallCounter++;
                        placeholderCounter++;
                }
            }
        }
    }
}

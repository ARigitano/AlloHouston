using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRCalibrationTool
{
    /// <summary>
    /// The lunar base
    /// </summary>
    public class Station : Room
    {
        private RoomManager _roomManager;

        // Use this for initialization
        void Start()
        {
            _roomManager = GameObject.Find("RoomManager").GetComponent<RoomManager>();

            _roomManager._room = gameObject;

            int placeholderCounter = 0; //Number of placeholders offered by this base
            int placeholderBottomCounter = 0;
            int decorationCounter = 0; //Number of decoration placeholders offered by this base
            foreach (Transform child in transform)
            {
                if (child.tag == "Placeholder")
                {
                        _roomManager._placeholdersRoom[placeholderCounter] = child.gameObject;
                        _roomManager._wallCounter++;
                        placeholderCounter++;
                } else if(child.tag == "Decoration")
                {
                    _roomManager._decorationRoom[decorationCounter] = child.gameObject;
                    decorationCounter++;
                } else if(child.tag == "PlaceholderBottom15")
                {
                    _roomManager._placeholdersRoomBottom[placeholderBottomCounter] = child.gameObject;
                    _roomManager._wallBottomCounter++;
                    placeholderBottomCounter++;
                } else if(child.tag == "AIScreen")
                {
                    _roomManager._aiScreen = child.GetComponent<AIScreen>();
                }
            }
        }
    }
}

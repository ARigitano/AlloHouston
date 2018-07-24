/// <summary>
/// Class for the lunar base's room.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRCalibrationTool
{
	public class Room : MonoBehaviour 
	{
        //private RoomManager _roomManager;
        public GameObject[] _placeholders; //The placeholders offered by this room.
        public GameObject canvas;
		[SerializeField] protected int _index; //1 = room, 2 = table

		// Use this for initialization
		void Start () 
		{
            //_roomManager = GameObject.Find("RoomManager").GetComponent<RoomManager>();

            if (_index == 1) 
			{
				//_roomManager._room = gameObject;
			}
			else if (_index == 2) 
			{
				//_roomManager._table = gameObject;
			}

			int placeholderCounter = 0;
			foreach (Transform child in transform) 
			{
				if (child.tag == "Placeholder") 
				{

					if (_index == 1) {
						//_roomManager._placeholdersRoom[placeholderCounter] = child.gameObject;
						//_roomManager.CreateButton (child.gameObject.name);
						//_roomManager._wallCounter++;
						placeholderCounter++;
					}
					else if (_index == 2) 
					{
						//_roomManager._placeholdersTable[placeholderCounter] = child.gameObject;
						//_roomManager.CreateButton (child.gameObject.name);
						//_roomManager._tableCounter++;
						placeholderCounter++;
					}
				}
			}
		}
	
	}
}

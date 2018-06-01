/// <summary>
/// Class for the lunar base's room.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRCalibrationTool
{
	public class Room : MonoBehaviour {

		private RoomManager _roomManager;
		public GameObject[] _placeholders; //The placeholders offered by this room.

		// Use this for initialization
		void Start () 
		{
			_roomManager = GameObject.Find ("RoomManager").GetComponent<RoomManager>();
			_roomManager.room = gameObject;

			int placeholderCounter = 0;
			foreach (Transform child in transform) {
				if (child.tag == "Placeholder") {
					_roomManager.placeholdersRoom[placeholderCounter] = child.gameObject;
					_roomManager.CreateButton (child.gameObject.name);
					_roomManager.j++;
					placeholderCounter++;
				}
			}
		}
	
	}
}

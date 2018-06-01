/// <summary>
/// Used to choose which experiments to add to the current session.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRCalibrationTool
{
	public class ExperienceButton : MonoBehaviour {

		private RoomManager _roomManager; 

		// Use this for initialization
		void Start () 
		{
			_roomManager = GameObject.Find ("RoomManager").GetComponent<RoomManager>();
		}

		/// <summary>
		/// Adds the selected experiment to the room manager's modules array.
		/// </summary>
		public void ChooseExperiment() 
		{
			if (_roomManager.j != 0) {
				_roomManager.j--;
				_roomManager.module [_roomManager.i] = gameObject.name;
				_roomManager.i++;
				}
		}
	}
}

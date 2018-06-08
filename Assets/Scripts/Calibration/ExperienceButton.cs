/// <summary>
/// Used to choose which experiments to add to the current session.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRCalibrationTool
{
	public class ExperienceButton : MonoBehaviour 
	{
		private RoomManager _roomManager; 
		public Experiment[] _experiments;

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
			if (_roomManager._wallCounter != 0) 
			{
				_roomManager._wallCounter--;
				_roomManager._tableCounter--;
				_roomManager._experiments [_roomManager._experimentsCounter] = _experiments[0];
				_roomManager._placeholdersRoomNeeded += 1;
				_roomManager._placeholdersTableNeeded += 1;
				_roomManager._experimentsCounter++;
			}
		}
	}
}

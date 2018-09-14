using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRCalibrationTool
{
    /// <summary>
    /// Used to choose which experiments to add to the current session.
    /// </summary>
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
            if (_roomManager._wallCounter >= _experiments[0].placeholdersWallTop && _roomManager._wallBottomCounter >= _experiments[0].placeholdersWallBottom)
            {
                _roomManager._wallCounter-= _experiments[0].placeholdersWallTop;
                _roomManager._wallBottomCounter -= _experiments[0].placeholdersWallBottom;
                _roomManager._experiments[_roomManager._experimentsCounter] = _experiments[0];
                _roomManager._placeholdersRoomNeeded += _experiments[0].placeholdersWallTop;
                _roomManager._placeholdersRoomNeededBottom += _experiments[0].placeholdersWallBottom;
                _roomManager._experimentsCounter++;
            }
        }
	}
}

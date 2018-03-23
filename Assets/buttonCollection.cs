using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRCalibrationTool
{
public class buttonCollection : MonoBehaviour {

		[SerializeField] private ViveControllerManager viveManager;
		[SerializeField] private int buttonID;


	public void AssignCollectionNumber() {
			viveManager.objectNumber = buttonID;
	}

	
}
}

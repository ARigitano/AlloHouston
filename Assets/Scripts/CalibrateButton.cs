using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VRCalibrationTool
{
	public class CalibrateButton : MonoBehaviour
	{
		public VirtualObject realObject;

		void Start ()
		{
			this.GetComponent<Button> ().onClick.AddListener (
				() => realObject.Calibrate (RealPositionManager.instance.controllerPositionTags)
			);
		}
	}
}

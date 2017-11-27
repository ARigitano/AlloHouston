using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalibrateButton : MonoBehaviour {
	public VirtualObject realObject;

	void Start()
	{
		this.GetComponent<Button> ().onClick.AddListener (
			() => realObject.Calibrate (MouseManager.instance.mousePositionTags)
		);
	}
}

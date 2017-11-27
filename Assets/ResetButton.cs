using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour {
	public VirtualObject virtualObject;
	private Vector3 _startPosition;
	private Vector3 _startScale;
	private Quaternion _startRotation;

	void Start()
	{
		_startPosition = virtualObject.transform.position;
		_startRotation = virtualObject.transform.rotation;
		_startScale = virtualObject.transform.localScale;
		this.GetComponent<Button> ().onClick.AddListener (
			() => {
				virtualObject.transform.position = _startPosition;
				virtualObject.transform.rotation = _startRotation;
				virtualObject.transform.localScale = _startScale;
				MouseManager.instance.ResetPositionTags();
			}
		);
	}
}

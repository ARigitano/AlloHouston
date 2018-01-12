using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VRCalibrationTool
{
	public class RandomButton : MonoBehaviour
	{
		public VirtualObject virtualObject;

		void Start ()
		{
			this.GetComponent<Button> ().onClick.AddListener (
				() => {
					virtualObject.transform.localScale = virtualObject.transform.localScale * Random.Range (0.0f, 10.0f);
					virtualObject.transform.rotation = Quaternion.Euler (new Vector3 (Random.Range (0, 360.0f), Random.Range (0, 360.0f), Random.Range (0, 360.0f)));
					virtualObject.transform.position = new Vector3 (Random.Range (-30.0f, 30.0f), Random.Range (-30.0f, 30.0f), Random.Range (-30.0f, 30.0f));
				}
			);
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class VirtualObject : MonoBehaviour {
	public PositionTag[] virtualPositionTags;

	void Start()
	{
		virtualPositionTags = GetComponentsInChildren<PositionTag> ().OrderBy (x => x.positionTagIndex).ToArray ();
	}

	public void Calibrate(PositionTag[] realPositionTags)
	{

		int length = Mathf.Min (realPositionTags.Length, virtualPositionTags.Length);
		int count = 0;
		float totalScale = 0;
		var totalRotation = Vector3.zero;
		var totalDist = Vector3.zero;
		var startScale = this.transform.localScale;
		var startRotation = this.transform.rotation;

		for (int i = 0; i < length; i++) {
			for (int j = i + 1; j < length; j++) {

				Vector3 realDir = realPositionTags [i].transform.position - realPositionTags [j].transform.position;
				Vector3 virtualDir = virtualPositionTags [i].transform.position - virtualPositionTags [j].transform.position;

				var rotation = Quaternion.FromToRotation (virtualDir, realDir).eulerAngles;

				var scale = realDir.magnitude / virtualDir.magnitude;

				Debug.Log ("scale = " + scale);
				Debug.Log ("i = " + i + " j = " + j);
				this.transform.localScale *= scale;

				this.transform.rotation *= Quaternion.Euler(rotation);

				var dist = realPositionTags [i].transform.position - virtualPositionTags [i].transform.position;

				count++;

				totalRotation += rotation;
				totalScale += scale;
				totalDist += dist;

				this.transform.localScale = startScale;
				this.transform.rotation = startRotation;
			}
		}

		this.transform.localScale *= totalScale / count;
		this.transform.rotation *= Quaternion.Euler(totalRotation / count);
		this.transform.position += totalDist / count;
	}
}

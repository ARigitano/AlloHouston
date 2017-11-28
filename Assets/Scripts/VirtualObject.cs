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
		var avgRotation = Quaternion.identity;
		var cumulativeRotation = Vector4.zero;
		var totalDist = Vector3.zero;
		var startScale = this.transform.localScale;
		var startRotation = this.transform.rotation;
		Quaternion? firstRotation = null;

		for (int i = 0; i < length; i++) {
			for (int j = i + 1; j < length; j++) {
				for (int k = j + 1; k < length; k++) {

					Vector3 ri = realPositionTags [i].transform.position;
					Vector3 rj = realPositionTags [j].transform.position;
					Vector3 rk = realPositionTags [k].transform.position;

					Vector3 vi = virtualPositionTags [i].transform.position;
					Vector3 vj = virtualPositionTags [j].transform.position;
					Vector3 vk = virtualPositionTags [k].transform.position;

					Vector3 realDir = Vector3.Cross (rj - ri, rk - ri);
					Vector3 virtualDir = Vector3.Cross (vj - vi, vk - vi);

					var rotation = Quaternion.FromToRotation (virtualDir.normalized, realDir.normalized);

					if (firstRotation == null)
						firstRotation = rotation;

					var scale = realDir.magnitude / virtualDir.magnitude;

					//Debug.Log ("scale = " + scale);
					//Debug.Log ("i = " + i + " j = " + j);
					this.transform.localScale *= scale;

					this.transform.rotation *= rotation;

					var dist = realPositionTags [i].transform.position - virtualPositionTags [i].transform.position;

					count++;

					avgRotation = MathHelper.AverageQuaternion(ref cumulativeRotation, rotation, firstRotation.Value, count);
					totalScale += scale;
					totalDist += dist;

					Debug.Log (avgRotation);

					this.transform.localScale = startScale;
					this.transform.rotation = startRotation;
				}
			}
		}
		this.transform.localScale *= totalScale / (float)count;
		this.transform.rotation *= avgRotation;
		this.transform.position += totalDist / (float)count;
	}
}

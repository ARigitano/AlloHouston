using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class VirtualObject : MonoBehaviour
{
	public PositionTag[] virtualPositionTags;

	void Start ()
	{
		virtualPositionTags = GetComponentsInChildren<PositionTag> ().OrderBy (x => x.positionTagIndex).ToArray ();
	}

	Vector3 GetCenterOfPoints (Vector3[] points)
	{
		var res = Vector3.zero;

		foreach (var point in points) {
			res += point;
		}

		return res / (float)points.Length;
	}

	Quaternion CalcRotationPivot (CalibrationPlane r, CalibrationPlane v, Transform[] vtrans, CalibrationPlane.Pivot pivot)
	{
		Vector3 n = Vector3.Cross (r.normal, v.normal);

		float alpha = (n != Vector3.zero && v.PivotVect (pivot) != Vector3.zero) ? Vector3.SignedAngle (v.PivotVect (pivot), n, v.normal) : 0.0f;
		float beta = (v.normal != Vector3.zero && r.normal != Vector3.zero) ? Vector3.SignedAngle (v.normal, r.normal, n) : 0.0f;
		float gamma = (n != Vector3.zero && r.PivotVect (pivot) != Vector3.zero) ? Vector3.SignedAngle (n, r.PivotVect (pivot), r.normal) : 0.0f;
	
		this.transform.RotateAround (v.PivotPoint (pivot), v.normal, alpha);

		v.SetPoints (vtrans);

		this.transform.RotateAround (v.PivotPoint (pivot), v.PivotVect (pivot), beta);

		v.SetPoints (vtrans);
	
		this.transform.RotateAround (v.PivotPoint (pivot), v.normal, gamma);

		return Quaternion.identity;
	}

	Quaternion CalcRotation (CalibrationPlane r, CalibrationPlane v, Transform[] vtrans)
	{
		CalcRotationPivot (r, v, vtrans, CalibrationPlane.Pivot.I);
		//CalcRotationPivot (r, v, vtrans, CalibrationPlane.Pivot.J);
		//CalcRotationPivot (r, v, vtrans, CalibrationPlane.Pivot.K);

		return Quaternion.identity;
	}

	float CalcScale (Vector3 nr, Vector3 nv)
	{
		float scale = Mathf.Sqrt (nr.magnitude / nv.magnitude);

		this.transform.localScale *= scale;

		return scale;
	}

	Vector3 CalcDist (CalibrationPlane r, CalibrationPlane v)
	{
		Vector3 rcenter = GetCenterOfPoints (new Vector3[]{ r.i, r.j, r.k });

		Vector3 vcenter = GetCenterOfPoints (new Vector3[]{ v.i, v.j, v.k });

		var dist = rcenter - vcenter;

		return dist;
	}

	public void Calibrate (PositionTag[] realPositionTags)
	{
		int length = Mathf.Min (realPositionTags.Length, virtualPositionTags.Length);
		int count = 0;
		float totalScale = 0;
		var avgRotation = Quaternion.identity;
		var cumulativeRotation = Vector4.zero;
		var totalDist = Vector3.zero;
		var startScale = this.transform.localScale;
		var startRotation = this.transform.rotation;
		var startPosition = this.transform.position;
		Quaternion? firstRotation = null;

		for (int i = 0; i < length; i++) {
			for (int j = i + 1; j < length; j++) {
				for (int k = j + 1; k < length; k++) {

					var rtrans = new Transform[] {
						realPositionTags [i].transform,
						realPositionTags [j].transform,
						realPositionTags [k].transform
					};

					var vtrans = new Transform[] {
						virtualPositionTags [i].transform,
						virtualPositionTags [j].transform,
						virtualPositionTags [k].transform
					};

					var r = new CalibrationPlane (rtrans);

					var v = new CalibrationPlane (vtrans);

					/*
					Vector3 n = Vector3.Cross (r.normal, v.normal);

					Debug.Log ("nr = " + r.normal.normalized);
					Debug.Log ("nv = " + v.normal.normalized);

					Debug.Log ("N = " + n.normalized);

					float alpha = (n != Vector3.zero && v.ij != Vector3.zero) ? Vector3.SignedAngle  (v.ij, n, v.normal) : 0.0f;
					float beta = (v.normal != Vector3.zero && r.normal != Vector3.zero) ? Vector3.SignedAngle (v.normal, r.normal, v.ij) : 0.0f;
					float gamma = (v.ij != Vector3.zero && r.ij != Vector3.zero) ? Vector3.SignedAngle (n, r.ij, v.normal) : 0.0f;

					Debug.Log ("a0 = " + alpha + " b0 = " + beta + " g0 = " + gamma);

					Debug.Log ("IJV initial = " + v.ij.normalized);

					Debug.Log ("Beta 1 =" + beta);

					// FIRST TRANSFORMATION
					this.transform.RotateAround (v.i, v.normal, alpha);

					Debug.LogError ("STOP");

					v.SetPoints (vtrans);

					alpha = (n != Vector3.zero && v.ij != Vector3.zero) ? Vector3.SignedAngle  (v.ij, n, v.normal) : 0.0f;
					beta = (v.normal != Vector3.zero && r.normal != Vector3.zero) ? Vector3.SignedAngle (v.normal, r.normal, v.ij) : 0.0f;
					gamma = (v.ij != Vector3.zero && r.ij != Vector3.zero) ? Vector3.SignedAngle (v.ij, r.ij, v.normal) : 0.0f;

					Debug.Log ("a1 = " + alpha + " b1 = " + beta + " g1 = " + gamma);
					
					Debug.Log ("IJV after = " + v.ij.normalized);

					// SECOND TRANSFORMATION
					this.transform.RotateAround (v.i, v.ij, beta);

					Debug.LogError ("STOP");

					v.SetPoints (vtrans);

					alpha = (n != Vector3.zero && v.ij != Vector3.zero) ? Vector3.SignedAngle  (v.ij, n, v.normal) : 0.0f;
					beta = (v.normal != Vector3.zero && r.normal != Vector3.zero) ? Vector3.SignedAngle (v.normal, r.normal, v.ij) : 0.0f;
					gamma = (v.ij != Vector3.zero && r.ij != Vector3.zero) ? Vector3.SignedAngle (v.ij, r.ij, v.normal) : 0.0f;

					Debug.Log ("a2 = " + alpha + " b2 = " + beta + " g2 = " + gamma);

					beta = (v.normal != Vector3.zero && r.normal != Vector3.zero) ? Vector3.Angle (v.normal, r.normal) : 0.0f;

					Debug.Log ("Beta 3 = " + beta);

					// THIRD TRANSFORMATION
					this.transform.RotateAround (v.i, v.normal, gamma);

					Debug.LogError ("STOP");
					*/

					var rotation = CalcRotation (r, v, vtrans);

					var scale = CalcScale (r.normal, v.normal);


					v.SetPoints (vtrans);

					var dist = CalcDist (r, v);
					count++;

					//avgRotation = rotation;
					//avgRotation = MathHelper.AverageQuaternion(ref cumulativeRotation, rotation, firstRotation.Value, count);
					totalScale += scale;
					totalDist += dist;

					Debug.Log (avgRotation);

					//this.transform.position = startPosition;
					//this.transform.localScale = startScale;
					// this.transform.rotation = startRotation;
				}
			}
		}
		//this.transform.localScale *= totalScale / (float)count;
		//this.transform.rotation *= avgRotation;
		this.transform.position += totalDist / (float)count;
	}
}

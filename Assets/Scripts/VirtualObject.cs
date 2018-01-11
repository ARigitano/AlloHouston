using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// A virtual object. This component can align itself to another objet by comparing their respective tags.
/// </summary>
public class VirtualObject : MonoBehaviour
{
	public PositionTag[] virtualPositionTags;

	void Start ()
	{
		virtualPositionTags = GetComponentsInChildren<PositionTag> ().OrderBy (x => x.positionTagIndex).ToArray ();
	}

	/// <summary>
	/// Gets the center of an array of points.
	/// </summary>
	/// <returns>The center of points as a Vector3</returns>
	/// <param name="points">An array of a points.</param>
	Vector3 GetCenterOfPoints (Vector3[] points)
	{
		var res = Vector3.zero;

		foreach (var point in points) {
			res += point;
		}

		return res / (float)points.Length;
	}

	/// <summary>
	/// Calculates the difference of rotation and position between two planes around a specific pivot point
	/// </summary>
	/// <returns>The rotation pivot, the difference of rotation and position between two planes.</returns>
	/// <param name="r">The real plane.</param>
	/// <param name="v">The virtual plane, it should a plane on this game object</param>
	/// <param name="vtrans">The position of the 3 points that define the virtual plane</param>
	/// <param name="pivot">The pivot point of the rotation.</param>
	RigidTransformation CalcRotationPivot (CalibrationPlane r, CalibrationPlane v, Transform[] vtrans, CalibrationPlane.Pivot pivot)
	{
		// We save the values of the current rotation and position to restore them at the end of the method
		Quaternion startRotation = this.transform.rotation;
		Vector3 startPosition = this.transform.position;

		// We calculate n, the normal vector between the two normal of the calibration planes
		Vector3 n = Vector3.Cross (r.normal, v.normal);

		// We make sure that all the points in the virtual calibration plane are corresponding to their transform value
		v.SetPoints (vtrans);

		// We compute the 3 euler angles.
		// Alpha is the rotation to align the virtual plane to the intersection of the two planes.
		// Beta is the rotation to make the two planes parallel.
		// Gamma is the rotation to align the points of the two planes now that the planes are parralel to each other.
		float alpha = (n != Vector3.zero && v.PivotVect (pivot) != Vector3.zero) ? Vector3.SignedAngle (v.PivotVect (pivot), n, v.normal) : 0.0f;
		float beta = (v.normal != Vector3.zero && r.normal != Vector3.zero) ? Vector3.SignedAngle (v.normal, r.normal, n) : 0.0f;
		float gamma = (n != Vector3.zero && r.PivotVect (pivot) != Vector3.zero) ? Vector3.SignedAngle (n, r.PivotVect (pivot), r.normal) : 0.0f;
	
		// Alpha rotation around the normal axis
		this.transform.RotateAround (v.PivotPoint (pivot), v.normal, alpha);

		// Once the rotation is done, we update the points
		v.SetPoints (vtrans);

		// Beta rotation around the pivot vect
		this.transform.RotateAround (v.PivotPoint (pivot), v.PivotVect (pivot), beta);

		// Once the rotation is done
		v.SetPoints (vtrans);
	
		// Gamma rotation
		this.transform.RotateAround (v.PivotPoint (pivot), v.normal, gamma);


		// The two planes are now parallel and aligned, we can compute the difference in rotation between the start and now.
		var rigidTransformation = new RigidTransformation (Quaternion.Inverse(startRotation) * this.transform.rotation, this.transform.position - startPosition);

		// We set the position and rotation back to where it was.
		this.transform.rotation = startRotation;
		this.transform.position = startPosition;

		// We then reset the point in the virtual plane
		v.SetPoints (vtrans);

		return rigidTransformation;
	}

	/// <summary>
	/// Calculates the difference of rotation and position by making an average of the rigid transformations for each of the 3 pivot points.
	/// </summary>
	/// <returns>The rotation pivot, the difference of rotation and position between two planes.</returns>
	/// <param name="r">The real plane.</param>
	/// <param name="v">The virtual plane, it should a plane on this game object</param>
	/// <param name="vtrans">The position of the 3 points that define the virtual plane</param>
	RigidTransformation CalcRotation (CalibrationPlane r, CalibrationPlane v, Transform[] vtrans)
	{
		var rigidTransformations = new RigidTransformation [3];
		rigidTransformations[0] = CalcRotationPivot (r, v, vtrans, CalibrationPlane.Pivot.I);
		rigidTransformations[1] = CalcRotationPivot (r, v, vtrans, CalibrationPlane.Pivot.J);
		rigidTransformations[2] = CalcRotationPivot (r, v, vtrans, CalibrationPlane.Pivot.K);

		var average = RigidTransformation.Average (rigidTransformations);

		this.transform.rotation *= average.rotation;
		this.transform.position += average.dist;

		Debug.Log ("average rotation=" + average.rotation.eulerAngles);
		Debug.Log ("average dist=" + average.dist);

		return average;
	}

	/// <summary>
	/// Calculates the difference in scale between the normals of the real plane and the virtual plane.
	/// </summary>
	/// <returns>The difference of scale</returns>
	/// <param name="nr">Normal vector of the real plane</param>
	/// <param name="nv">Normal vector of the virtual plane</param></param>
	float CalcScale (Vector3 nr, Vector3 nv)
	{
		float scale = Mathf.Sqrt (nr.magnitude / nv.magnitude);

		this.transform.localScale *= scale;

		return scale;
	}

	/// <summary>
	/// Calculates the difference in position between the real plane and the virtual plane.
	/// </summary>
	/// <returns>The difference in posi</returns>
	/// <param name="r">The red component.</param>
	/// <param name="v">V.</param>
	Vector3 CalcDist (CalibrationPlane r, CalibrationPlane v)
	{
		Vector3 rcenter = GetCenterOfPoints (new Vector3[]{ r.i, r.j, r.k });

		Vector3 vcenter = GetCenterOfPoints (new Vector3[]{ v.i, v.j, v.k });

		var dist = rcenter - vcenter;

		this.transform.position += dist;

		return dist;
	}

	/// <summary>
	/// Calibrate the objet to change its rotation, position and scale to match its position tags to the given positions tags.
	/// </summary>
	/// <param name="realPositionTags">Real position tags.</param>
	public void Calibrate (PositionTag[] realPositionTags)
	{
		int length = Mathf.Min (realPositionTags.Length, virtualPositionTags.Length);
		int count = 0;
		var startScale = this.transform.localScale;
		var startRotation = this.transform.rotation;
		var startPosition = this.transform.position;

		var scaleList = new List<float> ();
		var rotationList = new List<Quaternion> ();
		var distList = new List<Vector3> ();

		// To have the most accurate result we create two planes from all the possible unique combinations of 3 points and calculate an
		// average of all transformations in rotation, position and scale.
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

					var rigidTransformation = CalcRotation (r, v, vtrans);

					var scale = CalcScale (r.normal, v.normal);

					v.SetPoints (vtrans);

					var dist = CalcDist (r, v);
					count++;

					// We add the calculated transformation to the corresponding lists
					rotationList.Add(rigidTransformation.rotation);
					scaleList.Add (scale);
					distList.Add (rigidTransformation.dist + dist);

					// Once the transformations are saved, we can go back to the starting position
					this.transform.localScale = startScale;
					this.transform.rotation = startRotation;
					this.transform.position = startPosition;
				}
			}
		}
		this.transform.rotation *= MathHelper.AverageQuaternion (rotationList);
		this.transform.localScale *= scaleList.Average();
		this.transform.position += MathHelper.AverageVector3 (distList);
	}
}

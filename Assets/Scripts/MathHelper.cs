using UnityEngine;

public class MathHelper
{
	/// <summary>
	/// Get an average (mean) from more then two quaternions (with two, slerp would be used).
	/// Note: this only works if all the quaternions are relatively close together.
	/// </summary>
	/// <returns>The quaternion.</returns>
	/// <param name="cumulative">An external Vector4 which holds all the added x y z and w components.</param>
	/// <param name="newRotation">The next rotation to be added to the average pool.</param>
	/// <param name="firstRotation">The first quaternion of the array to be averaged.</param>
	/// <param name="addAmount">The total amount of quaternions which are currently added.</param>
	public static Quaternion AverageQuaternion(ref Vector4 cumulative, Quaternion newRotation, Quaternion firstRotation, int addAmount){

		float w = 0.0f;
		float x = 0.0f;
		float y = 0.0f;
		float z = 0.0f;

		//Before we add the new rotation to the average (mean), we have to check whether the quaternion has to be inverted. Because
		//q and -q are the same rotation, but cannot be averaged, we have to make sure they are all the same.
		if(!MathHelper.AreQuaternionsClose(newRotation, firstRotation)){

			newRotation = MathHelper.InverseSignQuaternion(newRotation);	
		}

		//Average the values
		float addDet = 1f/(float)addAmount;
		cumulative.w += newRotation.w;
		w = cumulative.w * addDet;
		cumulative.x += newRotation.x;
		x = cumulative.x * addDet;
		cumulative.y += newRotation.y;
		y = cumulative.y * addDet;
		cumulative.z += newRotation.z;
		z = cumulative.z * addDet;		

		//note: if speed is an issue, you can skip the normalization step
		return NormalizeQuaternion(x, y, z, w);
	}

	public static Quaternion NormalizeQuaternion(float x, float y, float z, float w){

		float lengthD = 1.0f / (w*w + x*x + y*y + z*z);
		w *= lengthD;
		x *= lengthD;
		y *= lengthD;
		z *= lengthD;

		return new Quaternion(x, y, z, w);
	}

	/// <summary>
	/// Changes the sign of the quaternion components. This is not the same as the inverse.
	/// </summary>
	/// <returns>A quaternion with inversed signss</returns>
	/// <param name="q">The quaternion that will be inversed</param>
	public static Quaternion InverseSignQuaternion(Quaternion q){

		return new Quaternion(-q.x, -q.y, -q.z, -q.w);
	}
		
	/// <summary>
	/// This can be used to check whether or not one of two quaternions which are supposed to
	/// be very similar but has its component signs reversed (q has the same rotation as -q)
	/// </summary>
	/// <returns><c>true</c>, if the two input quaternions are close to each other.</returns>
	/// <param name="q1">Q1.</param>
	/// <param name="q2">Q2.</param>
	public static bool AreQuaternionsClose(Quaternion q1, Quaternion q2){

		float dot = Quaternion.Dot(q1, q2);

		if(dot < 0.0f){

			return false;					
		}

		else{

			return true;
		}
	}
}
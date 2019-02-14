using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VRCalibrationTool
{
    public class MathHelper
    {
        /// <summary>
        /// Get the average quaternion a list of quaternions.
        /// </summary>
        /// <param name="quaternions"></param>
        /// <returns></returns>
        public static Quaternion AverageQuaternion(List<Quaternion> quaternions)
        {
            return AverageQuaternion(quaternions.ToArray());
        }
        /// <summary>
        /// Get the average of an array of quaternions.
        /// </summary>
        /// <param name="quaternions"></param>
        /// <returns></returns>
        public static Quaternion AverageQuaternion(Quaternion[] quaternions)
        {
            if (quaternions.Length == 1)
                return quaternions[0];

            var t = 1.0f / quaternions.Length;
            var average = Quaternion.identity;

            foreach (var quaternion in quaternions)
            {
                average *= Quaternion.Slerp(Quaternion.identity, quaternion, t);
            }

            return average;
        }
        /// <summary>
        /// Get the average vector of a list of vector.
        /// </summary>
        /// <param name="vector3List"></param>
        /// <returns></returns>
        public static Vector3 AverageVector3(List<Vector3> vector3List)
        {
            return AverageVector3(vector3List.ToArray());
        }
        /// <summary>
        /// Get the average vector of an array of vector.
        /// </summary>
        /// <param name="vector3Array"></param>
        /// <returns></returns>
        public static Vector3 AverageVector3(Vector3[] vector3Array)
        {
            if (vector3Array.Length == 1)
                return vector3Array[0];

            var sum = Vector3.zero;

            foreach (var vect in vector3Array)
            {
                sum += vect;
            }

            return sum / (float)vector3Array.Length;
        }
    }
}
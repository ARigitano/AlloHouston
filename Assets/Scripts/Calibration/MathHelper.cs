// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using UnityEngine;
using UnityEngine.Collections;
using System.Collections.Generic;

namespace VRCalibrationTool
{
    public class MathHelper
    {
        public static Quaternion AverageQuaternion(List<Quaternion> quaternions)
        {
            return AverageQuaternion(quaternions.ToArray());
        }

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

        public static Vector3 AverageVector3(List<Vector3> vector3List)
        {
            return AverageVector3(vector3List.ToArray());
        }

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
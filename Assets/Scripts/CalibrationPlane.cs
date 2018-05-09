// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRCalibrationTool
{
    public class CalibrationPlane
    {
        public enum Pivot
        {
            I,
            J,
            K,
        }

        public Vector3 i { get; private set; }

        public Vector3 j { get; private set; }

        public Vector3 k { get; private set; }

        public Vector3 ij { get { return j - i; } }

        public Vector3 jk { get { return k - j; } }

        public Vector3 ik { get { return k - i; } }

        public Vector3 normal { get { return Vector3.Cross(ij, ik); } }

        public CalibrationPlane(Vector3 i, Vector3 j, Vector3 k)
        {
            SetPoints(i, j, k);
        }

        public CalibrationPlane(Transform[] transforms)
        {
            SetPoints(transforms);
        }

        public Vector3 PivotPoint(Pivot pivot)
        {
            switch (pivot)
            {
                case Pivot.I:
                    return i;
                case Pivot.J:
                    return j;
                case Pivot.K:
                    return k;
            }
            return Vector3.zero;
        }

        public Vector3 PivotVect(Pivot pivot)
        {
            switch (pivot)
            {
                case Pivot.I:
                    return ij;
                case Pivot.J:
                    return jk;
                case Pivot.K:
                    return ik;
            }
            return Vector3.zero;
        }

        public void SetPoints(Transform[] transforms)
        {
            if (transforms.Length != 3)
                throw new UnityException("Length of the transform array should be 3");
            SetPoints(transforms[0].position, transforms[1].position, transforms[2].position);
        }

        public void SetPoints(Vector3 i, Vector3 j, Vector3 k)
        {
            this.i = i;
            this.j = j;
            this.k = k;
        }
    }
}
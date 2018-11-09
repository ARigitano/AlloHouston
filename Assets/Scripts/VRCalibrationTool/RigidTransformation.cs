// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using UnityEngine;
using System.Collections.Generic;

namespace VRCalibrationTool
{
    public class RigidTransformation
    {
        public Quaternion rotation { get; private set; }

        public Vector3 dist { get; private set; }

        public RigidTransformation(Quaternion rotation, Vector3 position)
        {
            this.rotation = rotation;
            this.dist = position;
        }

        public static RigidTransformation Average(RigidTransformation[] rigidTransformations)
        {
            if (rigidTransformations.Length == 1)
                return rigidTransformations[0];

            var rotations = new List<Quaternion>();
            var dists = new List<Vector3>();

            foreach (var rigidTransformation in rigidTransformations)
            {
                rotations.Add(rigidTransformation.rotation);
                dists.Add(rigidTransformation.dist);
            }

            return new RigidTransformation(MathHelper.AverageQuaternion(rotations), MathHelper.AverageVector3(dists));
        }
    }
}

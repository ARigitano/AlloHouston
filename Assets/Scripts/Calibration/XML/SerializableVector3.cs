using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace VRCalibrationTool
{
    /// <summary>
    /// Makes Vector3 serializable to be stored in XML file
    /// </summary>
    [Serializable]
    public struct SerializableVector3
    {
        public double x { get; private set; }
		public double y { get; private set; }
		public double z { get; private set; }

        public Vector3 Vector3
        {
            get
            {
                return new Vector3((float)x, (float)y, (float)z);
            }
        }

        public SerializableVector3(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }
    }
}

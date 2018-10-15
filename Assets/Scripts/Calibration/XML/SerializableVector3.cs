using UnityEngine;
using System;

namespace VRCalibrationTool
{
    /// <summary>
    /// Makes Vector3 serializable to be stored in XML file
    /// </summary>
    [Serializable] public class SerializableVector3
    {
        public double X;
		public double Y;
		public double Z;

        public Vector3 Vector3
        {
            get
            {
                return new Vector3((float)X, (float)Y, (float)Z);
            }
        }

        public SerializableVector3() { }
        public SerializableVector3(Vector3 vector)
        {
			double val;
			X = double.TryParse(vector.x.ToString(), out val) ? val : 0.0;
			Y = double.TryParse(vector.y.ToString(), out val) ? val : 0.0;
			Z = double.TryParse(vector.z.ToString(), out val) ? val : 0.0;
        }
    }
}

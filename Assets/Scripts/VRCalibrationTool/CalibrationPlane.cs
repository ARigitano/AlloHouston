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

        /// <summary>
        /// Creates a CalibrationPlane from 3 Vector3
        /// </summary>
        /// <param name="i">Vector3 i</param>
        /// <param name="j">Vector3 j</param>
        /// <param name="k">Vector3 k</param>
        public CalibrationPlane(Vector3 i, Vector3 j, Vector3 k)
        {
            SetPoints(i, j, k);
        }

        /// <summary>
        /// Creates a CalibrationPlane from 3 Transform
        /// </summary>
        /// <param name="transforms"></param>
        public CalibrationPlane(Transform i, Transform j, Transform k)
        {
            SetPoints(i, j, k);
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

        public void SetPoints(Transform i, Transform j, Transform k)
        {
            SetPoints(i.position, j.position, k.position);
        }

        public void SetPoints(Vector3 i, Vector3 j, Vector3 k)
        {
            this.i = i;
            this.j = j;
            this.k = k;
        }
    }
}
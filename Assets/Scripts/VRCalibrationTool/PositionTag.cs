using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRCalibrationTool
{
    public class PositionTag : MonoBehaviour
    {
        public int index;
        private Vector3 _startingPosition;

        private void Start()
        {
            _startingPosition = this.transform.position;
        }

        public void Reset()
        {
            this.transform.position = _startingPosition;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRCalibrationTool
{
    public class PositionTag : MonoBehaviour
    {
        public int index;
        /// <summary>
        /// Stating position.
        /// </summary>
        [SerializeField]
        [Tooltip("Starting position.")]
        private Vector3 _startingPosition;
        /// <summary>
        /// Position tag index text.
        /// </summary>
        [SerializeField]
        [Tooltip("Position tag index text.")]
        private TextMesh _positionTagIndexText = null;

        private void Start()
        {
            _startingPosition = this.transform.position;
            if (_positionTagIndexText == null)
                _positionTagIndexText = GetComponentInChildren<TextMesh>();
            _positionTagIndexText.text = index.ToString();
        }
        
        private void Update()
        {
            _positionTagIndexText.transform.LookAt(2 * _positionTagIndexText.transform.position - Camera.main.transform.position);
        }

        public void ResetPosition()
        {
            this.transform.position = _startingPosition;
        }
    }
}

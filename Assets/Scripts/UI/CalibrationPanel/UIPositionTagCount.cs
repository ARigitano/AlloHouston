using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Calibration.UI
{
    [RequireComponent(typeof(TextMesh))]
    public class UIPositionTagCount : MonoBehaviour
    {
        private CalibrationManager _calibrationManager;
        private TextMesh _text;

        private void Start()
        {
            if (_calibrationManager == null)
                _calibrationManager = GameObject.FindObjectOfType<CalibrationManager>();
            _text = GetComponent<TextMesh>();
        }

        private void Update()
        {
            if (_calibrationManager)
            {
                _text.text = _calibrationManager.canCreatePositionTag ? _calibrationManager.remainingPositionTags.Value.ToString() : "";
                transform.LookAt(2 * transform.position - Camera.main.transform.position);
            }
        }
    }
}

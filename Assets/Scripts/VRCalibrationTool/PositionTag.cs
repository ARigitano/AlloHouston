using UnityEngine;

namespace VRCalibrationTool
{
    public class PositionTag : MonoBehaviour
    {
        public int index;
        /// <summary>
        /// Stating position.
        /// </summary>
        private Vector3 _startingPosition;
        /// <summary>
        /// Position tag index text prefab.
        /// </summary>
        [SerializeField]
        [Tooltip("Position tag index text.")]
        private TextMesh _positionTagIndexTextPrefab = null;
        /// <summary>
        /// Position tag index text prefab.
        /// </summary>
        private TextMesh _positionTagIndexText= null;

        public bool managerTag { get; set; }

        private void Start()
        {
            _startingPosition = this.transform.position;
            _positionTagIndexText = Instantiate(_positionTagIndexTextPrefab, transform);
            _positionTagIndexText.text = (index + 1).ToString();
        }
        
        private void Update()
        {
            if (_positionTagIndexText != null && Camera.main != null)
                _positionTagIndexText.transform.LookAt(2 * _positionTagIndexText.transform.position - Camera.main.transform.position);
        }
        /// <summary>
        /// Resets the position of a tag.
        /// </summary>
        public void ResetPosition()
        {
            this.transform.position = _startingPosition;
        }
    }
}

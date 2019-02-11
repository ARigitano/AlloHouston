using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CRI.HelloHouston
{
    public class CameraTargetDetection : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The camera that will be used for the visibility detection.")]
        private Camera _camera;

        private GameObject[] _targets;
        private List<GameObject> _currentTargets = new List<GameObject>();

        private void Reset()
        {
            if (_camera == null)
                _camera = GetComponentInChildren<Camera>();
            if (_camera == null)
                _camera = GetComponentInParent<Camera>();
        }

        private void Start()
        {
            _currentTargets = new List<GameObject>();
            UpdateCameraVisibleList();
        }

        public void UpdateCameraVisibleList()
        {
            _targets = FindObjectsOfType<MonoBehaviour>().Where(x => x is ICameraTarget).Select(x => x.gameObject).ToArray();
        }

        private bool IsVisible(GameObject go)
        {
            Vector3 screenPoint = _camera.WorldToViewportPoint(go.transform.position);
            return (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1);
        }

        private void Update()
        {
            for (int i = 0; i < _targets.Length; i++)
            {
                var target = _targets[i];
                ICameraTarget cameraTarget = target.GetComponent<ICameraTarget>();
                bool visible = IsVisible(target);
                // Visible for the first time
                if (visible && !_currentTargets.Contains(target))
                {
                    cameraTarget.OnVisibleEnter();
                    _currentTargets.Add(target);
                }
                // Visible and already in the current target list.
                else if (visible)
                {
                    cameraTarget.OnVisibleStay();
                }
                // Not visible
                else
                {
                    cameraTarget.OnVisibleExit();
                    _currentTargets.Remove(target);
                }
            }
        }
    }
}
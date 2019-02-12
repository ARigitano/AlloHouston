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
        [SerializeField]
        [Tooltip("Rect of the camera visibility.")]
        private Rect _rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);

        private static List<CameraTarget> s_targets = new List<CameraTarget>();
        private List<CameraTarget> _currentTargets = new List<CameraTarget>();

        public static void Register(CameraTarget ct)
        {
            s_targets.Add(ct);
        }

        public static void Remove(CameraTarget ct)
        {
            s_targets.Remove(ct);
            ct.OnVisibleExit(null);
        }

        private void Reset()
        {
            if (_camera == null)
                _camera = GetComponentInChildren<Camera>();
            if (_camera == null)
                _camera = GetComponentInParent<Camera>();
        }

        private void Start()
        {
            _currentTargets = new List<CameraTarget>();
        }

        private bool IsVisible(CameraTarget ct)
        {
            Vector3 screenPoint = _camera.WorldToViewportPoint(ct.transform.position);
            return ct.gameObject.activeInHierarchy && (screenPoint.z > 0 && _rect.Contains(screenPoint));
        }

        private void Update()
        {
            _currentTargets.RemoveAll(x => !x.gameObject.activeInHierarchy);
            foreach (var target in s_targets)
            {
                CameraTarget cameraTarget = target.GetComponent<CameraTarget>();
                bool visible = IsVisible(target);
                // Visible for the first time
                if (visible && !_currentTargets.Contains(target))
                {
                    cameraTarget.OnVisibleEnter(_camera);
                    _currentTargets.Add(target);
                }
                // Visible and already in the current target list.
                else if (visible)
                {
                    cameraTarget.OnVisibleStay(_camera);
                }
                // Not visible and in the current target list.
                else if (!visible && _currentTargets.Contains(target))
                {
                    _currentTargets.Remove(target);
                    cameraTarget.OnVisibleExit(_camera);
                }
            }
        }
    }
}
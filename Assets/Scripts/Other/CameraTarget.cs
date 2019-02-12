using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CRI.HelloHouston
{
    public class CameraTarget : MonoBehaviour
    {
        [System.Serializable]
        public class CameraEvent : UnityEvent<Camera> { };
        /// <summary>
        /// List of allowed cameras. If the list is empty, any camera with the CameraTargetDetection script will trigger the events.
        /// </summary>
        [Tooltip("List of allowed cameras. If the list is empty, any camera with the CameraTargetDetection script will trigger the events.")]
        public List<Camera> allowedCameras = new List<Camera>(); 
        [SerializeField]
        [Tooltip("Called when the object becomes visible by the camera for the first time.")]
        private CameraEvent _onVisibleEnter = null;
        [SerializeField]
        [Tooltip("Called each frame if it's visible by the camera")]
        private CameraEvent _onVisibleStay = null;
        [SerializeField]
        [Tooltip("Called when the object stops being visible by the camera.")]
        private CameraEvent _onVisibleExit = null;

        public virtual void OnVisibleEnter(Camera camera)
        {
            if (allowedCameras == null || allowedCameras.Count == 0 || allowedCameras.Contains(camera))
                _onVisibleEnter.Invoke(camera);
        }

        public virtual void OnVisibleStay(Camera camera)
        {
            if (allowedCameras == null || allowedCameras.Count == 0 || allowedCameras.Contains(camera))
                _onVisibleEnter.Invoke(camera);
        }

        public virtual void OnVisibleExit(Camera camera)
        {
            if (allowedCameras == null || allowedCameras.Count == 0 || allowedCameras.Contains(camera))
                _onVisibleStay.Invoke(camera);
        }

        private void OnEnable()
        {
            CameraTargetDetection.Register(this);
        }

        private void OnDisable()
        {
            CameraTargetDetection.Remove(this);
        }
    }
}

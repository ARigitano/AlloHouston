using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CRI.HelloHouston
{
    public class CameraTarget : MonoBehaviour
    {
        [System.Serializable]
        public class CameraEvent : UnityEvent<Camera> { };
        [SerializeField]
        [Tooltip("Called the first frame when the object becomes visible by an allowed camera.")]
        private CameraEvent _onVisibleEnter = new CameraEvent();
        /// <summary>
        /// Called the first frame when the object becomes visible by an allowed camera.
        /// </summary>
        public CameraEvent onVisibleEnter
        {
            get
            {
                return _onVisibleEnter;
            }
        }
        [SerializeField]
        [Tooltip("Called each frame if it's visible by an allowed camera")]
        private CameraEvent _onVisibleStay = new CameraEvent();
        /// <summary>
        /// Called each frame if it's visible by an allowed camera.
        /// </summary>
        public CameraEvent onVisibleStay
        {
            get
            {
                return _onVisibleStay;
            }
        }
        [SerializeField]
        [Tooltip("Called the first frame when the object stops being visible by an allowed camera.")]
        private CameraEvent _onVisibleExit = new CameraEvent();
        /// <summary>
        /// Called the first frame when the object stops being visible by an allowed camera.
        /// </summary>
        public CameraEvent onVisibleExit
        {
            get
            {
                return _onVisibleExit;
            }
        }

        /// <summary>
        /// List of allowed cameras. If the list is empty, any camera with the CameraTargetDetection script will trigger the events.
        /// </summary>
        [Tooltip("List of allowed cameras. If the list is empty, any camera with the CameraTargetDetection script will trigger the events.")]
        public List<Camera> allowedCameras = new List<Camera>();
        [SerializeField]
        [Tooltip("If true, the camera target detection will check for occlusion.")]
        private bool _checkOcclusion = false;
        /// <summary>
        /// If true, the camera target detecttion will check for occlusion.
        /// </summary>
        public bool checkOcclusion
        {
            get
            {
                return _checkOcclusion;
            }
        }

        internal virtual void OnVisibleEnter(Camera camera)
        {
            try
            {
                if (allowedCameras == null || allowedCameras.Count == 0 || allowedCameras.Contains(camera))
                    _onVisibleEnter.Invoke(camera);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        internal virtual void OnVisibleStay(Camera camera)
        {
            try
            {
                if (allowedCameras == null || allowedCameras.Count == 0 || allowedCameras.Contains(camera))
                    _onVisibleStay.Invoke(camera);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        internal virtual void OnVisibleExit(Camera camera)
        {
            try
            {
                if (allowedCameras == null || allowedCameras.Count == 0 || allowedCameras.Contains(camera))
                    _onVisibleExit.Invoke(camera);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        private void OnEnable()
        {
            CameraTargetDetection.Register(this);
        }

        private void OnDisable()
        {
            CameraTargetDetection.Remove(this);
        }

        private void OnDestroy()
        {
            CameraTargetDetection.Remove(this);
        }
    }
}

using UnityEngine;

namespace CRI.HelloHouston
{
    public abstract class CameraTarget : MonoBehaviour
    {
        public abstract void OnVisibleEnter(Camera camera);
        public abstract void OnVisibleStay(Camera camera);
        public abstract void OnVisibleExit(Camera camera);

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

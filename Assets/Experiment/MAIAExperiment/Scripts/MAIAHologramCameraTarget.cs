using System;
using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class MAIAHologramCameraTarget : CameraTarget
    {
        [SerializeField]
        [Tooltip("The hologram tube.")]
        private MAIAHologramTube _hologramTube;

        private void Reset()
        {
            if (_hologramTube == null)
                _hologramTube = GetComponentInChildren<MAIAHologramTube>();
            if (_hologramTube == null)
                _hologramTube = GetComponentInParent<MAIAHologramTube>();
        }

        public override void OnVisibleEnter(Camera camera)
        {
            Debug.Log(camera.name);
            _hologramTube.OnVisibleEnter();
        }

        public override void OnVisibleExit(Camera camera) { }

        public override void OnVisibleStay(Camera camera) { }
    }
}

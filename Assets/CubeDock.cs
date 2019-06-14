using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CRI.HelloHouston.GameElements {
    public class CubeDock : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The hologram manager.")]
        private HologramManager _hologramManager = null;
        [SerializeField]
        [Tooltip("The communication screen.")]
        private UIComScreen _comScreen = null;
        [SerializeField]
        private AudioSource _appear;
        [SerializeField]
        private AudioSource _disappear;

        public void PlayAppear()
        {
            if (_appear.clip != null)
                _appear.Play();
        }

        public void PlayDisappear()
        {
            if (_disappear.clip != null)
                _disappear.Play();
        }


        void OnTriggerEnter(Collider other)
        {
            var holocube = other.GetComponentInParent<Holocube>();
            // We ignore this trigger if it's not linked in hierarchy with the holocube or if it's the holocube itself.
            if (holocube == null || other.GetComponent<Holocube>() != null)
                return;
            else
            {
                PlayAppear();

                if (holocube.stationFace.collider == other)
                {
                    holocube.ActivatedState();
                    _hologramManager.Enable();
                    _hologramManager.SwapHologram(holocube.stationFace.index);
                    _comScreen.gameObject.SetActive(true);
                }
                else if (holocube.tubexFace.collider == other)
                    _hologramManager.SwapHologram(holocube.tubexFace.index);
                else if (holocube.xpLeftFace.collider == other)
                    _hologramManager.SwapHologram(holocube.xpLeftFace.index);
                else if (holocube.xpRightFace.collider == other)
                    _hologramManager.SwapHologram(holocube.xpRightFace.index);
            }
        }

        void OnTriggerExit(Collider other)
        {
            var holocube = other.GetComponentInParent<Holocube>();
            if (holocube == null)
                return;
            var face = holocube.faces.FirstOrDefault(x => x.collider == other);
            if (face != null)
                _hologramManager.HideHologram();
        }
    }
}


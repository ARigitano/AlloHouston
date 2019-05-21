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

        void OnTriggerEnter(Collider other)
        {
            var holocube = other.GetComponentInParent<Holocube>();
            // We ignore this trigger if it's not linked in hierarchy with the holocube or if it's the holocube itself.
            if (holocube == null || other.GetComponent<Holocube>() != null)
                return;
            if (holocube.stationFace.face == other)
            {
                _hologramManager.Enable();
                _hologramManager.SwapHologram(holocube.stationFace.index);
                holocube.ActivateHolocube();
            }
            else if (holocube.tubexFace.face == other)
            {
                _hologramManager.SwapHologram(holocube.tubexFace.index);
                _comScreen.gameObject.SetActive(true);
            }
            else if (holocube.xpLeftFace.face == other)
                _hologramManager.SwapHologram(holocube.xpLeftFace.index);
            else if (holocube.xpRightFace.face == other)
                _hologramManager.SwapHologram(holocube.xpRightFace.index);
        }

        void OnTriggerExit(Collider other)
        {
            var holocube = other.GetComponentInParent<Holocube>();
            if (holocube == null)
                return;
            var face = holocube.faces.FirstOrDefault(x => x.face == other);
            if (face != null)
                _hologramManager.HideHologram();
        }
    }
}


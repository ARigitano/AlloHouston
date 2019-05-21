using UnityEngine;

namespace CRI.HelloHouston.GameElements
{
    public class HologramManager : MonoBehaviour
    {
        private IHologram[] _holograms;
        private IHologram _currentHologram;
        private bool _activated;

        private void Awake()
        {
            _holograms = GetComponentsInChildren<IHologram>();
            for (int i = 0; i < _holograms.Length; i++)
            {
                _holograms[i].visible = false;
                _holograms[i].HideHologram();
            }
        }

        /// <summary>
        /// Swaps the current active hologram.
        /// </summary>
        /// <param name="index">The index of the hologram to swap to. If there's none, nothing happens.</param>
        public void SwapHologram(int index)
        {
            if (index < _holograms.Length && _activated)
            {
                if (_currentHologram != null)
                    _currentHologram.HideHologram();
                if (_currentHologram == _holograms[index] && _currentHologram.visible)
                    return;
                _currentHologram = _holograms[index];
                _currentHologram.ShowHologram();
            }
        }

        /// <summary>
        /// Hides the current hologram.
        /// </summary>
        public void HideHologram()
        {
            if (_currentHologram != null && _currentHologram.visible)
                _currentHologram.HideHologram();
        }

        public void ShowHologram()
        {
            if (_currentHologram != null && _activated && !_currentHologram.visible)
                _currentHologram.ShowHologram();
        }

        public void Disable()
        {
            _activated = false;
            HideHologram();
        }

        public void Enable()
        {
            _activated = true;
            ShowHologram();
        }
    }
}

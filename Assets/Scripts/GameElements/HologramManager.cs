using UnityEngine;

namespace CRI.HelloHouston.GameElement
{
    public class HologramManager : MonoBehaviour
    {
        private IHologram[] _holograms;
        private IHologram _currentHologram;

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
            if (index < _holograms.Length)
            {
                if (_currentHologram != null)
                    _currentHologram.HideHologram();
                _currentHologram = _holograms[index];
                Debug.Log(_currentHologram);
                _currentHologram.ShowHologram();
            }
        }
    }
}

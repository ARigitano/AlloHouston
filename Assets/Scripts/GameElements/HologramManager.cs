using UnityEngine;

namespace CRI.HelloHouston.GameElement
{
    public class HologramManager : MonoBehaviour
    {
        private IHologram[] _holograms;
        private IHologram _currentHologram;

        private void Reset()
        {
            _holograms = GetComponentsInChildren<IHologram>();
        }

        public void SwapHologram(int index)
        {
            if (index < _holograms.Length)
            {
                if (_currentHologram != null)
                    _currentHologram.HideHologram();
                _currentHologram = _holograms[index];
                _currentHologram.ShowHologram();
            }
        }
    }
}

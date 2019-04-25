using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience.MAIA
{
    public enum MAIAParticleGridCellType
    {
        TubeSymbol,
        FeynmanSymbol,
        SignlessSymbol,
    }
    public class MAIAParticleGridCell : MAIAGridCell
    {
        [Header("Particle Grid Cell Fields")]
        /// <summary>
        /// Icon of the grid cell. Can be shown or hidden.
        /// </summary>
        [SerializeField]
        [Tooltip("Text of the grid cell. Can be shown or hidden.")]
        private Text _text = null;

        public Particle particle { get; private set; }

        public override void Show(bool visible)
        {
            base.Show(visible);
            _text.enabled = true;
        }

        public void SetText(string text)
        {
            _text.text = text;
        }

        public void Init(Particle particle, MAIAParticleGridCellType type)
        {
            switch (type)
            {
                case MAIAParticleGridCellType.TubeSymbol:
                    SetSprite(particle.symbolImage);
                    break;
                case MAIAParticleGridCellType.FeynmanSymbol:
                    SetSprite(particle.symbolImage2);
                    break;
                case MAIAParticleGridCellType.SignlessSymbol:
                    SetSprite(particle.signlessSymbolImage);
                    break;
            }
        }
    }
}

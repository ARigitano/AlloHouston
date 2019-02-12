using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class ParticleGridCell : GridCell
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

        public void Init(Particle particle)
        {
            SetSprite(particle.symbolImage2);
        }
    }
}

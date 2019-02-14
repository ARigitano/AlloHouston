using UnityEngine;
using UnityEngine.UI;


namespace CRI.HelloHouston.Experience.MAIA
{
    public class GridCell : MonoBehaviour
    {
        [Header("Grid Cell Field")]
        /// <summary>
        /// Icon of the grid cell. Can be shown or hidden.
        /// </summary>
        [SerializeField]
        [Tooltip("Icon of the grid cell. Can be shown or hidden.")]
        protected Image _icon = null;
        [SerializeField]
        [Tooltip("Canvas group of the grid cell.")]
        private CanvasGroup _canvasGroup = null;
        [SerializeField]
        [Tooltip("Alpha value of the canvas group when the grid cell is disabled")]
        private float _disabledAlpha = 0.1f;

        public virtual void Show(bool visible)
        {
            _icon.enabled = visible;
        }

        public virtual void Enable()
        {
            _canvasGroup.alpha = 1.0f;
        }

        public void Enable(bool enabled)
        {
            if (enabled)
                Enable();
            else
                Disable();
        }

        public virtual void Disable()
        {
            _canvasGroup.alpha = _disabledAlpha;
        }

        public void SetSprite(Sprite sprite)
        {
            _icon.sprite = sprite;
        }
    }
}

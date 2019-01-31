using UnityEngine;
using UnityEngine.UI;


namespace CRI.HelloHouston.Experience.MAIA
{
    public class GridCell : MonoBehaviour
    {
        /// <summary>
        /// Icon of the grid cell. Can be shown or hidden.
        /// </summary>
        [SerializeField]
        [Tooltip("Icon of the grid cell. Can be shown or hidden.")]
        private Image _icon = null;

        public void Show(bool visible)
        {
            _icon.enabled = visible;
        }

        public void SetSprite(Sprite sprite)
        {
            _icon.sprite = sprite;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class MAIACaseDiagram : MonoBehaviour
    {
        /// <summary>
        /// Image of the diagram.
        /// </summary>
        [SerializeField]
        [Tooltip("Image of the diagram.")]
        private Image _image = null;

        public  void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
        }
    }
}

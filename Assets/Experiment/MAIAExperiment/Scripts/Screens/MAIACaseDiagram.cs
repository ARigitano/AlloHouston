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
        private RawImage _image = null;

        public  void SetSprite(Texture texture)
        {
            _image.texture = texture;
        }
    }
}

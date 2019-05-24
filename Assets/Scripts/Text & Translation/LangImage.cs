using UnityEngine;

namespace CRI.HelloHouston.Translation
{
    /// <summary>
    /// An image associated with a language.
    /// </summary>
    [CreateAssetMenu(fileName = "New LangImage", menuName = "Translation/Lang Image", order = 1)]
    public class LangImage : ScriptableObject
    {
        /// <summary>
        /// The lang associated with the image.
        /// </summary>
        [Tooltip("The lang associated with the image.")]
        public LangApp lang;
        /// <summary>
        /// The image associated with the lang.
        /// </summary>
        [Tooltip("The image associated with the lang.")]
        public Sprite image;
    }
}
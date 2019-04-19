using UnityEngine;

namespace CRI.HelloHouston.Translation
{
    /// <summary>
    /// An image that can be translated in multiple languages.
    /// </summary>
    [CreateAssetMenu(fileName = "New TranslatedImage Asset", menuName = "Translation/Translated Image", order = 1)]
    public class TranslatedImageAsset : ScriptableObject
    {
        /// <summary>
        /// Array of images for each lang available.
        /// </summary>
        public LangImage[] images;

        public LangImage GetCurrentImage(LangApp lang)
        {
            LangImage la = null;
            foreach (LangImage image in images)
            {
                if (image.lang == lang)
                {
                    la = image;
                    break;
                }
            }
            return la;
        }
    }
}
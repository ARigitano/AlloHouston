using CRI.HelloHouston.Translation;
using UnityEngine;

namespace CRI.HelloHouston.Settings
{
    [System.Serializable]
    public struct LangSettings
    {
        /// <summary>
        /// List of all lang available.
        /// </summary>
        [Tooltip("List of all lang available.")]
        public LangApp[] langAppAvailable;
        /// <summary>
        /// The font used for the common.
        /// </summary>
        [Tooltip("The font used for the common.")]
        public Font commonFont;
        /// <summary>
        /// The default language.
        /// </summary>
        public LangApp defaultLanguage { get { return langAppAvailable[0]; } }
        /// <summary>
        /// All the different text files (one for each available language) of the application.
        /// </summary>
        [Tooltip("All the different text files (one for each available language) of the application.")]
        public TextAsset[] langTextFiles;
        /// <summary>
        /// The text file for the common text.
        /// </summary>
        [Tooltip("The text file for the common text")]
        public TextAsset commonTextFile;
    }
}
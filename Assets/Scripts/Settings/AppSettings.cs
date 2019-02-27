using UnityEngine;

namespace CRI.HelloHouston.Settings
{
    [CreateAssetMenu(fileName = "New Application Settings", menuName = "Settings/New Application Settings", order = 1)]
    public class AppSettings : ScriptableObject
    {
        /// <summary>
        /// The language settings.
        /// </summary>
        [Tooltip("The language settings.")]
        public LangSettings langSettings;
    }
}
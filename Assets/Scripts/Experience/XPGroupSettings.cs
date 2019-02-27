using CRI.HelloHouston.Experience.Actions;
using CRI.HelloHouston.Settings;
using CRI.HelloHouston.Translation;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    [CreateAssetMenu(fileName = "New Group Settings", menuName = "Experience/Group Settings", order = 3)]
    public class XPGroupSettings : ScriptableObject
    {
        /// <summary>
        /// All the possible actions for this experiment.
        /// </summary>
        [Tooltip("All the possible actions for this experiment.")]
        public ExperienceAction[] actions;
        /// <summary>
        /// The checklist of the experiment.
        /// </summary>
        [Tooltip("The checklist of the experiment.")]
        public string[] checklist;
        /// <summary>
        /// The language settings.
        /// </summary>
        [Tooltip("The language settings.")]
        public LangSettings langSettings;
    }
}
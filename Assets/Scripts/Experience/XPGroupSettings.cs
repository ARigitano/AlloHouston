using CRI.HelloHouston.Experience.Actions;
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
        /// All the different text files (one for each available language) of the experiment group. The first language of the array will be considered the default language.
        /// </summary>
        [Tooltip("All the different text files (one for each available language) of the experiment group. The first language of the array will be considered the default language.")]
        public TextAsset[] textFiles;
    }
}
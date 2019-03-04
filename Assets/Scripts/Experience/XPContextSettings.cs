using CRI.HelloHouston.Experience.Actions;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    [CreateAssetMenu(fileName = "New Context Settings", menuName = "Experience/Context Settings", order = 3)]
    public class XPContextSettings : ScriptableObject
    {
        /// <summary>
        /// All the possible actions for this experiment.
        /// </summary>
        [Tooltip("All the possible actions for this experiment.")]
        public ExperienceAction[] actions;
        /// <summary>
        /// The estimated duration (in minutes) of this version of the experiment.
        /// </summary>
        [Tooltip("The estimated duration (in minutes) of this version of the experiment.")]
        public int duration;
        [Tooltip("The number of steps of the context.")]
        [Range(1, 30)]
        public int steps;
        /// <summary>
        /// All the hints available for the experiment.
        /// </summary>
        [Tooltip("All the hints available for the experiment.")]
        public string[] availableHints;
        /// <summary>
        /// The checklist of the experiment.
        /// </summary>
        [Tooltip("The checklist of the experiment.")]
        public string[] checklist;
    }
}

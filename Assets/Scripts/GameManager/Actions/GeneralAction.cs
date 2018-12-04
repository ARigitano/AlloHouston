using UnityEngine;

namespace CRI.HelloHouston.Experience.Actions
{
    public abstract class GeneralAction: ScriptableObject
    {
        /// <summary>
        /// Icon of the game action.
        /// </summary>
        [Tooltip("Icon of the game action.")]
        public Sprite menuIcon;
        /// <summary>
        /// Tooltip of the action.
        /// </summary>
        [Tooltip("Action tooltip")]
        public string actionTooltip;
        /// <summary>
        /// Duration of the action (in seconds).
        /// </summary>
        [Tooltip("Duration of the action (in seconds).")]
        public float actionDuration;
    }
}


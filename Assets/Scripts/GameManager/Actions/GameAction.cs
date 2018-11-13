using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    public abstract class GameAction : ScriptableObject
    {
        /// <summary>
        /// Icon of the game action.
        /// </summary>
        [Tooltip("Icon of the game action.")]
        public Texture2D menuIcon;
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
        /// <summary>
        /// Applies the action to the GameActionController.
        /// </summary>
        /// <param name="controller"></param>
        public abstract void Act(GameActionController controller);
    }
}
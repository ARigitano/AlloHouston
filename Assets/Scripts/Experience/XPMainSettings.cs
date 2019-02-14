using CRI.HelloHouston.Experience.Actions;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{

    [CreateAssetMenu(fileName = "New XPSettings", menuName = "Experience/XpMainSettings", order = 2)]
    public class XPMainSettings : ScriptableObject
    {
        /// <summary>
        /// All the available hints.
        /// </summary>
        [Tooltip("All the available hints.")]
        public string[] hints;
        /// <summary>
        /// All the possible actions for the game.
        /// </summary>
        [Tooltip("All the possible actions for the game.")]
        public GameAction[] actions;
        /// <summary>
        /// Every possible errors that could be displayed on the table screen for the experiment.
        /// </summary>
        [Tooltip("Every possible errors that could be displayed on the table screen for the experiment.")]
        [SerializeField]
        public string[] possibleErrors;
    }
}
﻿using CRI.HelloHouston.Experience.Actions;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    [CreateAssetMenu(fileName = "New XPSettings", menuName = "Experience/XpSettings", order = 3)]
    public class XPSettings : ScriptableObject
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

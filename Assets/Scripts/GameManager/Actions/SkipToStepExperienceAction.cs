using System;
using UnityEngine;

namespace CRI.HelloHouston.Experience.Actions
{
    [CreateAssetMenu(fileName = "New SkipToStep Action", menuName = "Actions/Experience/Skip To Step")]
    public class SkipToStepExperienceAction : ExperienceAction
    {
        /// <summary>
        /// The step the action will skip to.
        /// </summary>
        [Tooltip("The step the action will skip to.")]
        public int step;

        public override void Act(XPSynchronizer controller)
        {
            controller.SkipToStep(step);
        }
    }
}
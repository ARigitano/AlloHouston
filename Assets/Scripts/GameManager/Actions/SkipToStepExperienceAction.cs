using CRI.HelloHouston.Experience.MAIA;
using UnityEngine;

namespace CRI.HelloHouston.Experience.Actions
{
    [CreateAssetMenu(fileName = "New SkipToStep Action", menuName = "Actions/Experience/Skip To Step")]
    public class SkipToStepExperienceAction : ExperienceAction
    {
        [Header("Skip To Step Parameter")]
        /// <summary>
        /// The step this action is skipping to.
        /// </summary>
        [Tooltip("The step this action is skipping to.")]
        public int step;

        public override void Act(XPManager controller)
        {
            if (controller != null)
                controller.SkipToStep(step);
        }
    }
}
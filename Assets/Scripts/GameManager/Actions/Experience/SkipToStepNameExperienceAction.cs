using UnityEngine;

namespace CRI.HelloHouston.Experience.Actions
{
    [CreateAssetMenu(fileName = "New SkipToStepName Action", menuName = "Actions/Experience/Skip To Step Name")]
    public class SkipToStepNameExperienceAction : ExperienceAction
    {
        [Header("Skip To Step Parameter")]
        /// <summary>
        /// The name of the step this action is skipping to.
        /// </summary>
        [Tooltip("The name of the step this action is skipping to.")]
        public string step;

        public override void Act(XPManager controller)
        {
            if (controller != null)
                controller.SkipToStep(step);
        }
    }
}
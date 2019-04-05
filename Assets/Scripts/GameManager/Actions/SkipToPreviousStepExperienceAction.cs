using UnityEngine;

namespace CRI.HelloHouston.Experience.Actions
{
    [CreateAssetMenu(fileName = "New SkipToPreviousStep Action", menuName = "Actions/Experience/Skip To Previous Step")]
    public class SkipToPreviousStepExperienceAction : ExperienceAction
    {
        public override void Act(XPManager controller)
        {
            if (controller != null)
                controller.SkipToPreviousStep();
        }
    }
}
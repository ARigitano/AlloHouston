using UnityEngine;

namespace CRI.HelloHouston.Experience.Actions
{
    [CreateAssetMenu(fileName = "New SkipToNextStep Action", menuName = "Actions/Experience/Skip To Next Step")]
    public class SkipToNextStepExperienceAction : ExperienceAction
    {
        public override void Act(XPManager controller)
        {
            if (controller != null)
                controller.SkipToNextStep();
        }
    }
}
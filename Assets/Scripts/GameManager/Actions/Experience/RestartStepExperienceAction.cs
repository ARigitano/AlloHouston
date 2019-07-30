using UnityEngine;

namespace CRI.HelloHouston.Experience.Actions
{
    [CreateAssetMenu(fileName = "New RestartStep Action", menuName = "Actions/Experience/Restart Step")]
    public class RestartStepExperienceAction : ExperienceAction
    {
        public override void Act(XPManager controller)
        {
            if (controller != null)
                controller.SkipToPreviousStep();
        }
    }
}
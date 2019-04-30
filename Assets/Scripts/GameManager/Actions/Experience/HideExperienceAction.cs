using UnityEngine;

namespace CRI.HelloHouston.Experience.Actions
{
    [CreateAssetMenu(fileName = "New Hide Action", menuName = "Actions/Experience/Hide")]
    public class HideExperienceAction : ExperienceAction
    {
        public override void Act(XPManager controller)
        {
            if (controller != null)
                controller.Hide();
        }
    }
}
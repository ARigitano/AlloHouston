using CRI.HelloHouston.Experience.Actions;
using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    [CreateAssetMenu(fileName = "New StartHologramAnimation Action", menuName = "Actions/Experience/MAIA/StartHologramAnimation")]
    public class MAIAStartAnimationAction : ExperienceAction
    {
        public override void Act(XPManager controller)
        {
            MAIAManager manager = controller as MAIAManager;
            if (manager != null)
                manager.StartHologramTubeAnimation();
        }
    }
}

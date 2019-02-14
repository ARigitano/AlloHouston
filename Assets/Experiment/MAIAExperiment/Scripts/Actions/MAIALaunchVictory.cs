using CRI.HelloHouston.Experience.Actions;
using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    [CreateAssetMenu(fileName = "New LaunchVictory Action", menuName = "Actions/Experience/MAIA/LaunchVictory")]
    public class MAIALaunchVictory : ExperienceAction
    {
        public override void Act(XPManager controller)
        {
            MAIAManager manager = controller as MAIAManager;
            if (manager != null)
                manager.LaunchVictory();
        }
    }
}

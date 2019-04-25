using System;
using CRI.HelloHouston.Experience.Actions;
using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    [CreateAssetMenu(fileName = "New ResetHologram Action", menuName = "Actions/Experience/MAIA/ResetHologram")]
    public class MAIAResetHologramAction : ExperienceAction
    {
        public override void Act(XPManager controller)
        {
            MAIAManager manager = controller as MAIAManager;
            if (manager != null)
                manager.ResetHologram();
        }
    }
}

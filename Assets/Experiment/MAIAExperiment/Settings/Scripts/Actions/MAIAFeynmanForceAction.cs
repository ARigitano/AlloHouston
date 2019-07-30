using System;
using CRI.HelloHouston.Experience.Actions;
using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    [CreateAssetMenu(fileName = "New FeynmanForce Action", menuName = "Actions/Experience/MAIA/ForceFeynman")]
    public class MAIAFeynmanForceAction : ExperienceAction
    {
        public override void Act(XPManager controller)
        {
            MAIAManager manager = controller as MAIAManager;
            if (manager != null)
                manager.ForceFeynmanHologram();
        }
    }
}

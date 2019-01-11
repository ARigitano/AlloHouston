using CRI.HelloHouston.Experience.MAIA;
using System;
using UnityEngine;

namespace CRI.HelloHouston.Experience.Actions
{
    [CreateAssetMenu(fileName = "New SkipToStep Action", menuName = "Actions/Experience/Skip To Step")]
    public class SkipToStepExperienceAction : ExperienceAction
    {
        public override void Act(XPManager controller)
        {
            MAIAManager synchronizer = controller as MAIAManager;
            if (synchronizer != null)
                synchronizer.SkipStepOne();
        }
    }
}
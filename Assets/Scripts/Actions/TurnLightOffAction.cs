using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Action
{
    [CreateAssetMenu(menuName = "GameActions/TurnLightOffAction")]
    public class TurnLightOffAction : GameAction
    {
        public override void Act(GameActionController controller)
        {
            foreach (Light light in controller.lights)
            {
                light.enabled = false;
            }
        }
    }
}

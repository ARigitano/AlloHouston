using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.Actions
{
    [CreateAssetMenu(fileName = "New TurnLightOff Action", menuName = "Actions/Game/Turn Light Off")]
    public class TurnLightOffAction : GameAction
    {
        public override void Act(GameManager controller)
        {
            /*
            foreach (Light light in controller.lights)
            {
                light.enabled = false;
            }
            */
        }
    }
}

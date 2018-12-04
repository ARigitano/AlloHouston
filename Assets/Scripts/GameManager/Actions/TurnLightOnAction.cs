using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.Actions
{
    [CreateAssetMenu(fileName = "New Turn Light On Action", menuName = "Actions/Game/Turn Light On")]
    public class TurnLightOnAction : GameAction
    {
        public override void Act(GameManager controller)
        {
            /*
            foreach (Light light in controller.lights)
            {
                light.enabled = true;
            }
            */
        }
    }
}

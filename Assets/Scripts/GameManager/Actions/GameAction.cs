using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.Actions
{
    public abstract class GameAction : GeneralAction
    {
        /// <summary>
        /// Applies the action to the GameActionController.
        /// </summary>
        /// <param name="controller"></param>
        public abstract void Act(GameActionController controller);
    }
}
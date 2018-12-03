using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.Actions
{
    public abstract class GameAction : GeneralAction
    {
        public abstract void Act(GameManager controller);
    }
}
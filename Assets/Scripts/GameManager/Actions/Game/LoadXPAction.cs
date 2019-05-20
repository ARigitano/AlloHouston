using CRI.HelloHouston.Audio;
using UnityEngine;

namespace CRI.HelloHouston.Experience.Actions
{
    [CreateAssetMenu(fileName = "New LoadXP Action", menuName = "Actions/Game/Load XP")]
    public class LoadXPAction : GameAction
    {
        [Tooltip("Index of the manager of the experience that will be loaded.")]
        public int xpManagerIndex;
        [Tooltip("Index of the wall top zone on which the experience will be loaded.")]
        public int wallTopIndex;

        public override void Act(GameManager controller)
        {
            if (controller != null)
                controller.LoadXP(xpManagerIndex, wallTopIndex);
        }
    }
}

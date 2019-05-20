using CRI.HelloHouston.Audio;
using UnityEngine;

namespace CRI.HelloHouston.Experience.Actions
{
    [CreateAssetMenu(fileName = "New UnloadXP Action", menuName = "Actions/Game/Unload XP")]
    public class UnloadXPAction : GameAction
    {
        [Tooltip("Index of the wall top zone on which the experience will be loaded.")]
        public int wallTopIndex;

        public override void Act(GameManager controller)
        {
            if (controller != null)
                controller.UnloadXP(wallTopIndex);
        }
    }
}

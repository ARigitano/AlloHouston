using CRI.HelloHouston.Audio;
using UnityEngine;

namespace CRI.HelloHouston.Experience.Actions
{
    [CreateAssetMenu(fileName = "New SetState Action", menuName = "Actions/Game/Set State")]
    public class SetStateAction : GameAction
    {
        /// <summary>
        /// The state to change to.
        /// </summary>
        [Tooltip("The state to change to.")]
        public GameState state;

        public override void Act(GameManager controller)
        {
            if (controller != null)
                controller.SetGameState(state);
        }
    }
}

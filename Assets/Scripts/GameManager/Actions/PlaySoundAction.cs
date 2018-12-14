using UnityEngine;

namespace CRI.HelloHouston.Experience.Actions
{
    [CreateAssetMenu(fileName = "New PlaySound Action", menuName = "Actions/Game/Play Sound")]
    public class PlaySoundAction : GameAction
    {
        /// <summary>
        /// The sound that will be played by the action.
        /// </summary>
        [Tooltip("The sound that will be played by the action.")]
        public PlayableSound sound;

        public override void Act(GameManager controller)
        {
            controller.PlaySound(sound);
        }
    }
}
using UnityEngine;

namespace CRI.HelloHouston.Experience.Actions
{
    [CreateAssetMenu(fileName = "New PlayMusic Action", menuName = "Actions/Game/Play Music")]
    public class PlayMusicAction : GameAction
    {
        /// <summary>
        /// The music that will be played by the action.
        /// </summary>
        [Tooltip("The music that will be played by the action.")]
        public AudioSource music;

        public override void Act(GameManager controller)
        {
            controller.PlayMusic(music);
        }
    }
}

using UnityEngine;

namespace CRI.HelloHouston.Experience.Actions
{
    [CreateAssetMenu(fileName = "New PlayMusic Action", menuName = "Actions/Experience/Play Music")]
    public class PlayMusicExperienceAction : ExperienceAction
    {
        /// <summary>
        /// The music that will be played by the action.
        /// </summary>
        [Tooltip("The music that will be played by the action.")]
        public AudioSource music;

        public override void Act(XPManager controller)
        {
            controller.PlayMusic(music);
        }
    }
}

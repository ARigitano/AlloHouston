using CRI.HelloHouston.Audio;
using System;
using UnityEngine;

namespace CRI.HelloHouston.Experience.Actions
{
    [CreateAssetMenu(fileName = "New PlaySound Action", menuName = "Actions/Experience/Play Sound")]
    public class PlaySoundExperienceAction : ExperienceAction
    {
        /// <summary>
        /// The sound that will be played by the action.
        /// </summary>
        [Tooltip("The sound that will be played by the action.")]
        public PlayableSound sound;

        public override void Act(XPManager controller)
        {
            controller.PlaySound(sound);
        }
    }
}
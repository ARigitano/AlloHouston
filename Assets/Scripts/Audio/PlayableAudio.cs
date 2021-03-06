﻿using UnityEngine;
using System.Collections.Generic;

namespace CRI.HelloHouston.Audio
{
    [System.Serializable]
    public abstract class PlayableAudio
    {
        /// <summary>
        /// The audio clip
        /// </summary>
        [Tooltip("The audio clip")]
        public AudioClip clip;

        /// <summary>
        /// The volume of the audio
        /// </summary>
        [Tooltip("The volume of the audio.")]
        [Range(0.0f, 1.0f)]
        public float volume = 1.0f;
        /// <summary>
        /// The pitch of the audio.
        /// </summary>
        [Tooltip("The pitch of the audio.")]
        [Range(-3.0f, 3.0f)]
        public float pitch = 1.0f;

        /// <summary>
        /// If true, the audio will start at a random point.
        /// </summary>
        public bool randomStart = false;

        /// <summary>
        /// The type of the mixer group
        /// </summary>
        public SoundManager.AudioMixerGroupType mixerGroupType
        {
            get
            {
                return GetAudioMixerGroupType();
            }
        }

        protected int _audioId = -1;

        public int audioId
        {
            get { return _audioId; }
            set { _audioId = value; }
        }

        public Audio.AudioType audioType
        {
            get
            {
                return GetAudioType();
            }
        }

        protected abstract Audio.AudioType GetAudioType();

        protected abstract SoundManager.AudioMixerGroupType GetAudioMixerGroupType();
    }

    public abstract class PlayableAbstractMusic : PlayableAudio
    {
        /// <summary>
        ///  How many seconds it needs for current music audio to fade out. It will override its own fade out seconds. If -1 is passed, current music will keep its own fade out seconds
        /// </summary>
        [Tooltip(" How many seconds it needs for current music audio to fade out. It will override its own fade out seconds. If -1 is passed, current music will keep its own fade out seconds")]
        public float currentMusicFadeOut = -1.0f;

        /// <summary>
        /// Whether the audio will be lopped
        /// </summary>
        [Tooltip("Whether the audio will be looped")]
        public bool loop = true;

        /// <summary>
        /// Whether the audio persists in between scene changes
        /// </summary>
        [Tooltip("Whether the audio persists between scene changes")]
        public bool persist = true;

        /// <summary>
        /// How many seconds it needs for the audio to fade in/ reach target volume (if higher than current)
        /// </summary>
        [Tooltip("How many seconds it needs for the audio to fade in/ reach target volume (if higher than current)")]
        public float fadeInSeconds = 1.0f;

        /// <summary>
        /// How many seconds it needs for the audio to fade out/ reach target volume (if lower than current)
        /// </summary>
        [Tooltip("How many seconds it needs for the audio to fade out/ reach target volume (if lower than current)")]
        public float fadeOutSeconds = 1.0f;


        protected override Audio.AudioType GetAudioType()
        {
            return Audio.AudioType.Music;
        }
    }

    [System.Serializable]
    public class PlayableMusic : PlayableAbstractMusic
    {
        public List<PlayableSubMusic> subMusics;

        protected override SoundManager.AudioMixerGroupType GetAudioMixerGroupType()
        {
            return SoundManager.AudioMixerGroupType.Music;
        }
    }

    [System.Serializable]
    public class PlayableSubMusic : PlayableAbstractMusic
    {
        public SoundManager.AudioMixerGroupType audioMixerGroupType;

        protected override SoundManager.AudioMixerGroupType GetAudioMixerGroupType()
        {
            return audioMixerGroupType;
        }
    }

    [System.Serializable]
    public abstract class PlayableAbstractSound : PlayableAudio
    {
        /// <summary>
        /// The minimum value of the pitch range. Make it equal to the max value if you don't a want random pitch
        /// </summary>
        [Tooltip("The minimum value of the pitch range. Make it equal to the max value if you don't a want random pitch")]
        public float minPitch = 1.0f;

        /// <summary>
        /// The maximum value of the pitch range. Make it equal to the min value if you don't a want random pitch
        /// </summary>
        [Tooltip("The maximum value of the pitch range. Make it equal to the min value if you don't a want random pitch")]
        public float maxPitch = 1.0f;
    }

    [System.Serializable]
    public class PlayableSound : PlayableAbstractSound
    {
        /// <summary>
        /// How many seconds it needs for the audio to fade in/ reach target volume (if higher than current)
        /// </summary>
        [Tooltip("How many seconds it needs for the audio to fade in/ reach target volume (if higher than current)")]
        public float fadeInSeconds = 0.0f;

        /// <summary>
        /// How many seconds it needs for the audio to fade out/ reach target volume (if lower than current)
        /// </summary>
        [Tooltip("How many seconds it needs for the audio to fade out/ reach target volume (if lower than current)")]
        public float fadeOutSeconds = 0.0f;

        /// <summary>
        /// Whether the audio will be lopped
        /// </summary>
        [Tooltip("Whether the audio will be looped")]
        public bool loop = false;

        protected override Audio.AudioType GetAudioType()
        {
            return Audio.AudioType.Sound;
        }

        protected override SoundManager.AudioMixerGroupType GetAudioMixerGroupType()
        {
            return SoundManager.AudioMixerGroupType.Sound;
        }
    }
}
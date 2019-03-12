using System.Collections;
using UnityEngine;

namespace CRI.HelloHouston.WindowTemplate
{
    public class AnimationSequence : AnimationElement
    {
        [SerializeField]
        [Tooltip("Animator component of the sub-animators. The order of this array impacts the order of apparition of the components of the animation.")]
        protected AnimationElement[] _animators = null;
        [SerializeField]
        [Tooltip("If true, the delay between the animation will be overrided by the interval delay. If false, the interval delay will only be added to the animator's initial delay.")]
        protected bool _overrideDelay;
        [SerializeField]
        [Tooltip("The delay between two animations. This delay will be added to the animator's initial delay value.")]
        protected float _postShowIntervalDelay;
        [SerializeField]
        [Tooltip("The delay between two animations. This delay will be added to the animator's initial delay value.")]
        protected float _postHideIntervalDelay;

        protected virtual IEnumerator HideAnimation()
        {
            for (int i = 0; i < _animators.Length; i++)
            {
                AnimationElement animator = _animators[(_animators.Length - 1) - i];
                float delay = _overrideDelay ? _postHideIntervalDelay : animator.postHideDelay + _postHideIntervalDelay;
                animator.StartHideAnimation();
                yield return new WaitForSeconds(delay);
            }
        }

        protected virtual IEnumerator ShowAnimation()
        {
            
            for (int i = 0; i < _animators.Length; i++)
            {
                AnimationElement animator = _animators[i];
                float delay = _overrideDelay ? _postShowIntervalDelay : animator.postShowDelay + _postShowIntervalDelay;
                animator.StartShowAnimation();
                yield return new WaitForSeconds(delay);
            }
        }

        public override void StartShowAnimation()
        {
            StartCoroutine(ShowAnimation());
        }

        public override void StartHideAnimation()
        {
            StartCoroutine(HideAnimation());
        }
    }
}

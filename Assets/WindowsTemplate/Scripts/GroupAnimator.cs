using System.Collections;
using UnityEngine;

namespace CRI.HelloHouston.Window
{
    public class GroupAnimator : GenericAnimator
    {
        [SerializeField]
        [Tooltip("Animator component of the sub-animators. The order of this array impacts the order of apparition of the components of the animation.")]
        protected GenericAnimator[] _animators = null;
        [SerializeField]
        [Tooltip("If true, the delay between the animation will be overrided by the interval delay. If false, the interval delay will only be added to the animator's initial delay.")]
        protected bool _overrideDelay;
        [SerializeField]
        [Tooltip("The delay between two animations. This delay will be added to the animator's initial delay value.")]
        protected float _intervalDelay;

        protected virtual IEnumerator HideAnimation()
        {
            for (int i = 0; i < _animators.Length; i++)
            {
                GenericAnimator animator = _animators[(_animators.Length - 1) - i];
                float delay = _overrideDelay ? _intervalDelay : animator.delay + _intervalDelay;
                animator.StartHideAnimation();
                yield return new WaitForSeconds(delay);
            }
        }

        protected virtual IEnumerator ShowAnimation()
        {
            
            for (int i = 0; i < _animators.Length; i++)
            {
                GenericAnimator animator = _animators[i];
                float delay = _overrideDelay ? _intervalDelay : animator.delay + _intervalDelay;
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

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace CRI.HelloHouston.WindowTemplate
{
    public class Window : MonoBehaviour
    {
        [Header("Window Attributes")]
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

        public bool hidden { get; protected set; }

        protected virtual IEnumerator HideAnimation(Action action)
        {
            for (int i = 0; i < _animators.Length; i++)
            {
                AnimationElement animator = _animators[(_animators.Length - 1) - i];
                float delay = _overrideDelay ? _postHideIntervalDelay : animator.postHideDelay + _postHideIntervalDelay;
                animator.StartHideAnimation();
                yield return new WaitForSeconds(delay);
            }
            hidden = true;
            if (action != null)
                action();
            gameObject.SetActive(false);
        }

        protected virtual IEnumerator ShowAnimation(Action action)
        {
            hidden = false;
            for (int i = 0; i < _animators.Length; i++)
            {
                AnimationElement animator = _animators[i];
                float delay = _overrideDelay ? _postShowIntervalDelay : animator.postShowDelay + _postShowIntervalDelay;
                animator.StartShowAnimation();
                yield return new WaitForSeconds(delay);
            }
            if (action != null)
                action();
        }

        public void ShowWindow()
        {
            ShowWindow(null);
        }

        public void ShowWindow(Action action)
        {
            Debug.Log("Show " + name);
            gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(ShowAnimation(action));
        }

        public void HideWindow()
        {
            HideWindow(null);
        }

        public void HideWindow(Action action)
        {
            Debug.Log("Hide " + name);
            if (gameObject.activeInHierarchy)
            {
                StopAllCoroutines();
                StartCoroutine(HideAnimation(action));
            }
            else if (action != null)
            {
                StopAllCoroutines();
                action();
            }
        }
    }
}

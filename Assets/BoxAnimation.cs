using System;
using System.Collections;
using UnityEngine;

namespace CRI.HelloHouston.Window
{
    public class BoxAnimation : GenericAnimator
    {
        [SerializeField]
        [Tooltip("Duration of the show animation.")]
        private float _showAnimationDuration = 1.0f;
        [SerializeField]
        [Tooltip("Duration of the hide animation.")]
        private float _hideAnimationDuration = 1.0f;
        [SerializeField]
        private Vector2 _startingSize;
        [SerializeField]
        [Tooltip("Animation curve.")]
        private AnimationCurve _animationCurve;

        private IEnumerator HideAnimation()
        {
            float startingTime = Time.time;
            RectTransform rt = GetComponent<RectTransform>();
            Vector2 finishSize = rt.sizeDelta;
            float halfDuration = _hideAnimationDuration / 2.0f;
            while (Time.time < startingTime + halfDuration)
            {
                rt.sizeDelta = new Vector2(rt.sizeDelta.x, Mathf.Lerp(finishSize.y, _startingSize.y, _animationCurve.Evaluate((Time.time - startingTime) / halfDuration)));
                yield return null;
            }
            while (Time.time < startingTime + _hideAnimationDuration)
            {
                rt.sizeDelta = new Vector2(Mathf.Lerp(finishSize.x, _startingSize.x, _animationCurve.Evaluate((Time.time - (startingTime + halfDuration)) / halfDuration)), rt.sizeDelta.y);
                yield return null;
            }
        }

        private IEnumerator ShowAnimation()
        {
            float startingTime = Time.time;
            RectTransform rt = GetComponent<RectTransform>();
            Vector2 finishSize = rt.sizeDelta;
            rt.sizeDelta = _startingSize;
            float halfDuration = _showAnimationDuration / 2.0f;
            while (Time.time < startingTime + halfDuration)
            {
                rt.sizeDelta = new Vector2(rt.sizeDelta.x, Mathf.Lerp(_startingSize.y, finishSize.y, _animationCurve.Evaluate((Time.time - startingTime) / halfDuration)));
                yield return null;
            }
            while (Time.time < startingTime + _showAnimationDuration)
            {
                rt.sizeDelta = new Vector2(Mathf.Lerp(_startingSize.x, finishSize.x, _animationCurve.Evaluate((Time.time - (startingTime + halfDuration)) / halfDuration)), rt.sizeDelta.y);
                yield return null;
            }
        }

        public override void StartHideAnimation()
        {
            StopAllCoroutines();
            StartCoroutine(HideAnimation());
        }

        public override void StartShowAnimation()
        {
            StopAllCoroutines();
            StartCoroutine(ShowAnimation());
        }
    }
}
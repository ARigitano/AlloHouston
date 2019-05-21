using CRI.HelloHouston.WindowTemplate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class MAIAHologramDiagram : AnimatorElement
    {
        [SerializeField]
        [Tooltip("The renderer of the screen.")]
        private MeshRenderer _screenRenderer = null;
        /// <summary>
        /// The renderer of the screen.
        /// </summary>
        public MeshRenderer screenRenderer
        {
            get
            {
                return _screenRenderer;
            }
        }
        [SerializeField]
        [Tooltip("The renderer of the content.")]
        private MeshRenderer _contentRenderer = null;
        /// <summary>
        /// The renderer of the content.
        /// </summary>
        public MeshRenderer contentRenderer
        {
            get
            {
                return _contentRenderer;
            }
        }
        [SerializeField]
        [Tooltip("Anchor point for the line.")]
        private Transform _anchorPoint = null;
        [SerializeField]
        [Tooltip("Duration of the animation before the animator animation.")]
        private float _preAnimationDuration = 0.0f;
        /// <summary>
        /// Anchor point for the line.
        /// </summary>
        public Transform anchorPoint
        {
            get
            {
                return _anchorPoint;
            }
        }

        public Vector3 center { get; set; }

        private Vector3 _startingPosition;

        public bool displayLine { get; set; }

        private void Start()
        {
            _startingPosition = transform.position;
        }

        private void Reset()
        {
            var meshRenderers = GetComponentsInChildren<MeshRenderer>();
            int length = meshRenderers.Length;
            if (length > 0)
                _screenRenderer = meshRenderers[0];
            if (length > 1)
                _contentRenderer = meshRenderers[1];
        }

        private IEnumerator ShowAnimation()
        {
            float startingTime = Time.time;
            displayLine = true;
            while (Time.time - startingTime <= _preAnimationDuration)
            {
                this.transform.position = Vector3.Lerp(center, _startingPosition, (Time.time - startingTime) / _preAnimationDuration);
                yield return null;
            }
            this.transform.position = _startingPosition;
            base.StartShowAnimation();
        }

        private IEnumerator HideAnimation()
        {
            float startingTime = Time.time;
            base.StartHideAnimation();
            while (Time.time - startingTime <= _preAnimationDuration)
            {
                this.transform.position = Vector3.Lerp(_startingPosition, center, (Time.time - startingTime) / _preAnimationDuration);
                yield return null;
            }
            this.transform.position = center; 
            displayLine = false;
        }

        protected override void StartShowAnimation()
        {
            StopAllCoroutines();
            StartCoroutine(ShowAnimation());
        }

        protected override void StartHideAnimation()
        {
            StopAllCoroutines();
            StartCoroutine(HideAnimation());
        }
    }
}

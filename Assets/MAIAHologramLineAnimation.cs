using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    [RequireComponent(typeof(XRLineRenderer))]
    public class MAIAHologramLineAnimation : MonoBehaviour
    {
        /// <summary>
        /// The line renderer.
        /// </summary>
        [SerializeField]
        [Tooltip("The line renderer")]
        private XRLineRenderer _lineRenderer = null;
        /// <summary>
        /// Duration of the explosion animation (in seconds).
        /// </summary>
        [SerializeField]
        [Tooltip("Duration of the animation (in seconds).")]
        private float _explosionDuration = 5.0f;
        /// <summary>
        /// Duration of the alpha animation (in seconds).
        /// </summary>
        [SerializeField]
        [Tooltip("Duration of the animation (in seconds).")]
        private float _alphaDuration = 1.0f;
        
        private float _startTime = 0.0f;

        private void Reset()
        {
            _lineRenderer = GetComponent<XRLineRenderer>();
        }

        private IEnumerator Animate()
        {
            _startTime = Time.time;
            while (Time.time - _startTime < _explosionDuration + _alphaDuration)
            {
                Color color = _lineRenderer.colorGradient.colorKeys[1].color;
                if (Time.time - _startTime < _explosionDuration)
                {
                    _lineRenderer.colorGradient.SetKeys(
                    new GradientColorKey[]
                    {
                        new GradientColorKey(Color.red, 0.0f),
                        new GradientColorKey(Color.red, 1.0f)
                    },
                    new GradientAlphaKey[]
                    {
                        new GradientAlphaKey(1.0f, 0.0f),
                        new GradientAlphaKey(0.0f, (Time.time - _startTime) / _explosionDuration),
                        new GradientAlphaKey(0.0f, 1.0f),
                    });
                    Debug.Log(string.Format("{0} {1}", _lineRenderer.colorGradient.alphaKeys[1].alpha, _lineRenderer.colorGradient.alphaKeys[1].time));
                }
                if (Time.time - _startTime < _explosionDuration + _alphaDuration)
                {
                    _lineRenderer.colorGradient.SetKeys(
                    new GradientColorKey[] {
                        new GradientColorKey(Color.red, 0.0f),
                        new GradientColorKey(Color.red, 1.0f)
                    },
                    new GradientAlphaKey[]
                    {
                        new GradientAlphaKey(1.0f, 0.0f),
                        new GradientAlphaKey((Time.time - _startTime) / (_explosionDuration + _alphaDuration), 1.0f),
                    });
                }
                yield return new WaitForSeconds(0.5f);
            }
        }

        public void StartAnimation()
        {
            StartCoroutine(Animate());
        }
    }
}

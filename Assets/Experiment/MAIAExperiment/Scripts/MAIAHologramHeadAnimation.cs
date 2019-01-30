using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAIAHologramHeadAnimation : MonoBehaviour
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
    private float _explosionDuration = 1.0f;

    public float explosionDuration
    {
        get
        {
            return _explosionDuration;
        }
    }
    /// <summary>
    /// The animation curve of the explosion.
    /// </summary>
    [SerializeField]
    [Tooltip("The animation curve of the explosion.")]
    private AnimationCurve _animationCurve = null;

    private float _startTime = 0.0f;

    private bool _animate;

    private void Reset()
    {
        _lineRenderer = GetComponent<XRLineRenderer>();
    }

    public void StartAnimation()
    {
        _animate = true;
        _startTime = Time.time;
    }

    private void Update()
    {
        if (_animate)
        {
            Color color = _lineRenderer.colorGradient.colorKeys[1].color;
            float time = Time.time;
            float t = (time - _startTime) / _explosionDuration;
            // We evaluate the value of the curve at the time of the explosion.
            float curveValue = _animationCurve.Evaluate(t);
            _lineRenderer.colorGradient.SetKeys(
                _lineRenderer.colorGradient.colorKeys,
            new GradientAlphaKey[]
            {
                        new GradientAlphaKey(0.0f, (1 - curveValue) - 0.01f),
                        new GradientAlphaKey(curveValue, 1 - curveValue),
                        new GradientAlphaKey(1.0f, 1.0f)
            });
            _lineRenderer.UpdateAll();
            // animate becomes false is the time is up.
            _animate = t < 1.0f;
        }
    }

    /// <summary>
    /// Clears the line.
    /// </summary>
    public void Clear()
    {
        _animate = false;
        _lineRenderer.colorGradient.SetKeys(
            _lineRenderer.colorGradient.colorKeys,
            new GradientAlphaKey[]
            {
                        new GradientAlphaKey(0.0f, 0.0f),
                        new GradientAlphaKey(0.0f, 1.0f),
            });
        _lineRenderer.UpdateAll();
    }
}

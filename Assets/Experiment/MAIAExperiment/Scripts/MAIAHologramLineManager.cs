using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAIAHologramLineManager : MonoBehaviour
{
    /// <summary>
    /// A list of anchor points the lines will be connected to.
    /// </summary>
    [SerializeField]
    [Tooltip("A list of anchor points the lines will be connected to.")]
    private Transform[] _anchorPoints = null;
    /// <summary>
    /// The origin point of all the lines.
    /// </summary>
    [SerializeField]
    [Tooltip("The origin point of all the lines.")]
    private Transform _originPoint = null;
    /// <summary>
    /// Prefab of the line renderer used to create the lines.
    /// </summary>
    [SerializeField]
    [Tooltip("Prefab of the line renderer used to create the lines.")]
    private XRLineRenderer _lineRendererPrefab = null;
    /// <summary>
    /// The number of points in each line. (min=2, max=50)
    /// </summary>
    [Range(2, 50)]
    [SerializeField]
    [Tooltip("The number of points in each line. (min=2, max=50)")]
    private int _numberOfPoints = 2;

    private XRLineRenderer[] _lines;
    private Vector3[] _previousPositions;

    public void Init()
    {
        int size = _anchorPoints.Length;
        _lines = new XRLineRenderer[size];
        _previousPositions = new Vector3[size];
        for (int i = 0; i < size; i++)
        {
            XRLineRenderer line = Instantiate(_lineRendererPrefab, transform);
            line.transform.localPosition = Vector3.zero;
            line.transform.localRotation = Quaternion.identity;
            SetPositions(line, _originPoint.position, _anchorPoints[i].position);
            _lines[i] = line;
            _previousPositions[i] = _anchorPoints[i].position;
        }
    }

    private void SetPositions(XRLineRenderer line, Vector3 originPoint, Vector3 anchorPoint)
    {
        Vector3 transformedOrigin = transform.InverseTransformPoint(originPoint);
        Vector3 transformedAnchor = transform.InverseTransformPoint(anchorPoint);
        Vector3[] points = new Vector3[_numberOfPoints];
        for (int i = 0; i < _numberOfPoints; i++)
        {
            points[i] = Vector3.Lerp(transformedOrigin, transformedAnchor, i / (float)(_numberOfPoints - 1));
        }
        line.SetPositions(points);
    }

    private void Update()
    {
        for (int i = 0; i < _lines.Length; i++)
        {
            if (_anchorPoints[i].position != _previousPositions[i])
            {
                SetPositions(_lines[i], _originPoint.position, _anchorPoints[i].position);
                _previousPositions[i] = _anchorPoints[i].position;
            }
        }
    }
}

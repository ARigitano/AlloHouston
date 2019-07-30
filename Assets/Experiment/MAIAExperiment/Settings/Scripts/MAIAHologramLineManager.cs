using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class MAIAHologramLineManager : MonoBehaviour
    {
        private MAIAHologramDiagram[] _diagrams;
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

        public Transform originPoint
        {
            get
            {
                return _originPoint;
            }
        }

        public void Init(MAIAHologramDiagram[] diagrams)
        {
            _diagrams = diagrams;
            int size = _diagrams.Length;
            _lines = new XRLineRenderer[size];
            _previousPositions = new Vector3[size];
            for (int i = 0; i < size; i++)
            {
                XRLineRenderer line = Instantiate(_lineRendererPrefab, transform);
                line.transform.localPosition = Vector3.zero;
                line.transform.localRotation = Quaternion.identity;
                SetPositions(line, _originPoint.position, _diagrams[i].anchorPoint.position);
                _lines[i] = line;
                _previousPositions[i] = _diagrams[i].anchorPoint.position;
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

        private void LateUpdate()
        {
            if (gameObject.activeInHierarchy && _lines != null && _diagrams != null)
            {
                for (int i = 0; i < _lines.Length && i < _diagrams.Length; i++)
                {
                    MAIAHologramDiagram diagram = _diagrams[i];
                    if (diagram.anchorPoint.position != _previousPositions[i])
                    {
                        SetPositions(_lines[i], _originPoint.position, diagram.anchorPoint.position);
                        _previousPositions[i] = diagram.anchorPoint.position;
                        _lines[i].gameObject.SetActive(diagram.displayLine);
                    }
                }
            }
        }
    }
}
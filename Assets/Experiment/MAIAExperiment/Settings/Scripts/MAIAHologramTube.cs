using CRI.HelloHouston.WindowTemplate;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    /// <summary>
    /// An hologram for the particle physics experiment.
    /// </summary>
    public class MAIAHologramTube : XPHologramElement
    {
        private class HologramSpline
        {
            public BezierSpline spline;
            public Particle particle;
            public Vector3 vDir;

            public HologramSpline(BezierSpline spline, Particle particle, Vector3 vDir)
            {
                this.spline = spline;
                this.particle = particle;
                this.vDir = vDir;
            }
        }
        /// <summary>
        /// The manager of the experiment.
        /// </summary>
        [Tooltip("The manager of the experiment.")]
        private MAIAManager _maiaManager = null;
        [Header("Particle Prefabs")]
        /// <summary>
        /// Prefab of the head of a particle line.
        /// </summary>
        [SerializeField]
        [Tooltip("Prefab of the head of a particle line..")]
        private GameObject _headPrefab = null;
        /// <summary>
        /// Prefab of the line of a particle.
        /// </summary>
        [SerializeField]
        [Tooltip("Prefab of the line of a particle.")]
        private XRLineRenderer _lineRendererPrefab = null;
        /// <summary>
        /// Prefab of the spark effect of the particle generation.
        /// </summary>
        [SerializeField]
        [Tooltip("Prefab of the spark effect of the particle generation.")]
        private MAIAHologramSparkAnimation _sparkPrefab = null;
        /// <summary>
        /// Spline prefab.
        /// </summary>
        [SerializeField]
        [Tooltip("Spline prefab.")]
        private BezierSpline _particleSplinePrefab = null;

        [Header("Curve Parameters")]
        /// <summary>
        /// Number of points in the lines.
        /// </summary>
        [Tooltip("Number of points in the lines.")]
        [SerializeField]
        private int _numberOfPoints = 20;
        /// <summary>
        /// Amplitude for the bezier curves curvature.
        /// </summary>
        [SerializeField]
        [Tooltip("Amplitude for the bezier curves curvature.")]
        private float _amplitudeA = 0.15f;
        /// <summary>
        /// Amplitude for the bezier curves curvature.
        /// </summary>
        [SerializeField]
        [Tooltip("Amplitude for the bezier curves curvature.")]
        private float _amplitudeB = 0.15f;
        [SerializeField]
        [Tooltip("Angle for shaping the bezier curves of the particle lines.")]
        private float _factorMin = 0.2f;
        [SerializeField]
        [Tooltip("Angle for shaping the bezier curves of the particle lines.")]
        private float _factorMax = 0.2f;
        /// <summary>
        /// Angle for shaping the bezier curves of the particle lines.
        /// </summary>
        /// 
        private float _theta = 0f;
        /// <summary>
        /// Angle for shaping the bezier curves of the particle lines.
        /// </summary>
        private float _phi = 0f;
        /// <summary>
        /// Array of particle splines.
        /// </summary>
        private HologramSpline[] _particleSplineArray;

        private List<XRLineRenderer> _particleHeads = new List<XRLineRenderer>();
        /// <summary>
        /// Array of the cylinders' mesh filters.
        /// </summary>
        [SerializeField]
        [Tooltip("Array of the cylinders' mesh filters.")]
        private MeshFilter[] _cylArray = null;
        /// <summary>
        /// Array of the cylinders' radiuses.
        /// </summary>
        private float[] _rMaxCylArray;
        /// <summary>
        /// Array of the cylinders' lengths.
        /// </summary>
        private float[] _lMaxCylArray;

        [Header("Animation Starting Points")]
        /// <summary>
        /// Transform of a starting point of the spark effect.
        /// </summary>
        [SerializeField]
        [Tooltip("Transform of a starting point of the spark effect.")]
        private Transform _start1 = null;
        /// <summary>
        /// Transform of a starting point of the spark effect.
        /// </summary>
        [SerializeField]
        [Tooltip("Transform of a starting point of the spark effect.")]
        private Transform _start2 = null;
        /// <summary>
        /// Transform used as a parent for the particles.
        /// </summary>
        [SerializeField]
        [Tooltip("Transform used as a parent for the particles.")]
        private Transform _particleTransform = null;
        [SerializeField]
        [Tooltip("Animation of the hologram tube.")]
        private AnimationElement _animationElement = null;

        private bool _splinesDisplayed = false;

        private bool _animationPlayedOnce = false;

        private System.Random _rand;

        private void OnDisable()
        {
            _animationPlayedOnce = false;
        }

        /// <summary>
        /// Animates the particle reaction hologram.
        /// </summary>
        /// <param name="particles">The combination of particles.</param>
        public void CreateSplines(List<Particle> particles)
        {
            _particleSplineArray = new HologramSpline[particles.Count];
            for (int i = 0; i < particles.Count; i++)
            {
                HologramSpline particleSpline = CreateSpline(particles[i]);
                particleSpline.spline.gameObject.name = particleSpline.particle.particleName + i;
                _particleSplineArray[i] = particleSpline;
            }
        }

        /// <summary>
        /// Creates a line for each particle to be displayed in the hologram.
        /// </summary>
        /// <param name="i">The index of the particle inside the combination.</param>
        /// <param name="particle">The particle which line is being generated.</param>
        /// <returns>The end position of the line.</returns>
        private HologramSpline CreateSpline(Particle particle)
        {
            //Setting rotation angles.
            if (particle.straight)
            {
                _theta = 0f;
                _phi = 0f;
            }
            else
            {
                _theta = (float)_rand.Range(Mathf.PI / 6f, Mathf.PI / 3f);
                _phi = (float)_rand.Range(Mathf.PI / 4f, Mathf.PI / 3f);

                if (particle.negative)
                {
                    _theta = -1f * _theta;
                    _phi = -1f * _phi;
                }
            }
            //Generating the spline.
            var spline = Instantiate(_particleSplinePrefab, Vector3.zero, Quaternion.identity, _particleTransform);
            spline.transform.localPosition = Vector3.zero;
            spline.transform.localRotation = Quaternion.identity;
            spline.Reset();
            //Setting the instantiating boundaries.
            int destination = particle.destination - 1;
            float rMax = _rMaxCylArray[destination];
            float lMax = _lMaxCylArray[destination];
            float rMin = destination > 0 ? _rMaxCylArray[destination - 1] : 0.0f;
            float lMin = destination > 0 ? _lMaxCylArray[destination - 1] : 0.0f;
            //Setting the coordinates of the spline points.
            Vector3 vDir = Vector3.zero;


            float factorTmpMin = particle.extremity ? 0.0f : _factorMin;
            float factorTmpMax = particle.extremity ? 0.0f : _factorMax;
            float lMaxFactor = lMax - factorTmpMax * (lMax - lMin);
            float lMinFactor = lMin + factorTmpMin * (lMax - lMin);
            float rMaxFactor = rMax - factorTmpMax * (rMax - rMin);
            float rMinFactor = rMin + factorTmpMin * (rMax - rMin);

            bool res = false;

            do
            {
                float r = particle.extremity ? rMax : (float)_rand.Range(_rMaxCylArray[0], rMaxFactor);
                float alpha = (float)_rand.Range(0, Mathf.PI * 2f);

                spline.points[3].x = r * Mathf.Cos(alpha);
                spline.points[3].y = (float)_rand.Range(-lMaxFactor, lMaxFactor);
                spline.points[3].z = r * Mathf.Sin(alpha);
                 
                spline.points[0] = Vector3.zero;

                float matrixRotation1 = (1.0f / spline.points[3].magnitude) * _amplitudeA;
                float matrixRotation2 = -(1.0f / spline.points[3].magnitude) * _amplitudeB;

                spline.points[1].x = matrixRotation1 * (spline.points[3].x * Mathf.Cos(_theta) + spline.points[3].z * Mathf.Sin(_theta));
                spline.points[1].y = matrixRotation1 * spline.points[3].y;
                spline.points[1].z = matrixRotation1 * ((-1f * spline.points[3].x) * Mathf.Sin(_theta) + spline.points[3].z * Mathf.Cos(_theta));

                vDir.x = matrixRotation2 * (spline.points[3].x * Mathf.Cos(-1f * _phi) + spline.points[3].z * Mathf.Sin(-1f * _phi));
                vDir.y = matrixRotation2 * spline.points[3].y;
                vDir.z = matrixRotation2 * ((-1f * spline.points[3].x) * Mathf.Sin(-1f * _phi) + spline.points[3].z * Mathf.Cos(-1f * _phi));

                spline.points[2].x = spline.points[3].x + vDir.x;
                spline.points[2].y = spline.points[3].y + vDir.y;
                spline.points[2].z = spline.points[3].z + vDir.z;

                res = (Mathf.Abs(spline.points[3].y) <= lMinFactor) && r <= rMinFactor;
            } while (res);

            return new HologramSpline(spline, particle, vDir);
        }

        private void DisplaySpline(HologramSpline hologramSpline)
        {
            Particle particle = hologramSpline.particle;
            BezierSpline spline = hologramSpline.spline;
            Vector3 vDir = hologramSpline.vDir;

            //Displaying the lines.
            if (particle.line)
            {     
                var line = Instantiate(_lineRendererPrefab, Vector3.zero, Quaternion.identity, spline.transform);
                line.transform.localPosition = Vector3.zero;
                line.transform.localRotation = Quaternion.identity;
                Vector3[] points = new Vector3[_numberOfPoints + 1];
                for (int i = 0; i <= _numberOfPoints; i++)
                    points[i] = spline.transform.InverseTransformPoint(spline.GetPoint(i / (float)_numberOfPoints));
                line.SetPositions(points);
                line.colorGradient.SetKeys(
                    new GradientColorKey[]
                    {
                        new GradientColorKey(Color.white, 0.0f),
                        new GradientColorKey(particle.endColor, 0.5f),
                    },
                    new GradientAlphaKey[]
                    {
                        new GradientAlphaKey(0.0f, 0.0f),
                        new GradientAlphaKey(0.0f, 1.0f),
                    });
                line.materials[1].SetColor("Color Tint", particle.endColor);
            }
            //Displaying the heads.
            if (particle.head)
            {
                
                GameObject lineHead = (GameObject)Instantiate(_headPrefab, Vector3.zero, Quaternion.identity, spline.transform);
                lineHead.GetComponentInChildren<XRLineRenderer>().colorGradient.SetKeys(
                    new GradientColorKey[]
                    {
                        new GradientColorKey(Color.white, 0.0f),
                        new GradientColorKey(particle.endColor, 0.2f),
                    },
                    new GradientAlphaKey[]
                    {
                        new GradientAlphaKey(0.0f, 0.0f),
                        new GradientAlphaKey(0.0f, 1.0f),
                    });
                lineHead.GetComponentInChildren<XRLineRenderer>().materials[1].SetColor("Color Tint", particle.endColor);
                lineHead.transform.position = spline.GetPoint(1.0f);
                lineHead.transform.localRotation = Quaternion.FromToRotation(lineHead.transform.up, vDir);
                _particleHeads.Add(lineHead.GetComponent<XRLineRenderer>());
            }
        }

        public void DisplayAllSplines()
        {
            if (!_splinesDisplayed)
            {
                foreach (HologramSpline hologramSpline in _particleSplineArray)
                {
                    DisplaySpline(hologramSpline);
                }
                _splinesDisplayed = true;
            }
        }

        private IEnumerator Animate()
        {
            _animationPlayedOnce = true;
            var lines = GetComponentsInChildren<MAIAHologramLineAnimation>();
            var heads = GetComponentsInChildren<MAIAHologramHeadAnimation>();
            foreach (var line in lines)
                line.Clear();
            foreach (var head in heads)
                head.Clear();
            var spark1 = Instantiate(_sparkPrefab, transform);
            spark1.Init(_start1.transform, transform);
            var spark2 = Instantiate(_sparkPrefab, transform);
            spark2.Init(_start2, transform);
            yield return new WaitForSeconds(_sparkPrefab.duration);
            foreach (var line in lines)
                line.StartAnimation();
            yield return new WaitForSeconds(_lineRendererPrefab.GetComponent<MAIAHologramLineAnimation>().explosionDuration * 0.7f);
            foreach (var head in heads)
                head.StartAnimation();
        }

        public void StartAnimation()
        {
            if (_splinesDisplayed && gameObject.activeInHierarchy)
            {
                StopAllCoroutines();
                StartCoroutine(Animate());
            }
        }

        private void DisableObject()
        {
            gameObject.SetActive(false);
            _particleTransform.gameObject.SetActive(false);
        }

        private void EnableObject()
        {
            gameObject.SetActive(true);
            _particleTransform.gameObject.SetActive(true);
        }

        public override void Show()
        {
            EnableObject();
            if (!_animationElement.visible)
                _animationElement.Show();
        }

        public override void Hide()
        {
            StopAllCoroutines();
            if (_animationElement.visible)
            {
                _animationElement.Hide();
                Invoke("DisableObject", _animationElement.postHideDelay);
            }
            else
                DisableObject();
        }

        private void Init(MAIAManager synchronizer)
        {
            _maiaManager = synchronizer;
            _lMaxCylArray = new float[_cylArray.Length];
            _rMaxCylArray = new float[_cylArray.Length];
            for (int i = 0; i < _cylArray.Length; i++)
            {
                _lMaxCylArray[i] = _cylArray[i].mesh.bounds.extents.y * _cylArray[i].transform.localScale.y;
                _rMaxCylArray[i] = _cylArray[i].mesh.bounds.extents.x * _cylArray[i].transform.localScale.x;
            }
        }

        public override void OnInit(XPManager manager, int randomSeed)
        {
            base.OnInit(manager, randomSeed);
            Init((MAIAManager)manager);
            _rand = new System.Random(randomSeed);
            DisableObject();
        }

        /// <summary>
        /// Effect when the experiment is activated the first time.
        /// </summary>
        public override void OnShow(int currentStep)
        {
            base.OnActivation();
            CreateSplines(_maiaManager.generatedParticles);
        }

        public override void Dismiss()
        {
            if (isActiveAndEnabled)
                StartCoroutine(DelayedDismiss());
            else
                base.Dismiss();
        }

        public IEnumerator DelayedDismiss()
        {
            if (_animationElement != null && _animationElement.visible)
            {
                _animationElement.Hide();
                yield return new WaitForSeconds(_animationElement.postHideDelay);
            }
            base. Dismiss();
        }

        public void OnVisibleStay(Camera camera)
        {
            if (_splinesDisplayed && !_animationPlayedOnce)
            {
                StartAnimation();
            }
        }
    }
}

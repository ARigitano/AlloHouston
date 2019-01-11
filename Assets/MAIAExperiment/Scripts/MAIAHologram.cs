using CRI.HelloHouston.Experience;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    /// <summary>
    /// An hologram for the particle physics experiment.
    /// </summary>
    public class MAIAHologram : XPHologramElement
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
        /// The synchronizer of the experiment.
        /// </summary>
        private MAIAManager _manager;
        /// <summary>
        /// Folder path for the particle scriptable objects
        /// </summary>
        private const string _path = "Particle";
        /// <summary>
        /// Prefab of the head of a particle line.
        /// </summary>
        [SerializeField]
        private GameObject _headPrefab = null;
        /// <summary>
        /// Angle for shaping the bezier curves of the particle lines.
        /// </summary>
        private float _theta = 0f;
        /// <summary>
        /// Angle for shaping the bezier curves of the particle lines.
        /// </summary>
        private float _phi = 0f;
        /// <summary>
        /// Amplitude for the bezier curves curvature.
        /// </summary>
        [SerializeField]
        private float _amplitudeA = 0.15f;
        /// <summary>
        /// Amplitude for the bezier curves curvature.
        /// </summary>
        [SerializeField]
        private float _amplitudeB = 0.15f;
        /// <summary>
        /// Spline prefab.
        /// </summary>
        [SerializeField]
        private BezierSpline _particleSplinePrefab = null;
        /// <summary>
        /// Array of particle splines.
        /// </summary>
        private HologramSpline[] _particleSplineArray;
        /// <summary>
        /// Particle list.
        /// </summary>
        private List<Particle> _particleList;
        /// <summary>
        /// Array of the cylinders' mesh filters.
        /// </summary>
        [SerializeField]
        [Tooltip("Array of the cylinders' mesh filters.")]
        private MeshFilter[] _cylArray;

        [SerializeField]
        private float _factor = 0.2f;
        /// <summary>
        /// Array of the cylinders' radiuses.
        /// </summary>
        private float[] _rMaxCylArray;
        /// <summary>
        /// Array of the cylinders' lengths.
        /// </summary>
        private float[] _lMaxCylArray;

        /// <summary>
        /// Animates the particle reaction hologram.
        /// </summary>
        /// <param name="particles">The combination of particles.</param>
        public void AnimHologram(List<Particle> particles)
        {
            _particleSplineArray = new HologramSpline[particles.Count];
            for (int i = 0; i < particles.Count; i++)
            {
                _particleSplineArray[i] = CreateSpline(particles[i], i);
            }
        }

        /// <summary>
        /// Creates a line for each particle to be displayed in the hologram.
        /// </summary>
        /// <param name="i">The index of the particle inside the combination.</param>
        /// <param name="particle">The particle which line is being generated.</param>
        /// <returns>The end position of the line.</returns>
        private HologramSpline CreateSpline(Particle particle, int index)
        {
            //Setting rotation angles.
            if (particle.straight)
            {
                _theta = 0f;
                _phi = 0f;
            }
            else
            {
                _theta = Random.Range(Mathf.PI / 6f, Mathf.PI / 3f);
                _phi = Random.Range(Mathf.PI / 4f, Mathf.PI / 3f);

                if (particle.negative)
                {
                    _theta = -1f * _theta;
                    _phi = -1f * _phi;
                }
            }
            //Generating the spline.
            var spline = Instantiate(_particleSplinePrefab, Vector3.zero, Quaternion.identity, transform);
            spline.transform.localPosition = Vector3.zero;
            spline.transform.localRotation = Quaternion.identity;
            spline.Reset();
            //Setting the instantiating boundaries.
            int destination = particle.destination - 1;
            float rMax = _rMaxCylArray[destination];
            float lMax = _lMaxCylArray[destination];
            float rMaxPrevious = destination > 0 ? _rMaxCylArray[destination - 1] : 0.0f;
            float lMaxPrevious = destination > 0 ? _lMaxCylArray[destination - 1] : 0.0f;
            //Setting the coordinates of the spline points.
            Vector3 vDir = Vector3.zero;
            float factorTmp;
            do
            {
                factorTmp = _factor;
                float r = Random.Range(1.5f * factorTmp, rMax * (1f - factorTmp));
                float alpha = Random.Range(0, Mathf.PI * 2f);

                if (particle.extremity)
                {
                    factorTmp = 0f;
                    r = rMax;
                }

                spline.points[3].x = r * Mathf.Cos(alpha);
                spline.points[3].y = lMax * (1f - factorTmp) * Random.Range(-lMax, lMax);
                spline.points[3].z = r * Mathf.Sin(alpha);

                spline.gameObject.name = particle.particleName + index;

                spline.points[0] = Vector3.zero;

                float matrixRotation1 = (1 / spline.points[3].magnitude) * _amplitudeA;
                float matrixRotation2 = (-1 / spline.points[3].magnitude) * _amplitudeB;

                spline.points[1].x = matrixRotation1 * (spline.points[3].x * Mathf.Cos(_theta) + spline.points[3].z * Mathf.Sin(_theta));
                spline.points[1].y = matrixRotation1 * spline.points[3].y;
                spline.points[1].z = matrixRotation1 * ((-1f * spline.points[3].x) * Mathf.Sin(_theta) + spline.points[3].z * Mathf.Cos(_theta));

                vDir.x = matrixRotation2 * (spline.points[3].x * Mathf.Cos(-1f * _phi) + spline.points[3].z * Mathf.Sin(-1f * _phi));
                vDir.y = matrixRotation2 * spline.points[3].y;
                vDir.z = matrixRotation2 * ((-1f * spline.points[3].x) * Mathf.Sin(-1f * _phi) + spline.points[3].z * Mathf.Cos(-1f * _phi));

                spline.points[2].x = spline.points[3].x + vDir.x;
                spline.points[2].y = spline.points[3].y + vDir.y;
                spline.points[2].z = spline.points[3].z + vDir.z;

            } while ((Mathf.Abs(spline.points[3].y) <= lMaxPrevious * (1f + factorTmp)) && Mathf.Sqrt(Mathf.Pow(spline.points[3].x, 2) + Mathf.Pow(spline.points[3].z, 2)) <= rMaxPrevious * (1f + factorTmp));

            return new HologramSpline(spline, particle, vDir);
        }

        private void PopulateLine(HologramSpline hologramSpline)
        {
            Particle particle = hologramSpline.particle;
            BezierSpline spline = hologramSpline.spline;
            Vector3 vDir = hologramSpline.vDir;
            //Displaying the lines.
            if (particle.line)
            {
                spline.GetComponent<SplineDecorator>().endColor = particle.endColor;
                spline.GetComponent<SplineDecorator>().Populate();
            }
            //Displaying the heads.
            if (particle.head)
            {
                GameObject lineHead = (GameObject)Instantiate(_headPrefab, Vector3.zero, Quaternion.identity, spline.transform);
                lineHead.GetComponent<Renderer>().material.SetColor("_Color", particle.endColor);
                lineHead.transform.localPosition = spline.points[3];
                lineHead.transform.localRotation = Quaternion.FromToRotation(lineHead.transform.forward, vDir);
            }
        }

        public void DisplaySplines()
        {
            foreach (HologramSpline hologramSpline in _particleSplineArray)
            {
                PopulateLine(hologramSpline);
            }
        }

        private void Init(MAIAManager synchronizer)
        {
            _manager = synchronizer;
            _lMaxCylArray = new float[_cylArray.Length];
            _rMaxCylArray = new float[_cylArray.Length];
            for (int i = 0; i < _cylArray.Length; i++)
            {
                _lMaxCylArray[i] = _cylArray[i].mesh.bounds.extents.y * _cylArray[i].transform.localScale.y;
                _rMaxCylArray[i] = _cylArray[i].mesh.bounds.extents.x * _cylArray[i].transform.localScale.x;
            }
        }
        /// <summary>
        /// Effect when the experiment is correctly resolved.
        /// </summary>
        public override void OnSuccess()
        {
            Debug.Log(name + "Resolved");
        }
        /// <summary>
        /// Effect when the experiment is failed.
        /// </summary>
        public override void OnFailure()
        {
            Debug.Log(name + "Failed");
        }
        /// <summary>
        /// Effect when the experiment is activated the first time.
        /// </summary>
        public override void OnActivation(XPManager manager)
        {
            Debug.Log(name + "Activated");
            Init((MAIAManager)manager);
            gameObject.SetActive(true);
        }
        /// <summary>
        /// Effect when the experiment is paused.
        /// </summary>
        public override void OnShow()
        {
            Debug.Log(name + "Paused");
            gameObject.SetActive(true);
        }
        /// <summary>
        /// Effect when the experiment is unpaused.
        /// </summary>
        public override void OnHide()
        {
            Debug.Log(name + "Unpaused");
            gameObject.SetActive(false);
        }
    }
}

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
        /// <summary>
        /// The synchronizer of the experiment.
        /// </summary>
        private MAIASynchronizer _synchronizer;
        /// <summary>
        /// Folder path for the particle scriptable objects
        /// </summary>
        private const string _path = "Particle";
        /// <summary>
        /// Lines to be displayed by the hologram.
        /// </summary>
        private List<BezierSpline> _lines = new List<BezierSpline>();
        //private GameObject[] lines;
        /// <summary>
        /// End point of the generated lines.
        /// </summary>
        private GameObject[] _pointsB = null;
        /// <summary>
        /// Prefab of the hologram.
        /// </summary>
        [SerializeField]
        private GameObject _hologram = null;
        /// <summary>
        /// Prefab of the head of a particle line.
        /// </summary>
        [SerializeField]
        private GameObject _headPrefab = null;
        /// <summary>
        /// Prefab of the head of a particle line.
        /// </summary>
        [SerializeField]
        private GameObject _headQuarkPrefab = null;
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
        /// Last point of spline prefab.
        /// </summary>
        [SerializeField]
        private GameObject _destination = null;
        /// <summary>
        /// First cylinder.
        /// </summary>
        [SerializeField]
        [Tooltip("First cylinder.")]
        private MeshFilter _cyl1 = null;
        /// <summary>
        /// Second cylinder.
        /// </summary>
        [SerializeField]
        [Tooltip("Second cylider.")]
        private MeshFilter _cyl2 = null;
        /// <summary>
        /// Third cylinder.
        /// </summary>
        [SerializeField]
        [Tooltip("Third cylinder.")]
        private MeshFilter _cyl3 = null;
        /// <summary>
        /// Fourth cylinder.
        /// </summary>
        [SerializeField]
        [Tooltip("Fourth cylinder.")]
        private MeshFilter _cyl4 = null;
        /// <summary>
        /// Particle reactor zones margin.
        /// </summary>
        [SerializeField]
        private float _factor = 0.2f;
        /// <summary>
        /// Maximum radius of first zone.
        /// </summary>
        private float _rMaxCyl1 = 0.125f;
        /// <summary>
        /// Maximum half width of first zone.
        /// </summary>
        private float _lMaxCyl1 = 0.25f;
        /// <summary>
        /// Maximum radius of second zone.
        /// </summary>
        private float _rMaxCyl2 = 0.25f;
        /// <summary>
        /// Maximum half width of second zone.
        /// </summary>
        private float _lMaxCyl2 = 0.5f;
        /// <summary>
        /// Maximum radius of third zone.
        /// </summary>
        private float _rMaxCyl3 = 0.375f;
        /// <summary>
        /// Maximum half width of third zone.
        /// </summary>
        private float _lMaxCyl3 = 0.75f;
        /// <summary>
        /// Maximum radius of fourth zone.
        /// </summary>
        private float _rMaxCyl4 = 1f;
        /// <summary>
        /// Maximum half width of fourth zone.
        /// </summary>
        private float _lMaxCyl4 = 1f;

        /// <summary>
        /// Animates the particle reaction hologram.
        /// </summary>
        /// <param name="particles">The combination of particles.</param>
        public void AnimHologram(List<Particle> particles)
        {
            Debug.Log("AnimHologram");
            _rMaxCyl1 = _cyl1.mesh.bounds.extents.x * _cyl1.transform.localScale.x;
            _lMaxCyl1 = _cyl1.mesh.bounds.extents.y * _cyl1.transform.localScale.y;
            _rMaxCyl2 = _cyl2.mesh.bounds.extents.x * _cyl2.transform.localScale.x;
            _lMaxCyl2 = _cyl2.mesh.bounds.extents.y * _cyl2.transform.localScale.y;
            _rMaxCyl3 = _cyl3.mesh.bounds.extents.x * _cyl3.transform.localScale.x;
            _lMaxCyl3 = _cyl3.mesh.bounds.extents.y * _cyl3.transform.localScale.y;
            _rMaxCyl4 = _cyl4.mesh.bounds.extents.x * _cyl4.transform.localScale.x;
            _lMaxCyl4 = _cyl4.mesh.bounds.extents.y * _cyl4.transform.localScale.y;
            _pointsB = new GameObject[particles.Count];
            for (int i = 0; i < particles.Count; i++)
            {
                Vector3 headPosition = CreateLine(i, particles[i]);
            }
        }

        /// <summary>
        /// Creates a line for each particle to be displayed in the hologram.
        /// </summary>
        /// <param name="i">The index of the particle inside the combination.</param>
        /// <param name="particle">The particle which line is being generated.</param>
        /// <returns>The end position of the line.</returns>
        private Vector3 CreateLine(int i, Particle particle)
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
            _lines.Add(spline);
            spline.Reset();

            _pointsB[i] = (GameObject)Instantiate(_destination, Vector3.zero, Quaternion.identity);
            _pointsB[i].transform.parent = this.gameObject.transform;

            //Setting the instantiating boundaries.
            float rMax = 0f;
            float lMax = 0f;
            float rMaxPrevious = 0f;
            float lMaxPrevious = 0f;

            switch (particle.destination)
            {
                case 1:
                    rMax = _rMaxCyl1;
                    lMax = _lMaxCyl1;
                    rMaxPrevious = 0f;
                    lMaxPrevious = 0f;
                    break;
                case 2:
                    rMax = _rMaxCyl2;
                    lMax = _lMaxCyl2;
                    rMaxPrevious = _rMaxCyl1;
                    lMaxPrevious = _lMaxCyl1;
                    break;
                case 3:
                    rMax = _rMaxCyl3;
                    lMax = _lMaxCyl3;
                    rMaxPrevious = _rMaxCyl2;
                    lMaxPrevious = _lMaxCyl2;
                    break;
                case 4:
                    rMax = _rMaxCyl4;
                    lMax = _lMaxCyl4;
                    rMaxPrevious = _rMaxCyl3;
                    lMaxPrevious = _lMaxCyl3;
                    break;
                default:
                    break;
            }

            //Setting the coordinates of the spline points.
            Vector3 vDir = new Vector3(0f, 0f, 0f);

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
                //Mathf.Cos(Random.Range(-1f * Mathf.PI , Mathf.PI ));
                spline.points[3].z = r * Mathf.Sin(alpha);

                spline.gameObject.name = particle.particleName + i;

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

            //Displaying the lines.
            if (particle.line)
            {
                spline.GetComponent<SplineDecorator>().endColor = particle.endColor;
                spline.GetComponent<SplineDecorator>().Populate();
            }

            //Displaying the heads.
            if (particle.head)
            {
                GameObject lineHead = (GameObject)Instantiate(_headPrefab, Vector3.zero, Quaternion.identity, _lines[i].transform);
                lineHead.GetComponent<Renderer>().material.SetColor("_Color", particle.endColor);
                lineHead.transform.localPosition = spline.points[3];
                lineHead.transform.localRotation = Quaternion.FromToRotation(lineHead.transform.forward, vDir);
            }

            return spline.points[3];
        }

        public void Init(MAIASynchronizer synchronizer)
        {
            _synchronizer = synchronizer;
        }
        //TO DO
        /// <summary>
        /// Effect when the experiment is correctly resolved.
        /// </summary>
        public override void OnSuccess()
        {
            Debug.Log(name + "Resolved");
        }
        //TO DO
        /// <summary>
        /// Effect when the experiment is failed.
        /// </summary>
        public override void OnFailure()
        {
            Debug.Log(name + "Failed");
        }
        //TO DO
        /// <summary>
        /// Effect when the experiment is activated the first time.
        /// </summary>
        public override void OnActivation()
        {
            Debug.Log(name + "Activated");
            gameObject.SetActive(true);
        }
        //TO DO
        /// <summary>
        /// Effect when the experiment is paused.
        /// </summary>
        public override void OnShow()
        {
            Debug.Log(name + "Paused");
            gameObject.SetActive(true);
        }
        //TO DO
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

using CRI.HelloHouston.Experience;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.ParticlePhysics
{
    /// <summary>
    /// An hologram for the particle physics experiment.
    /// </summary>
    public class FakeHologram : XPElement
    {
        /// <summary>
        /// The synchronizer of the experiment.
        /// </summary>
        [SerializeField]
        private FakeSynchronizer _synchronizer;
        /// <summary>
        /// Folder path for the particle scriptable objects
        /// </summary>
        private static string _path = "Particle";
        /// <summary>
        /// Lines to be displayed by the hologram.
        /// </summary>
        [SerializeField]
        private List<GameObject> lines;
        //private GameObject[] lines;
        /// <summary>
        /// End point of the generated lines.
        /// </summary>
        [SerializeField]
        private GameObject[] pointsB;
        /// <summary>
        /// Prefab of the hologram.
        /// </summary>
        [SerializeField]
        private GameObject _hologram;
        [SerializeField]
        /// <summary>
        /// Prefab of the sphere that will be generated several times to constitute a particle line.
        /// </summary>
        private GameObject _sphere,
        /// <summary>
        /// Prefab of the head of a particle line.
        /// </summary>
                           _head,
        /// <summary>
        /// Prefab of the head of a particle line.
        /// </summary>
                           _headQuark;
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
        private float _amplitudeA = 0.15f;
        /// <summary>
        /// Amplitude for the bezier curves curvature.
        /// </summary>
        private float _amplitudeB = 0.15f;
        /// <summary>
        /// Spline prefab.
        /// </summary>
        [SerializeField]
        private GameObject _particleSpline,
        /// <summary>
        /// Last point of spline prefab.
        /// </summary>
                           _destination;
        /// <summary>
        /// Number of particles to generate.
        /// </summary>
        public int particleCount = 18;
        /// <summary>
        /// Particle reactor zones margin.
        /// </summary>
        [SerializeField]
        private float _factor = 0.1f;
        /// <summary>
        /// Maximum radius of first zone.
        /// </summary>
        [SerializeField]
        private float _rMaxCyl1 = 0.125f;
        /// <summary>
        /// Maximum half width of first zone.
        /// </summary>
        [SerializeField]
        private float _lMaxCyl1 = 0.25f;
        /// <summary>
        /// Maximum radius of second zone.
        /// </summary>
        [SerializeField]
        private float _rMaxCyl2 = 0.25f;
        /// <summary>
        /// Maximum half width of second zone.
        /// </summary>
        [SerializeField]
        private float _lMaxCyl2 = 0.5f;
        /// <summary>
        /// Maximum radius of third zone.
        /// </summary>
        [SerializeField]
        private float _rMaxCyl3 = 0.375f;
        /// <summary>
        /// Maximum half width of third zone.
        /// </summary>
        [SerializeField]
        private float _lMaxCyl3 = 0.75f;
        /// <summary>
        /// Maximum radius of fourth zone.
        /// </summary>
        [SerializeField]
        private float _rMaxCyl4 = 1f;
        /// <summary>
        /// Maximum half width of fourth zone.
        /// </summary>
        [SerializeField]
        private float _lMaxCyl4 = 1f;
 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="head"></param>
        public void DestroySpline(GameObject head)
        {
            //head.GetComponent<MeshRenderer>().material.color = Color.red;
            Debug.Log("problem");

            lines.Remove(head.transform.parent.gameObject);
            _synchronizer.SynchronizeScreens("NewParticle");
        }

        /// <summary>
        /// Animates the particle reaction hologram.
        /// </summary>
        /// <param name="particles">The combination of particles.</param>
        public void AnimHologram(Particle[] particles, int particleCount)
        {
            for(int i = 0; i<particleCount; i++)
            {
                    int randomParticle = Random.Range(0, particles.Length);
                    Vector3 headPosition = createLine(i, particles[randomParticle]);

                    /*if (particles[randomParticle].head)
                    {
                        if(particles[randomParticle].symbol=="q")
                        {
                         _head = _headQuark;
                        }

                        GameObject lineHead = (GameObject)Instantiate(_head, Vector3.zero, Quaternion.LookRotation(headPosition, Vector3.forward), lines[i].transform);
                        lineHead.GetComponent<MeshRenderer>().material = particles[randomParticle].debugMaterial;

                        lineHead.transform.localPosition = headPosition;
                    } */
            }
        }

        /// <summary>
        /// Creates a line for each particle to be displayed in the hologram.
        /// </summary>
        /// <param name="i">The index of the particle inside the combination.</param>
        /// <param name="particle">The particle which line is being generated.</param>
        /// <returns>The end position of the line.</returns>
        private Vector3 createLine(int i, Particle particle)
        {
            //Setting rotation angles.
            _theta = Random.Range(Mathf.PI / 6f, Mathf.PI / 3f);
            _phi = Random.Range(Mathf.PI / 6f, Mathf.PI / 3f);

            if(particle.negative)
            {
                _theta = -1f * _theta;
                _phi = -1f * _phi;
            }

            //Generating the spline.
            GameObject lineParticle = (GameObject)Instantiate(_particleSpline, Vector3.zero, Quaternion.identity, transform);
            lines.Add(lineParticle);
            lineParticle.transform.localPosition = Vector3.zero;
            lineParticle.transform.localRotation = Quaternion.identity;
            BezierSpline spline = lineParticle.GetComponent<BezierSpline>();
            spline.Reset();

            pointsB[i] = (GameObject)Instantiate(_destination, Vector3.zero, Quaternion.identity);
            pointsB[i].transform.parent = this.gameObject.transform;

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

            do
            {

                float r = Random.Range(0, rMax * (1f - 1f * _factor));
                float alpha = Random.Range(0, Mathf.PI * 2f);

                spline.points[3].x = r * Mathf.Cos(alpha);
                spline.points[3].y = lMax * (1f - 1f * _factor) * Mathf.Cos(Random.Range(1f*Mathf.PI/6f,  5f*Mathf.PI/6f));
                spline.points[3].z = r * Mathf.Sin(alpha);

                /*if (particle.symbol == "μ")
                 {
                     rMax = 0.493f;
                     lMax = 1f;
                     Debug.Log("");

                     spline.points[3].x = rMax * Mathf.Cos(alpha);
                     spline.points[3].y = lMax * Mathf.Cos(Random.Range(Mathf.PI, -1f * Mathf.PI));
                     spline.points[3].z = rMax * Mathf.Sin(alpha);
                 }*/


                lineParticle.name = particle.particleName + i;

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

                if (particle.particleName == "photon")
                {
                    Debug.Log("photon");

                    spline.points[1] = new Vector3(0f, 0f, 0f);
                    spline.points[2] = new Vector3(0f, 0f, 0f);
                    spline.points[3] = new Vector3(0f, spline.points[3].y, 0f);
                }
            } while ((Mathf.Abs(spline.points[3].y) <= lMaxPrevious * (1f + _factor)) && Mathf.Sqrt(Mathf.Pow(spline.points[3].x, 2) + Mathf.Pow(spline.points[3].z, 2)) <= rMaxPrevious * (1f + _factor));

            
            //Displaying the lines.
            if (particle.line)
            {
                _sphere.GetComponent<MeshRenderer>().material = particle.debugMaterial;
                lineParticle.AddComponent<SplineDecorator>();
                SplineDecorator decorator = lineParticle.GetComponent<SplineDecorator>();
                decorator.spline = spline;
                decorator.frequency = 75;
                decorator.items = new Transform[1];
                decorator.items[0] = _sphere.transform;
                decorator.Populate();
            }

            //Displaying the heads.
            if (particle.head)
            {
                GameObject lineHead = (GameObject)Instantiate(_head, Vector3.zero, Quaternion.identity, lines[i].transform);
                lineHead.GetComponent<MeshRenderer>().material = particle.debugMaterial;

                lineHead.transform.localPosition = spline.points[3];
                lineHead.transform.localRotation = Quaternion.FromToRotation(lineHead.transform.forward, vDir);
            }

            return spline.points[3];
        }
        //TO DO
        /// <summary>
        /// Effect when the experiment is correctly resolved.
        /// </summary>
        public override void OnResolved()
        {
            Debug.Log(name + "Resolved");
        }
        //TO DO
        /// <summary>
        /// Effect when the experiment is failed.
        /// </summary>
        public override void OnFailed()
        {
            Debug.Log(name + "Failed");
        }
        //TO DO
        /// <summary>
        /// Effect when the experiment is activated the first time.
        /// </summary>
        public override void OnActivated()
        {
            Debug.Log(name + "Activated");
            _hologram.GetComponent<MeshRenderer>().enabled = true;
        }
        //TO DO
        /// <summary>
        /// Effect when the experiment is paused.
        /// </summary>
        public override void OnPause()
        {
            Debug.Log(name + "Paused");
            _hologram.GetComponent<MeshRenderer>().enabled = false;
        }
        //TO DO
        /// <summary>
        /// Effect when the experiment is unpaused.
        /// </summary>
        public override void OnUnpause()
        {
            Debug.Log(name + "Unpaused");
            _hologram.GetComponent<MeshRenderer>().enabled = true;
        }

        private void Start()
        {
           
        }
    }
}

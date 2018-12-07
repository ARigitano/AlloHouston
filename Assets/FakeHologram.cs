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
        //Never called?
        [SerializeField]
        private Particle[] _allParticles;
        /// <summary>
        /// Folder path for the particle scriptable objects
        /// </summary>
        private static string _path = "Particle";
        /// <summary>
        /// Lines to be displayed by the hologram.
        /// </summary>
        [SerializeField]
        private GameObject[] lines;
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
                           _head;
        /// <summary>
        /// 
        /// </summary>
        //TO DO rendre accessible dans editor
        private float _thetaMin = 0;
        //Mathf.PI / 6;
        /// <summary>
        /// 
        /// </summary>
        private float _thetaMax = 0f;
        //Mathf.PI / 3;
        /// <summary>
        /// 
        /// </summary>
        private float _phiMin = 0f;
        //Mathf.PI / 6;
        /// <summary>
        /// 
        /// </summary>
        private float _phiMax = 0f;
            //Mathf.PI / 3;
        /// <summary>
        /// 
        /// </summary>
        private float _amplitudeA = 1f;
        /// <summary>
        /// 
        /// </summary>
        private float _amplitudeB = 1f;
        /// <summary>
        /// 
        /// </summary>
        [SerializeField]
        private GameObject _particleSpline,

            _destination;

        private float Gamma(float x, float y, float z)
        {
            float gamma = Mathf.Atan2(y,Mathf.Sqrt(Mathf.Pow(x, 2)+Mathf.Pow(z, 2)));

            return gamma;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        private float AngleTrigonometry(float x1, float x4, float y1, float y4)
        {
            float beta = Mathf.Atan2((y4 - y1),(x4 - x1));
            Debug.Log("beta"+beta);
            

            return beta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amplitude"></param>
        /// <param name="angleMin"></param>
        /// <param name="angleMax"></param>
        /// <returns></returns>
        private float BezierCos(float amplitude, float angleMin, float angleMax, Vector3 firstPoint, Vector3 lastPoint)
        {
            float res = amplitude * Mathf.Cos(Random.Range(angleMin, angleMax)+AngleTrigonometry(firstPoint.x, lastPoint.x, firstPoint.z, lastPoint.z));

            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amplitude"></param>
        /// <param name="angleMin"></param>
        /// <param name="angleMax"></param>
        /// <returns></returns>
        private float BezierSin(float amplitude, float angleMin, float angleMax, Vector3 firstPoint, Vector3 lastPoint)
        {
            float res = amplitude * Mathf.Sin(Random.Range(angleMin, angleMax)+AngleTrigonometry(firstPoint.x, lastPoint.x, firstPoint.z, lastPoint.z));

            return res;
        }

        /// <summary>
        /// Animates the particle reaction hologram.
        /// </summary>
        /// <param name="particles">The combination of particles.</param>
        public void AnimHologram(Particle[] particles)
        {
            for(int i = 0; i<1; i++)
            {
                int randomParticle = Random.Range(0, particles.Length);
                Vector3 headPosition = createLine(i, particles[randomParticle]);

                if (particles[randomParticle].head)
                {
                    GameObject lineHead =  (GameObject) Instantiate(_head, headPosition, Quaternion.identity);
                    lineHead.transform.parent = this.gameObject.transform;
                }
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
            lines[i] = (GameObject)Instantiate(_particleSpline, Vector3.zero, Quaternion.identity, transform);
            

            lines[i].transform.localPosition = Vector3.zero;
            lines[i].transform.localRotation = Quaternion.identity;

            //lines[i] = new GameObject();
            BezierSpline spline = lines[i].GetComponent<BezierSpline>();

            pointsB[i] = (GameObject)Instantiate(_destination, Vector3.zero, Quaternion.identity);
            //pointsB[i] = new GameObject();
            pointsB[i].transform.parent = this.gameObject.transform;

            if (particle.destination == 2)
            {
                pointsB[i].transform.localPosition = new Vector3(Random.Range(0f, 0.2f), Random.Range(0f, 0.3f), Random.Range(0.14f, 0.22f));
            } else if (particle.destination == 3)
            {
                pointsB[i].transform.localPosition = new Vector3(Random.Range(0.21f, 0.37f), Random.Range(0.3f, 0.49f), Random.Range(0.25f, 0.35f));
            }

                int negationX = Random.Range(0, 2);
                int negationY = Random.Range(0, 2);
                int negationZ = Random.Range(0, 2);
                float x, y, z;

                if (negationX == 0)
                    x = -pointsB[i].transform.localPosition.x;
                else
                    x = pointsB[i].transform.localPosition.x;

                if (negationY == 0)
                    y = -pointsB[i].transform.localPosition.y;
                else
                    y = pointsB[i].transform.localPosition.y;

                if (negationZ == 0)
                    z = -pointsB[i].transform.localPosition.z;
                else
                    z = pointsB[i].transform.localPosition.z;

                pointsB[i].transform.localPosition = new Vector3(x, y, z);

            //pointsB[i].transform.localPosition = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.2f, 0.2f), Random.Range(-0.1f, 0.1f));
            GameObject line = lines[i];
            //line.AddComponent<BezierSpline>();
            
            spline.Reset();

            spline.points[0] = Vector3.zero;
            //spline.points[0] = this.gameObject.transform.position;
            

            //spline.points[3] = pointsB[i].transform.position;

            spline.points[3] = new Vector3(1, 1, 1);


            spline.points[1].x = Mathf.Cos(Mathf.PI/6)*(Mathf.Cos(Gamma(spline.points[3].x, spline.points[3].y, spline.points[3].z))*BezierCos(_amplitudeA, _thetaMin, _thetaMax, spline.points[0], spline.points[3])+spline.points[0].x);
            spline.points[1].y = Mathf.Sin(Gamma(spline.points[3].x, spline.points[3].y, spline.points[3].z));
            spline.points[1].z = Mathf.Sin(Mathf.PI/6)*(Mathf.Cos(Gamma(spline.points[3].x, spline.points[3].y, spline.points[3].z))*BezierSin(_amplitudeA, _thetaMin, _thetaMax, spline.points[0], spline.points[3])+spline.points[0].z);

            spline.points[2].x = -Mathf.Cos(Mathf.PI/6)*(-(Mathf.Cos(Gamma(spline.points[3].x, spline.points[3].y, spline.points[3].z))*BezierCos(_amplitudeA, _thetaMin, _thetaMax, spline.points[0], spline.points[3])+spline.points[0].x));
            spline.points[2].y = -(Mathf.Sin(Gamma(spline.points[3].x, spline.points[3].y, spline.points[3].z)));
            spline.points[2].z = -Mathf.Sin(Mathf.PI/6)*(-(Mathf.Cos(Gamma(spline.points[3].x, spline.points[3].y, spline.points[3].z))*BezierSin(_amplitudeA, _thetaMin, _thetaMax, spline.points[0], spline.points[3])+spline.points[0].z));

            /*spline.points[2].x = -BezierCos(_amplitudeB, _phiMin, _phiMax, spline.points[0], spline.points[3]) + spline.points[3].x;
            spline.points[2].y = spline.points[3].y;
            spline.points[2].z = -BezierSin(_amplitudeB, _phiMin, _phiMax, spline.points[0], spline.points[3]) + spline.points[3].z;*/

            Debug.Log("point0" + spline.points[0]);
            Debug.Log("point1" + spline.points[1]);
            Debug.Log("point2" + spline.points[2]);
            Debug.Log("point3" + spline.points[3]);



            //pointsB[i].transform.localPosition = new Vector3(x/2, y, z);

            //spline.points[1] = pointsB[i].transform.position;
            //spline.points[2] = pointsB[i].transform.position;

            /*if(particle.secondLine)
            {
                spline.AddCurve();
            }*/
            Debug.Log(lines[i].transform.rotation);
            if (true)
                //particle.line)
            {
                line.AddComponent<SplineDecorator>();
                SplineDecorator decorator = line.GetComponent<SplineDecorator>();
                decorator.spline = spline;
                decorator.frequency = 75;
                decorator.items = new Transform[1];
                decorator.items[0] = _sphere.transform;
                decorator.Populate();
            }
            Debug.Log(lines[i].transform.rotation);
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

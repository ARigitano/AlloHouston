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
        private float _theta = 0f;
        /// <summary>
        /// 
        /// </summary>
        private float _phi = 0f;
        /// <summary>
        /// 
        /// </summary>
        private float _amplitudeA = 0.15f;
        /// <summary>
        /// 
        /// </summary>
        private float _amplitudeB = 0.15f;
        /// <summary>
        /// 
        /// </summary>
        [SerializeField]
        private GameObject _particleSpline,

            _destination;

        /// <summary>
        /// Animates the particle reaction hologram.
        /// </summary>
        /// <param name="particles">The combination of particles.</param>
        public void AnimHologram(Particle[] particles)
        {
            for(int i = 0; i<18; i++)
            {
                int randomParticle = Random.Range(0, particles.Length);
                Vector3 headPosition = createLine(i, particles[randomParticle]);

                if (particles[randomParticle].head)
                {
                    GameObject lineHead =  (GameObject) Instantiate(_head, Vector3.zero, Quaternion.LookRotation(headPosition, Vector3.left));
                    lineHead.transform.parent = this.gameObject.transform;
                    lineHead.transform.localPosition = headPosition;
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
            _theta = Random.Range(Mathf.PI / 6f, Mathf.PI / 3f);
            _phi = Random.Range(Mathf.PI / 6f, Mathf.PI / 3f);

            if(particle.negative)
            {
                _theta = -1f * _theta;
                _phi = -1f * _phi;
            }

            lines[i] = (GameObject)Instantiate(_particleSpline, Vector3.zero, Quaternion.identity, transform);





            lines[i].transform.localPosition = Vector3.zero;
            lines[i].transform.localRotation = Quaternion.identity;

            BezierSpline spline = lines[i].GetComponent<BezierSpline>();
            spline.Reset();

            pointsB[i] = (GameObject)Instantiate(_destination, Vector3.zero, Quaternion.identity);
            pointsB[i].transform.parent = this.gameObject.transform;

            float rMax = 0.45f;
            float lMax = 0.95f;
            float r = Random.Range(0, rMax);
            float alpha = Random.Range(0, 2 * Mathf.PI);

            spline.points[3].x = r * Mathf.Cos(alpha);
            spline.points[3].y = lMax * Mathf.Cos(Random.Range(Mathf.PI, -1f * Mathf.PI));
            spline.points[3].z = r * Mathf.Sin(alpha);

           

            GameObject line = lines[i];
            
            

            spline.points[0] = Vector3.zero;

            float matrixRotation1 = (1 / spline.points[3].magnitude) * _amplitudeA;
            float matrixRotation2 = (-1 / spline.points[3].magnitude) * _amplitudeB;

            spline.points[1].x = matrixRotation1 * (spline.points[3].x * Mathf.Cos(_theta) + spline.points[3].z * Mathf.Sin(_theta));
            spline.points[1].y = matrixRotation1 * spline.points[3].y;
            spline.points[1].z = matrixRotation1 * ((-1f * spline.points[3].x) * Mathf.Sin(_theta) + spline.points[3].z * Mathf.Cos(_theta));

            spline.points[2].x = spline.points[3].x + matrixRotation2 * (spline.points[3].x * Mathf.Cos(-1f * _phi) + spline.points[3].z * Mathf.Sin(-1f * _phi));
            spline.points[2].y = spline.points[3].y +  matrixRotation2 * spline.points[3].y;
            spline.points[2].z = spline.points[3].z +  matrixRotation2 * ((-1f * spline.points[3].x) * Mathf.Sin(-1f * _phi) + spline.points[3].z * Mathf.Cos(-1f * _phi));

            



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

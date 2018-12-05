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
            lines[i] = new GameObject();
            lines[i].transform.parent = this.gameObject.transform;

            pointsB[i] = new GameObject();
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
            line.AddComponent<BezierSpline>();
            BezierSpline spline = lines[i].GetComponent<BezierSpline>();
            spline.Reset();
            spline.points[0] = this.gameObject.transform.position;
            spline.points[3] = pointsB[i].transform.position;

            pointsB[i].transform.localPosition = new Vector3(x/2, y, z);

            spline.points[1] = pointsB[i].transform.position;
            spline.points[2] = pointsB[i].transform.position;

            /*if(particle.secondLine)
            {
                spline.AddCurve();
            }*/

            if (particle.line)
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
            //createLine(0);
        }
    }
}

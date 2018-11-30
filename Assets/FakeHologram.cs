﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    public class FakeHologram : XPElement


    {
        [SerializeField]
        private Particle[] _allParticles;

        private static string _path = "Particle";

        [SerializeField]
        private GameObject[] lines;

        [SerializeField]
        private GameObject[] pointsB;

        [SerializeField]
        private GameObject _hologram;

        public void AnimHologram(Particle[] particles)
        {
            int i = 0;
            int zone = 3;
            foreach(Particle particle in particles)
            {
                createLine(i, particle.destination);
                i++;
            }
        }

        private void createLine(int i, int zone)
        {
            lines[i] = new GameObject();
            lines[i].transform.parent = this.gameObject.transform;

            pointsB[i] = new GameObject();
            pointsB[i].transform.parent = this.gameObject.transform;

            if (zone == 3)
            {
                pointsB[i].transform.localPosition = new Vector3(Random.Range(0f, 0.2f), Random.Range(0f, 0.3f), Random.Range(0.14f, 0.22f));
            } else if (zone == 3)
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

            lines[i].AddComponent<LineRenderer>();
            lines[i].GetComponent<LineRenderer>().SetWidth(0.01f, 0.01f);
            lines[i].GetComponent<LineRenderer>().SetPosition(0, this.gameObject.transform.position);
        }

        public override void OnResolved()
        {
            Debug.Log(name + "Resolved");
        }

        public override void OnFailed()
        {
            Debug.Log(name + "Failed");
        }

        public override void OnActivated()
        {
            Debug.Log(name + "Activated");
            _hologram.GetComponent<MeshRenderer>().enabled = true;
        }

        public override void OnPause()
        {
            Debug.Log(name + "Paused");
            _hologram.GetComponent<MeshRenderer>().enabled = false;
        }

        public override void OnUnpause()
        {
            Debug.Log(name + "Unpaused");
            _hologram.GetComponent<MeshRenderer>().enabled = true;
        }

        private void Start()
        {
            //createLine(0);
        }

        private void Update()
        {

            for(int i = 0; i < lines.Length; i++)
            {
                lines[i].GetComponent<LineRenderer>().SetPosition(1, pointsB[i].transform.position);
            }
        }




    }
}

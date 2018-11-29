using System.Collections;
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

        private void createLine(int i)
        {
            lines[i] = new GameObject();
            lines[i].transform.parent = this.gameObject.transform;

            pointsB[i] = new GameObject();
            pointsB[i].transform.parent = this.gameObject.transform;
            pointsB[i].transform.localPosition = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.2f, 0.2f), Random.Range(-0.1f, 0.1f));

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
            createLine(0);
        }

        private void Update()
        {
            lines[0].GetComponent<LineRenderer>().SetPosition(1, pointsB[0].transform.position);
        }




    }
}

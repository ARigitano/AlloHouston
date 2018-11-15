using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CRI.HelloHouston.Experience
{
    public class FakeTubeScreen : XPElement
    {

        [SerializeField]
        private GameObject _tube;


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
            _tube.GetComponent<MeshRenderer>().enabled = true;
        }

        public override void OnPause()
        {
            Debug.Log(name + "Paused");
            _tube.GetComponent<MeshRenderer>().enabled = false;
        }

        public override void OnUnpause()
        {
            Debug.Log(name + "Unpaused");
            _tube.GetComponent<MeshRenderer>().enabled = true;
        }

        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}


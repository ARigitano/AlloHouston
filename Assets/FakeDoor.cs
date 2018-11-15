using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    public class FakeDoor : XPElement
    {


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
        }

        public override void OnPause()
        {
            Debug.Log(name + "Paused");
        }

        public override void OnUnpause()
        {
            Debug.Log(name + "Unpaused");
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

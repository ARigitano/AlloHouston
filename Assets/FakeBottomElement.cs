using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    public class FakeBottomElement : XPElement
    {


        public override void OnResolved()
        {
            Debug.Log(name + "Resolved");
        }

        public override void OnFailed()
        {

        }

        public override void OnActivated()
        {
            Debug.Log(name + "Activated");
        }

        public override void OnPause()
        {

        }

        public override void OnUnpause()
        {

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


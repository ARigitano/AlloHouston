using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    public class FakeCornerScreen : XPElement
    {


        public override void OnSuccess()
        {
            Debug.Log(name + "Resolved");
        }

        public override void OnFailure()
        {
            Debug.Log(name + "Failed");
        }

        public override void OnActivation()
        {
            Debug.Log(name + "Activated");
        }

        public override void OnHide()
        {
            Debug.Log(name + "Paused");
        }

        public override void OnShow()
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

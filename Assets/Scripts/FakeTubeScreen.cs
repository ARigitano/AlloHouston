using CRI.HelloHouston.Experience;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CRI.HelloHouston.ParticlePhysics
{
    /// <summary>
    /// The top right screen of the experiment block for the particle physics experiment.
    /// </summary>
    public class FakeTubeScreen : XPElement
    {
        /// <summary>
        /// Prefab of the tube.
        /// </summary>
        [SerializeField]
        private GameObject _tube;

        //TO DO
        /// <summary>
        /// Effect when the experiment is correctly resolved.
        /// </summary>
        public override void OnSuccess()
        {
            Debug.Log(name + "Resolved");
        }
        //TO DO
        /// <summary>
        /// Effect when the experiment is failed.
        /// </summary>
        public override void OnFailure()
        {
            Debug.Log(name + "Failed");
        }
        //TO DO
        /// <summary>
        /// Effect when the experiment is activated the first time.
        /// </summary>
        public override void OnActivation()
        {
            Debug.Log(name + "Activated");
            //_tube.GetComponent<MeshRenderer>().enabled = true;
        }
        //TO DO
        /// <summary>
        /// Effect when the experiment is paused.
        /// </summary>
        public override void OnShow()
        {
            Debug.Log(name + "Paused");
            //_tube.GetComponent<MeshRenderer>().enabled = false;
        }
        //TO DO
        /// <summary>
        /// Effect when the experiment is unpaused.
        /// </summary>
        public override void OnHide()
        {
            Debug.Log(name + "Unpaused");
            //_tube.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}


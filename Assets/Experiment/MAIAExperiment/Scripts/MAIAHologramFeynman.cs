using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class MAIAHologramFeynman : XPHologramElement
    {
        /// <summary>
        /// The objects containing the Feynman diagrams.
        /// </summary>
        public GameObject[] feynmanBoxes;
        /// <summary>
        /// Name of the Feynman digram chosen by the player.
        /// </summary>
        public string feynmanBoxName;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void OnActivation(XPManager manager)
        {
            base.OnActivation(manager);
            gameObject.SetActive(false);
        }

        void OnTriggerStay(Collider other)
        {
            if (other.tag == "Feynmanbox")
            {
                feynmanBoxName = other.name;
                Debug.Log(feynmanBoxName);
            }
        }
    }
}

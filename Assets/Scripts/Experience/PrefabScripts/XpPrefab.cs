using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    public class XPPrefab : MonoBehaviour
    {
        protected XPSynchronizer _xpSynchronizer;
        public string index;

        // Use this for initialization
        void Start()
        {
            XPSynchronizer[] xpSynchronizers = FindObjectsOfType<XPSynchronizer>();

            //foreach(XpSynchronizer xpSynch)
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

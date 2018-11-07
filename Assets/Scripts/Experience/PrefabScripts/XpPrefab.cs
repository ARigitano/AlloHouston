using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    public class XpPrefab : MonoBehaviour
    {
        protected XpSynchronizer _xpSynchronizer;
        public string index;

        // Use this for initialization
        void Start()
        {
            XpSynchronizer[] xpSynchronizers = FindObjectsOfType<XpSynchronizer>();

            //foreach(XpSynchronizer xpSynch)
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

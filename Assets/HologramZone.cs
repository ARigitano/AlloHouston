using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.ParticlePhysics
{
    public class HologramZone : MonoBehaviour
    {

        [SerializeField]
        private FakeHologram _hologram;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "ParticleHead")
            {
                Debug.Log("wrong");
                _hologram.DestroySpline(other.gameObject);
            }
        }
    }
}

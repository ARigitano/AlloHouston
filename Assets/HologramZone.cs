using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.ParticlePhysics
{
    /// <summary>
    /// A zone for the particle reactor hologram.
    /// </summary>
    public class HologramZone : MonoBehaviour
    {
        /// <summary>
        /// The particle reactor hologram.
        /// </summary>
        [SerializeField]
        private FakeHologram _hologram;
        /// <summary>
        /// The type of particle to be detected.
        /// </summary>
        [SerializeField]
        private string _particle;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == _particle)
            {
                _hologram.DestroySpline(other.gameObject);
            }
        }
    }
}

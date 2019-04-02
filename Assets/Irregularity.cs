using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An irregularity inside of the core hologram for the tutorial.
/// </summary>
namespace CRI.HelloHouston.Experience.Tutorial
{
    public class Irregularity : MonoBehaviour
    {
        /// <summary>
        /// The hologram for the tutorial experiment.
        /// </summary>
        [SerializeField]
        private TutorialHologram _hologram;
        private bool _isOut = false;
        [SerializeField]
        private Material _safeMaterial, _failMaterial;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDestroy()
        {
            _hologram.UpdateNbIrregularities();
        }

        public void OutOfBound()
        {
            if(_isOut) 
                Destroy(gameObject, 1f);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Core")
            {
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                _isOut = true;
            }
        }
    }
}

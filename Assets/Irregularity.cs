using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.Tutorial
{
    public class Irregularity : MonoBehaviour
    {
        [SerializeField]
        private TutorialHologram _hologram;

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

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Core")
            {
                gameObject.GetComponent<MeshRenderer>().enabled = true; 
                Destroy(gameObject, 3f);
            }
        }
    }
}

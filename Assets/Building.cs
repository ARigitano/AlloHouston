using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.Tutorial
{
    public class Building : MonoBehaviour
    {
        [SerializeField]
        private TutorialHologramSecond _hologram;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        void OnTriggerExit(Collider other)
        {
            if (other.tag == "Irregularity")
            {
                _hologram.isBuilding = true;
            }
        }
    }
}

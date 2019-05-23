using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using VRTK;
using VRTK.GrabAttachMechanics;

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
        /// <summary>
        /// Is the irregularity out of the building?
        /// </summary>
        private bool _isOut = false;
        /// <summary>
        /// Is the irregularity corrupted?
        /// </summary>
        [SerializeField]
        private bool _isCorruptedData = false;
        /// <summary>
        /// The building in which the irregularity is hidden
        /// </summary>
        private GameObject _building;

        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "Building" || other.tag == "Core")
            {
                _building = other.gameObject;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Core")
            {
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                Destroy(gameObject, 2f);

                if (_isCorruptedData)
                {
                    other.GetComponent<MeshRenderer>().material = _hologram.materialSuccess;
                    _hologram.UpdateNbIrregularities();
                }
            }
        }
    }
}

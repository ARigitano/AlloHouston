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
    public class Irregularity : VRTK_InteractableObject
    {
        /// <summary>
        /// The hologram for the tutorial experiment.
        /// </summary>
        [SerializeField]
        private TutorialHologram _hologram;
        private bool _isOut = false;
        [SerializeField]
        private Material _safeMaterial, _failMaterial;
        [SerializeField]
        private bool _isCorrupted = false;
        [SerializeField]
        private GameObject _virus;
        

        // Start is called before the first frame update
        void Start()
        {
            //var hands = FindObjectsOfType<Hand>();
            //ushort duration = (ushort)Random.Range(99999, 99999999);
            //Debug.Log(hands[0]);
            //hands[0].controller.TriggerHapticPulse(duration);
            //hands[1}.controller.TriggerHapticPulse(duration);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject)
        {
            Debug.Log("okkokokorrr");
        }

            private void OnDestroy()
        {
                _hologram.UpdateNbIrregularities();
        }

        public void OutOfBound()
        {
            if (_isOut)
            {
                if (!_isCorrupted)
                {
                    Destroy(gameObject, 2f);
                }
                else
                {
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    _virus.SetActive(true);
                    this.enabled = false;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Building" || other.tag == "Core")
            {
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                _isOut = true;
                OutOfBound();
            }
        }
    }
}

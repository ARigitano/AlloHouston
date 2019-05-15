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
        private bool _isOut = false;
        [SerializeField]
        private bool _isVirus = false;
        [SerializeField]
        private bool _isCorruptedData = false;
        [SerializeField]
        private GameObject _virus;
        [SerializeField]
        private VRTK_InteractableObject _interact;
        private GameObject _building;
        private bool _isResolved = false;


        private void OnEnable()
        {
            _interact.InteractableObjectGrabbed += OnGrab;
            _interact.InteractableObjectUngrabbed += OnUngrab;
        }

        private void OnDisable()
        {
            _interact.InteractableObjectGrabbed -= OnGrab;
            _interact.InteractableObjectUngrabbed -= OnUngrab;
        }

        private void OnUngrab(object sender, InteractableObjectEventArgs e)
        {
            //e.interactingObject.GetComponen
            Debug.Log("ungrabbed");
        }

        private void OnGrab(object sender, InteractableObjectEventArgs e)
        {
            Debug.Log("grabbed");
        }


        // Start is called before the first frame update
        void Start()
        {
            //_hologram = GameObject.FindWithTag("").GetComponent<TutorialHologram>();
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

      


            private void OnDestroy()
        {
            //if(_hologram != null)
            // _hologram.UpdateNbIrregularities();
        }

        public void OutOfBound()
        {
            if (_isOut)
            {
                if (!_isVirus)
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

        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "Building" || other.tag == "Core")
            {
                _building = other.gameObject;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if ((other.tag == "Building" || other.tag == "Core") /*&& !_isResolved*/)
            {
                
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                _isOut = true;
                OutOfBound();
               // _isResolved = true;
                if (_isCorruptedData && !_isVirus)
                {
                    other.GetComponent<MeshRenderer>().material = _hologram.materialSuccess;
                    _hologram.UpdateNbIrregularities();
                   // _isResolved = true;

                }
                else if(!_isVirus)
                {
                    other.GetComponent<MeshRenderer>().material = _hologram.materialFail;
                   // _isResolved = true;
                }

            }/* else if (other.tag == "ViveController" && !_isCorruptedData && _building != null)
            {
                _building.GetComponent<MeshRenderer>().material = _hologram.materialSuccess;
                _hologram.UpdateNbIrregularities();
               // _isResolved = true;
            }*/
        }
    }
}

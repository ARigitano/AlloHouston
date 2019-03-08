using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRI.HelloHouston.Experience.MAIA;
using CRI.HelloHouston.Experience;


namespace Valve.VR.InteractionSystem
{
    /// <summary>
    /// Holographic tube containing the troubled experiment.
    /// </summary>
    public class ErrorTubeX : MonoBehaviour
    {
        /// <summary>
        /// Transforms of the original position and destination position for the tube to travel between.
        /// </summary>
        [SerializeField]
        private Transform _originalSlot, _destinationSlot;
        /// <summary>
        /// Script for the holographic station.
        /// </summary>
        [SerializeField]
        private ChangingTube _station;
        /// <summary>
        /// Materials depending if tube is available or not for replacement.
        /// </summary>
        [SerializeField]
        private Material _available, _notAvailable;
        /// <summary>
        /// Reference to the experiment contained in the tube.
        /// </summary>
        public XPManager experience;
        /// <summary>
        /// Is the tube fixed or moving?
        /// </summary>
        private bool isDocked = false;
        /// <summary>
        /// Speed by which the tube goes back to the original position if dropped.
        /// </summary>
        [SerializeField]
        private float _speed = 2f;
       

        // Start is called before the first frame update
        void Start()
        {
            _station.tubes.Add(this);
        }

        // Update is called once per frame
        void Update()
        {
            if (transform.position != _originalSlot.position && isDocked)
            {
                transform.position = Vector3.Lerp(transform.position, _originalSlot.position, Time.deltaTime * _speed);
            }
            else 
            {
                isDocked = false;
            }
        }

        /// <summary>
        /// The tube is available for replacement.
        /// </summary>
        public void IsAvailable()
        {
            gameObject.GetComponent<MeshRenderer>().material = _available;
            gameObject.GetComponent<CapsuleCollider>().enabled = true;
        }

        /// <summary>
        /// The tube is not available for replacement.
        /// </summary>
        public void IsNotAvailable()
        {
            gameObject.GetComponent<MeshRenderer>().material = _notAvailable;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }

        /// <summary>
        /// The tube is hold by the hand of the player.
        /// </summary>
        /// <param name="hand">The hand of the player.</param>
        private void OnAttachedToHand(Hand hand)
        {
            isDocked = false;
        }

        /// <summary>
        /// The tube leaves the hand of the player.
        /// </summary>
        /// <param name="hand">The hand of the player.</param>
        private void OnDetachedFromHand(Hand hand)
        {
            if(_destinationSlot != null)
            {
                gameObject.transform.parent = _destinationSlot;
                gameObject.transform.position = _destinationSlot.position;
                gameObject.transform.rotation = _destinationSlot.rotation;
                _station.LoadingTube(experience, _destinationSlot.GetComponent<TubeSlot>().topZone);
            } 
            else
            {
                gameObject.transform.parent = _originalSlot;
                isDocked = true;
                gameObject.transform.rotation = _originalSlot.rotation;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "TubeDock" && other.transform.childCount == 0)
            {
                _destinationSlot = other.transform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "TubeDock")
            {
                _destinationSlot = null;
            }
        }
    }
}

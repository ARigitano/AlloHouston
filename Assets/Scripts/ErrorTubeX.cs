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
        [SerializeField]
        private GameObject _statusPanel;
       

        // Start is called before the first frame update
        void Start()
        {
            _station = GameObject.FindGameObjectWithTag("Station").GetComponent <ChangingTube>();
            _station.tubes.Add(this);
        }

        // Update is called once per frame
        void Update()
        {
            if (transform.position != transform.parent.position && isDocked)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, transform.parent.rotation, Time.deltaTime * _speed);
                transform.position = Vector3.Lerp(transform.position, transform.parent.position, Time.deltaTime * _speed);
            }
        }

        /// <summary>
        /// The tube is available for replacement.
        /// </summary>
        public void IsAvailable()
        {
            gameObject.GetComponent<MeshRenderer>().material = _available;
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }

        /// <summary>
        /// The tube is not available for replacement.
        /// </summary>
        public void IsNotAvailable()
        {
            gameObject.GetComponent<MeshRenderer>().material = _notAvailable;
            gameObject.GetComponent<BoxCollider>().enabled = false;
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
                isDocked = true;
                _station.LoadingTube(experience, _destinationSlot.GetComponent<TubeSlot>().topZone);
            } 
            else
            {
                gameObject.transform.parent = _originalSlot;
                isDocked = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "TubeDock" && other.transform.childCount == 0)
            {
                _destinationSlot = other.transform;
            }
            else if (other.tag == "TubeBase")
            {
                _statusPanel.SetActive(true);
                isDocked = false;
                transform.position = other.transform.position;
                transform.rotation = other.transform.rotation;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "TubeDock")
            {
                _destinationSlot = null;
            }
            else if (other.tag == "TubeBase")
            {
                _statusPanel.SetActive(false);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRI.HelloHouston.Experience.MAIA;
using CRI.HelloHouston.Experience;
using CRI.HelloHouston.GameElements;

namespace Valve.VR.InteractionSystem
{
    /// <summary>
    /// Holographic tube containing the troubled experiment.
    /// </summary>
    public class XPTube : MonoBehaviour
    {
        /// <summary>
        /// Transforms of the original position and destination position for the tube to travel between.
        /// </summary>
        [SerializeField]
        private Transform _originalSlot;

        private Transform _destinationSlot;
        /// <summary>
        /// Materials depending if tube is available or not for replacement.
        /// </summary>
        [SerializeField]
        private Material _available, _notAvailable;
        /// <summary>
        /// Reference to the experiment contained in the tube.
        /// </summary>
        [HideInInspector]
        public XPManager manager;
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
        public void SetAvailable()
        {
            gameObject.GetComponent<MeshRenderer>().material = _available;
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }

        /// <summary>
        /// The tube is not available for replacement.
        /// </summary>
        public void SetUnavailable()
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
                _destinationSlot.GetComponent<TubeSlot>().LoadExperiment(manager);
            } 
            else
            {
                gameObject.transform.parent = _originalSlot;
                isDocked = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "ViveController")
            {
                isDocked = false;
            }
            if (other.tag == "TubeDock" && other.transform.childCount == 0)
            {
                _destinationSlot = gameObject.transform;
            }
            if (other.tag == "TubeBase")
            {
                _statusPanel.SetActive(true);
                isDocked = false;
                transform.position = other.transform.position;
                transform.rotation = other.transform.rotation;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "ViveController")
            {
                if (_destinationSlot != null)
                {
                    gameObject.transform.parent = _destinationSlot;
                    isDocked = true;
                    _destinationSlot.GetComponent<TubeSlot>().LoadExperiment(manager);
                }
                else
                {
                    gameObject.transform.parent = _originalSlot;
                    isDocked = true;
                }
            }
            if (other.tag == "TubeDock")
            {
                _destinationSlot.GetComponent<TubeSlot>().UnloadExperiment();
                _destinationSlot = null;
            }
            else if (other.tag == "TubeBase")
            {
                _statusPanel.SetActive(false);
            }
        }
    }
}

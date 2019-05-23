using UnityEngine;
using CRI.HelloHouston.Experience;
using VRTK;
using System;

namespace CRI.HelloHouston.GameElements
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

        private TubeSlot _destinationSlot;
        /// <summary>
        /// Materials depending if tube is available or not for replacement.
        /// </summary>
        [SerializeField]
        private Material _available, _notAvailable;
        /// <summary>
        /// Reference to the experiment contained in the tube.
        /// </summary>
        public XPManager manager { get; private set; }
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

        private bool _isAvailable;

        public bool isActive;

        private void OnEnable()
        {
            GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += InteractableObjectGrabbed;
            GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed += InteractableObjectUngrabbed;
            XPManager.onActivation += OnActivation;
            XPManager.onDeactivation += OnDeactivation;
        }
        

        private void OnDisable()
        {
            GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed -= InteractableObjectGrabbed;
            GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed -= InteractableObjectUngrabbed;
        }

        public void Init(XPManager manager)
        {
            this.manager = manager;
            if (this.manager.state == XPState.Inactive)
            {
                gameObject.SetActive(false);
                isActive = false;
            }
            else
                isActive = true;
        }

        private void OnActivation(object sender, XPManagerEventArgs e)
        {
            if (e.manager == manager)
            {
                gameObject.SetActive(true);
                isActive = true;
            }
        }

        private void OnDeactivation(object sender, XPManagerEventArgs e)
        {
            if (e.manager == manager)
            {
                gameObject.SetActive(false);
                isActive = false;
            }
        }

        private void InteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
        {
            if (gameObject.activeInHierarchy)
            {
                if (_destinationSlot != null)
                {
                    bool res = _destinationSlot.GetComponentInChildren<TubeSlot>().LoadExperiment(this, manager, SetAvailable);
                    if (res)
                    {
                        isDocked = false;
                        gameObject.transform.SetParent(_destinationSlot.transform);
                        transform.position = _destinationSlot.transform.position;
                        transform.rotation = _destinationSlot.transform.rotation;
                        SetUnavailable();
                    }
                }
                else
                {
                    gameObject.transform.SetParent(_originalSlot);
                    isDocked = true;
                }
            }
        }

        private void InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
        {
            isDocked = false;
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
        public void SetAvailable()
        {
            _isAvailable = true;
            gameObject.GetComponent<MeshRenderer>().material = _available;
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }

        /// <summary>
        /// The tube is not available for replacement.
        /// </summary>
        public void SetUnavailable()
        {
            _isAvailable = false;
            gameObject.GetComponent<MeshRenderer>().material = _notAvailable;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (gameObject.activeInHierarchy)
            {
                if (other.tag == "TubeDock" && other.GetComponent<TubeSlot>())
                {
                    _destinationSlot = other.GetComponent<TubeSlot>();
                    isDocked = false;
                }
                if (other.tag == "TubeBase")
                {
                    _statusPanel.SetActive(true);
                    isDocked = false;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (gameObject.activeInHierarchy)
            {
                if (other.tag == "TubeDock" && _destinationSlot != null)
                {
                    if (_destinationSlot.currentTube == this)
                    {
                        Debug.Log("Unload");
                        bool res = _destinationSlot.UnloadExperiment(SetAvailable);
                        if (res)
                        {
                            SetUnavailable();
                            isDocked = true;
                            _destinationSlot = null;
                            gameObject.transform.SetParent(_originalSlot);
                        }
                    }
                    else
                    {
                        isDocked = true;
                        _destinationSlot = null;
                        gameObject.transform.SetParent(_originalSlot);
                    }
                }
                else if (other.tag == "TubeBase")
                {
                    _statusPanel.SetActive(false);
                }
            }
        }
    }
}

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
        private Material _available, _notAvailable, success, disabled;
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

        [SerializeField]
        private MeshRenderer _dockMesh;

        private Collider _collider;
        private int _index;

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

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        public void Init(XPManager manager, int index)
        {
            _index = index;
            this.manager = manager;
            if (this.manager.state == XPState.Inactive)
            {
                gameObject.GetComponent<MeshRenderer>().material = disabled;
                _dockMesh.material = disabled;
                isActive = false;
                _isAvailable = false;
            }
            else
            {
                isActive = true;
                _isAvailable = true;
            }
        }

        private void OnActivation(object sender, XPManagerEventArgs e)
        {
            if (e.manager == manager)
            {
                _dockMesh.material = _available;
                gameObject.GetComponent<MeshRenderer>().material = _available;
                isActive = true;
                _isAvailable = true;
            }
        }

        private void OnDeactivation(object sender, XPManagerEventArgs e)
        {
            if (e.manager == manager)
            {
                gameObject.GetComponent<MeshRenderer>().material = disabled;
                isActive = false;
                _isAvailable = false;
            }
        }

        private void InteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
        {
            if (isActive && gameObject.activeInHierarchy)
            {
                if (_destinationSlot != null)
                {
                    bool res = _destinationSlot.GetComponentInChildren<TubeSlot>().LoadExperiment(this, _index, manager, SetAvailable);
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
            _collider.enabled = isActive && _isAvailable;

        }

        /// <summary>
        /// The tube is available for replacement.
        /// </summary>
        public void SetAvailable()
        {
            _isAvailable = true;
            gameObject.GetComponent<MeshRenderer>().material = _available;
        }

        /// <summary>
        /// The tube is not available for replacement.
        /// </summary>
        public void SetUnavailable()
        {
            _isAvailable = false;
            gameObject.GetComponent<MeshRenderer>().material = _notAvailable;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isActive && gameObject.activeInHierarchy)
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
            if (isActive && gameObject.activeInHierarchy)
            {
                if (other.tag == "TubeDock" && _destinationSlot != null)
                {
                    if (_destinationSlot.currentTube == this)
                    {
                        Debug.Log("Unload");
                        bool res = _destinationSlot.UnloadExperiment(_index, SetAvailable);
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

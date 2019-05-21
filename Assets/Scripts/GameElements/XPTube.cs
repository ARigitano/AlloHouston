using UnityEngine;
using CRI.HelloHouston.Experience;
using VRTK;

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

        private void OnEnable()
        {
            GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += InteractableObjectGrabbed;
            GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed += InteractableObjectUngrabbed;
        }

        private void OnDisable()
        {
            GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed -= InteractableObjectGrabbed;
            GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed -= InteractableObjectUngrabbed;
        }

        private void InteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
        {
            if (_destinationSlot != null)
            {
                isDocked = false;
                _destinationSlot.currentTube = this;
                gameObject.transform.SetParent(_destinationSlot.transform);
                transform.position = _destinationSlot.transform.position;
                transform.rotation = _destinationSlot.transform.rotation;
                SetUnavailable();
                _destinationSlot.GetComponentInChildren<TubeSlot>().LoadExperiment(manager, SetAvailable);
            }
            else
            {
                gameObject.transform.SetParent(_originalSlot);
                isDocked = true;
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

        private void OnTriggerEnter(Collider other)
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

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "TubeDock" && _destinationSlot != null)
            {
                if (_destinationSlot.currentTube == this)
                {
                    SetUnavailable();
                    _destinationSlot.UnloadExperiment(SetAvailable);
                    _destinationSlot.currentTube = null;
                    isDocked = true;
                }
                _destinationSlot = null;
                gameObject.transform.SetParent(_originalSlot);
            }
            else if (other.tag == "TubeBase")
            {
                _statusPanel.SetActive(false);
            }
        }
    }
}

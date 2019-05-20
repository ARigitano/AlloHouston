using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.GrabAttachMechanics;

namespace CRI.HelloHouston.Experience.Tutorial
{
    /// <summary>
    /// Virus for the second hologram of the tutorial experiment
    /// </summary>
    public class VirusNew : MonoBehaviour
    {
        /// <summary>
        /// Second hologram
        /// </summary>
        private TutorialHologramVirus _core;
        /// <summary>
        /// Speed at which the virus moves
        /// </summary>
        [SerializeField]
        private float _speed;
        /// <summary>
        /// Is the virus currently moving?
        /// </summary>
        private bool isMoving = true;
        /// <summary>
        /// Is the virus currently inside a building?
        /// </summary>
        private bool isInside = false;
        /// <summary>
        /// Current destination for the virus trajectory
        /// </summary>
        public Transform _destination;
        /// <summary>
        /// Can the virus be catched right now?
        /// </summary>
        private bool _isCatchable = false;
        
        // Start is called before the first frame update
        void Start()
        {
            _core = GameObject.FindGameObjectWithTag("Core").GetComponent<TutorialHologramVirus>();
            _destination = _core.attaches[Random.Range(0, _core.attaches.Count)];
            _core.nbVirus++;
        }

        /// <summary>
        /// Makes the virus move from one building to another
        /// </summary>
        /// <returns></returns>
        IEnumerator Moving()
        {
            isMoving = false;
            _isCatchable = true;
            _destination = _core.attaches[Random.Range(0, _core.attaches.Count)];
            yield return new WaitForSeconds(1f);
            isMoving = true;
            _isCatchable = false;
        }

        /// <summary>
        /// Gives the permission to move the virus
        /// </summary>
        /// <returns></returns>
        IEnumerator ComeBack()
        {
            yield return new WaitForSeconds(3f);
            isMoving = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (_destination != null && transform.position != _destination.position && isMoving)
            {
                transform.position = Vector3.MoveTowards(transform.position, _destination.position, _speed * Time.deltaTime);
            } else
            {
                StartCoroutine("Moving");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "ViveController" && isInside && _isCatchable)
            {
                isMoving = false;
                StartCoroutine("ComeBack");
            }
        }

        void OnTriggerStay(Collider other)
        {
            if (other.tag == "Core")
            {
                isInside = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.tag == "Core")
            {
                if (_core.nbVirus < _core._maxVirus)
                {
                    _core.InstantiateVirus(transform);
                    _core.InstantiateVirus(transform);
                }
                isMoving = true;
            }
        }
    }
}

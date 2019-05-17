using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.GrabAttachMechanics;

namespace CRI.HelloHouston.Experience.Tutorial
{
    public class VirusNew : MonoBehaviour
    {
        private TutorialHologramVirus _core;
        [SerializeField]
        private float _speed;
        private bool isMoving = true;
        private bool isInside = false;
        public Transform _destination;
        private bool _isCatchable = false;
        

        // Start is called before the first frame update
        void Start()
        {
            _core = GameObject.FindGameObjectWithTag("Core").GetComponent<TutorialHologramVirus>();
            _destination = _core.attaches[Random.Range(0, _core.attaches.Count)];
            //StartCoroutine("Moving");
            _core.nbVirus++;
        }

        IEnumerator Moving()
        {
            isMoving = false;
            _isCatchable = true;
            _destination = _core.attaches[Random.Range(0, _core.attaches.Count)];
            yield return new WaitForSeconds(1f);
            isMoving = true;
            _isCatchable = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (_destination != null && transform.position != _destination.position && isMoving)
            {
                Debug.Log("movang");
                transform.position = Vector3.MoveTowards(transform.position, _destination.position, _speed * Time.deltaTime);
            } else
            {
                StartCoroutine("Moving");
                
                Debug.Log("moved");
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

        IEnumerator ComeBack()
        {
            yield return new WaitForSeconds(3f);
            isMoving = true;
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
                //StartCoroutine("ComeBack");
            }
        }
    }
}

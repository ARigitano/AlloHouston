using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.GrabAttachMechanics;

namespace CRI.HelloHouston.Experience.Tutorial
{
    public class Virus : MonoBehaviour
    {
        [SerializeField]
        private TutorialHologramSecond _core;
        private Transform _returnPoint;
        [SerializeField]
        private float _speed = 2f;
        [SerializeField]
        private GameObject _virus;
        private bool isAttach = true;
        [SerializeField]
        private int _id;
        private bool isMoving = false;
        private bool isFiring = false;
        private bool isStarted = false;
        private float _pointsDistance, _virusDistance;

        IEnumerator WaitStart()
        {
            yield return new WaitForSeconds(2f);
            isStarted = true;
        }

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine("WaitStart");

            _core = GameObject.FindGameObjectWithTag("Core").GetComponent<TutorialHologramSecond>();
            _id = _core.nbViruses;
            _core.nbViruses++;

            if (_id > _core.attaches.Count)
                Destroy(gameObject);
            do
            {
                _returnPoint = _core.attaches[Random.Range(0, _core.attaches.Count)].transform;
                isAttach = true;

                for (int i = 0; i < _core.freeAttaches.Length; i++)
                {
                    if (_core.freeAttaches[i] == _returnPoint)
                    {
                        isAttach = false;
                        break;
                    }
                }

                if (isAttach)
                    _core.freeAttaches[_id] = _returnPoint;
            }
            while
            (
                isAttach == false
            );

            StartCoroutine("WaitReturn");
        }

        IEnumerator WaitReturn()
        {
      
            yield return new WaitUntil(() =>_virusDistance >= _pointsDistance);
            ReturnToCore();
        }

    
    


        public void ReturnToCore()
        {
            _returnPoint = _core.attaches[Random.Range(0, _core.attaches.Count)].transform;
            //if(_core.nbViruses < _core.attaches.Count)
            //Instantiate(_virus, gameObject.transform.position, gameObject.transform.rotation);
            do
            {
                _returnPoint = _core.attaches[Random.Range(0, _core.attaches.Count)].transform;
                isAttach = true;

                for (int i = 0; i < _core.freeAttaches.Length; i++)
                {
                    if (_core.freeAttaches[i] == _returnPoint)
                    {
                        isAttach = false;
                        break;
                    }
                }

                if (isAttach)
                    _core.freeAttaches[_id] = _returnPoint;
            }
            while
            (
                isAttach == false
            );
            StartCoroutine("WaitReturn");
        }

        IEnumerator Popping()
        {
            Debug.Log("more");
            //ReturnToCore();
            isMoving = true;
            yield return new WaitForSeconds(2f);
            isMoving = false;
            isFiring = false;
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.tag=="Building" && isStarted /*&& other.transform == _returnPoint*/)
            {
                //ReturnToCore();
            }
        }

        // Update is called once per frame
        void Update()
        {
            //_pointsDistance = Vector3.Distance(_core.point1.position, _core.point2.position);
            //_virusDistance = Vector3.Distance(_core.point1.position, gameObject.transform.position);

           /* if (_returnPoint != null && _virusDistance >= _pointsDistance)
            {

            }*/

            if (_returnPoint != null && transform.position != _returnPoint.position)
            {
                transform.position = Vector3.Lerp(transform.position, _returnPoint.position, Time.deltaTime * _speed);
            }

           /* if(Vector3.Distance(_returnPoint.position, transform.position) >= 0.5f && !isFiring)
            {
                isFiring = true;
                //ReturnToCore();
                isFiring = false;
            }*/

            
        }

        
    }
}

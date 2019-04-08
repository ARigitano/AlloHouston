using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.Tutorial
{
    public class Virus : MonoBehaviour
    {
        private TutorialHologramSecond _core;
        private Transform _returnPoint;
        [SerializeField]
        private float _speed = 2f;
        [SerializeField]
        private GameObject _virus;
        private bool isAttach = true;
        [SerializeField]
        private int _id;

        // Start is called before the first frame update
        void Start()
        {
            

            _core = GameObject.FindGameObjectWithTag("Core").GetComponent<TutorialHologramSecond>();
            _core.nbViruses++;
            _id = _core.nbViruses;
            do
            {
                _returnPoint = _core.attaches[Random.Range(0, _core.attaches.Count)].transform;
                isAttach = true;

                for(int i = 0; i < _core.freeAttaches.Length; i++)
                {
                    if(_core.freeAttaches[i] == _returnPoint)
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


        }

        

        public void ReturnToCore()
        {
            _returnPoint = _core.attaches[Random.Range(0, _core.attaches.Count)].transform;
            Instantiate(_virus, gameObject.transform.position, gameObject.transform.rotation);
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
        }

        // Update is called once per frame
        void Update()
        {
            if (_returnPoint != null && transform.position != _returnPoint.position)
            {
                transform.position = Vector3.Lerp(transform.position, _returnPoint.position, Time.deltaTime * _speed);
            }

            
        }

        
    }
}

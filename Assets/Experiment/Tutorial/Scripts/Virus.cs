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

        // Start is called before the first frame update
        void Start()
        {
            //transform.localscale = Vector3.one
            _core = GameObject.FindGameObjectWithTag("Core").GetComponent<TutorialHologramSecond>();
            _returnPoint = _core.attaches[Random.Range(0, _core.attaches.Count)].transform;
            //gameObject.transform.parent = _returnPoint.transform;
        }

        public void ReturnToCore()
        {
            /*gameObject.transform.parent = null;
            do
            {
                _returnPoint = _core.attaches[Random.Range(0, _core.attaches.Count)].transform;
            } while (_returnPoint.transform.childCount != 0);*/
            _returnPoint = _core.attaches[Random.Range(0, _core.attaches.Count)].transform;
            Instantiate(_virus, gameObject.transform.position, gameObject.transform.rotation);
            //gameObject.transform.parent = _returnPoint.transform;
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

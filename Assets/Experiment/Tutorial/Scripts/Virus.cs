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
            _core = GameObject.FindGameObjectWithTag("Core").GetComponent<TutorialHologramSecond>();
            _returnPoint = _core.attaches[Random.Range(0, _core.attaches.Length)].transform;
        }

        public void ReturnToCore()
        {
            _returnPoint = _core.attaches[Random.Range(0, _core.attaches.Length)].transform;
            Instantiate(_virus, gameObject.transform.position, gameObject.transform.rotation);
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

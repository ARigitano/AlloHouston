using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.Tutorial
{
    public class CorruptedData : MonoBehaviour
    {
        [SerializeField]
        private GameObject _virus;
    // Start is called before the first frame update
    void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDestroy()
        {
            Instantiate(_virus, gameObject.transform.position, gameObject.transform.rotation);
        }
    }
}

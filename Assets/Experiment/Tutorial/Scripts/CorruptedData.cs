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
            StartCoroutine("Division");
        }

        IEnumerator Division()
        {
            int _timer = 5;
            while (_timer > 0)
            {
                yield return new WaitForSeconds(1f);
                _timer--;
                if (_timer == 0)
                {
                    Instantiate(_virus, gameObject.transform.position, gameObject.transform.rotation);
                    _timer = 5;
                }
            }

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDestroy()
        {
            
        }
    }
}

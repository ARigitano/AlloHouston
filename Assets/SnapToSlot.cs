using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRI.HelloHouston.Experience.MAIA;

namespace Valve.VR.InteractionSystem
{
    public class SnapToSlot : MonoBehaviour
    {

        [SerializeField]
        private Transform _originalSlot, _destinationSlot;
        [SerializeField]
        private MaiaHologramTest _station;
       

        // Start is called before the first frame update
        void Start()
        {
            //_station.tubes 
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDetachedFromHand(Hand hand)
        {
            if(_destinationSlot != null)
            {
                gameObject.transform.parent = _destinationSlot;
                gameObject.transform.position = _destinationSlot.position;
                gameObject.transform.rotation = _destinationSlot.rotation;
                _station.LoadingTube();
            } 
            else
            {
                gameObject.transform.parent = _originalSlot;
                gameObject.transform.position = _originalSlot.position;
                gameObject.transform.rotation = _originalSlot.rotation;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "TubeDock" && other.transform.childCount == 0 && !_station.isLoading)
            {
                _destinationSlot = other.transform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "TubeDock")
            {
                _destinationSlot = null;
            }
        }
    }
}

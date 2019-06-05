using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.WindowTemplate
{
    public class UISounds : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _appear;
        [SerializeField]
        private AudioSource _pressed;
        [SerializeField]
        private AudioSource _disappear;

        public void PlayAppear()
        {
            if(_appear.clip != null)
                _appear.Play();
        }

        public void PlayPressed()
        {
            if (_pressed.clip != null)
                _pressed.Play();
        }

        public void PlayDisappear()
        {
            if (_disappear.clip != null)
                _disappear.Play();
        }


    }
}

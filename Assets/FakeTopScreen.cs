using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience
{
    public class FakeTopScreen : XPElement
    {
        [SerializeField]
        private Image _panelImage;


        public override void OnResolved()
        {
            Debug.Log(name + "Resolved");
        }

        public override void OnFailed()
        {

        }

        public override void OnActivated()
        {
            Debug.Log(name + "Activated");
            var tempColor = _panelImage.color;
            tempColor.a = 1f;
            _panelImage.color = tempColor;
        }

        public override void OnPause()
        {

        }

        public override void OnUnpause()
        {

        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

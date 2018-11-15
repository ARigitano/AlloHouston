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
            Debug.Log(name + "Failed");
        }

        public override void OnActivated()
        {
            Debug.Log(name + "Activated");
            ChangeOpacity(1f);
        }

        public override void OnPause()
        {
            Debug.Log(name + "Paused");
            ChangeOpacity(0f);
        }

        public override void OnUnpause()
        {
            Debug.Log(name + "Unpaused");
            ChangeOpacity(1f);
        }

        private void ChangeOpacity(float opacity)
        {
            var tempColor = _panelImage.color;
            tempColor.a = opacity;
            _panelImage.color = tempColor;
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

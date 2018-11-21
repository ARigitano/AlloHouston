using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience
{
    public class FakeTopScreen : XPElement
    {
        [SerializeField]
        private FakeSynchronizer _synchronizer;
        [SerializeField]
        private Image _panelImage;
        [SerializeField]
        private Slider _slider;
        [SerializeField]
        private float _speed = 0.2f;
        [SerializeField]
        private Text _percentage;
        [SerializeField]
        private GameObject _b1A2, _b1A3, _b1A4, _b1A5;

        IEnumerator Loading()
        {
            while (_slider.value <= 1f)
            {
                _slider.value += Time.deltaTime * _speed;
                _percentage.text = Mathf.Round(_slider.value * 100) + "%";
                if (_slider.value >= 0.9f)
                {
                    _slider.value = 1f;
                    _b1A3.SetActive(true);
                    _b1A2.SetActive(false);
                    _synchronizer.SynchronizeScreens("loadingBarFinished");
                }
                yield return null;
            }
        }

        public void ManualOverride()
        {
            _b1A4.SetActive(true);
            _b1A3.SetActive(false);
        }

        public void AccessCode()
        {
            _b1A5.SetActive(true);
            _b1A4.SetActive(false);
        }


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
            StartCoroutine("Loading");
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

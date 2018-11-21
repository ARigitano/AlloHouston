using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience
{
    public class FakeTabletScreen : XPElement
    {
        [SerializeField]
        private FakeSynchronizer _synchronizer;
        [SerializeField]
        private GameObject _panel, _b1C2, _b1C4, _b1C4Left, _b1C5Left;
        [SerializeField]
        private Slider _slider;
        [SerializeField]
        private float _speed = 0.2f;

        IEnumerator FakeLoading()
        {
            float i = 0f;
            while (_slider.value <= 0.75f)
            {
                i += Time.deltaTime * _speed;
                _slider.value = Mathf.Sqrt(i);
                yield return null;
            }
        }

        public void WaitingConfirmation()
        {
            _b1C2.SetActive(true);
            _panel.SetActive(false);
        }

        public void OverrideButtonClicked()
        {
            _synchronizer.SynchronizeScreens("OverrideButtonClicked");
            _b1C5Left.SetActive(true);
            _b1C4Left.SetActive(false);
        }

        public void StartButtonClicked()
        {
            _synchronizer.SynchronizeScreens("StartButtonClicked");
            _b1C4.SetActive(true);
            _b1C2.SetActive(false);
            StartCoroutine("FakeLoading");
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
        }

        public override void OnPause()
        {
            Debug.Log(name + "Paused");
        }

        public override void OnUnpause()
        {
            Debug.Log(name + "Unpaused");
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

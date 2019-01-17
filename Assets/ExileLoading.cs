using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class ExileLoading : MonoBehaviour
    {
        /// <summary>
        /// Script for the whole top screen.
        /// </summary>
        [SerializeField]
        private MAIATopScreen _maiaTopScreen;
        /// <summary>
        /// The loading bar of the splash screen.
        /// </summary>
        [SerializeField]
        private Image _slider;
        /// <summary>
        /// Speed of the loding bar
        /// </summary>
        [SerializeField]
        private float _speed = 0.2f;
        /// <summary>
        /// Text displaying the percentage loaded on the splash screen.
        /// </summary>
        [SerializeField]
        private Text _percentage;
        /// <summary>
        /// Text that displays the loading states of the experiment according to the loading bar progression.
        /// </summary>
        [SerializeField]
        private Text _loadingText;
        /// <summary>
        /// The loading states of the experiment.
        /// </summary>
        [SerializeField]
        private string[] _loadingStrings;
        /// <summary>
        /// Has the experiment finished loading?
        /// </summary>
        [HideInInspector]
        public bool isLoaded = false;

        /// <summary>
        /// Loading delay of the splash screen.
        /// </summary>
        /// <returns></returns>
        IEnumerator Loading()
        {
            if (!isLoaded)
            {
                while (_slider.fillAmount < 1f)
                {
                    _slider.fillAmount += Time.deltaTime * _speed;
                    _percentage.text = Mathf.Round(_slider.fillAmount * 100) + "%";

                    if (_slider.fillAmount * 10 <= _loadingStrings.Length)
                    {
                        _loadingText.text = _loadingStrings[Mathf.FloorToInt(_slider.fillAmount * 10)];
                    }
                    if (_slider.fillAmount >= 0.9f)
                    {
                        //TODO:Change protection levels or rewrite
                        _slider.fillAmount = 1f;
                        //_maiaTopScreen._maiaLoadingScreen.SetActive(true);
                        //_maiaTopScreen._currentPanel = _maiaLoadingScreen;
                        //_maiaTopScreen._exileLoadingScreen.SetActive(false);
                        isLoaded = true;
                        //_maiaTopScreen._manager.LoadingBarFinished();
                        yield return null;
                    }
                }
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

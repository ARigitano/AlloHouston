using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class MAIAOverview : MonoBehaviour
    {
        /// <summary>
        /// Script for the whole top screen.
        /// </summary>
        [SerializeField]
        [Tooltip("Script for the whole top screen.")]
        private MAIATopScreen _maiaTopScreen;
        /// <summary>
        /// Long error string to be displayed in a scrolling manner.
        /// </summary>
        [SerializeField]
        [Tooltip("Long error string to be displayed in a scrolling manner.")]
        private string _scrollingText = null;
        /// <summary>
        /// Text that displays the scrolling error.
        /// </summary>
        [SerializeField]
        [Tooltip("Text that displays the scrolling error.")]
        private Text _scrollError = null;
        /// <summary>
        /// Crash popup.
        /// </summary>
        [SerializeField]
        [Tooltip("Crash popup.")]
        private GameObject _popupCrash = null;
        /// <summary>
        /// Error popup.
        /// </summary>
        [SerializeField]
        [Tooltip("Error popup.")]
        private GameObject _popupErrorMessage = null;
        /// <summary>
        /// Info message.
        /// </summary>
        [SerializeField]
        [Tooltip("Info message popup.")]
        private GameObject _popupInfoMessage = null;

        //TO DO: find better way of changing panel
        /// <summary>
        /// Displays the manual override screen when the start button is pressed.
        /// </summary>
        public void ManualOverride()
        {
            //TODO: rewrite
            /*_maiaLoadingScreen.SetActive(false);
            _maiaOverviewScreen.SetActive(true);
            _currentPanel = _maiaOverviewScreen;*/
            _popupErrorMessage.SetActive(true);
            _popupInfoMessage.SetActive(true);
            //_manager.ManualOverrideActive();
            
        }

        /// <summary>
        /// Displays a long scrolling error.
        /// </summary>
        /// <returns></returns>
        IEnumerator ScrollingError()
        {
            int i = 0;

            while (i < _scrollingText.Length)
            {
                _scrollError.text += _scrollingText[i++];
                yield return new WaitForSeconds(0.0001f);
            }
        }
    }
}

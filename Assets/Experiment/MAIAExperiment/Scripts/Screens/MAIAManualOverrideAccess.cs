using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class MAIAManualOverrideAccess : MonoBehaviour
    {
        /// <summary>
        /// Script for the whole top screen.
        /// </summary>
        [SerializeField]
        [Tooltip("Script for the whole top screen.")]
        private MAIATopScreen _maiaTopScreen = null;
        /// <summary>
        /// Error popup.
        /// </summary>
        [SerializeField]
        [Tooltip("Error popup.")]
        private GameObject _errorMessage = null;
        /// <summary>
        /// Info message.
        /// </summary>
        [SerializeField]
        [Tooltip("Info message popup.")]
        private GameObject _infoMessage = null;
        /// <summary>
        /// Access granted popup.
        /// </summary>
        [SerializeField]
        [Tooltip("Access granted popup.")]
        private GameObject _popupAccessGranted = null;
        /// <summary>
        /// Access denied error popup.
        /// </summary>
        [SerializeField]
        [Tooltip("Access denied error popup.")]
        private GameObject _popupErrorAccessDenied = null;
        /// <summary>
        /// The password window.
        /// </summary>
        [SerializeField]
        [Tooltip("The password window.")]
        private GameObject _passwordWindow = null;
        /// <summary>
        /// Password input transform
        /// </summary>
        [SerializeField]
        [Tooltip("Password input transform.")]
        private Transform _passwordBox = null;
        /// <summary>
        /// Input box prefab.
        /// </summary>
        [SerializeField]
        [Tooltip("Input box prefab.")]
        private GameObject _inputBoxPrefab = null;
        /// <summary>
        /// Slots to enter the numbers for the password.
        /// </summary>
        [SerializeField]
        [Tooltip("Slots to enter the numbers for the password")]
        private GameObject[] _passwordSlots = null;
        [SerializeField]
        [Tooltip("Wait time until error message.")]
        private float _waitTimeBeforeErrorMessage = 2.0f;
        [SerializeField]
        [Tooltip("Wait time until info message.")]
        private float _waitTimeBeforeInfoMessage = 2.0f;

        private string _realPassword;

        public void DisplayPassword(string password)
        {
            _realPassword = password;
            _passwordSlots = new GameObject[password.Length];
            for (int i = 0; i < password.Length; i++)
                _passwordSlots[i] = GameObject.Instantiate(_inputBoxPrefab, _passwordBox);
            _passwordWindow.SetActive(true);
        }

        /// <summary>
        /// Displays the pasword that is being entered.
        /// </summary>
        /// <param name="password">The password being entered.</param>
        public bool CheckPassword(string password)
        {
            bool res = false;
            for (int i = 0; i < _passwordSlots.Length; i++)
                _passwordSlots[i].GetComponentInChildren<Image>().enabled = i < password.Length;
            if (password.Length == _realPassword.Length)
            {
                res = password == _realPassword;
                Access(res);
            }
            return res;
        }

        /// <summary>
        /// Waiting delay when access granted.
        /// </summary>
        /// <returns></returns>
        IEnumerator WaitCorrect()
        {        
            yield return new WaitForSeconds(2);
            _popupAccessGranted.SetActive(false);
            _maiaTopScreen.AccessGranted();
        }

        /// <summary>
        /// Waiting delay when access denied.
        /// </summary>
        /// <returns></returns>
        IEnumerator WaitDenied()
        {
            yield return new WaitForSeconds(2);
            _popupErrorAccessDenied.SetActive(false);
            for (int i = 0; i < _passwordSlots.Length; i++)
            {
                _passwordSlots[i].GetComponentInChildren<Image>().enabled = false;
            }
        }

        /// <summary>
        /// Decides what to display depending on the password entered.
        /// </summary>
        /// <param name="access"></param>
        public void Access(bool access)
        {
            if (access)
            {
                _popupAccessGranted.SetActive(true);
                StartCoroutine(WaitCorrect());
            }
            else
            {
                _popupErrorAccessDenied.SetActive(true);
                StartCoroutine(WaitDenied());
            }
        }

        private IEnumerator ShowAnimation()
        {
            yield return new WaitForSeconds(_waitTimeBeforeErrorMessage);
            _errorMessage.SetActive(true);
            yield return new WaitForSeconds(_waitTimeBeforeInfoMessage);
            _infoMessage.SetActive(true);
            _maiaTopScreen.ActivateManualOverride();
        }

        //TODO: remove when windows class integrated
        /// <summary>
        /// Displays the manual override screen when the start button is pressed.
        /// </summary>
        public void Show()
        {
            StartCoroutine(ShowAnimation());
        }
    }
}

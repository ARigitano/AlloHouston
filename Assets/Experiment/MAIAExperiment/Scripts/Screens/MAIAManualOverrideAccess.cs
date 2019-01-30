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
        /// Star image displayed when a password numbered has been entered.
        /// </summary>
        [SerializeField]
        [Tooltip("Star image displayed when a password numbered has been entered.")]
        private Sprite _starPassword = null;
        /// <summary>
        /// Cursor image to display the numbers yet to be entered for the password.
        /// </summary>
        [SerializeField]
        [Tooltip("Cursor image to display the numbers yet to be entered for the password.")]
        private Sprite _cursorPassword = null;
        /// <summary>
        /// Slots to enter the numbers for the password.
        /// </summary>
        [SerializeField]
        [Tooltip("Slots to enter the numbers for the password")]
        private GameObject[] _slotPassword = null;
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
        /// Displays the pasword that is being entered.
        /// </summary>
        /// <param name="password">The password being entered.</param>
        public bool CheckPassword(string password)
        {
            bool res = false;
            for (int i = 0; i < password.Length; i++)
            {
                _slotPassword[i].GetComponent<Image>().sprite = _starPassword;
            }
            string realPassword = _maiaTopScreen.manager.settings.password;
            if (password.Length == realPassword.Length)
            {
                res = password == realPassword;
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
            for (int i = 0; i < _slotPassword.Length; i++)
            {
                _slotPassword[i].GetComponent<Image>().sprite = _cursorPassword;
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
    }
}

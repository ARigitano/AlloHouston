using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        /// Grid cell prefab.
        /// </summary>
        [SerializeField]
        [Tooltip("Grid cell prefab.")]
        private GridCell _gridCellPrefab = null;
        /// <summary>
        /// Slots to enter the numbers for the password.
        /// </summary>
        private GridCell[] _passwordSlots = null;
        /// <summary>
        /// Wait time until error message.
        /// </summary>
        [SerializeField]
        [Tooltip("Wait time until error message.")]
        private float _waitTimeBeforeErrorMessage = 2.0f;
        /// <summary>
        /// Wait time until info message.
        /// </summary>
        [SerializeField]
        [Tooltip("Wait time until info message.")]
        private float _waitTimeBeforeInfoMessage = 2.0f;

        private string _realPassword;

        private bool _interactable = true;

        public void InitPasswordInput(string password)
        {
            _realPassword = password;
            for (int i = 0; _passwordSlots != null && i < _passwordSlots.Length; i++)
                Destroy(_passwordSlots[i].gameObject);
            _passwordSlots = new GridCell[password.Length];
            for (int i = 0; i < password.Length; i++)
                _passwordSlots[i] = GameObject.Instantiate(_gridCellPrefab, _passwordBox);
            _passwordWindow.SetActive(true);
        }

        /// <summary>
        /// Displays the pasword that is being entered.
        /// </summary>
        /// <param name="password">The password being entered.</param>
        /// <returns>False if the password is already being checked.</returns>
        public bool CheckPasswordInput(string password)
        {
            bool res = false;
            if (_interactable)
            {
                for (int i = 0; i < _passwordSlots.Length; i++)
                    _passwordSlots[i].Show(i < password.Length);
                if (password.Length == _realPassword.Length)
                {
                    Access(password == _realPassword);
                }
                res = true;
            }
            return res;
        }

        /// <summary>
        /// Waiting delay when access granted.
        /// </summary>
        /// <returns></returns>
        IEnumerator WaitCorrect()
        {
            _interactable = false;
            yield return new WaitForSeconds(2);
            _popupAccessGranted.SetActive(false);
            _maiaTopScreen.maiaManager.OnPasswordSuccess();
        }

        /// <summary>
        /// Waiting delay when access denied.
        /// </summary>
        /// <returns></returns>
        IEnumerator WaitDenied()
        {
            _interactable = false;
            yield return new WaitForSeconds(2);
            _popupErrorAccessDenied.SetActive(false);
            for (int i = 0; i < _passwordSlots.Length; i++)
                _passwordSlots[i].Show(false);
            _interactable = true;
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
            _maiaTopScreen.maiaManager.OnLoadingSuccess();
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

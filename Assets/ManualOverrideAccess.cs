using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class ManualOverrideAccess : MonoBehaviour
    {
        /// <summary>
        /// Script for the whole top screen.
        /// </summary>
        [SerializeField]
        private MAIATopScreen _maiaTopScreen;
        [SerializeField]
        /// <summary>
        /// Star image displayed when a password numbered has been entered.
        /// </summary>
        private Sprite _starPasswword,
                       /// <summary>
                       /// Cursor image to display the numbers yet to be entered for the password.
                       /// </summary>
                       _cursorPassword;
        /// <summary>
        /// Slots to enter the numbers for the password.
        /// </summary>
        [SerializeField]
        private GameObject[] _slotPassword;
        /// <summary>
        /// Access password popups
        /// </summary>
        [SerializeField]
        private GameObject _popupAccessGranted, _popupErrorAccessDenied;

        /// <summary>
        /// Displays the pasword that is being entered.
        /// </summary>
        /// <param name="password">The password being entered.</param>
        public void DisplayPassword(string password)
        {
            for (int i = 0; i < password.Length; i++)
            {
                _slotPassword[i].GetComponent<SpriteRenderer>().sprite = _starPasswword;
            }
        }

        /// <summary>
        /// Waiting delay when access granted.
        /// </summary>
        /// <returns></returns>
        IEnumerator WaitCorrect()
        {
            //TODO:rewrite
            yield return new WaitForSeconds(2);
            /*_popupAccessGranted.SetActive(false);
            _manualOverride1.SetActive(true);
            _currentPanel = _manualOverride1;
            _manualOverrideAccess.SetActive(false);
            _manager.AccessGranted();*/
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
                _slotPassword[i].GetComponent<SpriteRenderer>().sprite = _cursorPassword;
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

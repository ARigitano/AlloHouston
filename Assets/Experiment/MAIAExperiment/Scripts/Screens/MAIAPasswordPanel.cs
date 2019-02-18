using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class MAIAPasswordPanel : MonoBehaviour
    {
        /// <summary>
        /// Password entered by the player.
        /// </summary>
        private string _enteredPassword = "";

        private string _realPassword;

        private bool _isTouched;

        private MAIAManualOverrideAccess _moaScreen;


        /// <summary>
        /// Adds a number to the password.
        /// </summary>
        /// <param name="character">The character to add.</param>
        public void EnteringDigit(string character)
        {
            if (!_isTouched)
            {
                Debug.Log(character);
                _isTouched = true;
                _enteredPassword += character.ToString();
                bool interactable = _moaScreen.CheckPasswordInput(_enteredPassword);
                if (_enteredPassword.Length == _realPassword.Length || !interactable)
                    _enteredPassword = "";
                StartCoroutine(WaitButton());
            }
        }

        IEnumerator WaitButton()
        {
            yield return new WaitForSeconds(0.5f);
            _isTouched = false;
        }

        public void Init(MAIAManualOverrideAccess manualOverrideAccessScreen, string realPassword)
        {
            _realPassword = realPassword;
            _moaScreen = manualOverrideAccessScreen;
        }
    }
}

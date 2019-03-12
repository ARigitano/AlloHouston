using CRI.HelloHouston.WindowTemplate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class MAIAPasswordPanel : Window
    {
        /// <summary>
        /// Password entered by the player.
        /// </summary>
        private string _enteredPassword = "";

        private string _realPassword;

        private bool _isTouched;

        private MAIAManualOverrideAccess _moaScreen;

        private LogExperienceController _logController;

        private XPContext _xpContext;
        
        private void OnDisable()
        {
            _enteredPassword = "";
        }

        /// <summary>
        /// Adds a number to the password.
        /// </summary>
        /// <param name="character">The character to add.</param>
        public void EnteringDigit(string character)
        {
            _logController.AddLogInput("Input Password: " + character, _xpContext);
            _enteredPassword += character.ToString();
            bool interactable = _moaScreen.CheckPasswordInput(_enteredPassword);
            if (_enteredPassword.Length == _realPassword.Length || !interactable)
                _enteredPassword = "";
        }

        public void Init(LogExperienceController logController, XPContext context, MAIAManualOverrideAccess manualOverrideAccessScreen, string realPassword)
        {
            _logController = logController;
            _xpContext = context;
            _realPassword = realPassword;
            _moaScreen = manualOverrideAccessScreen;
        }
    }
}

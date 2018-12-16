using CRI.HelloHouston.Experience;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.ParticlePhysics
{
    /// <summary>
    /// The top left screen of the experiment block for the particle physics experiment.
    /// </summary>
    public class FakeTopScreen : XPElement
    {
        /// <summary>
        /// The synchronizer of the experiment.
        /// </summary>
        [SerializeField]
        private FakeSynchronizer _synchronizer;
        /// <summary>
        /// TODO: the current panel displayed on the screen.
        /// </summary>
        [SerializeField]
        private Image _panelImage;
        /// <summary>
        /// The loading bar of the splash screen.
        /// </summary>
        [SerializeField]
        private Image _slider;
        [SerializeField]
        private float _speed = 0.2f;
        [SerializeField]
        /// <summary>
        /// Text displaying the percentage loaded on the splash screen.
        /// </summary>
        private Text _percentage,
        /// <summary>
        /// Text displaying the password entered on the access screen.
        /// </summary>
                    _passwordText,
        /// <summary>
        /// Text displaying the particles entered on the particle identification screen.
        /// </summary>
                   _particlesText;
        /// <summary>
        /// All the panels of the top left screen of the experiment.
        /// </summary>
        [SerializeField]
        private GameObject _Exile_Loading_Screen, _MAIA_Loading_Screen, _MAIA_overview, _Manual_override_access, _Popup_Access_granted, _PopupError_Access_denied, _Manual_override_1, _b1A7bis, _PopupError_Message_Particles;
        [SerializeField]
        private Text _loadingText;
        [SerializeField]
        private string[] _loadingStrings;
        [SerializeField]
        private GameObject _particlesGrid;
        [SerializeField]
        private GameObject _gridCase;

        public void ParticleGrid(List<Particle> particles)
        {
            foreach (Particle particle in particles)
            {
                Instantiate(_gridCase, _particlesGrid.transform.position, _particlesGrid.transform.rotation, _particlesGrid.transform);
            }
        }

        /// <summary>
        /// Displays a popup if the correct combination of particles has been entered.
        /// </summary>
        public void CorrectParticle()
        {
            _b1A7bis.SetActive(true);
        }

        /// <summary>
        /// Displays a popup if an incorrect combination of particles has been entered.
        /// </summary>
        public void IncorrectParticle()
        {
            _PopupError_Message_Particles.SetActive(true);
        }

        /// <summary>
        /// Displays the pasword that is being entered.
        /// </summary>
        /// <param name="password">The password being entered.</param>
        public void DisplayPassword(string password)
        {
            string displayedPassword = "";

            for (int i = 0; i<password.Length; i++)
            {
                displayedPassword += "*";
            }

            while(displayedPassword.Length<4)
            {
                displayedPassword += "-";
            }

            _passwordText.text = "[" + displayedPassword + "]";
        }

        /// <summary>
        /// Waiting delay when access granted.
        /// </summary>
        /// <returns></returns>
        IEnumerator WaitCorrect()
        {
            yield return new WaitForSeconds(2);
            _Popup_Access_granted.SetActive(false);
            _synchronizer.SynchronizeScreens("AccessGranted");
            _Manual_override_1.SetActive(true);
            _Manual_override_access.SetActive(false);
        }

        /// <summary>
        /// Waiting delay when access denied.
        /// </summary>
        /// <returns></returns>
        IEnumerator WaitDenied()
        {
            yield return new WaitForSeconds(2);
            _PopupError_Access_denied.SetActive(false);
            _passwordText.text = "[----]";
        }

        //TO MODIFY: Cases instead of field.
        /// <summary>
        /// Displays the particles combination while they are being opened.
        /// </summary>
        /// <param name="particles">The particles combination that is being entered.</param>
        public void DisplayParticles(List<string> particles)
        {
            string displayedParticles = "";

            for (int i = 0; i<particles.Count; i++)
            {
                if(particles[i] != "")
                {
                    displayedParticles += particles[i];
                }
                else
                {
                    displayedParticles += ".";
                }
                _particlesText.text = displayedParticles;
            }
        }

        /// <summary>
        /// Decides what to display depending on the password entered.
        /// </summary>
        /// <param name="access"></param>
        public void Access(bool access)
        {
            if(access)
            {
                _Popup_Access_granted.SetActive(true);
                StartCoroutine(WaitCorrect());
                

            } else
            {
                _PopupError_Access_denied.SetActive(true);
                StartCoroutine(WaitDenied());
                
                
            }
        }

        /// <summary>
        /// Loading delay of the splash screen.
        /// </summary>
        /// <returns></returns>
        IEnumerator Loading()
        {
            while (_slider.fillAmount <= 1f)
            {
                _slider.fillAmount += Time.deltaTime * _speed;
                _percentage.text = Mathf.Round(_slider.fillAmount * 100) + "%";

                if(_slider.fillAmount*10 <= _loadingStrings.Length)
                {
                    _loadingText.text = _loadingStrings[Mathf.FloorToInt(_slider.fillAmount * 10)];
                }
                if(_slider.fillAmount >=0.9f)
                {
                    _slider.fillAmount = 1f;
                    _MAIA_Loading_Screen.SetActive(true);
                    _Exile_Loading_Screen.SetActive(false);
                    _synchronizer.SynchronizeScreens("loadingBarFinished");
                }
                yield return null;
            }
        }

        //TO DO: find better way of changing panel
        /// <summary>
        /// Displays the manual override screen when the start button is pressed.
        /// </summary>
        public void ManualOverride()
        {
            _MAIA_overview.SetActive(true);
            _MAIA_Loading_Screen.SetActive(false);
        }

        /// <summary>
        /// Displays the access screen when the manual override button is pressed.
        /// </summary>
        public void AccessCode()
        {
            _Manual_override_access.SetActive(true);
            _MAIA_overview.SetActive(false);
        }
        //TO DO
        /// <summary>
        /// Effect when the experiment is correctly resolved.
        /// </summary>
        public override void OnResolved()
        {
            Debug.Log(name + "Resolved");
        }
        //TO DO
        /// <summary>
        /// Effect when the experiment is failed.
        /// </summary>
        public override void OnFailed()
        {
            Debug.Log(name + "Failed");
        }

        /// <summary>
        /// Effect when the experiment is activated the first time.
        /// </summary>
        public override void OnActivated()
        {
            Debug.Log(name + "Activated");
            ChangeOpacity(1f);
            StartCoroutine("Loading");
        }
        
        /// <summary>
        /// Effect when the experiment is paused.
        /// </summary>
        public override void OnPause()
        {
            Debug.Log(name + "Paused");
            ChangeOpacity(0f);
        }
        
        /// <summary>
        /// Effect when the experiment is unpaused.
        /// </summary>
        public override void OnUnpause()
        {
            Debug.Log(name + "Unpaused");
            ChangeOpacity(1f);
        }

        /// <summary>
        /// Changes the opacity of the screen.
        /// </summary>
        /// <param name="opacity">Opacity value the screen should change to.</param>
        private void ChangeOpacity(float opacity)
        {
            var tempColor = _panelImage.color;
            tempColor.a = opacity;
            _panelImage.color = tempColor;
        }
    }
}

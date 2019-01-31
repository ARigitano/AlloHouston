using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

namespace CRI.HelloHouston.Experience.MAIA
{
    /// <summary>
    /// The tablet screen of the experiment block for the particle physics experiment.
    /// </summary>
    public class MAIATabletScreen : XPElement
    {
        private MAIAManager _manager;
        /// <summary>
        /// All the panels for the tablet screen.
        /// </summary>
        [SerializeField]
        private GameObject _startFull, _panelFull, _overrideLeft, _passwordLeft, _particlesLeft, _diagramsBrowsingRight, _diagramsSelectionLeft;
        /// <summary>
        /// Loading bar to display the time remaining.
        /// </summary>
        [SerializeField]
        private Slider _slider = null;
        /// <summary>
        /// Speed of the time remaining loading bar.
        /// </summary>
        [SerializeField]
        private float _speed = 0.2f;
        /// <summary>
        /// To check if a button have been pressed by the VR controller.
        /// </summary>
        private bool _isTouched = false;
        /// <summary>
        /// Stores the panels currently being displayed.
        /// </summary>
        private GameObject _currentPanelLeft, _currentPanelRight, _currentPanel;
        private MAIATopScreen _topScreen;
        public MAIAHologram hologram;
        public MAIATubeScreen tubeScreen;
        /// <summary>
        /// Password entered by the player.
        /// </summary>
        private string _enteredPassword = "";
        /// <summary>
        /// The particles entered by the player.
        /// </summary>
        [HideInInspector]
        private List<Particle> _enteredParticles = new List<Particle>();
        /// <summary>
        /// Particle identification screen.
        /// </summary>
        [SerializeField]
        [Tooltip("Particle identification screen.")]
        private ParticlesIdentification _particleIdentificationScreen;

        /// <summary>
        /// Tells the main screen that a particle has been entered.
        /// </summary>
        public void EnteringParticles()
        {
            _topScreen.DisplayParticles(_enteredParticles);
        }

        public void DeleteParticle()
        {
            if (!_isTouched)
            {
                _isTouched = true;
                _enteredParticles.RemoveAt(_enteredParticles.Count - 1);
                _particleIdentificationScreen.DeleteParticle(_enteredParticles.Count);
                _particleIdentificationScreen.FillNbParticlesDetected(_manager.generatedParticles, _enteredParticles);
                StartCoroutine("WaitButton");
            }
        }

        /// <summary>
        /// Clears the particles combination entered.
        /// </summary>
        public void ClearParticles()
        {
            _enteredParticles.Clear();
            _particleIdentificationScreen.ClearParticles();
            _particleIdentificationScreen.FillNbParticlesDetected(_manager.generatedParticles, _enteredParticles);
        }

        /// <summary>
        /// Called by the synchronizer to skip directly to the Feynman diagrams step.
        /// </summary>
        public void SkipStepOne()
        {
            if (_currentPanel != null)
                _currentPanel.SetActive(false);

            _overrideLeft.SetActive(false);

            if (_currentPanelLeft != null)
            {
                _currentPanelLeft.SetActive(false);
            }

            if (_currentPanelRight != null)
                _currentPanelRight.SetActive(false);

            _panelFull.SetActive(true);
            _diagramsBrowsingRight.SetActive(true);
            _diagramsSelectionLeft.SetActive(true);
        }

        /// <summary>
        /// Activates the manual override panel.
        /// </summary>
        public void ManualOverride()
        {
            _overrideLeft.SetActive(true);
        }

        private void Start()
        {
            _currentPanel = _startFull;
        }

        /// <summary>
        /// A generic coroutine to wait during a method.
        /// </summary>
        /// <param name="time">The time to wait.</param>
        /// <param name="action">The action to be done after the waiting delay is over.</param>
        /// <returns></returns>
        IEnumerator WaitGeneric(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action.Invoke();
        }

        //TODO: check if useful
        /// <summary>
        /// Selects a reaction.
        /// </summary>
        public void SelectReaction()
        {
            ReactionSelected();
        }

        /// <summary>
        /// Activates the reaction selection panels.
        /// </summary>
        public void OverrideSecond()
        {
            _diagramsBrowsingRight.SetActive(true);
            _currentPanelRight = _diagramsBrowsingRight;
            _diagramsSelectionLeft.SetActive(true);
            _currentPanelLeft = _diagramsSelectionLeft;
            _particlesLeft.SetActive(false);
        }

        /// <summary>
        /// Sends an error message to the top screen.
        /// </summary>
        public void ParticleSendErrorMessage(string particleErrorString)
        {
            _particleIdentificationScreen.DisplayErrorMessage(particleErrorString);
        }
        
        //TODO: replace by a generic wait
        IEnumerator WaitButton()
        {
            yield return new WaitForSeconds(0.5f);
            _isTouched = false;
        }

        //TODO:redistribute
        /// <summary>
        /// Fake delay that can never be attained.
        /// </summary>
        /// <returns>null</returns>
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

        /// <summary>
        /// Tells every screen that the right combination of particles has been entered.
        /// </summary>
        public void ParticleRightCombination()
        {
            _topScreen.OverrideSecond();
            OverrideSecond();
            //Disabled for the demo version
            //_tubeScreen.OverrideSecond(_tabletScreen._allReactions);
        }

        /// <summary>
        /// Submits the particles combination entered.
        /// </summary>
        public void SubmitParticles()
        {
            //Checks if the combination entered has the right number of particles.
            if (_enteredParticles.Count == _manager.generatedParticles.Count)
            {
                var l1 = _enteredParticles.OrderBy(particle => particle.name);
                var l2 = _manager.generatedParticles.OrderBy(particle => particle.name);
                // Checks if the right symbols have been entered.
                if (l1.Select(particle => particle.name).SequenceEqual(l2.Select(particle => particle.name)))
                {
                    //Check if the right symbols have been entered.
                    if (l1.SequenceEqual(l2))
                    {
                        //The right combination of particles have been entered.
                        Debug.Log("The right combination of particles have been entered.");
                        ParticleRightCombination();
                    }
                    else
                    {
                        //A wrong combination of charges have been entered.
                        Debug.Log("A wrong combination of charges have been entered.");
                        ParticleSendErrorMessage("WRONG NUMBER OF CHARGES!");
                    }
                }
                else
                {
                    //A wrong combination of symbols have been entered.
                    Debug.Log("A wrong combination of symbols have been entered.");
                    ParticleSendErrorMessage("WRONG PARTICLES!");
                }
            }
            else
            {
                //A combination of particles with a wrong length has been entered.
                Debug.Log("A combination of particles with a wrong length has been entered.");
                ParticleSendErrorMessage("WRONG NUMBER OF PARTICLES!");
            }
        }

        /// <summary>
        /// Tells the main screen that a reaction has been selected.
        /// </summary>
        public void ReactionSelected()
        {
            _topScreen.ReactionSelected(_manager.selectedReaction, tubeScreen.diagramSelected);
        }

        /// <summary>
        /// Adds a particle to the combination.
        /// </summary>
        /// <param name="particleButton">The particle to add.</param>
        public void EnteringParticle(Particle particle)
        {
            if (!_isTouched && _enteredParticles.Count < _manager.generatedParticles.Count)
            {
                _isTouched = true;
                _enteredParticles.Add(particle);
                _particleIdentificationScreen.FillNbParticlesDetected(_manager.generatedParticles, _enteredParticles);
                StartCoroutine("WaitButton");
            }
        }

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
                string realPassword = _manager.settings.password;
                _enteredPassword += character.ToString();
                bool interactable = _topScreen.CheckPassword(_enteredPassword);
                if (_enteredPassword.Length == realPassword.Length || !interactable)
                    _enteredPassword = "";
                StartCoroutine("WaitButton");
            }
        }
        /// <summary>
        /// Displays particle selection panel after the correct password have been entered.
        /// </summary>
        public void AccessGranted()
        {
            _passwordLeft.SetActive(false);
            _particlesLeft.SetActive(true);
            _currentPanelLeft = _particlesLeft;
        }

        /// <summary>
        /// Displays start panel after the splash screen has finished loading.
        /// </summary>
        public void WaitingConfirmation()
        {
            _startFull.SetActive(true);
            _currentPanel = _startFull;
        }
        /// <summary>
        /// Displays password panel adter override button has been clicked.
        /// </summary>
        public void OverrideButtonClicked()
        {
            _topScreen.AccessCode();
            StartCoroutine(WaitGeneric(0.2f, () =>
            {
                _passwordLeft.SetActive(true);
                _currentPanelLeft = _passwordLeft;
                _overrideLeft.SetActive(false);
            }));

        }

        private void Init(MAIAManager manager)
        {
            _manager = manager;
            _topScreen = manager.topScreen;
            _particleIdentificationScreen = _topScreen.particleIdentificationScreen;
        }

        public override void OnActivation(XPManager synchronizer)
        {
            base.OnActivation(synchronizer);
            Init((MAIAManager)_manager);
        }
    }
}

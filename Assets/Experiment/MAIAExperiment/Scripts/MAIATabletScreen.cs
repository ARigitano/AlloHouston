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

        private MAIATopScreen _topScreen;

        private ParticlesIdentification _particleIdentificationScreen;

        private MAIAManualOverrideAccess _manualOverrideAccessScren;
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
        /// <summary>
        /// The particles entered by the player.
        /// </summary>
        private List<Particle> _enteredParticles = new List<Particle>();

        private string _enteredPassword = "";

        public void DeleteParticle()
        {
            if (!_isTouched)
            {
                _isTouched = true;
                _enteredParticles.RemoveAt(_enteredParticles.Count - 1);
                _manager.DeleteParticle();
                StartCoroutine("WaitButton");
            }
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

        /// <summary>
        /// Selects a reaction.
        /// </summary>
        public void SelectReaction()
        {
            _manager.ReactionSelected();
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
        /// Displays the next Feynman diagram after the right button is pressed.
        /// </summary>
        public void NextDiagram()
        {
            if (!_isTouched)
            {
                _isTouched = true;
                
                if (_manager.displayedDiagram < _manager.settings.allReactions.Length - 1)
                {
                    _manager.displayedDiagram++;
                   
                } else
                {
                    _manager.displayedDiagram = 0;
                }
                _manager.OtherDiagram();
                StartCoroutine("WaitButton");
            }
        }


        /// <summary>
        /// Displays the previous Feynman diagram after the left diagram is pressed.
        /// </summary>
        public void PreviousDiagram()
        {
            if (!_isTouched)
            {
                _isTouched = true;
               
                if (_manager.displayedDiagram > 0)
                {
                    _manager.displayedDiagram--;

                    
                } else
                {
                    _manager.displayedDiagram = _manager.settings.allReactions.Length-1;
                }
                _manager.OtherDiagram();
                StartCoroutine("WaitButton");
            }
        }

        /// <summary>
        /// Display a symbol on the current Feynman diagram to preselect it based on its exits.
        /// </summary>
        public void PreselectExits()
        {
            if (!_isTouched)
            {
                _isTouched = true;

                _manager.SelectExit();

                StartCoroutine("WaitButton");
            }
        }

        /// <summary>
        /// Display a symbol on the current Feynman diagram to preselect it based on its interactions.
        /// </summary>
        public void PreselectInteraction()
        {
            if (!_isTouched)
            {
                _isTouched = true;

                _manager.SelectInteraction();

                StartCoroutine("WaitButton");
            }
        }

        //TODO: replace by a generic wait
        IEnumerator WaitButton()
        {
            yield return new WaitForSeconds(0.5f);
            _isTouched = false;
        }

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
        /// Clears the particles combination entered.
        /// </summary>
        public void ClearParticles()
        {
            _enteredParticles.Clear();
            _manager.ClearParticles();
        }

        /// <summary>
        /// Submits the particles combination entered.
        /// </summary>
        public void SubmitParticles()
        {
            //Checks if the combination entered has the right number of particles.
            if (_enteredParticles.Count == _manager.producedParticles.Count)
            {
                var l1 = _enteredParticles.OrderBy(particle => particle.name);
                var l2 = _manager.producedParticles.OrderBy(particle => particle.name);
                // Checks if the right symbols have been entered.
                if (l1.Select(particle => particle.name).SequenceEqual(l2.Select(particle => particle.name)))
                {
                    //Check if the right symbols have been entered.
                    if (l1.SequenceEqual(l2))
                    {
                        //The right combination of particles have been entered.
                        Debug.Log("The right combination of particles have been entered.");
                        //_manager.ParticleRightCombination();
                    }
                    else
                    {
                        //A wrong combination of charges have been entered.
                        Debug.Log("A wrong combination of charges have been entered.");
                        //_manager.particleErrorString = "WRONG NUMBER OF CHARGES!";
                        //_manager.ParticleWrongCharge();
                    }
                }
                else
                {
                    //A wrong combination of symbols have been entered.
                    Debug.Log("A wrong combination of symbols have been entered.");
                    //_manager.particleErrorString = "WRONG PARTICLES!";
                    //_manager.ParticleWrongSymbol();
                }
            }
            else
            {
                //A combination of particles with a wrong length has been entered.
                Debug.Log("A combination of particles with a wrong length has been entered.");
                //_manager.particleErrorString = "WRONG NUMBER OF PARTICLES!";
                //_manager.ParticleWrongLength();
            }
        }

        /// <summary>
        /// Adds a particle to the combination.
        /// </summary>
        /// <param name="particleButton">The particle to add.</param>
        public void EnteringParticle(Particle particle)
        {
            if (!_isTouched && _enteredParticles.Count < _manager.producedParticles.Count)
            {
                _isTouched = true;
                _enteredParticles.Add(particle);
                _particleIdentificationScreen.DisplayParticles(_enteredParticles);
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
                if (_enteredPassword.Length < realPassword.Length)
                {
                    _enteredPassword += character.ToString();
                    _manualOverrideAccessScren.CheckPassword(_enteredPassword);
                }
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
            _manager.OverrideButtonClicked();
            StartCoroutine(WaitGeneric(0.2f, () =>
            {
                _passwordLeft.SetActive(true);
                _currentPanelLeft = _passwordLeft;
                _overrideLeft.SetActive(false);
            }));

        }
        /// <summary>
        /// Displays override panel after start button has been clicked.
        /// </summary>
        public void StartButtonClicked()
        {
            Debug.Log("StartButtonClicked");
            _manager.StartButtonClicked();
            StartCoroutine(WaitGeneric(0.2f, () =>
            {
                _panelFull.SetActive(true);
                _currentPanel = _panelFull;
                _startFull.SetActive(false);
                StartCoroutine("FakeLoading");
            }));
        }

        private void Init(MAIAManager manager)
        {
            _manager = manager;
        }

        public override void OnActivation(XPManager manager)
        {
            Init((MAIAManager)manager);
        }
    }
}

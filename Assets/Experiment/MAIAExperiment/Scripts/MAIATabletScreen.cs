using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

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
        public MAIATopScreen topScreen;
        public MAIAHologram hologram;
        public MAIATubeScreen tubeScreen;
        /// <summary>
        /// Password entered by the player.
        /// </summary>
        [HideInInspector]
        public string enteredPassword;
        /// <summary>
        /// The particles entered by the player.
        /// </summary>
        [HideInInspector]
        public List<Particle> enteredParticles = new List<Particle>();

        /// <summary>
        /// Tells the main screen to clear all the entered particles.
        /// </summary>


        /// <summary>
        /// Tells the tube screen to display another Feynman diagram.
        /// </summary>
        public void OtherDiagram()
        {
            //TODO: rewrite
            //tubeScreen.OtherDiagram(displayedDiagram, _tabletSc_allReactions);
        }

        /// <summary>
        /// Tells the tube screen to mark a Feynman diagram for its exits.
        /// </summary>
        public void SelectExit()
        {
            //TODO:rewrite
            //_tubeScreen.SelectExit(_tabletScreen.displayedDiagram);
        }

        /// <summary>
        /// Tells the tube screen to mark a Feynman diagram for its interactions.
        /// </summary>
        public void SelectInteraction()
        {
            //TODO:rewrite
            //_tubeScreen.SelectInteraction(_tabletScreen.displayedDiagram);
        }



        /// <summary>
        /// Tells the main screen that the right password has been entered.
        /// </summary>
        public void CorrectPassword()
        {
            topScreen.Access(true);
        }

        /// <summary>
        /// Tells the main screen that an incorrect password has been entered.
        /// </summary>
        public void IncorrectPassword()
        {
            topScreen.Access(false);
        }

        /// <summary>
        /// Tells the main screen that a password digit has been entered.
        /// </summary>
        public void EnteringDigit()
        {
            topScreen.DisplayPassword(enteredPassword);
        }

        /// <summary>
        /// Tells the main screen that a particle has been entered.
        /// </summary>
        public void EnteringParticles()
        {
            topScreen.DisplayParticles(enteredParticles);
        }

        public void DeleteParticle()
        {
            if (!_isTouched)
            {
                _isTouched = true;
                enteredParticles.RemoveAt(enteredParticles.Count - 1);
                //TODO: rewrite
                //topScreen.DeleteParticle(_enteredParticles.Count);
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
        /// Displays the next Feynman diagram after the right button is pressed.
        /// </summary>
        public void NextDiagram()
        {
            if (!_isTouched)
            {
                _isTouched = true;
                
                if (topScreen.displayedDiagram < _manager.allReactions.Length-1)
                {
                    topScreen.displayedDiagram++;
                   
                } else
                {
                    topScreen.displayedDiagram = 0;
                }
                OtherDiagram();
                StartCoroutine("WaitButton");
            }
        }

        //TODO: only one of these methods needed
        /// <summary>
        /// Tells the top screen that a combination of particles with a wrong length has been entered.
        /// </summary>
        public void ParticleWrongLength()
        {
            topScreen.ErrorParticles(topScreen.particleErrorString);
        }

        /// <summary>
        /// Tells the top screen that a combination of particles with the wrong symbols has been entered.
        /// </summary>
        public void ParticleWrongSymbol()
        {
            topScreen.ErrorParticles(topScreen.particleErrorString);
        }

        /// <summary>
        /// Tells the top screen that a combination of particles with the wrong charges has been entered.
        /// </summary>
        public void ParticleWrongCharge()
        {
            topScreen.ErrorParticles(topScreen.particleErrorString);
        }


        /// <summary>
        /// Displays the previous Feynman diagram after the left diagram is pressed.
        /// </summary>
        public void PreviousDiagram()
        {
            if (!_isTouched)
            {
                _isTouched = true;
               
                if (topScreen.displayedDiagram > 0)
                {
                    topScreen.displayedDiagram--;

                    
                } else
                {
                    topScreen.displayedDiagram = _manager.allReactions.Length-1;
                }
                OtherDiagram();
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

                SelectExit();

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

                SelectInteraction();

                StartCoroutine("WaitButton");
            }
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
        /// Clears the particles combination entered.
        /// </summary>
        public void ClearParticles()
        {
            enteredParticles.Clear();
            topScreen.ClearParticles();
        }

        /// <summary>
        /// Tells every screen that the right combination of particles has been entered.
        /// </summary>
        public void ParticleRightCombination()
        {
            topScreen.OverrideSecond();
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
            if (enteredParticles.Count == _manager.reactionExits.Count)
            {
                int nbQuark = 0;
                int nbAntiquark = 0;
                int nbMuon = 0;
                int nbAntimuon = 0;
                int nbElectron = 0;
                int nbAntielectron = 0;
                int nbNeutrino = 0;
                int nbPhoton = 0;

                //Counts each particles detected of every kind.
                foreach (Particle particle in _manager.reactionExits)
                {
                    switch (particle.symbol)
                    {
                        case "q":
                            nbQuark++;
                            break;
                        case "qBar":
                            nbAntiquark++;
                            break;
                        case "μ":
                            nbMuon++;
                            break;
                        case "μBar":
                            nbAntimuon++;
                            break;
                        case "e":
                            nbElectron++;
                            break;
                        case "eBar":
                            nbAntielectron++;
                            break;
                        case "v":
                            nbNeutrino++;
                            break;
                        case "vBar":
                            nbNeutrino++;
                            break;
                        case "γ":
                            nbPhoton++;
                            break;
                        default:
                            break;
                    }
                }

                int nbQuarkEntered = 0;
                int nbAntiquarkEntered = 0;
                int nbMuonEntered = 0;
                int nbAntimuonEntered = 0;
                int nbElectronEntered = 0;
                int nbAntielectronEntered = 0;
                int nbNeutrinoEntered = 0;
                int nbPhotonEntered = 0;

                //Counts each particles entered of every kind.
                foreach (Particle particle in enteredParticles)
                {
                    switch (particle.symbol)
                    {
                        case "q":
                            nbQuarkEntered++;
                            break;
                        case "qBar":
                            nbAntiquarkEntered++;
                            break;
                        case "μ":
                            nbMuonEntered++;
                            break;
                        case "μBar":
                            nbAntimuonEntered++;
                            break;
                        case "e":
                            nbElectronEntered++;
                            break;
                        case "eBar":
                            nbAntielectronEntered++;
                            break;
                        case "v":
                            nbNeutrinoEntered++;
                            break;
                        case "vBar":
                            nbNeutrinoEntered++;
                            break;
                        case "γ":
                            nbPhotonEntered++;
                            break;
                        default:
                            break;
                    }
                }

                //Checks if the right symbols have been entered.
                if ((nbElectron + nbAntielectron == nbElectronEntered + nbAntielectronEntered) && (nbMuon + nbAntimuon == nbMuonEntered + nbAntimuonEntered) && (nbQuark + nbAntiquark == nbQuarkEntered + nbAntiquarkEntered) && nbNeutrino == nbNeutrinoEntered && nbPhoton == nbPhotonEntered)
                {
                    //Check if the right symbols have been entered.
                    if (nbElectron == nbElectronEntered && nbAntielectron == nbAntielectronEntered && nbMuon == nbMuonEntered && nbAntimuon == nbAntimuonEntered && nbQuark == nbQuarkEntered && nbAntiquark == nbAntiquarkEntered && nbNeutrino == nbNeutrinoEntered && nbPhoton == nbPhotonEntered)
                    {
                        //The right combination of particles have been entered.
                        Debug.Log("The right combination of particles have been entered.");
                        ParticleRightCombination();
                    }
                    else
                    {
                        //A wrong combination of charges have been entered.
                        Debug.Log("A wrong combination of charges have been entered.");
                        topScreen.particleErrorString = "WRONG NUMBER OF CHARGES!";
                        ParticleWrongCharge();
                    }
                }
                else
                {
                    //A wrong combination of symbols have been entered.
                    Debug.Log("A wrong combination of symbols have been entered.");
                    topScreen.particleErrorString = "WRONG PARTICLES!";
                    ParticleWrongSymbol();
                }
            }
            else
            {
                //A combination of particles with a wrong length has been entered.
                Debug.Log("A combination of particles with a wrong length has been entered.");
                topScreen.particleErrorString = "WRONG NUMBER OF PARTICLES!";
                ParticleWrongLength();
            }
        }

        /// <summary>
        /// Tells the main screen that a reaction has been selected.
        /// </summary>
        public void ReactionSelected()
        {
            topScreen.ReactionSelected(_manager.realReaction, tubeScreen.diagramSelected);
        }

        /// <summary>
        /// Adds a particle to the combination.
        /// </summary>
        /// <param name="particleButton">The particle to add.</param>
        public void EnteringParticle(string particleButton)
        {
            if (!_isTouched && enteredParticles.Count < _manager.reactionExits.Count)
            {
                _isTouched = true;

                foreach (Particle particle in _manager.reactionExits)
                {
                    if (particle.symbol == particleButton)
                    {
                        enteredParticles.Add(particle);
                        break;
                    }
                }
                EnteringParticles();
                StartCoroutine("WaitButton");
            }
        }

        /// <summary>
        /// Adds a number to the password.
        /// </summary>
        /// <param name="number">The number to add.</param>
        public void EnteringDigit(int number)
        {
            if (!_isTouched)
            {
                Debug.Log(number);
                _isTouched = true;

                if (enteredPassword.Length < _manager.realPassword.Length)
                {
                    enteredPassword += number.ToString();
                    EnteringDigit();

                    if (enteredPassword.Length == _manager.realPassword.Length && enteredPassword == _manager.realPassword)
                    {
                        Debug.Log("correct");
                        CorrectPassword();
                    }
                    else if (enteredPassword.Length == _manager.realPassword.Length && enteredPassword != _manager.realPassword)
                    {
                        Debug.Log("wrong");
                        IncorrectPassword();
                        enteredPassword = "";
                    }
                }
                StartCoroutine("WaitButton");
            }
        }
        /// <summary>
        /// Displays particle selection panel after the correct password have been entered.
        /// </summary>
        public void AccessGranted()
        {
            hologram.ActivateHologram(true);
            _manager.generateExits();
            topScreen.FillNbParticlesDetected(_manager.reactionExits);
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
            topScreen.AccessCode();
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
            topScreen.ManualOverride();
            StartCoroutine(WaitGeneric(0.2f, () =>
            {
                _panelFull.SetActive(true);
                _currentPanel = _panelFull;
                _startFull.SetActive(false);
                StartCoroutine("FakeLoading");
            }));
        }

        private void Init(MAIAManager synchronizer)
        {
            _manager = synchronizer;
        }

        public override void OnActivation(XPManager manager)
        {
            Init((MAIAManager)manager);
        }
    }
}

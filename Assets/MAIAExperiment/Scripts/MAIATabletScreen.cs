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
        private Slider _slider;
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

        public void DeleteParticle()
        {
            if (!_isTouched)
            {
                _isTouched = true;
                _manager._enteredParticles.RemoveAt(_manager._enteredParticles.Count - 1);
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
            
            if(_manager.realParticles.Count == 0)
                ParticlesCombination();
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
                
                if (_manager.displayedDiagram < _manager._allReactions.Length-1)
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
                    _manager.displayedDiagram = _manager._allReactions.Length-1;
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
        /// Selects the ongoing particle reactions for this game.
        /// </summary>
        private void ReactionsCombination()
        {

            _manager._allReactions = Resources.LoadAll<Reaction>(_manager._pathReaction);

            List<Reaction> fundamentals = new List<Reaction>();

            foreach (Reaction reaction in _manager._allReactions)
            {
                if (reaction.fundamental)
                {
                    fundamentals.Add(reaction);
                }
            }

            for (int i = 0; i < _manager._numberChosenReaction; i++)
            {
                int randNumber = UnityEngine.Random.Range(0, fundamentals.Count);
                _manager._chosenReactions.Add(fundamentals[randNumber]);
                fundamentals.RemoveAt(randNumber); ;
            }

            _manager._realReaction = _manager._chosenReactions[UnityEngine.Random.Range(0, _manager._chosenReactions.Count)];
            manager.logController.AddLog(_manager._realReaction.name, manager.xpContext);
        }

        /// <summary>
        /// Counts the number of particles detetected of each kind.
        /// </summary>
        /// <param name="particles">The particles detected.</param>
        public void CountParticles(List<Particle> particles)
        {
            foreach (Particle particle in particles)
            {
                switch (particle.symbol)
                {
                    
                    case "q":
                        _manager.nbQuark++;
                        break;
                    case "qBar":
                        _manager.nbAntiquark++;
                        break;
                    case "μ":
                        _manager.nbMuon++;
                        break;
                    case "μBar":
                        _manager.nbAntimuon++;
                        break;
                    case "e":
                        _manager.nbElectron++;
                        break;
                    case "eBar":
                        _manager.nbAntielectron++;
                        break;
                    case "v":
                        _manager.nbNeutrino++;
                        break;
                    case "vBar":
                        _manager.nbNeutrino++;
                        break;
                    case "γ":
                        _manager.nbPhoton++;
                        break;
                    default:
                        break;
                }
            }
            manager.logController.AddLog("Quarks:"+ _manager.nbQuark, manager.xpContext);
            manager.logController.AddLog("Antiquarks:" + _manager.nbAntiquark, manager.xpContext);
            manager.logController.AddLog("Muons:" + _manager.nbMuon, manager.xpContext);
            manager.logController.AddLog("Antimuons:" + _manager.nbAntimuon, manager.xpContext);
            manager.logController.AddLog("Electrons:" + _manager.nbElectron, manager.xpContext);
            manager.logController.AddLog("Antielectrons:" + _manager.nbAntielectron, manager.xpContext);
            manager.logController.AddLog("Neutrinos:" + _manager.nbNeutrino, manager.xpContext);
            manager.logController.AddLog("Photons:" + _manager.nbPhoton, manager.xpContext);
        }

       
        /// <summary>
        /// Lists the particles produced by the ongoing reactions.
        /// </summary>
        /// <returns>The list of produced particles.</returns>
        public List<Particle> ParticlesCombination()
        {
            _manager._allParticles = Resources.LoadAll<Particle>(_manager._path);

            ReactionsCombination();

            foreach (Reaction reaction in _manager._chosenReactions)
            {
                string[] particlesStrings = reaction.exits.ToString().Split('_');

                for (int i = 0; i < particlesStrings.Length; i++)
                {
                    foreach (Particle particle in _manager._allParticles)
                    {
                        if (particle.symbol == particlesStrings[i])
                        {
                            _manager.reactionExits.Add(particle);
                            manager.logController.AddLog(particle.particleName, manager.xpContext);
                            _manager.realParticles.Add(particle.symbol);
                        }
                    }
                }

               
            }

            CountParticles(_manager.reactionExits);
            GenerateParticleString();
            return _manager.reactionExits;
        }

        /// <summary>
        /// Converts the produced particles into a string.
        /// </summary
        private void GenerateParticleString()
        {
            string type = "";

            
            for (int i = 0; i < _manager.realParticles.Count; i++)
            {
                type += _manager.realParticles[i];
            }
            _manager.CorrectParticle();
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

            _manager._enteredParticles.Clear();
            _manager.ClearParticles();
        }

        /// <summary>
        /// Submits the particles combination entered.
        /// </summary>
        public void SubmitParticles()
        {
            //Checks if the combination entered has the right number of particles.
            if (_manager._enteredParticles.Count == _manager.reactionExits.Count)
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
                foreach (Particle particle in _manager._enteredParticles)
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
                        _manager.ParticleRightCombination();
                    }
                    else
                    {
                        //A wrong combination of charges have been entered.
                        Debug.Log("A wrong combination of charges have been entered.");
                        _manager.particleErrorString = "WRONG NUMBER OF CHARGES!";
                        _manager.ParticleWrongCharge();
                    }
                }
                else
                {
                    //A wrong combination of symbols have been entered.
                    Debug.Log("A wrong combination of symbols have been entered.");
                    _manager.particleErrorString = "WRONG PARTICLES!";
                    _manager.ParticleWrongSymbol();
                }
            }
            else
            {
                //A combination of particles with a wrong length has been entered.
                Debug.Log("A combination of particles with a wrong length has been entered.");
                _manager.particleErrorString = "WRONG NUMBER OF PARTICLES!";
                _manager.ParticleWrongLength();
            }
        }

        /// <summary>
        /// Adds a particle to the combination.
        /// </summary>
        /// <param name="particleButton">The particle to add.</param>
        public void EnteringParticle(string particleButton)
        {
            if (!_isTouched && _manager._enteredParticles.Count < _manager.reactionExits.Count)
            {
                _isTouched = true;

                foreach (Particle particle in _manager.reactionExits)
                {
                    if (particle.symbol == particleButton)
                    {
                        _manager._enteredParticles.Add(particle);
                        break;
                    }
                }
                _manager.EnteringParticles();
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

                if (_manager.enteredPassword.Length < _manager._realPassword.Length)
                {
                    _manager.enteredPassword += number.ToString();
                    _manager.EnteringDigit();

                    if (_manager.enteredPassword.Length == _manager._realPassword.Length && _manager.enteredPassword == _manager._realPassword)
                    {
                        Debug.Log("correct");
                        _manager.CorrectPassword();
                    }
                    else if (_manager.enteredPassword.Length == _manager._realPassword.Length && _manager.enteredPassword != _manager._realPassword)
                    {
                        Debug.Log("wrong");
                        _manager.IncorrectPassword();
                        _manager.enteredPassword = "";
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using CRI.HelloHouston.ParticlePhysics;

namespace CRI.HelloHouston.Experience.MAIA
{
    /// <summary>
    /// The tablet screen of the experiment block for the particle physics experiment.
    /// </summary>
    public class MAIATabletScreen : XPElement
    {
        /// <summary>
        /// All the particle scriptable objects.
        /// </summary>
        [SerializeField]
        private Particle[] _allParticles;
        /// <summary>
        /// All the reaction scriptable objects.
        /// </summary>
        [SerializeField]
        public Reaction[] _allReactions;
        /// <summary>
        /// Path to the particle scriptable objects folder.
        /// </summary>
        private static string _path = "Particles";
        /// <summary>
        /// Path to the particle scriptable objects folder.
        /// </summary>
        private static string _pathReaction = "reactions";
        /// <summary>
        /// Contains the combination of particles randomly generated.
        /// </summary>
        public Particle[] particleTypes;
        /// <summary>
        /// Synchronizer for this experiment.
        /// </summary>
        [SerializeField]
        private MAIASynchronizer _synchronizer;
        /// <summary>
        /// All the panels for the tablet screen.
        /// </summary>
        [SerializeField]
        private GameObject _panel, _b1C2, _b1C4, _b1C4Left, _b1C5Left, _b1C6Left, _b1C6Right, _b1C7Left;
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
        /// Real password to get access.
        /// </summary>
        [SerializeField]
        private string _realPassword;
        /// <summary>
        /// Password entered by the player.
        /// </summary>
        public string enteredPassword;
        /// <summary>
        /// The combination of particles randomly generated rewritten as a string.
        /// </summary>
        public List<string> realParticles = new List<string>();
        /// <summary>
        /// The particles entered by the player.
        /// </summary>
        public List<Particle> _enteredParticles = new List<Particle>();
        [SerializeField]
        /// <summary>
        /// The buttons to enter digits for the password.
        /// </summary>
        private Button[] _digitButtons,
        /// <summary>
        /// The buttons to enter particles.
        /// </summary>
                         _particleButtons;
        /// <summary>
        /// String displayed depending on the particles combination entered.
        /// </summary>
        public string[] result;
        /// <summary>
        /// Number of ongoing reactions.
        /// </summary>
        [SerializeField]
        private int _numberChosenReaction = 4;
        /// <summary>
        /// The ongoing reactions.
        /// </summary>
        [SerializeField]
        public List<Reaction> _chosenReactions = new List<Reaction>();
        /// <summary>
        /// The reaction to idetify.
        /// </summary>
        [SerializeField]
        public Reaction _realReaction;
        /// <summary>
        /// The particles produced by the ongoing reactions.
        /// </summary>
        [SerializeField]
        public List<Particle> reactionExits = new List<Particle>();
        private bool isTouched = false;
        //TODO: not needed?
        public Particle particleToEnter;
        public int displayedDiagram = 0;
        public string particleErrorString;
        private GameObject _currentPanelLeft, _currentPanelRight, _currentPanel;

        private void Start()
        {
            _currentPanel = _b1C2;
        }

        public void SkipStepOne()
        {
            if (_currentPanel != null)
                _currentPanel.SetActive(false);

            _b1C4Left.SetActive(false);

            if (_currentPanelLeft != null)
            {
                
                _currentPanelLeft.SetActive(false);
            }

            if (_currentPanelRight != null)
                _currentPanelRight.SetActive(false);

            _b1C4.SetActive(true);
            _b1C6Right.SetActive(true);
            _b1C7Left.SetActive(true);

            ParticlesCombination();
        }

        IEnumerator WaitGeneric(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action.Invoke();
        }

        public void SelectReaction()
        {
            _synchronizer.ReactionSelected();
        }

        public void OverrideSecond()
        {

            _b1C6Right.SetActive(true);
            _currentPanelRight = _b1C6Right;
            _b1C7Left.SetActive(true);
            _currentPanelLeft = _b1C7Left;
            _b1C6Left.SetActive(false);


        }

        public void NextDiagram()
        {
            if (!isTouched)
            {
                isTouched = true;
                
                if (displayedDiagram < _allReactions.Length)
                {
                    displayedDiagram++;
                   
                } else
                {
                    displayedDiagram = 0;
                }
                _synchronizer.OtherDiagram();
                StartCoroutine("WaitButton");
            }
        }



        public void PreviousDiagram()
        {
            if (!isTouched)
            {
                isTouched = true;
                Debug.Log("CALLED");
                if (displayedDiagram > 0)
                {
                    displayedDiagram--;

                    
                } else
                {
                    displayedDiagram = _allReactions.Length;
                }
                _synchronizer.OtherDiagram();
                StartCoroutine("WaitButton");
            }
        }

        public void PreselectExits()
        {
            if (!isTouched)
            {
                isTouched = true;

                _synchronizer.SelectExit();

                StartCoroutine("WaitButton");
            }
        }

        public void PreselectInteraction()
        {
            if (!isTouched)
            {
                isTouched = true;

                _synchronizer.SelectInteraction();

                StartCoroutine("WaitButton");
            }
        }

        IEnumerator WaitButton()
        {
            yield return new WaitForSeconds(0.5f);
            isTouched = false;
        }

        /// <summary>
        /// Selects the ongoing particle reactions for this game.
        /// </summary>
        private void ReactionsCombination()
        {
            _allReactions = Resources.LoadAll<Reaction>(_pathReaction);

            List<Reaction> fundamentals = new List<Reaction>();

            foreach (Reaction reaction in _allReactions)
            {
                if (reaction.fundamental)
                {
                    fundamentals.Add(reaction);
                }
            }

            for (int i = 0; i < _numberChosenReaction; i++)
            {
                _chosenReactions.Add(fundamentals[UnityEngine.Random.Range(0, fundamentals.Count)]);
            }

            _realReaction = _chosenReactions[UnityEngine.Random.Range(0, _chosenReactions.Count)];

            synchronizer.logController.AddLog(_realReaction.name, synchronizer.xpContext);
        }

        /// <summary>
        /// Lists the particles produced by the ongoing reactions.
        /// </summary>
        /// <returns>The list of produced particles.</returns>
        public List<Particle> ParticlesCombination()
        {
            _allParticles = Resources.LoadAll<Particle>(_path);

            ReactionsCombination();

            foreach (Reaction reaction in _chosenReactions)
            {
                string[] particlesStrings = reaction.exits.ToString().Split('_');

                for (int i = 0; i < particlesStrings.Length; i++)
                {
                    foreach (Particle particle in _allParticles)
                    {
                        if (particle.symbol == particlesStrings[i])
                        {
                            reactionExits.Add(particle);
                            synchronizer.logController.AddLog(particle.particleName, synchronizer.xpContext);
                            realParticles.Add(particle.symbol);
                        }
                    }
                }
            }
            GenerateParticleString();
            return reactionExits;
        }

        /// <summary>
        /// Converts the produced particles into a string.
        /// </summary
        private void GenerateParticleString()
        {
            string type = "";

            for (int i = 0; i < realParticles.Count; i++)
            {
                type += realParticles[i];
            }
            _synchronizer.CorrectParticle();
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
            _synchronizer.ClearParticles();
        }
        /// <summary>
        /// Submits the particles combination entered.
        /// </summary>
        public void SubmitParticles()
        {
            if (_enteredParticles.Count == reactionExits.Count)
            {
                int nbQuark = 0;
                int nbAntiquark = 0;
                int nbMuon = 0;
                int nbAntimuon = 0;
                int nbElectron = 0;
                int nbAntielectron = 0;
                int nbNeutrino = 0;
                int nbPhoton = 0;

                foreach (Particle particle in reactionExits)
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

                foreach (Particle particle in _enteredParticles)
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

                if ((nbElectron + nbAntielectron == nbElectronEntered + nbAntielectronEntered) && (nbMuon + nbAntimuon == nbMuonEntered + nbAntimuonEntered) && (nbQuark + nbAntiquark == nbQuarkEntered + nbAntiquarkEntered) && nbNeutrino == nbNeutrinoEntered && nbPhoton == nbPhotonEntered)
                {
                    if (nbElectron == nbElectronEntered && nbAntielectron == nbAntielectronEntered && nbMuon == nbMuonEntered && nbAntimuon == nbAntimuonEntered && nbQuark == nbQuarkEntered && nbAntiquark == nbAntiquarkEntered && nbNeutrino == nbNeutrinoEntered && nbPhoton == nbPhotonEntered)
                    {
                        Debug.Log("correct");
                        _synchronizer.ParticleRightCombination();
                    }
                    else
                    {
                        Debug.Log("pas meme charge");
                        particleErrorString = "WRONG NUMBER OF CHARGES!";
                        _synchronizer.ParticleWrongCharge();
                    }
                }
                else
                {
                    Debug.Log("pas meme symbol");
                    particleErrorString = "WRONG PARTICLES!";
                    _synchronizer.ParticleWrongSymbol();
                }

            }
            else
            {
                Debug.Log("pas meme longueur");
                particleErrorString = "WRONG NUMBER OF PARTICLES!";
                _synchronizer.ParticleWrongLength();
            }
        }
        /// <summary>
        /// Adds a particle to the combination.
        /// </summary>
        /// <param name="particleButton">The particle to add.</param>
        public void EnteringParticle(string particleButton)
        {
            if (!isTouched && _enteredParticles.Count < reactionExits.Count)
            {
                isTouched = true;

                foreach (Particle particle in reactionExits)
                {
                    if (particle.symbol == particleButton)
                    {
                        _enteredParticles.Add(particle);
                        break;
                    }
                }
                _synchronizer.EnteringParticles();
                StartCoroutine("WaitButton");
            }
        }
        /// <summary>
        /// Adds a number to the password.
        /// </summary>
        /// <param name="number">The number to add.</param>
        public void EnteringDigit(int number)
        {
            if (!isTouched)
            {
                isTouched = true;
                if (enteredPassword.Length < _realPassword.Length)
                {
                    enteredPassword += number.ToString();
                    _synchronizer.EnteringDigit();

                    if (enteredPassword.Length == _realPassword.Length && enteredPassword == _realPassword)
                    {
                        _synchronizer.CorrectPassword();
                    }
                    else if (enteredPassword.Length == _realPassword.Length && enteredPassword != _realPassword)
                    {
                        _synchronizer.IncorrectPassword();
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
            _b1C5Left.SetActive(false);
            _b1C6Left.SetActive(true);
            _currentPanelLeft = _b1C6Left;
        }
        /// <summary>
        /// Displays start panel after the splash screen has finished loading.
        /// </summary>
        public void WaitingConfirmation()
        {
            _b1C2.SetActive(true);
            _currentPanel = _b1C2;
            _panel.SetActive(false);
        }
        /// <summary>
        /// Displays password panel adter override button has been clicked.
        /// </summary>
        public void OverrideButtonClicked()
        {
            _synchronizer.OverrideButtonClicked();
            StartCoroutine(WaitGeneric(0.2f, () =>
            {
                _b1C5Left.SetActive(true);
                _currentPanelLeft = _b1C5Left;
                _b1C4Left.SetActive(false);
            }));

        }
        /// <summary>
        /// Displays override panel after start button has been clicked.
        /// </summary>
        public void StartButtonClicked()
        {
            _synchronizer.StartButtonClicked();
            StartCoroutine(WaitGeneric(0.2f, () =>
            {
                _b1C4.SetActive(true);
                _currentPanel = _b1C4;
                _b1C2.SetActive(false);
                StartCoroutine("FakeLoading");
            }));
        }

        public void Init(MAIASynchronizer synchronizer)
        {
            _synchronizer = synchronizer;
        }
    }
}

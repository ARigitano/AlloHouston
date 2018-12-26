using CRI.HelloHouston.ParticlePhysics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience.MAIA
{
    /// <summary>
    /// The top left screen of the experiment block for the particle physics experiment.
    /// </summary>
    public class MAIATopScreen : XPElement
    {
        /// <summary>
        /// The synchronizer of the experiment.
        /// </summary>
        [SerializeField]
        private MAIASynchronizer _synchronizer;
        /// <summary>
        /// The loading bar of the splash screen.
        /// </summary>
        [SerializeField]
        private Image _slider;
        [SerializeField]
        private float _speed = 0.2f;
        /// <summary>
        /// Text displaying the percentage loaded on the splash screen.
        /// </summary>
        [SerializeField]
        private Text _percentage;
        /// <summary>
        /// All the panels of the top left screen of the experiment.
        /// </summary>
        [SerializeField]
        private GameObject _exileLoadingScreen, _maiaLoadingScreen, _maiaOverview, _manualOverrideAccess, _popupAccessGranted, _popupErrorAccessDenied, _manualOverride1, _popupErrorMessageParticles, _pverrideScreen2;
        /// <summary>
        /// Text that displays the loading states of the experiment according to the loading bar progression.
        /// </summary>
        [SerializeField]
        private Text _loadingText;
        /// <summary>
        /// The loading states of the experiment.
        /// </summary>
        [SerializeField]
        private string[] _loadingStrings;
        /// <summary>
        /// Grid that displays the cases for the detected particles to enter.
        /// </summary>
        [SerializeField]
        private GameObject _particlesGrid;
        /// <summary>
        /// Prefab of a case for the particle grid.
        /// </summary>
        [SerializeField]
        private GameObject _gridCase;
        /// <summary>
        /// All the cases for the detected particles.
        /// </summary>
        [HideInInspector]
        public List<Image> _particleCases = new List<Image>();
        /// <summary>
        /// Texts that display the number of particles detected for each type of particles.
        /// </summary>
        [SerializeField]
        private Text _textQuark, _textAntiquark, _textMuon, _textAntimuon, _textElectron, _textAntielectron, _textNeutrino, _textPhoton;
        /// <summary>
        /// Slots where the diagrams of the chosen reactions are displayed.
        /// </summary>
        [SerializeField]
        private Image[] _diagrams;
        /// <summary>
        /// Displays the type of interaction of the real reaction.
        /// </summary>
        [SerializeField]
        private Text _textInteraction;
        /// <summary>
        /// Long error string to be displayed in a scrolling manner.
        /// </summary>
        [SerializeField]
        private string _scrollingText;
        /// <summary>
        /// Text that displays the scrolling error.
        /// </summary>
        [SerializeField]
        private Text _scrollError;
        /// <summary>
        /// Has the experiment finished loading?
        /// </summary>
        [HideInInspector]
        public bool isLoaded = false;
        /// <summary>
        /// The total number of particles detected.
        /// </summary>
        [SerializeField]
        private Text _nbParticlesDetected;
        /// <summary>
        /// Popup that displays either a correct or wrong identification of the detected particles.
        /// </summary>
        [SerializeField]
        private GameObject _errorParticles, _successParticles;
        /// <summary>
        /// Errors popup on the overview panel.
        /// </summary>
        [SerializeField]
        private GameObject _popupCrash, _popupErrorMessage, _popupInfoMessage;
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
        /// Popups displayed when selecting the right or wrong Feynman diagram.
        /// </summary>
        [SerializeField]
        private GameObject _popupWin, _popupLose;
        /// <summary>
        /// Stores the panel currently being displayed.
        /// </summary>
        private GameObject _currentPanel;
        /// <summary>
        /// Image to be displayed insted of the Feynman diagram for the real reaction.
        /// </summary>
        [SerializeField]
        private Sprite _diagramMissing;

        private void Start()
        {
            //TODO: change when exile loading screen created;
            _currentPanel = _maiaLoadingScreen;
        }

        /// <summary>
        /// Called by the synchronizer to skip directly to the Feynman diagrams step.
        /// </summary>
        public void SkipStepOne()
        {
            if(_currentPanel != null)
                 _currentPanel.SetActive(false);

            _pverrideScreen2.SetActive(true);
        }

        /// <summary>
        /// Effect if the correct Feynman diagram is selected, or a wrong one.
        /// </summary>
        /// <param name="realReaction">The real reaction.</param>
        /// <param name="reactionSelected">The reaction selected by the player.</param>
        public void ReactionSelected(Reaction realReaction, Sprite reactionSelected)
        {
            if (reactionSelected == realReaction.diagramImage)
            {
                _popupWin.SetActive(true);
            }
            else
            {
                _popupLose.SetActive(true);

                StartCoroutine(WaitGeneric(2f, () =>
                {
                    _popupLose.SetActive(false);
                }));
            }

        }

        public void OverrideSecond()
        {
            _pverrideScreen2.SetActive(true);
            _currentPanel = _pverrideScreen2;
            _manualOverride1.SetActive(false);
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
        /// Displays a popup if the player selected the wrong Feynman diagram.
        /// </summary>
        /// <param name="error">The error to be displayed on the popup depending on the player's mistake.</param>
        public void ErrorParticles(string error)
        {
            _errorParticles.SetActive(true);
            _errorParticles.GetComponentInChildren<Text>().text = error;
            StartCoroutine(WaitGeneric(2f, () =>
            {
                _errorParticles.SetActive(false);
            }));
        }

        /// <summary>
        /// Displays a popup if the player selected the right Feynman diagram.
        /// </summary>
        private void SuccessParticles()
        {
            _successParticles.SetActive(true);
        }

        /// <summary>
        /// Fills the number of particles that have been detected on the reaction summary window.
        /// </summary>
        /// <param name="particles">The particles that have been detected.</param>
        public void FillNbParticlesDetected(List<Particle> particles)
        {
            _nbParticlesDetected.text = particles.Count + " particles have been detected.";
        }

        /// <summary>
        /// Displays a long scrolling error.
        /// </summary>
        /// <returns></returns>
        IEnumerator ScrollingError()
        {
            int i = 0;

            while (i < _scrollingText.Length)
            {
                _scrollError.text += _scrollingText[i++];
                yield return new WaitForSeconds(0.0001f);
            }
        }

        /// <summary>
        /// Fills the interaction type of the real reaction on the interaction type window.
        /// </summary>
        /// <param name="chosenReaction">The chosen reaction.</param>
        public void FillInteractionType(Reaction chosenReaction)
        {
            _textInteraction.text = chosenReaction.entries.ToString();
        }

        /// <summary>
        /// Displays the Feynman diagrams of each chosen reaction except the real one.
        /// </summary>
        /// <param name="reactions"></param>
        /// <param name="chosenReaction"></param>
        public void FillChosenDiagrams(List<Reaction> reactions, Reaction chosenReaction)
        {
            int i = 0;

            List<Reaction> reactionsTemp = new List<Reaction>();

            foreach (Reaction reaction in reactions)
            {
                reactionsTemp.Add(reaction);
            }

            while (reactionsTemp.Count != 0)
            {
                if (reactionsTemp[0].diagramImage != chosenReaction.diagramImage)
                {
                    _diagrams[i].sprite = reactionsTemp[0].diagramImage;
                } 
                else
                {
                    _diagrams[i].sprite = _diagramMissing;
                }
                reactionsTemp.RemoveAt(0);
                i++;
            }
        }

        public void FillParticlesTable(List<Particle> particles)
        {
            int nbQuark = 0;
            int nbAntiquark = 0;
            int nbMuon = 0;
            int nbAntimuon = 0;
            int nbElectron = 0;
            int nbAntielectron = 0;
            int nbNeutrino = 0;
            int nbPhoton = 0;

            foreach (Particle particle in particles)
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
            _textQuark.text = nbQuark.ToString();
            _textAntiquark.text = nbAntiquark.ToString();
            _textMuon.text = nbMuon.ToString();
            _textAntimuon.text = nbAntimuon.ToString();
            _textElectron.text = nbElectron.ToString();
            _textAntielectron.text = nbAntielectron.ToString();
            _textNeutrino.text = nbNeutrino.ToString();
            _textPhoton.text = nbPhoton.ToString();
            Debug.Log("LAOK");
        }

        public void ParticleGrid(List<Particle> particles)
        {
            Debug.Log(particles.Count);
            foreach (Particle particle in particles)
            {
                GameObject newCase = (GameObject)Instantiate(_gridCase, _particlesGrid.transform.position, _particlesGrid.transform.rotation, _particlesGrid.transform);
                _particleCases.Add(newCase.GetComponentInChildren<Image>());
            }
        }
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
            yield return new WaitForSeconds(2);
            _popupAccessGranted.SetActive(false);
            _manualOverride1.SetActive(true);
            _currentPanel = _manualOverride1;
            _manualOverrideAccess.SetActive(false);
            _synchronizer.AccessGranted();
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

        public void ClearParticles()
        {
            foreach (Image particleCase in _particleCases)
            {
                particleCase.enabled = false;
            }
        }
        /// <summary>
        /// Displays the particles combination while they are being opened.
        /// </summary>
        /// <param name="particles">The particles combination that is being entered.</param>
        public void DisplayParticles(List<Particle> particles)
        {
            for (int i = 0; i < particles.Count; i++)
            {
                _particleCases[i].enabled = true;
                _particleCases[i].sprite = particles[i].symbolImage;
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
        /// <summary>
        /// Loading delay of the splash screen.
        /// </summary>
        /// <returns></returns>
        IEnumerator Loading()
        {
            if (!isLoaded)
            {
                while (_slider.fillAmount < 1f)
                {
                    _slider.fillAmount += Time.deltaTime * _speed;
                    _percentage.text = Mathf.Round(_slider.fillAmount * 100) + "%";

                    if (_slider.fillAmount * 10 <= _loadingStrings.Length)
                    {
                        _loadingText.text = _loadingStrings[Mathf.FloorToInt(_slider.fillAmount * 10)];
                    }
                    if (_slider.fillAmount >= 0.9f)
                    {
                        _slider.fillAmount = 1f;
                        _maiaLoadingScreen.SetActive(true);
                        _currentPanel = _maiaLoadingScreen;
                        _exileLoadingScreen.SetActive(false);
                        isLoaded = true;
                        _synchronizer.LoadingBarFinished();
                        yield return null;
                    }
                }
            }
        }
        //TO DO: find better way of changing panel
        /// <summary>
        /// Displays the manual override screen when the start button is pressed.
        /// </summary>
        public void ManualOverride()
        {
            _maiaLoadingScreen.SetActive(false);
            _maiaOverview.SetActive(true);
            _currentPanel = _maiaOverview;
            StartCoroutine(WaitGeneric(10f, () =>
            {
                _popupCrash.SetActive(true);
                StartCoroutine("ScrollingError");
                _popupErrorMessage.SetActive(true);
                _popupInfoMessage.SetActive(true);
                _synchronizer.ManualOverrideActive();
            }));
        }
        /// <summary>
        /// Displays the access screen when the manual override button is pressed.
        /// </summary>
        public void AccessCode()
        {
            _manualOverrideAccess.SetActive(true);
            _currentPanel = _manualOverrideAccess;
            _maiaOverview.SetActive(false);
        }

        public void Init(MAIASynchronizer synchronizer)
        {
            _synchronizer = synchronizer;
        }
        //TO DO
        /// <summary>
        /// Effect when the experiment is correctly resolved.
        /// </summary>
        public override void OnSuccess()
        {
            Debug.Log(name + "Resolved");
        }
        //TO DO
        /// <summary>
        /// Effect when the experiment is failed.
        /// </summary>
        public override void OnFailure()
        {
            Debug.Log(name + "Failed");
        }

        /// <summary>
        /// Effect when the experiment is activated the first time.
        /// </summary>
        public override void OnActivation()
        {
            Debug.Log(name + "Activated");
            //StartCoroutine("Loading");
        }

        /// <summary>
        /// Effect when the experiment is paused.
        /// </summary>
        public override void OnHide()
        {
            Debug.Log(name + "Paused");
        }

        /// <summary>
        /// Effect when the experiment is unpaused.
        /// </summary>
        public override void OnShow()
        {
            Debug.Log(name + "Unpaused");
        }
    }
}

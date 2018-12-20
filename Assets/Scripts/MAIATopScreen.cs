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
                    _passwordText;
        /// <summary>
        /// All the panels of the top left screen of the experiment.
        /// </summary>
        [SerializeField]
        private GameObject _Exile_Loading_Screen, _MAIA_Loading_Screen, _MAIA_overview, _Manual_override_access, _Popup_Access_granted, _PopupError_Access_denied, _Manual_override_1, _PopupError_Message_Particles, _Override_Screen2;
        [SerializeField]
        private Text _loadingText;
        [SerializeField]
        private string[] _loadingStrings;
        [SerializeField]
        private GameObject _particlesGrid;
        [SerializeField]
        private GameObject _gridCase;
        public List<Image> _particleCases = new List<Image>();
        [SerializeField]
        private Text _textQuark, _textAntiquark, _textMuon, _textAntimuon, _textElectron, _textAntielectron, _textNeutrino, _textPhoton;
        [SerializeField]
        private Image[] _diagrams;
        [SerializeField]
        private Text _textInteraction;
        [SerializeField]
        private string _scrollingText;
        [SerializeField]
        private Text _scrollError;
        public bool _isLoaded = false;
        [SerializeField]
        private Text _nbParticlesDetected;
        [SerializeField]
        private GameObject _errorParticles, _successParticles;
        [SerializeField]
        private GameObject _popupCrash, _popupErrorMessage, _popupInfoMessage;
        [SerializeField]
        private Sprite _starPasswword, _cursorPassword;
        [SerializeField]
        private GameObject[] _slotPassword;
        [SerializeField]
        private GameObject _popupWin, _popupLose;
        private GameObject _currentPanel;

        private void Start()
        {
            //TODO: change when exile loading screen created;
            _currentPanel = _MAIA_Loading_Screen;
        }

        public void SkipStepOne()
        {
            if(_currentPanel != null)
                 _currentPanel.SetActive(false);

            _Override_Screen2.SetActive(true);
        }

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
            _Override_Screen2.SetActive(true);
            _currentPanel = _Override_Screen2;
            _Manual_override_1.SetActive(false);
        }

        IEnumerator WaitGeneric(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action.Invoke();
        }

        public void ErrorParticles(string error)
        {
            _errorParticles.SetActive(true);
            _errorParticles.GetComponentInChildren<Text>().text = error;
            StartCoroutine(WaitGeneric(2f, () =>
            {
                _errorParticles.SetActive(false);
            }));


        }

        private void SuccessParticles()
        {
            _successParticles.SetActive(true);
        }

        public void FillNbParticlesDetected(List<Particle> particles)
        {
            _nbParticlesDetected.text = particles.Count + " particles have been detected.";
        }

        IEnumerator ScrollingError()
        {
            int i = 0;

            while (i < _scrollingText.Length)
            {
                _scrollError.text += _scrollingText[i++];
                yield return new WaitForSeconds(0.0001f);
            }
        }

        public void FillInteractionType(Reaction chosenReaction)
        {
            _textInteraction.text = chosenReaction.entries.ToString();
        }

        public void FillChosenDiagrams(List<Reaction> reactions, Reaction chosenReaction)
        {
            int i = 0;
            int j = 0;
            foreach (Reaction reaction in reactions)
            {
                Debug.Log(i);
                Debug.Log(j);
                if (reactions[j].diagramImage != chosenReaction.diagramImage)
                {
                    _diagrams[i].sprite = reactions[j].diagramImage;
                    i++;
                }
                j++;
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
            _Popup_Access_granted.SetActive(false);
            _Manual_override_1.SetActive(true);
            _currentPanel = _Manual_override_1;
            _Manual_override_access.SetActive(false);
            _synchronizer.AccessGranted();
        }
        /// <summary>
        /// Waiting delay when access denied.
        /// </summary>
        /// <returns></returns>
        IEnumerator WaitDenied()
        {
            yield return new WaitForSeconds(2);
            _PopupError_Access_denied.SetActive(false);
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
                _Popup_Access_granted.SetActive(true);
                StartCoroutine(WaitCorrect());
            }
            else
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
            if (!_isLoaded)
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
                        _MAIA_Loading_Screen.SetActive(true);
                        _currentPanel = _MAIA_Loading_Screen;
                        _Exile_Loading_Screen.SetActive(false);
                        _isLoaded = true;
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
            _MAIA_Loading_Screen.SetActive(false);
            _MAIA_overview.SetActive(true);
            _currentPanel = _MAIA_overview;
            StartCoroutine(WaitGeneric(10f, () =>
            {
                _popupCrash.SetActive(true);
                StartCoroutine("ScrollingError");
                _popupErrorMessage.SetActive(true);
                _popupInfoMessage.SetActive(true);
            })
            );
        }
        /// <summary>
        /// Displays the access screen when the manual override button is pressed.
        /// </summary>
        public void AccessCode()
        {
            _Manual_override_access.SetActive(true);
            _currentPanel = _Manual_override_access;
            _MAIA_overview.SetActive(false);
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
            ChangeOpacity(1f);
            //StartCoroutine("Loading");
        }

        /// <summary>
        /// Effect when the experiment is paused.
        /// </summary>
        public override void OnHide()
        {
            Debug.Log(name + "Paused");
            ChangeOpacity(0f);
        }

        /// <summary>
        /// Effect when the experiment is unpaused.
        /// </summary>
        public override void OnShow()
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

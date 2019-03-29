using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using CRI.HelloHouston.Translation;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class ParticlesIdentification : MonoBehaviour
    {
        public enum ErrorType
        {
            WrongNumberCharges,
            WrongParticles,
            WrongNumberParticles,
        }

        [Serializable]
        public class ErrorMessage
        {
            public ErrorType errorType;
            public string errorMessage;
        }
        /// <summary>
        /// Script for the whole top screen.
        /// </summary>
        [SerializeField]
        private MAIATopScreen _maiaTopScreen = null;
        /// <summary>
        /// The lang manager.
        /// </summary>
        private ILangManager _langManager;
        /// <summary>
        /// Message to  be displayed for the number of particles.
        /// </summary>
        private string _particleDetectedTextMessage = "";

        private string _particleGuessedTextMessage = "";
        /// <summary>
        /// Message to  be displayed for the number of particles.
        /// </summary>
        private string _chargesDetectedTextMessage = "";

        private string _chargesGuessedTextMessage = "";
        /// <summary>
        /// The total number of particles detected.
        /// </summary>
        [SerializeField]
        private Text _nbParticlesDetected = null;
        [SerializeField]
        private Text _nbParticlesGuessed = null;
        /// <summary>
        /// The total number of particles detected.
        /// </summary>
        [SerializeField]
        private Text _nbChargesDetected = null;
        [SerializeField]
        private Text _nbChargesGuessed = null;
        [SerializeField]
        private GameObject _particlePanel;
        [SerializeField]
        private GameObject _chargePanel;
        /// <summary>
        /// Popup that displays the wrong identification of the detected particles.
        /// </summary>
        [SerializeField]
        private GameObject _errorParticles = null;
        /// <summary>
        /// Popup that displays the correct identification of the detected particles.
        /// </summary>
        [SerializeField]
        private GameObject _successParticles = null;

        /// <summary>
        /// Error popup duration.
        /// </summary>
        [SerializeField]
        [Tooltip("Error popup duration.")]
        private float _errorPopupDuration = 3.0f;
        /// <summary>
        /// Success popup duration.
        /// </summary>
        [SerializeField]
        [Tooltip("Success popup duration.")]
        private float _successPopupDuration = 1.0f;
        /// <summary>
        /// Dictionary with key error type and value the error message.
        /// </summary>
        [SerializeField]
        private ErrorMessage[] _errorMessages = null;
        /// <summary>
        /// Particle grid cell prefab.
        /// </summary>
        [SerializeField]
        private ParticleGridCell _particleGridCellPrefab = null;
        /// <summary>
        /// Particle grid cell dictionary.
        /// </summary>
        private Dictionary<Particle, ParticleGridCell> _particleGridCellDictionary;
        /// <summary>
        /// Particle grid cell dictionary.
        /// </summary>
        private Dictionary<Particle, ParticleGridCell> _chargeGridCellDictionary;

        private Dictionary<Particle, int> _particleValues;

        private Dictionary<Particle, int> _chargeValues;
        
        /// <summary>
        /// Particle grid transform.
        /// </summary>
        [SerializeField]
        private Transform _particleGridTransform = null;
        /// <summary>
        /// Particle charges transform.
        /// </summary>
        [SerializeField]
        private Transform _chargeGridTransform = null;

        private int _currentCount;

        private void OnEnable()
        {
            if (_langManager != null)
                _langManager.langManager.onLangChange += OnLangChange;
        }

        private void OnLangChange(LangApp lang)
        {
            _particleDetectedTextMessage = _langManager.textManager.GetText(_nbParticlesDetected.GetComponent<TranslatedText>().textKey);
            _particleGuessedTextMessage = _langManager.textManager.GetText(_nbParticlesGuessed.GetComponent<TranslatedText>().textKey);
            FillNbParticlesDetected();
        }

        /// <summary>
        /// Displays a popup if the player selected the wrong Feynman diagram.
        /// </summary>
        /// <param name="error">The error to be displayed on the popup depending on the player's mistake.</param>
        public void DisplayErrorMessage(ErrorType errorType)
        {
            ILangManager manager = GetComponentInParent<XPElement>().manager;
            _errorParticles.SetActive(true);
            _errorParticles.GetComponentInChildren<TranslatedText>().InitTranslatedText(manager, _errorMessages.First(x => x.errorType == errorType).errorMessage);
            StartCoroutine(_maiaTopScreen.WaitGeneric(_errorPopupDuration, () =>
            {
                _errorParticles.SetActive(false);
            }));
        }

        /// <summdwary>
        /// Fills the number of particles that have been detected on the reaction summary window.
        /// </summary>
        /// <param name="particles">The number of particles that have been detected.</param>
        private void FillNbParticlesDetected()
        {
            int guessed = _particleValues.Sum(x => x.Value);
            int total = _maiaTopScreen.maiaManager.generatedParticles.Count;
            _nbParticlesDetected.text = _particleDetectedTextMessage.Replace("[p]", total.ToString());
            _nbParticlesGuessed.text = _particleGuessedTextMessage.Replace("[p]", (guessed != total ? string.Format("<color=red>{0}</color>", guessed) : guessed.ToString()));
        }

        /// <summdwary>
        /// Fills the number of particles that have been detected on the reaction summary window.
        /// </summary>
        /// <param name="particles">The number of particles that have been detected.</param>
        private void FillNbChargesDetected()
        {
            int guessed = _chargeValues.Sum(x => x.Value);
            int total = _maiaTopScreen.maiaManager.generatedParticles.Count;
            _nbParticlesDetected.text = _particleDetectedTextMessage.Replace("[p]", total.ToString());
            _nbParticlesGuessed.text = _particleGuessedTextMessage.Replace("[p]", (guessed != total ? string.Format("<color=red>{0}</color>", guessed) : guessed.ToString()));
        }
        /// <summary>
        /// Displays a popup if the player selected the right Feynman diagram.
        /// </summary>
        private void SuccessParticles()
        {
            _successParticles.SetActive(true);
            StartCoroutine(_maiaTopScreen.WaitGeneric(_successPopupDuration, () =>
            {
                _successParticles.SetActive(false);
                _maiaTopScreen.maiaManager.OnPISuccess();
            }));
        }

        /// <summary>
        /// Generates a grid for particles to be entered in.
        /// </summary>
        /// <param name="particles">The particles detected.</param>
        public void CreateParticleGrid(List<Particle> particles)
        {
            if (_particleGridCellDictionary == null || _particleGridCellDictionary.Count == 0)
            {
                _langManager = GetComponentInParent<XPElement>().manager;
                _nbParticlesDetected.GetComponent<XPTranslatedText>().Init(_langManager);
                _nbParticlesGuessed.GetComponent<XPTranslatedText>().Init(_langManager);
                _nbChargesDetected.GetComponent<XPTranslatedText>().Init(_langManager);
                _nbChargesGuessed.GetComponent<XPTranslatedText>().Init(_langManager);
                _langManager.langManager.onLangChange += OnLangChange;
                _particleDetectedTextMessage = _nbParticlesDetected.text;
                _particleGuessedTextMessage = _nbParticlesGuessed.text;
                _chargesDetectedTextMessage = _nbChargesDetected.text;
                _chargesGuessedTextMessage = _nbChargesGuessed.text;
                InitParticleGridCellDictionary();
                InitParticleChargesGridCellDictionary();
                FillNbParticlesDetected();
                FillNbChargesDetected();
            }
        }

        private void InitParticleGridCellDictionary()
        {
            _particleGridCellDictionary = new Dictionary<Particle, ParticleGridCell>();
            _particleValues = new Dictionary<MAIA.Particle, int>();
            foreach (var particleGroup in _maiaTopScreen.maiaManager.settings.allParticles.Where(particle => !particle.negative).OrderBy(particle => particle.symbol).GroupBy(particle => particle))
            {
                var particleGridCell = Instantiate(_particleGridCellPrefab, _particleGridTransform);
                particleGridCell.Init(particleGroup.Key);
                particleGridCell.SetText("0");
                _particleGridCellDictionary.Add(particleGroup.Key, particleGridCell);
                _particleValues.Add(particleGroup.Key, 0);
            }
            UpdateParticles(_particleValues);
        }

        private void InitParticleChargesGridCellDictionary()
        {
            _chargeGridCellDictionary = new Dictionary<Particle, ParticleGridCell>();
            _chargeValues = new Dictionary<MAIA.Particle, int>();
            foreach (var particleGroup in _maiaTopScreen.maiaManager.settings.allParticles.Where(particle => particle.line).OrderBy(particle => particle.symbol).GroupBy(particle => particle))
            {
                var particleGridCell = Instantiate(_particleGridCellPrefab, _chargeGridTransform);
                particleGridCell.Init(particleGroup.Key);
                particleGridCell.SetText("0");
                _chargeGridCellDictionary.Add(particleGroup.Key, particleGridCell);
                _chargeValues.Add(particleGroup.Key, 0);
            }
            UpdateParticleCharges(_chargeValues);
        }

        private void UpdateParticles(Dictionary<Particle, int> dictionary)
        {
            for (int i = 0; i < dictionary.Count; i++)
            {
                var group = dictionary.ElementAt(i);
                _particleGridCellDictionary[group.Key].SetText(group.Value.ToString());
                if (group.Value == 0)
                    _particleGridCellDictionary[group.Key].Disable();
            }
        }

        private void UpdateParticleCharges(Dictionary<Particle, int> dictionary)
        {
            for (int i = 0; i < dictionary.Count; i++)
            {
                var group = dictionary.ElementAt(i);
                _chargeGridCellDictionary[group.Key].SetText(group.Value.ToString());
                if (group.Value == 0)
                    _chargeGridCellDictionary[group.Key].Disable();
            }
        }

        public void DisplayParticlePanel()
        {
            _particlePanel.SetActive(true);
            _chargePanel.SetActive(false);
        }

        public void DisplayChargePanel()
        {
            _particlePanel.SetActive(false);
            _chargePanel.SetActive(true);
        }

        public void UpdateParticleCharges(ParticleEventArgs e)
        {
            _chargeValues[e.particle] = (int)e.value;
            UpdateParticleCharges(_chargeValues);
            FillNbChargesDetected();
        }

        public void UpdateParticles(ParticleEventArgs e)
        {
            _particleValues[e.particle] = (int)e.value;
            UpdateParticles(_particleValues);
            FillNbParticlesDetected();
        }
    }
}

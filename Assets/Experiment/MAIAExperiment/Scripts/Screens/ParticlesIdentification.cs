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
        /// Message to  be displayed for the number of particles.
        /// </summary>
        private string _particleTextMessage = "";
        /// <summary>
        /// Grid that displays the cases for the detected particles to enter.
        /// </summary>
        [SerializeField]
        private Transform _particlesGrid = null;
        /// <summary>
        /// Prefab of a case for the particle grid.
        /// </summary>
        [SerializeField]
        private GridCell _gridCellPrefab = null;
        /// <summary>
        /// All the cases for the detected particles.
        /// </summary>
        private GridCell[] _gridParticles;
        /// <summary>
        /// The total number of particles detected.
        /// </summary>
        [SerializeField]
        private Text _nbParticlesDetected = null;
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
        private void FillNbParticlesDetected(int count)
        {
            _nbParticlesDetected.text = _particleTextMessage.Replace("[p]", count.ToString());
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
            if (!_nbParticlesDetected.GetComponent<XPTranslatedText>().initialized)
            {
                ILangManager manager = GetComponentInParent<XPElement>().manager;
                _nbParticlesDetected.GetComponent<XPTranslatedText>().Init(manager);
            }
            _particleTextMessage = _nbParticlesDetected.text;
            _gridParticles = new GridCell[particles.Count];
            for (int i = 0; i < particles.Count; i++)
            {
                var gridCell = Instantiate(_gridCellPrefab, _particlesGrid);
                _gridParticles[i] = gridCell;
            }
            FillNbParticlesDetected(_maiaTopScreen.maiaManager.generatedParticles.Count);
        }

        /// <summary>
        /// Displays the particles combination while they are being entered.
        /// </summary>
        /// <param name="particles">The particles combination that is being entered.</param>
        private void DisplayParticles(List<Particle> particles)
        {
            for (int i = 0; i < _gridParticles.Length; i++)
            {
                if (i < particles.Count)
                {
                    _gridParticles[i].Show(true);
                    _gridParticles[i].SetSprite(particles[i].symbolImage);
                }
                else
                    _gridParticles[i].Show(false);
            }
        }

        public void UpdateParticles(List<Particle> enteredParticles)
        {
            DisplayParticles(enteredParticles);
            FillNbParticlesDetected(_maiaTopScreen.maiaManager.generatedParticles.Count - enteredParticles.Count);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class ParticlesIdentification : MonoBehaviour
    {
        /// <summary>
        /// Script for the whole top screen.
        /// </summary>
        [SerializeField]
        private MAIATopScreen _maiaTopScreen = null;
        /// <summary>
        /// Grid that displays the cases for the detected particles to enter.
        /// </summary>
        [SerializeField]
        private GameObject _particlesGrid = null;
        /// <summary>
        /// Prefab of a case for the particle grid.
        /// </summary>
        [SerializeField]
        private GameObject _gridCase = null;
        /// <summary>
        /// All the cases for the detected particles.
        /// </summary>
        [HideInInspector]
        public List<Image> _particleCases = new List<Image>();
        /// <summary>
        /// Texts that display the number of particles detected for each type of particles.
        /// </summary>
        [SerializeField]
        public Text _textQuark, _textAntiquark, _textMuon, _textAntimuon, _textElectron, _textAntielectron, _textNeutrino, _textPhoton;
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
        /// Victory popup for the demo version.
        /// </summary>
        [SerializeField]
        private GameObject _victoryPopup = null;

        /// <summary>
        /// Ends the game for the demo version.
        /// </summary>
        public void OverrideSecond()
        {
            _victoryPopup.SetActive(true);
            // _pverrideScreen2.SetActive(true);
            // _currentPanel = _pverrideScreen2;
            // _manualOverride1.SetActive(false);
        }

        /// <summary>
        /// Displays a popup if the player selected the wrong Feynman diagram.
        /// </summary>
        /// <param name="error">The error to be displayed on the popup depending on the player's mistake.</param>
        public void ErrorParticles(string error)
        {
            _errorParticles.SetActive(true);
            _errorParticles.GetComponentInChildren<Text>().text = error;
            StartCoroutine(_maiaTopScreen.WaitGeneric(2f, () =>
            {
                _errorParticles.SetActive(false);
            }));
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
        /// Displays a popup if the player selected the right Feynman diagram.
        /// </summary>
        private void SuccessParticles()
        {
            _successParticles.SetActive(true);
        }

        /// <summary>
        /// Generates a grid for particles to be entered in.
        /// </summary>
        /// <param name="particles">The particles detected.</param>
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
        /// Delete all the entered particles
        /// </summary>
        public void ClearParticles()
        {
            foreach (Image particleCase in _particleCases)
            {
                particleCase.enabled = false;
            }
        }

        /// <summary>
        /// Delete the last particle entered
        /// </summary>
        /// <param name="nbParticles">The number of particles already entered.</param>
        public void DeleteParticle(int nbParticles)
        {
            _particleCases[nbParticles].enabled = false;
        }

        /// <summary>
        /// Displays the particles combination while they are being entered.
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
    }
}

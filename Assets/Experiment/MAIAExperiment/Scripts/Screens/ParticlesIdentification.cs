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
        /// Victory popup for the demo version.
        /// </summary>
        [SerializeField]
        private GameObject _victoryPopup = null;

        private void Start()
        {
            _particleTextMessage = _nbParticlesDetected.text;
        }

        /// <summary>
        /// Ends the game for the demo version.
        /// </summary>
        public void OverrideSecond()
        {
            _victoryPopup.SetActive(true);
        }

        /// <summary>
        /// Displays a popup if the player selected the wrong Feynman diagram.
        /// </summary>
        /// <param name="error">The error to be displayed on the popup depending on the player's mistake.</param>
        public void DisplayErrorMessage(string error)
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
        public void FillNbParticlesDetected(List<Particle> generatedParticles, List<Particle> particles)
        {
            _nbParticlesDetected.text = _particleTextMessage.Replace("[p]", (generatedParticles.Count - particles.Count).ToString());
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
            _gridParticles = new GridCell[particles.Count];
            for (int i = 0; i < particles.Count; i++)
            {
                var gridCell = Instantiate(_gridCellPrefab, _particlesGrid);
                _gridParticles[i] = gridCell;
            }
        }

        /// <summary>
        /// Delete all the entered particles
        /// </summary>
        public void ClearParticles()
        {
            foreach (GridCell gridCell in _gridParticles)
            {
                gridCell.Show(false);
            }
        }

        /// <summary>
        /// Delete the last particle entered
        /// </summary>
        /// <param name="nbParticles">The number of particles already entered.</param>
        public void DeleteParticle(int nbParticles)
        {
            _gridParticles[nbParticles].enabled = false;
        }

        /// <summary>
        /// Displays the particles combination while they are being entered.
        /// </summary>
        /// <param name="particles">The particles combination that is being entered.</param>
        public void DisplayParticles(List<Particle> particles)
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
    }
}

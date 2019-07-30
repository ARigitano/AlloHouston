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
        public MAIAManager maiaManager { get; private set; }
        /// <summary>
        /// Analysis screen.
        /// </summary>
        [SerializeField]
        private MAIAAnalysisScreen _analysisScreen = null;
        /// <summary>
        /// Script for the Manual Override Access screen.
        /// </summary>
        [SerializeField]
        private MAIAManualOverrideAccess _manualOverrideAccess = null;
        [SerializeField]
        [Tooltip("")]
        private AudioClip _accessDenied = null;
        [SerializeField]
        [Tooltip("")]
        private AudioClip _accessGranted = null;
        [SerializeField]
        [Tooltip("")]
        private AudioClip _analysis = null;
        [SerializeField]
        [Tooltip("")]
        private AudioClip _windowOpen = null;

        public MAIAManualOverrideAccess manualOverrideAccessScreen
        {
            get
            {
                return _manualOverrideAccess;
            }
        }
        /// <summary>
        /// Script for the Particles Identification screen.
        /// </summary>
        [SerializeField]
        private MAIAParticlesIdentification _particlesIdentification = null;

        public MAIAParticlesIdentification particleIdentificationScreen
        {
            get
            {
                return _particlesIdentification;
            }
        }
        /// <summary>
        /// Script for the Reactions Identification screen.
        /// </summary>
        [SerializeField]
        private MAIAReactionIdentificationScreen _reactionsIdentification = null;
        [SerializeField]
        private GameObject _victoryScreen = null;
        /// <summary>
        /// Stores the panel currently being displayed.
        /// </summary>
        public GameObject _currentPanel { get; private set; }
        public MAIATabletScreen tabletScreen;
        //TODO: obsolete second part of experiment?
        /// <summary>
        /// An error depending on the player's diagram selection mistake.
        /// </summary>
        [HideInInspector]
        public string particleErrorString;

        public void CreateParticleGrid(List<Particle> reactionExits)
        {
            _particlesIdentification.CreateParticleGrid(reactionExits);
        }

        private void ActivatePanel(GameObject newPanel)
        {
            if (_currentPanel != null)
                _currentPanel.SetActive(false);
            newPanel.SetActive(true);
            _currentPanel = newPanel;
        }
        
        public void StartLoading()
        {
            ActivatePanel(_manualOverrideAccess.gameObject);
            _manualOverrideAccess.Show();
        }

        public void StartMO()
        {
            ActivatePanel(_manualOverrideAccess.gameObject);
        }

        public void StartPassword()
        {
            ActivatePanel(_manualOverrideAccess.gameObject);
            _manualOverrideAccess.DisplayPasswordWindow();
        }

        public void StartPI()
        {
            ActivatePanel(_particlesIdentification.gameObject);
            _particlesIdentification.DisplayParticlePanel();
        }

        public void StartCI()
        {
            ActivatePanel(_particlesIdentification.gameObject);
            _particlesIdentification.DisplayChargePanel();
        }

        /// <summary>
        /// Tells the Particle Identification panel that the right combination of particles has been entered.
        /// </summary>
        public void StartAnalysisAnimation()
        {
            ActivatePanel(_analysisScreen.gameObject);
            _analysisScreen.StartAnalysisAnimation();
        }

        public void StartRI()
        {
            ActivatePanel(_reactionsIdentification.gameObject);
            _reactionsIdentification.StartReactionIdentification();
        }

        public void StartVictory()
        {
            ActivatePanel(_victoryScreen.gameObject);
        }

        /// <summary>
        /// A generic coroutine to wait during a method.
        /// </summary>
        /// <param name="time">The time to wait.</param>
        /// <param name="action">The action to be done after the waiting delay is over.</param>
        /// <returns></returns>
        public IEnumerator WaitGeneric(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action.Invoke();
        }

        public void Init(MAIAManager synchronizer)
        {
            maiaManager = synchronizer;
        }

        public void Access(bool isGranted)
        {
            _manualOverrideAccess.Access(isGranted);
        }

        public void OnReactionSelected(bool correctDiagram)
        {
            _reactionsIdentification.ReactionSelected(correctDiagram);
        }
        
        /// <summary>
        /// Effect when the experiment is correctly resolved.
        /// </summary>
        public override void OnSuccess()
        {
            StartVictory();
        }

        public override void OnInit(XPManager manager, int randomSeed)
        {
            base.OnInit(manager, randomSeed);
            Init((MAIAManager)manager);
        }

        /// <summary>
        /// Effect when the experiment is activated the first time.
        /// </summary>
        public override void OnShow(int step)
        {
            base.OnShow(step);
            _manualOverrideAccess.InitPasswordInput(maiaManager.settings.password);
            _particlesIdentification.CreateParticleGrid(maiaManager.generatedParticles);
        }
    }
}
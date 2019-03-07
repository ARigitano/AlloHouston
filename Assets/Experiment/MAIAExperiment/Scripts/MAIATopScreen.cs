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
        /// Script for the Exile Loading screen.
        /// </summary>
        [SerializeField]
        private ExileLoading _exileLoading = null;
        /// <summary>
        /// Script for the Maia Loading screen.
        /// </summary>
        [SerializeField]
        private MAIALoading _maiaLoading = null;
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
        private ParticlesIdentification _particlesIdentification = null;

        public ParticlesIdentification particleIdentificationScreen
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

        public void Victory()
        {
            _currentPanel.SetActive(false);
            _victoryScreen.SetActive(true);
        }

        private void ActivatePanel(GameObject newPanel)
        {
            if (_currentPanel != null)
                _currentPanel.SetActive(false);
            newPanel.SetActive(true);
            _currentPanel = newPanel;
        }

        /// <summary>
        /// Called by the synchronizer to skip directly to the Feynman diagrams step.
        /// </summary>
        public void SkipToSecondPart()
        {
            StartAnalysisAnimation();
        }

        /// <summary>
        /// Tells the MAIA Overwiew panel that the start button has been pressed.
        /// </summary>
        public void StartManualOverride()
        {
            ActivatePanel(_manualOverrideAccess.gameObject);
            _manualOverrideAccess.Show();
            _currentPanel = _manualOverrideAccess.gameObject;
        }

        public void StartPI()
        {
            ActivatePanel(_particlesIdentification.gameObject);
            _particlesIdentification.CreateParticleGrid(maiaManager.generatedParticles);
        }

        /// <summary>
        /// Tells the Particle Identification panel that the right combination of particles has been entered.
        /// </summary>
        public void StartAnalysisAnimation()
        {
            ActivatePanel(_analysisScreen.gameObject);
            _particlesIdentification.gameObject.SetActive(false);
            _analysisScreen.StartAnalysisAnimation();
        }

        public void StartRI()
        {
            _analysisScreen.gameObject.SetActive(false);
            ActivatePanel(_reactionsIdentification.gameObject);
            _reactionsIdentification.StartReactionIdentification();
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

        /// <summary>
        /// Displays the access screen when the manual override button is pressed.
        /// </summary>
        public void InitPasswordInput()
        {
            _manualOverrideAccess.InitPasswordInput(maiaManager.settings.password);
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
            Victory();
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
            StartManualOverride();
        }
    }
}
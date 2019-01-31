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
        public MAIAManager manager { get; private set; }
        /// <summary>
        /// Script for the Exile Loading screen.
        /// </summary>
        [SerializeField]
        private ExileLoading _exileLoading;
        /// <summary>
        /// Script for the Exile Loading screen.
        /// </summary>
        [SerializeField]
        private MAIALoading _maiaLoading;
        /// <summary>
        /// Script for the MAIA Overview screen.
        /// </summary>
        [SerializeField]
        private MAIAOverview _maiaOverview = null;
        /// <summary>
        /// Script for the Manual Override Access screen.
        /// </summary>
        [SerializeField]
        private MAIAManualOverrideAccess _manualOverrideAccess = null;
        /// <summary>
        /// Script for the Particles Identification screen.
        /// </summary>
        [SerializeField]
        private ParticlesIdentification _particlesIdentification = null;
        /// <summary>
        /// Script for the Reactions Identification screen.
        /// </summary>
        [SerializeField]
        private ReactionsIdentification _reactionsIdentification;
        //TODO: delete when windowws class integrated
        /// <summary>
        /// All the panels of the top left screen of the experiment.
        /// </summary>
        public GameObject _exileLoadingScreen, _maiaLoadingScreen, _maiaOverviewScreen, _manualOverrideAccessScreen, _manualOverride1, _popupErrorMessageParticles, _pverrideScreen2;

        public void ManualOverrideActive()
        {
            manager.ManualOverrideActive();
        }

        /// <summary>
        /// Stores the panel currently being displayed.
        /// </summary>
        public GameObject _currentPanel;
        public MAIATabletScreen tabletScreen;
        //TODO: obsolete second part of experiment?
        /// <summary>
        /// An error depending on the player's diagram selection mistake.
        /// </summary>
        [HideInInspector]
        public string particleErrorString;

        public void ParticleGrid(List<Particle> reactionExits)
        {
            _particlesIdentification.ParticleGrid(reactionExits);
        }

        public void DisplayPassword(string enteredPassword)
        {
            _manualOverrideAccess.CheckPassword(enteredPassword);
        }

        public void FillNbParticlesDetected(List<Particle> reactionExits)
        {
            _particlesIdentification.FillNbParticlesDetected(reactionExits);
        }

        public void DisplayParticles(List<Particle> _enteredParticles)
        {
            _particlesIdentification.DisplayParticles(_enteredParticles);
        }

        //TODO: obsolete second part of experiment?
        /*public void FillParticlesTable(int nbAntielectron, _textAntielectron)
        {
            _reactionsIdentification.FillParticlesTable(nbAntielectron, _textAntielectron)
        }*/

        /// <summary>
        /// Tells the MAIA Overwiew panel that the start button has been pressed.
        /// </summary>
        public void ManualOverride()
        {
            _maiaOverview.ManualOverride();
        }

        public void DeleteParticle(int count)
        {
            _particlesIdentification.DeleteParticle(count);
        }

        /// <summary>
        /// Tells the particle identification panel to clear all the entered particles.
        /// </summary>
        public void ClearParticles()
        {
            _particlesIdentification.ClearParticles();
        }

        /// <summary>
        /// Tells the Particle Identification panel that the right combination of particles has been entered.
        /// </summary>
        public void OverrideSecond()
        {
            _particlesIdentification.OverrideSecond();
        }

        private void Start()
        {
            //TODO: change when exile loading screen created;
            _currentPanel = _maiaLoadingScreen;
        }

        public void ParticleIdentification()
        {
            //ParticleGrid(manager.generatedParticles);
            Debug.Log(manager);
            Debug.Log(manager.generatedParticles.Count);
            _reactionsIdentification.FillParticlesTable(manager.generatedParticles);
            //_reactionsIdentification.FillChosenDiagrams(manager.ongoingReactions, manager.selectedReaction);
        }

        /// <summary>
        /// Called by the synchronizer to skip directly to the Feynman diagrams step.
        /// </summary>
        public void SkipStepOne()
        {
            if (_currentPanel != null)
                _currentPanel.SetActive(false);

            _pverrideScreen2.SetActive(true);
            ParticleIdentification();
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
        public void AccessCode()
        {
            _manualOverrideAccessScreen.SetActive(true);
            _currentPanel = _manualOverrideAccessScreen;
            _maiaOverviewScreen.SetActive(false);
        }

        public void Init(MAIAManager synchronizer)
        {
            manager = synchronizer;
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
        public override void OnActivation(XPManager manager)
        {
            Debug.Log(name + "Activated");
            Init((MAIAManager)manager);
            //StartCoroutine("Loading");
        }

        public void AccessGranted()
        {
            _manualOverride1.SetActive(true);
            _currentPanel = _manualOverride1;
            _manualOverrideAccessScreen.SetActive(false);
            _particlesIdentification.ParticleGrid(manager.generatedParticles);
            _particlesIdentification.DisplayParticles(manager.generatedParticles);
            tabletScreen.AccessGranted();
        }

        public void Access(bool isGranted)
        {
            _manualOverrideAccess.Access(isGranted);
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

        public void ErrorParticles(string particleErrorString)
        {
            _particlesIdentification.ErrorParticles(particleErrorString);
        }

        public void ReactionSelected(bool correctDiagram)
        {
            _reactionsIdentification.ReactionSelected(correctDiagram);
        }
    }
}
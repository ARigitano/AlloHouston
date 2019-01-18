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
        private MAIAManager _manager;
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
        private MAIAOverview _maiaOverview;
        /// <summary>
        /// Script for the Manual Override Access screen.
        /// </summary>
        [SerializeField]
        private ManualOverrideAccess manualOverrideAccess;
        /// <summary>
        /// Script for the Particles Identification screen.
        /// </summary>
        [SerializeField]
        private ParticlesIdentification _particlesIdentification;
        /// <summary>
        /// Script for the Reactions Identification screen.
        /// </summary>
        [SerializeField]
        private ReactionsIdentification _reactionsIdentification;
        /// <summary>
        /// All the panels of the top left screen of the experiment.
        /// </summary>
        [SerializeField]
        private GameObject _exileLoadingScreen, _maiaLoadingScreen, _maiaOverviewScreen, _manualOverrideAccess, _manualOverride1, _popupErrorMessageParticles, _pverrideScreen2;
        /// <summary>
        /// Stores the panel currently being displayed.
        /// </summary>
        private GameObject _currentPanel;

        /// <summary>
        /// Tells the MAIA Overwiew panel that the start button has been pressed.
        /// </summary>
        public void ManualOverride()
        {
            _maiaOverview.ManualOverride();
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

        /// <summary>
        /// Called by the synchronizer to skip directly to the Feynman diagrams step.
        /// </summary>
        public void SkipStepOne()
        {
            if (_currentPanel != null)
                _currentPanel.SetActive(false);

            _pverrideScreen2.SetActive(true);
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
            _manualOverrideAccess.SetActive(true);
            _currentPanel = _manualOverrideAccess;
            _maiaOverviewScreen.SetActive(false);
        }

        public void Init(MAIAManager synchronizer)
        {
            _manager = synchronizer;
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
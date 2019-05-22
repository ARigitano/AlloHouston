using System.Collections;
using UnityEngine;
using System;
using CRI.HelloHouston.WindowTemplate;

namespace CRI.HelloHouston.Experience.MAIA
{
    /// <summary>
    /// The tablet screen of the experiment block for the particle physics experiment.
    /// </summary>
    public class MAIATabletScreen : XPElement
    {
        public MAIAManager maiaManager { get; private set; }
        public MAIATopScreen topScreen { get; private set; }
        private MAIATubeScreen _tubeScreen;
        [Header("MAIATabletScreen Attributes")]
        [SerializeField]
        [Tooltip("The manual override panel.")]
        private Window _moPanel = null;
        [SerializeField]
        [Tooltip("The password identification panel.")]
        private MAIAPasswordPanel _passwordPanel = null;
        [SerializeField]
        [Tooltip("The particles panel.")]
        private MAIAParticlePanel _particlesPanel = null;
        [SerializeField]
        [Tooltip("The particle charges.")]
        private MAIAParticleChargesPanel _particleChargesPanel = null;
        [SerializeField]
        [Tooltip("The advanced manual override panel.")]
        private Window _amoPanel = null;
        [SerializeField]
        [Tooltip("The diagrams panel.")]
        private MAIAReactionPanel _reactionPanel = null;
        /// <summary>
        /// The panel currently being displayed.
        /// </summary>
        private Window _currentPanel;
        [SerializeField]
        [Tooltip("")]
        private AudioClip _btnManualOverride = null;
        [SerializeField]
        [Tooltip("")]
        private AudioClip _btnNumber = null;
        [SerializeField]
        [Tooltip("")]
        private AudioClip _panelNumbersIn = null;
        [SerializeField]
        [Tooltip("")]
        private AudioClip _panelSlidersIn = null;
        [SerializeField]
        [Tooltip("")]
        private AudioClip _windowOpen = null;

        private Window _previousPanel;

        public void StartVictory()
        {
            DeactivatePanel();
        }

        private void DeactivatePanel(Action action = null)
        {
            ActivatePanel(null, action);
        }

        private void ActivatePanel(Window newPanel, Action action = null)
        {
            if (newPanel != null && action == null)
                action = newPanel.ShowWindow;
            // We stop the previous panel animation if it didn't finish yet.
            if (_previousPanel != null && _previousPanel.visible)
            {
                _previousPanel.StopAllCoroutines();
                _previousPanel.gameObject.SetActive(false);
            }
            if (_currentPanel == newPanel)
                return;
            else if (_currentPanel != null)
                _currentPanel.HideWindow(action);
            else if (newPanel != null)
                newPanel.ShowWindow();
            _previousPanel = _currentPanel;
            _currentPanel = newPanel;
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

        public void HideAllPanels()
        {
            DeactivatePanel();
        }

        /// <summary>
        /// Tells every screen that the right combination of particles has been entered.
        /// </summary>
        public void OnRightParticleCombination()
        {
            topScreen.maiaManager.OnPISuccess();
        }

        public void OnRightChargeCombination()
        {
            topScreen.maiaManager.OnCISuccess();
        }

        public void OnAMOClick()
        {
            DeactivatePanel();
            topScreen.maiaManager.OnAMOSuccess();
        }

        public void StartAMO()
        {
            ActivatePanel(_amoPanel);
        }

        /// <summary>
        /// Displays the particle reaction panel.
        /// </summary>
        public void StartRI()
        {
            ActivatePanel(_reactionPanel);
        }

        /// <summary>
        /// Displays particle selection panel.
        /// </summary>
        public void StartPI()
        {
            ActivatePanel(_particlesPanel);
        }

        /// <summary>
        /// Displays the particle charge panel.
        /// </summary>
        public void StartCI()
        {
            ActivatePanel(_particleChargesPanel);
        }

        /// <summary>
        /// Activates the manual override panel.
        /// </summary>
        public void StartMO()
        {
            ActivatePanel(_moPanel);
        }

        public void StartPassword()
        {
            ActivatePanel(_passwordPanel);
        }

        /// <summary>
        /// Displays password panel adter override button has been clicked.
        /// </summary>
        public void OnMOClick()
        {
            maiaManager.OnMOSuccess();
        }
        
        public override void OnInit(XPManager manager, int randomSeed)
        {
            base.OnInit(manager, randomSeed);
            this.maiaManager = (MAIAManager)manager;
        }

        public override void OnShow(int step)
        {
            topScreen = maiaManager.topScreen;
            _passwordPanel.Init(maiaManager.logController, maiaManager.xpContext, topScreen.manualOverrideAccessScreen, maiaManager.settings.password);
            _particlesPanel.Init(maiaManager, topScreen.particleIdentificationScreen);
            _particleChargesPanel.Init(maiaManager, topScreen.particleIdentificationScreen);
            _reactionPanel.Init(topScreen, maiaManager);
        }

        public override void Dismiss()
        {
            DeactivatePanel(base.Dismiss);
        }
    }
}

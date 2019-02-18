using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

namespace CRI.HelloHouston.Experience.MAIA
{
    /// <summary>
    /// The tablet screen of the experiment block for the particle physics experiment.
    /// </summary>
    public class MAIATabletScreen : XPElement
    {
        public MAIAManager manager { get; private set; }
        public MAIATopScreen topScreen { get; private set; }
        private MAIAHologramTube _hologramTube;
        private MAIAHologramFeynman _hologramFeynman;
        private MAIATubeScreen _tubeScreen;
        [Header("MAIATabletScreen Attributes")]
        [SerializeField]
        [Tooltip("The manual override panel.")]
        private GameObject _moPanel = null;
        [SerializeField]
        [Tooltip("The password identification panel.")]
        private MAIAPasswordPanel _passwordPanel = null;
        [SerializeField]
        [Tooltip("The particles panel.")]
        private MAIAParticlePanel _particlesPanel = null;
        [SerializeField]
        [Tooltip("The advanced manual override panel.")]
        private GameObject _amoPanel = null;
        [SerializeField]
        [Tooltip("The diagrams panel.")]
        private MAIAReactionPanel _reactionPanel = null;
        /// <summary>
        /// The panel currently being displayed.
        /// </summary>
        private GameObject _currentPanel;

        public void Victory()
        {
            ActivatePanel(null);
        }

        /// <summary>
        /// Called by the synchronizer to skip directly to the Feynman diagrams step.
        /// </summary>
        public void SkipStepOne()
        {
            HideAllPanels();
        }

        private void ActivatePanel(GameObject newPanel)
        {
            if (_currentPanel != null)
                _currentPanel.gameObject.SetActive(false);
            if (newPanel != null)
                newPanel.gameObject.SetActive(true);
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
            ActivatePanel(null);
        }

        /// <summary>
        /// Tells every screen that the right combination of particles has been entered.
        /// </summary>
        public void OnRightParticleCombination()
        {
            topScreen.manager.StartAnalysisAnimation();
            ActivatePanel(null);
        }

        public void StartAdvancedManualOverride()
        {
            ActivatePanel(null);
            topScreen.manager.StartReactionIdentification();
        }

        public void ShowAdvanceManualOverride()
        {
            ActivatePanel(_amoPanel);
        }

        public void ShowReactionIdentification()
        {
            ActivatePanel(_reactionPanel.gameObject);
        }
        /// <summary>
        /// Displays particle selection panel after the correct password have been entered.
        /// </summary>
        public void ShowParticlesPanel()
        {
            ActivatePanel(_particlesPanel.gameObject);
        }

        /// <summary>
        /// Activates the manual override panel.
        /// </summary>
        public void ShowManualOverridePanel()
        {
            ActivatePanel(_moPanel);
        }

        /// <summary>
        /// Displays password panel adter override button has been clicked.
        /// </summary>
        public void OnOverrideButtonClicked()
        {
            topScreen.InitPasswordInput();
            StartCoroutine(WaitGeneric(0.2f, () =>
            {
                ActivatePanel(_passwordPanel.gameObject);
            }));
        }

        private void Init(MAIAManager manager)
        {
            this.manager = manager;
            Debug.Log(manager);
            topScreen = manager.topScreen;
            _passwordPanel.Init(topScreen.manualOverrideAccessScreen, this.manager.settings.password);
            _particlesPanel.Init(manager, topScreen.particleIdentificationScreen);
            _reactionPanel.Init(topScreen, manager);
            _hologramTube = manager.hologramTube;
            _currentPanel = _moPanel;
        }

        public override void OnActivation(XPManager manager)
        {
            base.OnActivation(manager);
            Init((MAIAManager)manager);
        }
    }
}

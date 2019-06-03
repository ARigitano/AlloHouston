using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRI.HelloHouston.WindowTemplate;
using System;

/// <summary>
/// The tablet screen for the tutorial experiment.
/// </summary>
namespace CRI.HelloHouston.Experience.Tutorial
{
    public class TutorialTabletScreen : XPElement
    {
        /// <summary>
        /// The manager for the experiment.
        /// </summary>
        public TutorialManager tutorialManager { get; private set; }
        /// <summary>
        /// Panel to launch the maintenance process
        /// </summary>
        [SerializeField]
        private Window _launchPanel = null;
        /// <summary>
        /// Panel to launch the second maintenance process
        /// </summary>
        [SerializeField]
        private Window _secondMaintenancePanel = null;
        /// <summary>
        /// The panel currently being displayed.
        /// </summary>
        private Window _currentPanel;
        /// <summary>
        /// The panel previously displayd
        /// </summary>
        private Window _previousPanel;

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
        /// Displays the launch panel
        /// </summary>
        public void StartLaunch()
        {
            ActivatePanel(_launchPanel, null);
        }

        /// <summary>
        /// Launches the maintenance process
        /// </summary>
        public void PressedLaunch()
        {
            Debug.Log("Maintenance launched");
            DeactivatePanel(null);
            tutorialManager.OnLaunchSuccess();
        }

        /// <summary>
        /// Displays the second launch panel
        /// </summary>
        public void StartSecondMaintenance()
        {
            ActivatePanel(_secondMaintenancePanel);
            Debug.Log("Second Maintenance button activated");
        }

        /// <summary>
        /// Launches the second maintenance process
        /// </summary>
        public void PressedSecondMaintenance()
        {
            Debug.Log("Second Maintenance launched");
            DeactivatePanel(null);
            tutorialManager.MaintenanceVirus();
        }
        
        public override void OnShow(int currentStep)
        {
            base.OnShow(currentStep);
            StartLaunch();
        }

        private void Init(TutorialManager synchronizer)
        {
            tutorialManager = synchronizer;
        }

        public override void OnInit(XPManager manager, int randomSeed)
        {
            base.OnInit(manager, randomSeed);
            Init((TutorialManager)manager);
        }
    }
}

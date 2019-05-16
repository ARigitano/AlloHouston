using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRI.HelloHouston.WindowTemplate;

/// <summary>
/// The tablet screen for the tutorial experiment.
/// </summary>
namespace CRI.HelloHouston.Experience.Tutorial
{
    public class TutorialTabletScreen : XPElement
    {
        public TutorialManager tutorialManager { get; private set; }
        [SerializeField]
        private Window _launchPanel = null;
        [SerializeField]
        private Window _secondMaintenancePanel = null;
        /// <summary>
        /// The panel currently being displayed.
        /// </summary>
        private Window _currentPanel;

        private Window _previousPanel;

        private void ActivatePanel(Window newPanel)
        {
            // We stop the previous panel animation if it didn't finish yet.
            if (_previousPanel != null && _previousPanel.visible)
            {
                _previousPanel.StopAllCoroutines();
                _previousPanel.gameObject.SetActive(false);
            }
            if (_currentPanel == newPanel)
                return;
            else if (_currentPanel != null && newPanel != null)
                _currentPanel.HideWindow(newPanel.ShowWindow);
            else if (_currentPanel != null && newPanel == null)
                _currentPanel.HideWindow();
            else if (newPanel != null)
                newPanel.ShowWindow();
            _previousPanel = _currentPanel;
            _currentPanel = newPanel;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        public void StartLaunch()
        {
            ActivatePanel(_launchPanel);
            Debug.Log("Launch button activated");
        }

        public void PressedLaunch()
        {
            Debug.Log("Maintenance launched");
            tutorialManager.OnLaunchSuccess();
        }

        public void StartSecondMaintenance()
        {
            ActivatePanel(_secondMaintenancePanel);
            Debug.Log("Second Maintenance button activated");
        }

        public void PressedSecondMaintenance()
        {
            Debug.Log("Second Maintenance launched");
            tutorialManager.MaintenanceVirus();
        }

        // Update is called once per frame
        void Update()
        {

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience.UI
{
    internal class UIExperienceStatus : MonoBehaviour
    {
        /// <summary>
        /// Text field for the name of the experience.
        /// </summary>
        [SerializeField]
        [Tooltip("Text field for the name of the experience.")]
        private Text _nameText = null;
        /// <summary>
        /// Button to launch the experiment.
        /// </summary>
        [SerializeField]
        [Tooltip("Button to launch the experiment.")]
        private Button _launchButton = null;
        /// <summary>
        /// Button to finish successfully the experiment.
        /// </summary>
        [SerializeField]
        [Tooltip("Button to finish successfully the experiment.")]
        private Button _successButton = null;
        /// <summary>
        /// Button to automatically fail the experiment.
        /// </summary>
        [SerializeField]
        [Tooltip("Button to automatically fail the experiment.")]
        private Button _failButton = null;
        /// <summary>
        /// Canvas group of the icon / text when the experiment is in progress.
        /// </summary>
        [SerializeField]
        [Tooltip("Canvas group of the icon or text when the experiment is in progress.")]
        private CanvasGroup _inProgress = null;
        /// <summary>
        /// Canvas group of the icon / text when the experiment is finished.
        /// </summary>
        [SerializeField]
        [Tooltip("Canvas group of the icon or text when the experiment is finished.")]
        private CanvasGroup _finished = null;

        /// <summary>
        /// Canvas group of the icon / text when the experiment is finished.
        /// </summary>
        [SerializeField]
        [Tooltip("Canvas group of the icon / text when the experiment is inactive.")]
        private CanvasGroup _inactive = null;
        /// <summary>
        /// Color of the text of the button that wasn't selected by the user.
        /// </summary>
        [SerializeField]
        [Tooltip("Color of the text of the button that wasn't selected by the user.")]
        private Color _unselectedButtonColor = Color.white;
        /// <summary>
        /// Prefab of a popup.
        /// </summary>
        [SerializeField]
        [Tooltip("Prefab of a popup.")]
        private UIPopup _popupPrefab = null;
        /// <summary>
        /// Text of the fail popup.
        /// </summary>
        [SerializeField]
        [Tooltip("Text of the fail popup.")]
        private string _failPopupText = null;
        /// <summary>
        /// Text of the success popup.
        /// </summary>
        [SerializeField]
        [Tooltip("Text of the success popup.")]
        private string _successPopupText = null;
        /// <summary>
        /// Text of the popup when there's not enough time left.
        /// </summary>
        [SerializeField]
        [Tooltip("Text of the popup when there's not enough time left.")]
        private string _notEnoughTimeText = null;

        private XPSynchronizer _xpSynchronizer;

        public void Init(GameManager gameManager, XPSynchronizer xpSynchronizer)
        {
            _xpSynchronizer = xpSynchronizer;
            _nameText.text = xpSynchronizer.xpContext.contextName;
            _launchButton.onClick.AddListener(() =>
            {
                if (gameManager.xpTimeEstimate * 60 < gameManager.timeSinceGameStart + (xpSynchronizer.xpContext.xpSettings.duration * 60))
                    CreatePopup(_notEnoughTimeText, LaunchAction);
                else
                    LaunchAction();
            });
            _launchButton.GetComponent<CanvasGroup>().Show();
            _failButton.onClick.AddListener(() => CreatePopup(_failPopupText, FailAction));
            _failButton.GetComponent<CanvasGroup>().Hide();
            _successButton.onClick.AddListener(() => CreatePopup(_successPopupText, SuccessAction));
            _successButton.GetComponent<CanvasGroup>().Hide();
            SetState(xpSynchronizer.state);
            xpSynchronizer.onStateChange += SetState;
        }

        private void CreatePopup(string popupText, UnityAction action)
        {
            UIPopup popup = GameObject.Instantiate(_popupPrefab, GetComponentInParent<Canvas>().transform);
            popup.Init(popupText, action);
        }

        private void LaunchAction()
        {
            _xpSynchronizer.Activate();
            _launchButton.GetComponent<CanvasGroup>().Hide();
            _failButton.GetComponent<CanvasGroup>().Show();
            _successButton.GetComponent<CanvasGroup>().Show();
        }

        private void FailAction()
        {
            _xpSynchronizer.Fail();
            _successButton.interactable = false;
            _failButton.interactable = false;
            if (_successButton.GetComponentInChildren<Text>())
                _successButton.GetComponentInChildren<Text>().color = _unselectedButtonColor;
        }

        private void SuccessAction()
        {
            _xpSynchronizer.Success();
            _successButton.interactable = false;
            _failButton.interactable = false;
            if (_failButton.GetComponentInChildren<Text>())
                _failButton.GetComponentInChildren<Text>().color = _unselectedButtonColor;
        }

        private void SetState(XPState state)
        {
            if (state == XPState.Inactive)
            {
                _inProgress.Hide();
                _finished.Hide();
                _inactive.Show();
            }
            else if (_xpSynchronizer.active)
            {
                _inProgress.Show();
                _finished.Hide();
                _inactive.Hide();
            }
            else
            {
                _inProgress.Hide();
                _finished.Show();
                _inactive.Hide();
            }
        }
    }
}

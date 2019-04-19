using CRI.HelloHouston.Translation;
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
        /// Button to display all the actions available for this experience.
        /// </summary>
        [SerializeField]
        [Tooltip("Button to display all the actions available for this experience.")]
        private UIExperienceActionButton _actionButton = null;
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
        /// Dropdown that selects the language of the experiment.
        /// </summary>
        [SerializeField]
        [Tooltip("Dropdown that selects the language of the experiment.")]
        private UILangDropdown _langDropdown = null;
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
        private Color _failButtonColor;
        private Color _successButtonColor;
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
        private string _failPopupTextKey = null;
        /// <summary>
        /// Text of the success popup.
        /// </summary>
        [SerializeField]
        [Tooltip("Text of the success popup.")]
        private string _successPopupTextKey = null;
        /// <summary>
        /// Text of the popup when there's not enough time left.
        /// </summary>
        [SerializeField]
        [Tooltip("Text of the popup when there's not enough time left.")]
        private string _timePopupTextKey = null;

        private XPManager _xpManager;

        private void OnDisable()
        {
            if (_xpManager != null)
                _xpManager.onStateChange -= OnStateChange;
        }

        public void Init(GameManager gameManager, XPManager xpManager)
        {
            TextManager textManager = GameManager.instance.textManager;
            LangManager langManager = xpManager.langManager;
            _xpManager = xpManager;
            _nameText.text = xpManager.xpContext.contextName;
            _actionButton.Init(xpManager.xpContext.xpSettings.actions, xpManager.actionController);
            _langDropdown.InitDropdown(langManager);
            _launchButton.onClick.AddListener(() =>
            {
                if (gameManager.xpTimeEstimate * 60 < gameManager.timeSinceGameStart + (xpManager.xpContext.xpSettings.duration * 60))
                    CreatePopup(textManager.GetText(_timePopupTextKey), LaunchAction);
                else
                    LaunchAction();
            });
            _failButton.onClick.AddListener(() => CreatePopup(textManager.GetText(_failPopupTextKey), FailAction));
            if (_failButton.GetComponentInChildren<Text>())
                _failButtonColor = _failButton.GetComponentInChildren<Text>().color;
            _successButton.onClick.AddListener(() => CreatePopup(textManager.GetText(_successPopupTextKey), SuccessAction));
            if (_successButton.GetComponentInChildren<Text>())
                _successButtonColor = _successButton.GetComponentInChildren<Text>().color;
            if (xpManager.state == XPState.Inactive)
            {
                _launchButton.GetComponent<CanvasGroup>().Show();
                _failButton.GetComponent<CanvasGroup>().Hide();
                _successButton.GetComponent<CanvasGroup>().Hide();
            }
            else
            {
                _launchButton.GetComponent<CanvasGroup>().Hide();
                _failButton.GetComponent<CanvasGroup>().Show();
                _successButton.GetComponent<CanvasGroup>().Show();
            }
            SetState(xpManager.state);
            xpManager.onStateChange += OnStateChange;
        }

        private void OnStateChange(object sender, XPManagerEventArgs e)
        {
            SetState(e.currentState);
        }

        private void CreatePopup(string popupText, UnityAction action)
        {
            UIPopup popup = GameObject.Instantiate(_popupPrefab, GetComponentInParent<Canvas>().transform);
            popup.Init(popupText, action);
        }

        private void LaunchAction()
        {
            _xpManager.Activate();
            _launchButton.GetComponent<CanvasGroup>().Hide();
            _failButton.GetComponent<CanvasGroup>().Show();
            _successButton.GetComponent<CanvasGroup>().Show();
        }

        private void FailAction()
        {
            _xpManager.Fail();
        }

        private void SuccessAction()
        {
            _xpManager.Success();
        }

        private void SetState(XPState state)
        {
            if (state == XPState.Inactive)
            {
                _inProgress.Hide();
                _finished.Hide();
                _inactive.Show();
            }
            else if (state == XPState.Success)
            {
                _successButton.interactable = false;
                _failButton.interactable = false;
                if (_failButton.GetComponentInChildren<Text>())
                    _failButton.GetComponentInChildren<Text>().color = _unselectedButtonColor;
            }
            else if (state == XPState.Failure)
            {
                _successButton.interactable = false;
                _failButton.interactable = false;
                if (_successButton.GetComponentInChildren<Text>())
                    _successButton.GetComponentInChildren<Text>().color = _unselectedButtonColor;
            }
            else if (state == XPState.InProgress)
            {
                _successButton.interactable = true;
                _failButton.interactable = true;
                if (_successButton.GetComponentInChildren<Text>())
                    _successButton.GetComponentInChildren<Text>().color = _successButtonColor;
                if (_failButton.GetComponentInChildren<Text>())
                    _failButton.GetComponentInChildren<Text>().color = _failButtonColor;
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

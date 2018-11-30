using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        private string _failPopupText;
        /// <summary>
        /// Text of the success popup.
        /// </summary>
        [SerializeField]
        [Tooltip("Text of the success popup.")]
        private string _successPopupText = null;

        private XPSynchronizer _xpSynchronizer;

        public void Init(XPSynchronizer xpSynchronizer)
        {
            _xpSynchronizer = xpSynchronizer;
            _nameText.text = xpSynchronizer.xpContext.contextName;
            _launchButton.onClick.AddListener(() =>
            {
                xpSynchronizer.Activate();
                _launchButton.GetComponent<CanvasGroup>().Hide();
                _failButton.GetComponent<CanvasGroup>().Show();
                _successButton.GetComponent<CanvasGroup>().Show();
            });
            _launchButton.GetComponent<CanvasGroup>().Show();
            _failButton.onClick.AddListener(() =>
            {
                UIPopup popup = GameObject.Instantiate(_popupPrefab, GetComponentInParent<Canvas>().transform);
                popup.Init(_failPopupText, () => { }, () =>
                {
                    xpSynchronizer.Fail();
                    _successButton.interactable = false;
                    _failButton.interactable = false;
                    if (_successButton.GetComponentInChildren<Text>())
                        _successButton.GetComponentInChildren<Text>().color = _unselectedButtonColor;
                });
            });
            _failButton.GetComponent<CanvasGroup>().Hide();
            _successButton.onClick.AddListener(() =>
            {
                UIPopup popup = GameObject.Instantiate(_popupPrefab, GetComponentInParent<Canvas>().transform);
                popup.Init(_successPopupText, () => { }, () =>
                {
                    xpSynchronizer.Success();
                    _successButton.interactable = false;
                    _failButton.interactable = false;
                    if (_failButton.GetComponentInChildren<Text>())
                        _failButton.GetComponentInChildren<Text>().color = _unselectedButtonColor;
                });
            });
            _successButton.GetComponent<CanvasGroup>().Hide();
            SetState(xpSynchronizer.state);
            xpSynchronizer.onStateChange += SetState;
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

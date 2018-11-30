using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace CRI.HelloHouston.Experience.UI
{
    internal class UIPopup : MonoBehaviour
    {
        /// <summary>
        /// Text component of the popup.
        /// </summary>
        [SerializeField]
        [Tooltip("Text component of the popup.")]
        private Text _text = null;
        /// <summary>
        /// Cancel button of the popup.
        /// </summary>
        [SerializeField]
        [Tooltip("Cancel button of the popup.")]
        private Button _cancelButton = null;
        /// <summary>
        /// OK button of the popup.
        /// </summary>
        [SerializeField]
        [Tooltip("OK button of the popup.")]
        private Button _okButton = null; 

        /// <summary>
        /// Initializes the popup and set the action for the ok button.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="okButtonAction"></param>
        public void Init(string text,
            UnityAction okButtonAction)
        {
            _text.text = text;
            _cancelButton.onClick.AddListener(() => Destroy(gameObject));
            _okButton.onClick.AddListener(() => Destroy(gameObject));
            _okButton.onClick.AddListener(okButtonAction);
        }

        /// <summary>
        /// Initializes the popup and set the action for the ok button and the cancel button.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="okButtonAction"></param>
        /// <param name="cancelButtonAction"></param>
        public void Init(string text,
            UnityAction okButtonAction,
            UnityAction cancelButtonAction)
        {
            Init(text, okButtonAction);
            _cancelButton.onClick.AddListener(cancelButtonAction);
        }

        /// <summary>
        /// Initializes the popup and set the actions and texts for the ok button and the cancel button.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="okButtonAction"></param>
        /// <param name="cancelButtonAction"></param>
        /// <param name="okButtonText"></param>
        /// <param name="cancelButtonText"></param>
        public void Init(string text,
            UnityAction okButtonAction,
            UnityAction cancelButtonAction,
            string okButtonText,
            string cancelButtonText)
        {
            Init(text, okButtonAction, cancelButtonAction);
            _okButton.GetComponent<Text>().text = okButtonText;
            _cancelButton.GetComponent<Text>().text = cancelButtonText;
        }
    }
}

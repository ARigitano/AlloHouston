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

        public void Init(string text,
            UnityAction cancelButtonAction,
            UnityAction okButtonAction)
        {
            _text.text = text;
            _cancelButton.onClick.AddListener(() => Destroy(gameObject));
            _cancelButton.onClick.AddListener(cancelButtonAction);
            _okButton.onClick.AddListener(() => Destroy(gameObject));
            _okButton.onClick.AddListener(okButtonAction);
        }
    }
}

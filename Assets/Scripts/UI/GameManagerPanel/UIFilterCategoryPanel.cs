using CRI.HelloHouston.Translation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience.UI {
    internal class UIFilterCategoryPanel : MonoBehaviour {
        /// <summary>
        /// The prefab of the filter toggle.
        /// </summary>
        [SerializeField]
        [Tooltip("The prefab of the filter toggle.")]
        private Toggle _togglePrefab = null;
        /// <summary>
        /// The transform of the content panel.
        /// </summary>
        [SerializeField]
        [Tooltip("Transform of the content panel.")]
        private Transform _toggleTransform = null;
        [SerializeField]
        private RectTransform _panel = null;
        [SerializeField]
        private UICategoryToggle _categoryToggle = null;

        public void Init(UILogDisplay logDisplay, LogFilter[] filters, string categoryName)
        {
            Toggle[] toggles = new Toggle[filters.Length];
            LangManager langManager = GameManager.instance.langManager;
            TextManager textManager = GameManager.instance.textManager;
            _toggleTransform.SetParent(null);
            for (int i = 0; i < filters.Length; i++)
            {
                var filter = filters[i];
                var go = Instantiate(_togglePrefab, _toggleTransform);
                go.onValueChanged.AddListener((value) =>
                {
                    filter.enabled = value;
                    logDisplay.RefreshList();
                    _categoryToggle.Refresh();
                });
                go.isOn = true;
                go.GetComponentInChildren<MainTranslatedText>().InitTranslatedText(langManager, textManager, filter.filterTextKey);
                go.name = "Toggle " + filter.filterTextKey;
                toggles[i] = go;
            }
            _panel.sizeDelta = new Vector2(20 + filters.Length * _togglePrefab.GetComponent<RectTransform>().sizeDelta.x, _panel.sizeDelta.y);
            _toggleTransform.SetParent(_panel);
            _toggleTransform.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            _toggleTransform.GetComponent<RectTransform>().offsetMax = Vector2.zero;
            _categoryToggle.Init(toggles, categoryName);
        } 
    }
}
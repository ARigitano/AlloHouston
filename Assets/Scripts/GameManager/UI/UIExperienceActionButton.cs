using CRI.HelloHouston.Experience.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

namespace CRI.HelloHouston.Experience.UI
{
    [RequireComponent(typeof(Button))]
    public class UIExperienceActionButton : MonoBehaviour, IPointerExitHandler
    {
        /// <summary>
        /// Button to click to display the experience actions.
        /// </summary>
        [SerializeField]
        [Tooltip("Button to click to display the experience actions.")]
        private Button _button = null;
        /// <summary>
        /// Panel where the actions will be displayed.
        /// </summary>
        [SerializeField]
        [Tooltip("Panel where the action will be displayed.")]
        private CanvasGroup _displayPanel = null;
        /// <summary>
        /// UIAction prefab.
        /// </summary>
        [SerializeField]
        [Tooltip("UIAction prefab.")]
        private UIAction _actionPrefab = null;

        private bool _visible = false;

        private void Reset()
        {
            _button = GetComponent<Button>();
        }

        private void Awake()
        {
            if (!_displayPanel.gameObject.GetComponent<UIHoveredItem>())
                _displayPanel.gameObject.AddComponent<UIHoveredItem>();
            _displayPanel.GetComponent<UIHoveredItem>().onHover += OnHoverDisplayPanel;
        }

        private void OnHoverDisplayPanel(bool hovered)
        {
            Debug.Log(hovered);
            if (!hovered && !_visible)
                _displayPanel.Hide();
            _visible = false;
        }

        public void Init(ExperienceAction[] actions, ExperienceActionController actionController)
        {
            foreach (var action in actions)
            {
                var go = Instantiate(_actionPrefab, _displayPanel.transform);
                go.Init(action, actionController);
            }
            _button.onClick.AddListener(() =>
            {
                if (_visible)
                {
                    _displayPanel.Hide();
                }
                else
                {
                    _displayPanel.Show();
                    StopAllCoroutines();
                    _displayPanel.transform.SetParent(GetComponentInParent<Canvas>().transform);
                }
                _visible = !_visible;
            });
        }

        private IEnumerator Hide()
        {
            yield return new WaitForSeconds(0.2f);
            if (!_displayPanel.GetComponent<UIHoveredItem>().hovered && _visible)
            {
                _displayPanel.Hide();
                _visible = false;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            StartCoroutine(Hide());
        }
    }
}

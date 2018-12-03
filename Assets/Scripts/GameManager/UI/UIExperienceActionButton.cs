using CRI.HelloHouston.Experience.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience.UI
{
    [RequireComponent(typeof(Button))]
    public class UIExperienceActionButton : MonoBehaviour
    {
        /// <summary>
        /// Button to click to display the experience actions.
        /// </summary>
        [SerializeField]
        [Tooltip("Button to click to display the experience actions.")]
        private Button _button;
        /// <summary>
        /// Panel where the actions will be displayed.
        /// </summary>
        [SerializeField]
        [Tooltip("Panel where the action will be displayed.")]
        private CanvasGroup _displayPanel;

        private UIAction _actionPrefab;

        private ExperienceAction[] _actions;

        private void Reset()
        {
            _button = GetComponent<Button>();
        }

        public void Init(ExperienceAction[] actions)
        {
            _actions = actions;
        }
    }
}

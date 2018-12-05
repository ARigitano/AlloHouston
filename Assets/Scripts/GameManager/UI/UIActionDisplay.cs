using CRI.HelloHouston.Experience.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.UI
{
    public class UIActionDisplay : MonoBehaviour
    {
        /// <summary>
        /// UIAction prefab.
        /// </summary>
        [SerializeField]
        [Tooltip("UIAction prefab.")]
        private UIAction _actionPrefab = null;
        /// <summary>
        /// Panel where the actions will be displayed.
        /// </summary>
        [SerializeField]
        [Tooltip("Panel where the action will be displayed.")]
        private Transform _displayPanel = null;

        public void Init(GameAction[] actions, GameActionController actionController)
        {
            foreach (var action in actions)
            {
                var go = Instantiate(_actionPrefab, _displayPanel);
                go.Init(action, actionController);
            }
        }
    }
}

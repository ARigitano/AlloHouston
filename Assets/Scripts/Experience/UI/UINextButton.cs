using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston
{
    [RequireComponent(typeof(Button))]
    public class UINextButton : MonoBehaviour
    {
        /// <summary>
        /// The current UI panel.
        /// </summary>
        [SerializeField]
        [Tooltip("The current UI panel.")]
        private UIPanel _current = null;
        /// <summary>
        /// The next UI panel.
        /// </summary>
        [SerializeField]
        [Tooltip("The next UI panel.")]
        private UIPanel _next = null;
        /// <summary>
        /// The next button.
        /// </summary>
        private Button _button;
        /// <summary>
        /// Use to enable the ability to select the button.
        /// </summary>
        public bool interactable
        {
            get
            {
                return _button.interactable;
            }
            set
            {
                _button.interactable = value;
            }
        }
        /// <summary>
        /// If not null, the next button will send an item to the next panel.
        /// </summary>
        public object nextObject { get; set; }

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => Next());
        }

        /// <summary>
        /// Add an OnClick action to the next button.
        /// </summary>
        /// <param name="call"></param>
        public void AddOnClickAction(UnityEngine.Events.UnityAction call)
        {
            _button.onClick.AddListener(call);
        }

        private void Next()
        {
            _current.Hide();
            if (nextObject != null)
                _next.Init(nextObject);
            _next.Show();
        }
    }
}

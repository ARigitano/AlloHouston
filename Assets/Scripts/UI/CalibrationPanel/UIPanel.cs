using System;
using UnityEngine;

namespace CRI.HelloHouston
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIPanel : MonoBehaviour
    {
        /// <summary>
        /// If true, this panel is hidden.
        /// </summary>
        protected bool _hidden = false;
        /// <summary>
        /// The next UIPanel.
        /// </summary>
        [SerializeField]
        [Tooltip("The next UIPanel.")]
        private UIPanel _next;
        public UIPanel next
        {
            get
            {
                return _next; 
            }
        }
        /// <summary>
        /// The CanvasGroup component.
        /// </summary>
        protected CanvasGroup _canvasGroup;
        /// <summary>
        /// If true, hides the UIPanel on start.
        /// </summary>
        [Tooltip("If true, hides the UIPanel on strt.")]
        public bool hideOnStart;

        protected object _nextObject;

        protected virtual void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            if (hideOnStart)
                Hide();
            else
                Show();
        }

        public void Hide()
        {
            _hidden = true;
            _canvasGroup.Hide();
        }

        public void Show()
        {
            _hidden = false;
            _canvasGroup.Show();
        }

        public virtual void Next()
        {
            if (_next != null)
            {
                Hide();
                _next.Init(_nextObject);
                _next.Show();
            }
        }

        public abstract void Init(object obj);
    }
}
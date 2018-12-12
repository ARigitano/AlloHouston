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
        [SerializeField]
        private UIPanel _previous;
        public UIPanel previous
        {
            get
            {
                return _previous;
            }
        }
        [SerializeField]
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
            Hide();
            if (_nextObject != null)
                _next.Init(_nextObject);
            _next.Show();
        }

        public virtual void Init(object obj)
        {

        }
    }
}
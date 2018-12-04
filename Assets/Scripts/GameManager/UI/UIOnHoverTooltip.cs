using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CRI.HelloHouston.Experience.UI
{
    internal class UIOnHoverTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Serializable]
        internal struct Settings
        {
            /// <summary>
            /// Time (in seconds) until the tooltip show up when hovered by the mouse.
            /// </summary>
            [Tooltip("Time (in seconds) until the tooltip shows up when hovered by the mouse.")]
            public float hoverTime;
            /// <summary>
            /// The text of the tooltip.
            /// </summary>
            [SerializeField]
            [Tooltip("The text of the tooltip.")]
            public string tooltipText;
            /// <summary>
            /// Offset of the tooltip compared to the object.
            /// </summary>
            [SerializeField]
            [Tooltip("Offset of the tooltip compared to the object.")]
            public Vector3 tooltipOffset;

            public Settings(float hoverTime, string tooltipText, Vector3 tooltipOffset)
            {
                this.hoverTime = hoverTime;
                this.tooltipText = tooltipText;
                this.tooltipOffset = tooltipOffset;
            }
        }
        /// <summary>
        /// A prefab of a tooltip.
        /// </summary>
        [SerializeField]
        [Tooltip("A prefab of a tooltip.")]
        private UITooltip _tooltipPrefab = null;
        /// <summary>
        /// An instance of a tooltip.
        /// </summary>
        private UITooltip _tooltip = null;
        /// <summary>
        /// The settings for the hover tooltip.
        /// </summary>
        [SerializeField]
        [Tooltip("The settings for the hover tooltip.")]
        private Settings _tooltipSettings;
        /// <summary>
        /// True when the pointer is on the component.
        /// </summary>
        private bool _enter = false;
        /// <summary>
        /// The time when the pointer entered the component.
        /// </summary>
        private float _enterTime = 0.0f;

        private void Awake()
        {
            _tooltipPrefab = Resources.Load<UITooltip>("UI/UITooltip");
        }

        public void Init(float hoverTime, string tooltipText, Vector3 tooltipOffset)
        {
            Init(new Settings(hoverTime, tooltipText, tooltipOffset));
        }

        public void Init(Settings tooltipSettings)
        {
            _tooltipSettings = tooltipSettings;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _enter = true;
            _enterTime = Time.time;
            if (_tooltip != null)
                Destroy(_tooltip.gameObject); 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _enter = false;
            if (_tooltip != null)
               Destroy(_tooltip.gameObject);
        }

        private void Update()
        {
            if (_enter && Time.time - _enterTime > _tooltipSettings.hoverTime && _tooltip == null)
            {
                _tooltip = Instantiate(_tooltipPrefab, transform.position + _tooltipSettings.tooltipOffset, Quaternion.identity, GetComponentInParent<Canvas>().transform);
                _tooltip.Init(_tooltipSettings.tooltipText);
            }
        }
    }
}

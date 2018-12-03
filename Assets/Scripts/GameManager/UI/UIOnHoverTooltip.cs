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
        [SerializeField]
        [Tooltip("The serialized field")]
        private int _hoverTime = 0;
        /// <summary>
        /// The text of the tooltip.
        /// </summary>
        [SerializeField]
        [Tooltip("The text of the tooltip.")]
        private string _tooltipText = "";
        /// <summary>
        /// True when the pointer is on the component.
        /// </summary>
        private bool _enter = false;
        /// <summary>
        /// The time when the pointer entered the component.
        /// </summary>
        private float _enterTime = 0.0f;
        /// <summary>
        /// Offset of the tooltip compared to the object.
        /// </summary>
        [SerializeField]
        [Tooltip("Offset of the tooltip compared to the object.")]
        private Vector3 _tooltipOffset = Vector3.zero;

        public void Init(int hoverTime, UITooltip tooltipPrefab, Vector3 tooltipOffset, string tooltipText)
        {
            _hoverTime = hoverTime;
            _tooltipPrefab = tooltipPrefab;
            _tooltipText = tooltipText;
            _tooltipOffset = tooltipOffset;
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
            if (_enter && Time.time - _enterTime > _hoverTime && _tooltip == null)
            {
                _tooltip = Instantiate(_tooltipPrefab, transform.position + _tooltipOffset, Quaternion.identity, GetComponentInParent<Canvas>().transform);
                _tooltip.Init(_tooltipText);
            }
        }
    }
}

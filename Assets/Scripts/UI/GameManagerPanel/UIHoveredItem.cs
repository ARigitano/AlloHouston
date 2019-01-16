using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CRI.HelloHouston.Experience.UI
{
    public class UIHoveredItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public delegate void HoveredItemEvent(bool hovered);
        public event HoveredItemEvent onHover;
        public bool hovered { get; private set; }

        public void OnPointerEnter(PointerEventData eventData)
        {
            hovered = true;
            if (onHover != null)
                onHover(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            hovered = false;
            if (onHover != null)
                onHover(false);
        }
    }
}

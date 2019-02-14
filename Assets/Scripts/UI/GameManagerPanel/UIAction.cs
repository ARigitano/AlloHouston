using CRI.HelloHouston.Experience.Actions;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience.UI
{
    [RequireComponent(typeof(UIOnHoverTooltip))]
    internal class UIAction : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        [SerializeField]
        private Image _icon = null;
        /// <summary>
        /// Time (in seconds) until the tooltip show up when hovered by the mouse.
        /// </summary>
        [SerializeField]
        [Tooltip("Time (in seconds) until the tooltip show up when hovered by the mouse.")]
        private float _hoverTime = 0.0f;
        /// <summary>
        /// Offset of the tooltip compared to the object.
        /// </summary>
        [SerializeField]
        [Tooltip("Offset of the tooltip compared to the object.")]
        private Vector3 _tooltipOffset = Vector3.zero;

        private void Reset()
        {
            _button = GetComponentInChildren<Button>();
        }

        public void Init<T>(T action, GeneralActionController<T> actionController) where T: GeneralAction
        {
            _icon.sprite = action.menuIcon;
            GetComponent<UIOnHoverTooltip>().Init(_hoverTime, action.actionTooltip, _tooltipOffset);
            _button.onClick.AddListener(() => actionController.AddAction(action));
        }
    }
}
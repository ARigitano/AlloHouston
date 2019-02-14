using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience.UI
{
    internal class UITooltip : MonoBehaviour
    {
        /// <summary>
        /// The text field of the tooltip.
        /// </summary>
        [SerializeField]
        [Tooltip("The text field of the tooltip.")]
        private Text _text;

        private void Reset()
        {
            _text = GetComponentInChildren<Text>();
        }

        public void Init(string tooltipText)
        {
            _text.text = tooltipText;
        }
    }
}
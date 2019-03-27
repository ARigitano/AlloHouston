using CRI.HelloHouston.Experience;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Calibration.UI
{
    internal class UIStartPanel : UIPanel
    {
        /// <summary>
        /// Next button.
        /// </summary>
        [SerializeField]
        [Tooltip("Next button.")]
        private Button _nextButton = null;

        public override void Init(object obj)
        {
            _nextObject = obj;
            _nextButton.onClick.AddListener(Next);
        }

    }
}

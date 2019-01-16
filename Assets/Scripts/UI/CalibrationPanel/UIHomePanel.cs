using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Calibration.UI
{
    public class UIHomePanel : UIPanel
    {
        /// <summary>
        /// Next button.
        /// </summary>
        [SerializeField]
        [Tooltip("Next button.")]
        private Button _nextButton = null;

        private void Start()
        {
            Init(null);
        }

        public override void Init(object obj)
        {
            _nextObject = obj;
            _nextButton.onClick.AddListener(Next);
        }
    }
}
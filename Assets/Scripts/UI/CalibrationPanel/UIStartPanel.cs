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
        /// <summary>
        /// Input field.
        /// </summary>
        [SerializeField]
        [Tooltip("Input field.")]
        private InputField _inputField = null;

        public override void Init(object obj)
        {
            _nextObject = obj;
            _nextButton.onClick.AddListener(Next);
            //Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("Default"));
        }

        public override void Next()
        {
            RoomXPPair rxpp = (RoomXPPair)_nextObject;
            int res;
            if (int.TryParse(_inputField.text, out res))
                rxpp.seed = res;
            _nextObject = rxpp;
            base.Next();
        }
    }
}

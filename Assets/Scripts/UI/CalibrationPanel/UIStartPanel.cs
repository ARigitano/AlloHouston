using CRI.HelloHouston.Experience;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

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
        /// Random seed input field.
        /// </summary>
        [SerializeField]
        [Tooltip("The random seed input field.")]
        private InputField _seedInputField = null;
        /// <summary>
        /// Time input field.
        /// </summary>
        [SerializeField]
        [Tooltip("The time input field.")]
        private InputField _timeInputField = null;
        /// <summary>
        /// The player gameobject.
        /// </summary>
        [SerializeField]
        [Tooltip("The player gameobject")]
        private VRTK_SDKManager _player = null;
        private Camera[] playerCameras;
        [SerializeField]
        private LayerMask _introLayerMask = new LayerMask();


        private RoomSettings _rmst;

        public override void Init(object obj)
        {
            _nextObject = obj;
            _rmst = (RoomSettings)obj;
            _nextButton.onClick.AddListener(Next);

            RoomSettings rmst = (RoomSettings)_nextObject;
            _timeInputField.text = _rmst.timeEstimate.ToString();

            if (_player != null && _player.loadedSetup != null)
            {
                playerCameras = _player.loadedSetup.actualHeadset.GetComponentsInChildren<Camera>();
                foreach (Camera playerCamera in playerCameras)
                    playerCamera.cullingMask = _introLayerMask;
            }

        }

        public override void Next()
        {
            int seed;
            int timeEstimate;
            if (int.TryParse(_seedInputField.text, out seed))
                _rmst.seed = seed;
            if (int.TryParse(_timeInputField.text, out timeEstimate))
                _rmst.timeEstimate = timeEstimate;
            _nextObject = _rmst;
            base.Next();
        }
    }
}

using CRI.HelloHouston.Experience;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.GameElements
{
    public class UIComTube : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Transform of the object in which the fill items will be displayed.")]
        private Transform _fillGroup = null;
        [SerializeField]
        [Tooltip("Prefab of the fill items.")]
        private GameObject _fillItemPrefab = null;
        [SerializeField]
        [Tooltip("Text of the experiment name.")]
        private Text _xpNameText = null;

        [SerializeField]
        [Tooltip("The alpha value of the fill when the experiment is a success.")]
        private Color _colorOK = Color.green;
        [SerializeField]
        [Tooltip("The alpha value of the fill when the experiment is a failure.")]
        private Color _colorKO = Color.red;
        [SerializeField]
        [Tooltip("The alpha value of the fill when the experiment is in progress.")]
        private Color _colorProgress = Color.yellow;
        [SerializeField]
        [Tooltip("The alpha value of the fill when it's not activated.")]
        private Color _colorEmpty = Color.black;

        private Image[] _fillItems;
        /// <summary>
        /// The manager associated with the tube.
        /// </summary>
        private XPManager _xpManager;

        public void OnEnable()
        {
            if (_xpManager != null)
            {
                _xpManager.stepManager.onStepChange += OnStepChange;
                _xpManager.onStateChange += OnStateChange;
            }
        }

        public void OnDisable()
        {
            if (_xpManager != null)
            {
                _xpManager.stepManager.onStepChange -= OnStepChange;
                _xpManager.onStateChange -= OnStateChange;
            }
        }

        public void Init(XPManager xpManager)
        {
            int steps = xpManager.stepManager.maxStepValue;
            _xpManager = xpManager;
            _xpManager.stepManager.onStepChange += OnStepChange;
            _xpManager.onStateChange += OnStateChange;
            _fillItems = new Image[steps];
            _xpNameText.text = xpManager.xpContext.xpGroup.experimentName;
            for (int i = 0; i < steps; i++)
                _fillItems[i] = Instantiate(_fillItemPrefab, _fillGroup).GetComponentInChildren<Image>();
            UpdateSteps(_xpManager.stepManager.sumValue, _xpManager.state);
        }

        public void OnStepChange(object sender, XPStepEventArgs e)
        {
            UpdateSteps(e.currentStepValue, _xpManager.state);
        }


        private void OnStateChange(object sender, XPManagerEventArgs e)
        {
            UpdateSteps(_xpManager.stepManager.sumValue, e.currentState);
        }

        private void UpdateSteps(int currentStepValue, XPState currentState)
        {
            Debug.Log(currentState);
            int maxSteps = _fillItems.Length;
            for (int i = 0; i < _fillItems.Length; i++)
            {
                Color color;
                if (i < (maxSteps - currentStepValue))
                    color = _colorEmpty;
                else if (currentState == XPState.Failure)
                    color = _colorKO;
                else if (currentState == XPState.Success)
                    color = _colorOK;
                else
                    color = _colorProgress;
                _fillItems[i].color = color;
            }
        }
    }
}
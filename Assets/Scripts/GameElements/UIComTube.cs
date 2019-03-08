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
        private CanvasGroup _fillItemPrefab = null;
        [SerializeField]
        [Tooltip("Text of the experiment name.")]
        private Text _xpNameText = null;

        [SerializeField]
        [Tooltip("The alpha value of the fill when it's activated.")]
        private float _alphaFilledValue = 1.0f;
        [SerializeField]
        [Tooltip("The alpha value of the fill when it's not activated.")]
        private float _alphaEmptyValue = 0.1f;

        private CanvasGroup[] _fillItems;
        /// <summary>
        /// The manager associated with the tube.
        /// </summary>
        private XPManager _xpManager;

        public void OnEnable()
        {
            if (_xpManager != null)
                _xpManager.stepManager.onStepChange += OnStepChange;
        }

        public void OnDisable()
        {
            if (_xpManager != null)
                _xpManager.stepManager.onStepChange -= OnStepChange;
        }

        public void Init(XPManager xpManager)
        {
            int steps = xpManager.stepManager.maxStepValue;
            _xpManager = xpManager;
            _xpManager.stepManager.onStepChange += OnStepChange;
            _fillItems = new CanvasGroup[steps];
            _xpNameText.text = xpManager.xpContext.xpGroup.experimentName;
            for (int i = 0; i < steps; i++)
                _fillItems[i] = Instantiate(_fillItemPrefab, _fillGroup);
            OnStepChange(null, _xpManager.stepManager.currentStepValue);
        }

        public void OnStepChange(XPStepManager.StepAction stepAction, int stepValue)
        {
            int maxSteps = _fillItems.Length;
            for (int i = 0; i < _fillItems.Length; i++)
            {
                _fillItems[i].alpha = i >= (maxSteps - stepValue) ? _alphaFilledValue : _alphaEmptyValue;
            }
        }
    }
}
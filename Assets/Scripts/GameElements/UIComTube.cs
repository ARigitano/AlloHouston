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
        [Tooltip("The alpha value of the fill when it's activated.")]
        private Color _colorFilled = Color.white;
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
                _xpManager.onStepChange += OnStepChange;
        }

        public void OnDisable()
        {
            if (_xpManager != null)
                _xpManager.onStepChange -= OnStepChange;
        }

        public void Init(XPManager xpManager)
        {
            int steps = xpManager.xpContext.xpSettings.steps;
            _xpManager = xpManager;
            _xpManager.onStepChange += OnStepChange;
            _fillItems = new Image[steps];
            _xpNameText.text = xpManager.xpContext.xpGroup.experimentName;
            for (int i = 0; i < steps; i++)
                _fillItems[i] = Instantiate(_fillItemPrefab, _fillGroup).GetComponentInChildren<Image>();
            OnStepChange(_xpManager.currentStep);
        }

        public void OnStepChange(int step)
        {
            int maxSteps = _fillItems.Length;
            for (int i = 0; i < _fillItems.Length; i++)
            {
                _fillItems[i].color = i >= (maxSteps - step) ? _colorFilled : _colorEmpty;
            }
        }
    }
}
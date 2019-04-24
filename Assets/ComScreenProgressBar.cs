using CRI.HelloHouston.Experience;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.GameElements
{
    public class ComScreenProgressBar : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Slider representing the success value of the experiments.")]
        private Slider _xpSlider = null;
        [SerializeField]
        [Tooltip("Slider representing the danger value.")]
        private Slider _dangerSlider = null;
        [SerializeField]
        [Tooltip("Filling factor.")]
        private float _fillFactor = 0.5f;

        private bool init;
        private XPManager[] _managers;
        private GameManager _gameManager;

        
        private void OnEnable()
        {
            foreach (XPManager manager in _managers)
            {
                manager.onStateChange += OnManagerStateChange;
            }
        }

        private void OnDisable()
        {
            foreach (XPManager manager in _managers)
            {
                manager.onStateChange -= OnManagerStateChange;
            }
        }

        public void Init(GameManager gameManager, XPManager[] managers)
        {
            init = true;
            _gameManager = gameManager;
            _managers = managers;
            foreach (XPManager manager in managers)
            {
                manager.onStateChange += OnManagerStateChange;
            }
            UpdateXPSlider();
            UpdateDangerSlider();
        }

        private void OnManagerStateChange(object sender, XPManagerEventArgs e)
        {
            UpdateXPSlider();
        }

        private void UpdateXPSlider()
        {
            float fillAmount = _managers.Count(manager => manager.state == XPState.Success) / (float)_managers.Count();
            float diff = (fillAmount + _dangerSlider.value) - 1.0f;
            // If the sliders will be connected after the operation.
            if (diff >= 0.0f)
                fillAmount = (fillAmount - diff) + diff * _fillFactor;
            _xpSlider.value = fillAmount;
            if (_xpSlider.value + _dangerSlider.value >= 1.0f)
                _dangerSlider.value = 1.0f - _xpSlider.value;
        }

        private void UpdateDangerSlider()
        {
            float sum = 0.0f;
            for (int i = 0; i < _managers.Length; i++)
            {
                if (_managers[i].xpContext.xpSettings != null)
                    sum += _managers[i].xpContext.xpSettings.duration;
            }
            float fillAmount = Mathf.Min(_gameManager.timeSinceGameStart / (sum * 60.0f), 1.0f);
            float diff = (fillAmount + _xpSlider.value) - 1.0f;
            // If the sliders will be connected after the operation
            if (diff >= 0.0f)
                fillAmount = (fillAmount - diff) + diff * _fillFactor;
            _dangerSlider.value = fillAmount;
            if (_xpSlider.value + _dangerSlider.value >= 1.0f)
                _xpSlider.value = 1.0f - _dangerSlider.value;
        }

        private void Update()
        {
            if (init)
                UpdateDangerSlider();
        }
    }
}
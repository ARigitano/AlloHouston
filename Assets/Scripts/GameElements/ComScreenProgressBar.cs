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
        private float _totalDuration;
        private int _totalManagers;

        private void OnEnable()
        {
            for (int i = 0; _managers != null && i < _managers.Length; i++)
            {
                _managers[i].onStateChange += OnManagerStateChange;
            }
        }

        private void OnDisable()
        {
            for (int i = 0; _managers != null && i < _managers.Length; i++)
            {
                _managers[i].onStateChange -= OnManagerStateChange;
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
            _totalDuration = _managers.Where(manager => manager.xpContext.xpSettings != null).Sum(manager => manager.xpContext.xpSettings.duration) * 60.0f;
            _totalManagers = _managers.Count();
            _xpSlider.value = 0.0f;
            _dangerSlider.value = 0.0f;
            UpdateXPSlider();
            UpdateDangerSlider();
        }

        private void OnManagerStateChange(object sender, XPManagerEventArgs e)
        {
            UpdateXPSlider();
        }

        private void UpdateXPSlider()
        {
            float fillAmount = _managers.Count(manager => manager.state == XPState.Success) / (float)_totalManagers;
            float diff = fillAmount - _xpSlider.value;

            // If the sliders are already connected before the operation.ss
            if (_xpSlider.value + _dangerSlider.value >= 1.0f && diff >= 0.0f)
                diff *= _fillFactor;
            else if (_xpSlider.value + diff + _dangerSlider.value >= 1.0f && diff >= 0.0f)
            {
                // We compute the value that exceed
                float diff2 = (_xpSlider.value + _dangerSlider.value + diff) - 1.0f;
                diff = (diff - diff2) + diff2 * _fillFactor;
            }
            _xpSlider.value += diff;
            if (_xpSlider.value + _dangerSlider.value >= 1.0f)
                _dangerSlider.value = 1.0f - _xpSlider.value;
        }

        private void UpdateDangerSlider()
        {
            float diff = 1.0f / _totalDuration * Time.deltaTime;
            if (_xpSlider.value + _dangerSlider.value >= 1.0f && diff >= 0)
                diff *= _fillFactor;
            _dangerSlider.value += diff;
            // the danger slider "pushes" the xp slider away.
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
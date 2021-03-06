﻿using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience.UI
{
    internal class UITimerDisplay : MonoBehaviour
    {
        /// <summary>
        /// The text field.
        /// </summary>
        [SerializeField]
        [Tooltip("The text field.")]
        private Text _text;
        /// <summary>
        /// Color of the text when the timer is over estimate.
        /// </summary>
        [SerializeField]
        [Tooltip("Color of the text when the timer is over estimate.")]
        private Color _overestimateColor;

        private string _baseColorHex;
        private string _overestimateColorHex;

        private FontStyle _baseFontStyle;

        private GameManager _gameManager;

        private void Reset()
        {
            _text = GetComponentInChildren<Text>();
            _overestimateColor = Color.red;
        }

        public void Init(GameManager gameManager)
        {
            _gameManager = gameManager;
            _baseColorHex = ColorUtility.ToHtmlStringRGB(_text.color);
            _overestimateColorHex = ColorUtility.ToHtmlStringRGB(_overestimateColor);
            _baseFontStyle = _text.fontStyle;
        }

        private void Update()
        {
            if (_gameManager != null)
            {
                int timeSinceGameStart = (int)_gameManager.timeSinceGameStart;
                int timeEstimate = _gameManager.xpTimeEstimate;
                bool overestimate = timeSinceGameStart >= (timeEstimate * 60);
                _text.text = string.Format("<color=#{5}>{0}:{1:00}:{2:00} (Estimate : {3:00}:{4:00}:00)</color>",
                    (timeSinceGameStart / 3600),
                    (timeSinceGameStart / 60) % 60,
                    timeSinceGameStart % 60,
                    timeEstimate / 60,
                    timeEstimate % 60,
                    overestimate ? _overestimateColorHex : _baseColorHex);
                _text.fontStyle = overestimate ? FontStyle.Bold : _baseFontStyle;
            }
        }
    }
}

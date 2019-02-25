using CRI.HelloHouston.Translation;
using UnityEngine;
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
        /// <summary>
        /// Key of the estimate text.
        /// </summary>
        [SerializeField]
        [Tooltip("Key of the estimate text")]
        private string _estimateTextKey;

        private string _baseColorHex;
        private string _overestimateColorHex;
        private string _estimateText;

        private FontStyle _baseFontStyle;

        private GameManager _gameManager;

        private void OnEnable()
        {
            TextManager.onLangChange += OnLangChange;
        }

        private void OnDisable()
        {
            TextManager.onLangChange -= OnLangChange;
        }

        private void OnLangChange(LangApp lang)
        {
            _estimateText = TextManager.instance.GetText(_estimateTextKey);
        }

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
            _estimateText = TextManager.instance.GetText(_estimateTextKey);
        }

        private void Update()
        {
            if (_gameManager != null)
            {
                int timeSinceGameStart = (int)_gameManager.timeSinceGameStart;
                int timeEstimate = _gameManager.xpTimeEstimate;
                bool overestimate = timeSinceGameStart >= (timeEstimate * 60);
                _text.text = string.Format("<color=#{5}>{0}:{1:00}:{2:00} ({6} : {3:00}:{4:00}:00)</color>",
                    (timeSinceGameStart / 3600),
                    (timeSinceGameStart / 60) % 60,
                    timeSinceGameStart % 60,
                    timeEstimate / 60,
                    timeEstimate % 60,
                    overestimate ? _overestimateColorHex : _baseColorHex,
                    _estimateText);
                _text.fontStyle = overestimate ? FontStyle.Bold : _baseFontStyle;
            }
        }
    }
}

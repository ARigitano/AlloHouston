using CRI.HelloHouston.Translation;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience
{
    [RequireComponent(typeof(Text))]
    public class XPTranslatedText : MonoBehaviour
    {
        /// <summary>
        /// The key to the text to translate.
        /// </summary>
        [Tooltip("The key to the text to translate.")]
        [SerializeField]
        protected string _textKey;

        /// <summary>
        /// The key of the text to translate.
        /// </summary>
        public string textKey
        {
            get
            {
                return _textKey;
            }
        }

        /// <summary>
        /// The text.
        /// </summary>
        [SerializeField]
        [Tooltip("The text.")]
        private Text _text;

        /// <summary>
        /// Is the text in the common file text ?
        /// </summary>
        [SerializeField]
        [Tooltip("Is the text in the common file text ?")]
        protected bool _isCommon = false;
        /// <summary>
        /// If true, it will use its own font instead of the font defined for the language.
        /// </summary>
        [SerializeField]
        [Tooltip("If true, the translated text will use the font of the text component instead of the font defined for the language.")]
        protected bool _useFont = true;

        protected TextManager _textManager;

        private void OnEnable()
        {
            if (_textManager != null)
                _textManager.onLangChange += OnLangChange;
        }

        private void OnDisable()
        {
            if (_textManager != null)
                _textManager.onLangChange -= OnLangChange;
        }

        private void Reset()
        {
            _text = GetComponent<Text>();
        }

        /// <summary>
        /// Called whenever the OnLangChange event of the TextManager is triggered. Sets the text to its current lang value.
        /// </summary>
        /// <param name="lang"></param>
        private void OnLangChange(LangApp lang)
        {
            SetText();
        }

        /// <summary>
        /// Set the text to its translated value.
        /// </summary>
        private void SetText()
        {
            if (textKey != "")
            {
                _text.text = _textManager.GetText(textKey, _isCommon);
            }
        }

        public void Init(TextManager textManager)
        {
            _textManager = textManager;
            _text = GetComponent<Text>();
            SetText();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Translation
{
    /// <summary>
    /// A text UI that will be translated.
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class TranslatedText : MonoBehaviour
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

        protected bool _initialized = false;
        /// <summary>
        /// If true, this translated text has already been initialized once.
        /// </summary>
        public bool initialized
        {
            get
            {
                return _initialized;
            }
        }

        protected TextManager _textManager;
        protected LangManager _langManager;

        private void OnEnable()
        {
            if (_textManager != null)
                _textManager.langManager.onLangChange += OnLangChange;
        }

        private void OnDisable()
        {
            if (_textManager != null)
                _textManager.langManager.onLangChange -= OnLangChange;
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
        /// Init the translated text.
        /// </summary>
        /// <param name="textKey">The text key.</param>
        /// <param name="isCommon">Is the text common ?</param>
        public void InitTranslatedText(LangManager langManager, TextManager textManager, string textKey, bool isCommon = false)
        {
            _textKey = textKey;
            _isCommon = isCommon;
            Init(langManager, textManager);
        }
        /// <summary>
        /// Init the translated text.
        /// </summary>
        /// <param name="textManager">The text manager.</param>
        public void Init(LangManager langManager, TextManager textManager)
        {
            _initialized = true;
            _langManager = langManager;
            _textManager = textManager;
            _text = GetComponent<Text>();
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
    }
}

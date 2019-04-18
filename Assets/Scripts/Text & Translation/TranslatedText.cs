using CRI.HelloHouston.Experience;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Translation
{
    public struct TranslatedTextEventArgs
    {
        public LangApp lang;
        public string text;
    }

    public delegate void TranslatedTextEventHandler(object sender, TranslatedTextEventArgs e);
    /// <summary>
    /// A text UI that will be translated.
    /// </summary>
    [RequireComponent(typeof(Text))]
    public abstract class TranslatedText : MonoBehaviour
    {
        public event TranslatedTextEventHandler onLangChange;
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
        /// <summary>
        /// If true, the translated text will find a textmanager by itself at start.
        /// </summary>
        [SerializeField]
        [Tooltip("If true, the translated text will find a textmanager by itself at start.")]
        protected bool _autoInit = true;
        /// <summary>
        /// If true, the translated text will update automatically when the lang in the text manager changes.
        /// </summary>
        [SerializeField]
        [Tooltip("If true, the translated text will update automatically when the lang in the text manager changes.")]
        protected bool _autoUpdate = true;

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

        [SerializeField]
        [Tooltip("The lang manager. If this field is empty, the TranslatedText component will find a suitable LangManager automatically. (recommended)")]
        protected ILangManager _manager;

        private void OnEnable()
        {
            if (_manager != null)
                _manager.langManager.onLangChange += OnLangChange;
        }

        private void OnDisable()
        {
            if (_manager != null)
                _manager.langManager.onLangChange -= OnLangChange;
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
            if (!_autoUpdate)
                return;
            SetText();
            if (onLangChange != null)
                onLangChange(this, new TranslatedTextEventArgs() { lang = lang, text = _text.text });
        }

        /// <summary>
        /// Init the translated text.
        /// </summary>
        /// <param name="textKey">The text key.</param>
        /// <param name="isCommon">Is the text common ?</param>
        public void InitTranslatedText(ILangManager manager, string textKey, bool isCommon = false)
        {
            _textKey = textKey;
            _isCommon = isCommon;
            Init(manager);
        }
        /// <summary>
        /// Init the translated text.
        /// </summary>
        /// <param name="textManager">The text manager.</param>
        public void Init(ILangManager manager)
        {
            _initialized = true;
            _manager = manager;
            if (_manager.langManager != null)
                _manager.langManager.onLangChange += OnLangChange;
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
                _text.text = _manager.textManager.GetText(textKey, _isCommon);
            }
        }

        protected abstract void FindManager();

        private void Start()
        {
            if (!_initialized && _autoInit && _manager == null)
            {
                FindManager();
            }
        }
    }
}

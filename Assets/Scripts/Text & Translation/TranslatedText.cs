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
        protected bool _isCommon = false;

        private void OnEnable()
        {
            TextManager.onLangChange += OnLangChange;
        }

        private void OnDisable()
        {
            TextManager.onLangChange -= OnLangChange;
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
        public void InitTranslatedText(string textKey, bool isCommon = false)
        {
            _text = GetComponent<Text>();
            this._textKey = textKey;
            this._isCommon = isCommon;
            SetText();
        }

        /// <summary>
        /// Set the text to its translated value.
        /// </summary>
        private void SetText()
        {
            if (textKey != "")
            {
                _text.text = TextManager.instance.GetText(textKey, _isCommon);
                if (TextManager.instance.HasFont(_isCommon))
                    _text.font = TextManager.instance.GetFont(_isCommon);
            }
        }

        private void Start()
        {
            SetText();
        }
    }
}

using CRI.HelloHouston.Translation;
using CRI.HelloHouston.WindowTemplate;
using UnityEngine;
using UnityEngine.UI;
using CRI.HelloHouston.Utility;
using System;

namespace CRI.HelloHouston.WindowsTemplate
{
    [RequireComponent(typeof(Text))]
    public class TextTyperAnimationElement : AnimationElement
    {
        [SerializeField]
        [Tooltip("The text component that will be used to display the typing animation.")]
        private Text _text;

        private TranslatedText _translatedText;

        private void Reset()
        {
            _text = GetComponent<Text>();
        }

        private void Start()
        {
            if (_text == null)
                _text = GetComponent<Text>();
            _translatedText = GetComponent<TranslatedText>();
            if (_translatedText != null)
            {
                _translatedText.onLangChange += OnLangChange;
            }
        }

        protected override void StartHideAnimation()
        {
            _text.SkipTypeText();
        }

        protected override void StartShowAnimation()
        {
            _text.TypeText(_text.text);
        }

        private void OnLangChange(object sender, TranslatedTextEventArgs e)
        {
            _text.SkipTypeText();
        }
    }
}

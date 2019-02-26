using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace CRI.HelloHouston.Translation
{
    public class TextManager
    {
        public delegate void TextManagerLangHandler(LangApp lang);
        public event TextManagerLangHandler onLangChange;

        /// <summary>
        /// The current language of the application.
        /// </summary>
        protected LangApp _currentLang;
        /// <summary>
        /// The current language of the application. If the value is changed, it will trigger the OnLangChange event.
        /// </summary>
        public LangApp currentLang
        {
            get
            {
                return _currentLang;
            }
            set
            {
                _currentLang = value;
                if (onLangChange != null)
                    onLangChange(_currentLang);
            }
        }
        /// <summary>
        /// List of all lang available.
        /// </summary>]
        protected LangApp[] _langAppAvailable;
        /// <summary>
        /// The default language of the application.
        /// </summary>
        protected LangApp _defaultLanguage;
        /// <summary>
        /// A list of all the LangText.
        /// </summary>
        [SerializeField]
        protected List<LangText> _langTextList = new List<LangText>();

        protected TextManager(LangApp[] langAppAvailable, LangApp defaultLanguage)
        {
            _langAppAvailable = langAppAvailable;
            _defaultLanguage = defaultLanguage;
            SetDefaultLang();
        }

        /// <summary>
        /// Sets the current language to the default language of the application as described in the game settings.
        /// </summary>
        public virtual void SetDefaultLang()
        {
            currentLang = _defaultLanguage;
        }
        /// <summary>
        /// Finds the text of a language by using a specific key and a lang code.
        /// Exemple: For the key "SCREEN2_TITLE" and the lang "fr" it will return "REGLES DU JEU."
        /// For the key "SCREEN2_TITLE" and the lang "en" it will return "GAME RULES."
        /// </summary>
        /// <param name="key">The key of the text.</param>
        /// <param name="langCode">The code of the language.</param>
        /// <returns>The text translated to a specific language.</returns>
        public string GetText(string key, string langCode)
        {
            string res = "";
            try
            {
                res = _langTextList.First(x => x.code == langCode).arrayOfLangTextEntry.First(x => x.key == key).text;
            }
            catch (InvalidOperationException)
            {
                Debug.LogError("InvalidOperationException : Key \"" + key + "\" not found for LangCode \"" + langCode + "\"");
                res = key;
            }
            return res;
        }

        /// <summary>
        /// Finds the text of the current language by using a specific key.
        /// Exemple: For the key "SCREEN2_TITLE" and the current lang "fr" it will return "REGLES DU JEU."
        /// For the key "SCREEN2_TITLE" and the current lang "en" it will return "GAME RULES."
        /// </summary>
        /// <param name="key">The key of the text.</param>
        /// <param name="common">Is the text common between all languages ?</param>
        /// <returns>The text translated to a specific language.</returns>
        public string GetText(string key, bool common = false)
        {
            return GetText(key, common ? "COM" : _currentLang.code);
        }

        public void ChangeLang(int index)
        {
            currentLang = _langAppAvailable[index];
        }

        public void ChangeLang(string langCode)
        {
            if (currentLang.code != langCode && _langAppAvailable.Any(x => x.code == langCode))
                currentLang = _langAppAvailable.First(x => x.code == langCode);
        }

        public void ChangeLang(LangApp lang)
        {
            ChangeLang(lang.code);
        }
    }
}

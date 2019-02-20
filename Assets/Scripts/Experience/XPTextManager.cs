using CRI.HelloHouston.Translation;
using System;
using System.Linq;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    public class XPTextManager
    {
        public delegate void TextManagerLangHandler(string langCode);
        public event TextManagerLangHandler onLangChange;

        /// <summary>
        /// The current language of the application.
        /// </summary>
        private string _currentLang;
        /// <summary>
        /// The current language of the application. If the value is changed, it will trigger the OnLangChange event.
        /// </summary>
        public string currentLang
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

        private LangText[] _langTexts;

        private string[] _availableLangCode;

        public XPTextManager(TextAsset[] textFiles)
        {
            _langTexts = LoadLangText(textFiles);
            _availableLangCode = _langTexts.Select(textFile => textFile.code).ToArray();
            ChangeLang(_langTexts[0].code);
        }

        public LangText[] LoadLangText(TextAsset[] textFiles)
        {
            return textFiles.Select(textFile => LangText.LoadFromText(textFile.text)).ToArray();
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
                res = _langTexts.First(x => x.code == langCode).arrayOfLangTextEntry.First(x => x.key == key).text;
            }
            catch (InvalidOperationException)
            {
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
            return GetText(key, common ? "COM" : _currentLang);
        }

        public void ChangeLang(string langCode)
        {
            LangText textFile = _langTexts.FirstOrDefault(x => x.code == langCode);
            if (textFile == null && _langTexts[0] != null)
                _currentLang = _langTexts[0].code;
            else if (textFile != null)
                _currentLang = langCode;
        }

        public void ChangeLang(LangApp langApp)
        {
            ChangeLang(langApp.code);
        }
    }
}

using CRI.HelloHouston.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CRI.HelloHouston.Translation
{
    public class TextManager
    {
        /// <summary>
        /// A list of all the LangText.
        /// </summary>
        [SerializeField]
        protected List<LangText> _langTextList = new List<LangText>();
        /// <summary>
        /// The language manager.
        /// </summary>
        public LangManager langManager { get; protected set; }

        public TextManager(LangManager langManager, LangSettings langSettings) : this(langManager, langSettings.langTextFiles, langSettings.commonTextFile) { }

        public TextManager(LangManager langManager, TextAsset[] langTextFiles, TextAsset commonTextFile)
        {
            this.langManager = langManager;
            if (langTextFiles.Length == 0)
                return;
            foreach (var textAsset in langTextFiles)
            {
                try
                {
                    LangText langText = LoadLangText(textAsset);
                    _langTextList.Add(langText);
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);
                }
            }
            LangText commonText = LoadLangText(commonTextFile);
            commonText.code = "COM";
            _langTextList.Add(commonText);
        }

        /// <summary>
        /// Loads the text from a specific text file.
        /// </summary>
        /// <param name="langTextAsset">The text asset</param>
        /// <returns>An instance of LangText</returns>
        public LangText LoadLangText(TextAsset langTextAsset)
        {
            return LangText.LoadFromText(langTextAsset.text);
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
            return GetText(key, common ? "COM" : langManager.currentLang.code);
        }
    }
}

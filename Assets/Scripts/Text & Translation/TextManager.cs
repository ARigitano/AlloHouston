using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace CRI.HelloHouston.Translation
{
    public class TextManager : MonoBehaviour
    {
        public delegate void TextManagerLangHandler(LangApp lang);
        public static event TextManagerLangHandler onLangChange;

        /// <summary>
        /// Static instance of the TextManager. If it's called an no TextManager exists, creates one.
        /// </summary>
        public static TextManager instance
        {
            get
            {
                if (s_instance == null)
                {
                    new GameObject("TextManager").AddComponent<TextManager>().Init();
                }
                return s_instance;
            }
        }

        /// <summary>
        /// Static instance of the TextManager.
        /// </summary>
        private static TextManager s_instance = null;

        /// <summary>
        /// The path of the text data for each language. The [lang_app] value will be replaced by the code of the language. For exemple for French, it will be "fr".
        /// </summary>
        public const string text_lang_path_base = "Lang/[lang_app]/text/text";
        /// <summary>
        /// The path of the text data for all the text that is common between all languages.
        /// </summary>
        public const string text_lang_common_path = "Lang/Common/text/text";

        private const string default_setting_path = "DefaultSettings";
        /// <summary>
        /// The current language of the application.
        /// </summary>
        private LangApp _currentLang;
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
        /// A list of all the LangText.
        /// </summary>
        [SerializeField]
        private List<LangText> _langTextList = new List<LangText>();
        /// <summary>
        /// The app settings of the project.
        /// </summary>
        [SerializeField]
        [Tooltip("The app settings of the project.")]
        private AppSettings _appSettings = null;

        private void Awake()
        {
            Init();
            if (_langTextList.Count == 0)
            {
                foreach (var langEnable in _appSettings.langAppAvailable)
                {
                    var langText = LoadLangText(langEnable.code);
                    _langTextList.Add(langText);
                }

                var commonText = LoadCommonLangText();
                _langTextList.Add(commonText);
            }
            else
            {
                foreach (var langText in _langTextList)
                {
                    langText.Save(string.Format("Resources/{0}.json", GetLangTextPath(langText.code)));
                }
            }
            _currentLang = _appSettings.defaultLanguage;
        }

        private void Init()
        {
            if (!s_instance)
            {
                s_instance = this;
                if (!_appSettings)
                    _appSettings = Resources.Load<AppSettings>(default_setting_path);
                DontDestroyOnLoad(gameObject);
            }
            else if (s_instance != this)
                Destroy(gameObject);
        }

        /// <summary>
        /// Sets the current language to the default language of the application as described in the game settings.
        /// </summary>
        public void SetDefaultLang()
        {
            currentLang = _appSettings.defaultLanguage;
        }

        /// <summary>
        /// Get the path of the text of a language.
        /// </summary>
        /// <param name="langCode">The code of the language.</param>
        /// <returns>The path to find the text of that language.</returns>
        public string GetLangTextPath(string langCode)
        {
            var path = text_lang_path_base.Replace("[lang_app]", langCode);
            return path;
        }

        /// <summary>
        /// Loads the text that is common for all languages.
        /// </summary>
        /// <returns>A LangText with all the common data.</returns>
        public LangText LoadCommonLangText()
        {
            var text = Resources.Load<TextAsset>(text_lang_common_path) as TextAsset;
            return LangText.LoadFromText(text.text);
        }
        /// <summary>
        /// Loads the text for a specific language.
        /// </summary>
        /// <param name="langCode">The code of the language.</param>
        /// <returns>A LangText will all the data for that language.</returns>
        public LangText LoadLangText(string langCode)
        {
            var path = GetLangTextPath(langCode);
            var text = Resources.Load<TextAsset>(path);
            return LangText.LoadFromText(text.text);
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

        /// <summary>
        /// Returns true if the current language or the common language have a defined font.
        /// </summary>
        /// <param name="common">If true, it will check only the common language instead.</param>
        /// <returns>True if the current language has a defined font.</returns>
        public bool HasFont(bool common = false)
        {
            return ((common && _appSettings.commonFont != null) || _currentLang.font != null || _appSettings.commonFont != null);
        }

        /// <summary>
        /// Gets the current language's font. If there's no current language's font, gets the common font.
        /// </summary>
        /// <param name="common">If true, it will check the common language's font instead.</param>
        /// <returns>The current language's font.</returns>
        public Font GetFont(bool common = false)
        {
            if (common || _currentLang.font == null)
                return _appSettings.commonFont;
            return _currentLang.font;
        }

        private void OnDestroy()
        {
            s_instance = null;
        }

        public void ChangeLang(int index)
        {
            currentLang = _appSettings.langAppAvailable[index];
        }
    }
}

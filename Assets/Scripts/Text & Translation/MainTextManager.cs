using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CRI.HelloHouston.Translation
{
    public class MainTextManager : TextManager
    {
        /// <summary>
        /// Static instance of the TextManager. If it's called an no TextManager exists, creates one.
        /// </summary>
        public static MainTextManager instance
        {
            get
            {
                if (s_instance == null)
                {
                    var appSettings = Resources.Load<AppSettings>(default_setting_path);
                    s_instance = new MainTextManager(appSettings);
                }
                return s_instance;
            }
        }

        /// <summary>
        /// Static instance of the TextManager.
        /// </summary>
        private static MainTextManager s_instance = null;

        /// <summary>
        /// The path of the text data for each language. The [lang_app] value will be replaced by the code of the language. For exemple for French, it will be "fr".
        /// </summary>
        public const string text_lang_path_base = "Lang/[lang_app]/text/text";
        /// <summary>
        /// The path of the text data for all the text that is common between all languages.
        /// </summary>
        public const string text_lang_common_path = "Lang/Common/text/text";

        private const string default_setting_path = "Settings/Default Application Settings";

        private Font _commonFont;

        public MainTextManager(AppSettings appSettings) : this(appSettings.langAppAvailable, appSettings.defaultLanguage, appSettings.langTextFiles, appSettings.commonTextFile, appSettings.commonFont) { }

        public MainTextManager(LangApp[] langAppAvailable, LangApp defaultLang, TextAsset[] langTextFiles, TextAsset commonTextFile, Font commonFont) : base(langAppAvailable, defaultLang)
        {
            if (_langTextList.Count == 0)
            {
                if (langTextFiles.Length != 0)
                {
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
                else
                {
                    foreach (var langEnable in langAppAvailable)
                    {
                        var langText = LoadLangText(langEnable.code);
                        _langTextList.Add(langText);
                    }

                    var commonText = LoadCommonLangText();
                    commonText.code = "COM";
                    _langTextList.Add(commonText);
                }
            }
            else
            {
                foreach (var langText in _langTextList)
                {
                    langText.Save(string.Format("Resources/{0}.json", GetLangTextPath(langText.code)));
                }
            }
            _commonFont = commonFont;
            InitAllText(SceneManager.GetActiveScene().GetRootGameObjects().SelectMany(go => go.GetComponentsInChildren<TranslatedText>(true)));
        }

        public void InitAllText(IEnumerable<TranslatedText> texts)
        {
            foreach (var text in texts)
            {
                text.Init(this);
            }
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

        public LangText LoadLangText(TextAsset textAsset)
        {
            return LangText.LoadFromText(textAsset.text);
        }

        /// <summary>
        /// Returns true if the current language or the common language have a defined font.
        /// </summary>
        /// <param name="common">If true, it will check only the common language instead.</param>
        /// <returns>True if the current language has a defined font.</returns>
        public bool HasFont(bool common = false)
        {
            return ((common && _commonFont != null) || _currentLang.font != null || _commonFont != null);
        }

        /// <summary>
        /// Gets the current language's font. If there's no current language's font, gets the common font.
        /// </summary>
        /// <param name="common">If true, it will check the common language's font instead.</param>
        /// <returns>The current language's font.</returns>
        public Font GetFont(bool common = false)
        {
            if (common || _currentLang.font == null)
                return _commonFont;
            return _currentLang.font;
        }

        private void OnDestroy()
        {
            s_instance = null;
        }
    }
}

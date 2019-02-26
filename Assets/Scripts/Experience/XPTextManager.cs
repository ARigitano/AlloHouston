using CRI.HelloHouston.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    public class XPTextManager :  TextManager
    {
        private LangText[] _langTexts;

        public XPTextManager(XPGroupSettings settings) : this(settings.langAppAvailable, settings.defaultLanguage, settings.langTextFiles) { }

        public XPTextManager(LangApp[] langAppAvailable, LangApp defaultLanguage, TextAsset[] langTextFiles) : base(langAppAvailable, defaultLanguage)
        {
            try
            {
                _langTextList = LoadLangText(langTextFiles);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
            ChangeLang(defaultLanguage);
        }

        public List<LangText> LoadLangText(TextAsset[] textFiles)
        {
            return textFiles.Select(textFile => LangText.LoadFromText(textFile.text)).ToList();
        }
    }
}

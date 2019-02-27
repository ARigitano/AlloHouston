using CRI.HelloHouston.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRI.HelloHouston.Translation
{
    public class LangManager
    {
        public delegate void LangManagerHandler(LangApp lang);
        public event LangManagerHandler onLangChange;
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
        public LangApp[] langAppAvailable { get; protected set; }
        /// <summary>
        /// The default language of the application.
        /// </summary>
        public LangApp defaultLanguage { get; protected set; }
        /// <summary>
        /// The text manager.
        /// </summary>
        public TextManager textManager { get; private set;}

        public LangManager(LangSettings langSettings)
        {
            this.langAppAvailable = langSettings.langAppAvailable;
            this.defaultLanguage = langSettings.defaultLanguage;
            _currentLang = defaultLanguage;
            this.textManager = new TextManager(this, langSettings);
        }
        /// <summary>
        /// Sets the current language to the default language of the application as described in the game settings.
        /// </summary>
        public virtual void SetDefaultLang()
        {
            currentLang = defaultLanguage;
        }

        public void ChangeLang(int index)
        {
            _currentLang = langAppAvailable[index];
        }

        public void ChangeLang(string langCode)
        {
            if (currentLang.code != langCode && langAppAvailable.Any(x => x.code == langCode))
                currentLang = langAppAvailable.First(x => x.code == langCode);
        }

        public void ChangeLang(LangApp lang)
        {
            ChangeLang(lang.code);
        }
    }
}

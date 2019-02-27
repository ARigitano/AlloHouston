using CRI.HelloHouston.Translation;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience
{
    /// <summary>
    /// This component automatically translate a text component using a translation key.
    /// It will check an XPManager in an XPElement in its hierarchy and use its LangManager and TextManager to get the corresponding text in the current language.
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class XPTranslatedText : TranslatedText
    {
        private void Start()
        {
            if (!_initialized)
            {
                XPElement element = GetComponentInParent<XPElement>();
                LangManager langManager = element.manager.langManager;
                TextManager textManager = langManager.textManager;
                Init(langManager, textManager);
            }
        }
    }
}

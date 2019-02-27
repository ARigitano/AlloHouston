using CRI.HelloHouston.Translation;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience
{
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

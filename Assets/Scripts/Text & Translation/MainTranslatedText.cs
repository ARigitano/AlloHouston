using CRI.HelloHouston.Experience;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Translation
{
    /// <summary>
    /// A text UI that will be translated.
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class MainTranslatedText : TranslatedText
    {
        private void Awake()
        {
            if (!_initialized)
            {
                LangManager langManager = GameManager.instance.langManager;
                TextManager textManager = GameManager.instance.textManager;
                Init(langManager, textManager);
            }
        }
    }
}

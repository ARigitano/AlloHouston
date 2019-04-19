using System;
using CRI.HelloHouston.Translation;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience
{
    /// <summary>
    /// This component automatically translate a image component using a translation key.
    /// It will check an XPManager in an XPElement in its hierarchy and use its LangManager to get the corresponding image in the current language.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class XPTranslatedImage : TranslatedImage
    {
        protected override void FindManager()
        {
            ILangManager manager = GetComponentInParent<XPElement>().manager;
            Init(manager);
        }                
    }
}

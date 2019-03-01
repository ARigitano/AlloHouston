using System;
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
        protected override void FindManager()
        {
            Init(GameManager.instance);
        }
    }
}

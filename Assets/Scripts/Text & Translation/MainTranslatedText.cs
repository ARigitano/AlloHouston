using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Init(MainTextManager.instance);
        }
    }
}

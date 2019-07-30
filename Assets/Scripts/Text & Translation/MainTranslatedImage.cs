using System;
using CRI.HelloHouston.Experience;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Translation
{
    /// <summary>
    /// An image UI that will be translated.
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class MainTranslatedImage : TranslatedImage
    {
        protected override void FindManager()
        {
            Init(GameManager.instance);
        }
    }
}

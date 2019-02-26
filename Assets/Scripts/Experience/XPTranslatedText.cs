using CRI.HelloHouston.Translation;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience
{
    [RequireComponent(typeof(Text))]
    public class XPTranslatedText : TranslatedText
    {
        private void Awake()
        {
            Debug.Log(GetComponentInParent<XPElement>());
            Debug.Log(GetComponentInParent<XPElement>().manager);
            Init(GetComponentInParent<XPElement>().manager.textManager);
        }
    }
}

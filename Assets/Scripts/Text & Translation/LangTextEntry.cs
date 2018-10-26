using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace CRI.HelloHouston.Translation
{
    [System.Serializable]
    public struct LangTextEntry
    {
        [XmlAttribute("key")]
        public string key;
        [XmlText]
        public string text;

        public LangTextEntry(string key, string text)
        {
            this.key = key;
            this.text = text;
        }
    }
}

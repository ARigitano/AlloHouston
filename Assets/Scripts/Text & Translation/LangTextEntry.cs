using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace CRI.HelloHouston.Translation
{
    [System.Serializable]
    public struct LangTextEntry
    {
        /// <summary>
        /// The key of the lang text entry.
        /// </summary>
        [Tooltip("The key of the lang text entry.")]
        [XmlAttribute("key")]
        public string key;
        /// <summary>
        /// The text of the lang text entry.
        /// </summary>
        [Tooltip("The text of the lang text entry.")]
        [XmlText]
        public string text;

        public LangTextEntry(string key, string text)
        {
            this.key = key;
            this.text = text;
        }
    }
}

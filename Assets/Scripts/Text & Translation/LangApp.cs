using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

namespace Translation
{
    /// <summary>
    /// Describes the lang of the application.
    /// </summary>
    [System.Serializable]
    public struct LangApp
    {
        [XmlAttribute("code")]
        /// <summary>
        /// The code ISO 639-1 of the language.
        /// </summary>
        public string code;
        [XmlAttribute("name")]
        /// <summary>
        /// The english name of the language.
        /// </summary>
        public string name;

        public bool Equals(LangApp other)
        {
            return code == other.code && name == other.name;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

namespace CRI.HelloHouston.Translation
{
    /// <summary>
    /// Describes the lang of the application.
    /// </summary>
    [System.Serializable]
    public struct LangApp
    {
        /// <summary>
        /// The code ISO 639-1 of the language.
        /// </summary>
        [Tooltip("The code ISO 639-1 of the language.")]
        public string code;
        /// <summary>
        /// The english name of the language.
        /// </summary>
        [Tooltip("The english name of the language.")]
        public string name;
        /// <summary>
        /// The font corresponding to the language. If null, the default font will be used.
        /// </summary>
        [Tooltip("The font corresponding to the language. If null, the default font will be used.")]
        public Font font;

        public bool Equals(LangApp other)
        {
            return code == other.code && name == other.name;
        }
    }
}

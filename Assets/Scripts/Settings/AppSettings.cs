using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRI.HelloHouston.Translation;
using UnityEngine;


[CreateAssetMenu(fileName = "New Application Settings", menuName = "Settings/New Application Settings", order = 1)]
public class AppSettings : ScriptableObject
{
    /// <summary>
    /// List of all lang available.
    /// </summary>
    [Tooltip("List of all lang available.")]
    public LangApp[] langAppAvailable;
    /// <summary>
    /// The font used for the common.
    /// </summary>
    [Tooltip("The font used for the common.")]
    public Font commonFont;
    /// <summary>
    /// The default language.
    /// </summary>
    public LangApp defaultLanguage { get { return langAppAvailable[0]; } }
}
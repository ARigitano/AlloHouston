using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRI.HelloHouston.Translation;
using UnityEngine;


[CreateAssetMenu(fileName = "New Application Settings", menuName = "Settings/New Application Settings", order = 1)]
public class AppSettings : ScriptableObject
{
    public LangApp[] langAppAvailable;
    public Font commonFont;
    public LangApp defaultLanguage { get { return langAppAvailable[0]; } }
}
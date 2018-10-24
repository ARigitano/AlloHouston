using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Translation;
using UnityEngine;


[CreateAssetMenu(fileName = "New Application Settings", menuName = "Settings/New Application Settings", order = 1)]
public class AppSettings : ScriptableObject
{
    public LangApp[] langAppAvailable;
    public LangApp defaultLanguage;
}
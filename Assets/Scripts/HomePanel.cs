using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomePanel : MonoBehaviour
{

    [SerializeField]
    private Text title, next, language;
    [SerializeField]
    private Translation translation;
    [SerializeField]
    private Language setLanguage;

    void Update()
    {
        translation = setLanguage.translation;
        title.text = translation.homeTitle;
        next.text = translation.homeNext;
        language.text = translation.homeLanguage;
    }
}

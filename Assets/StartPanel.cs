using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{

    [SerializeField]
    private Text title, next;
    [SerializeField]
    private Translation translation;
    [SerializeField]
    private Language setLanguage;

    void OnEnable()
    {
        translation = setLanguage.translation;
        title.text = translation.startTitle;
        next.text = translation.startNext;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
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
        title.text = translation.resultTitle;
        next.text = translation.resultNext;
    }
}

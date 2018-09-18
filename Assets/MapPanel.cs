using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapPanel : MonoBehaviour
{

    [SerializeField]
    private Text title, next, legend, module1, module2, module3;
    [SerializeField]
    private Translation translation;
    [SerializeField]
    private Language setLanguage;

    void OnEnable()
    {
        translation = setLanguage.translation;
        title.text = translation.mapTitle;
        next.text = translation.mapNext;
        legend.text = translation.mapLegend;
        module1.text = translation.mapModule1;
        module2.text = translation.mapModule2;
        module3.text = translation.mapModule3;
    }
}

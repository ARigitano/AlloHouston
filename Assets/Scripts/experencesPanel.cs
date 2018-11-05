using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class experencesPanel : MonoBehaviour
{

    [SerializeField]
    private Text title, next, experiment, difficulty, active, pTop, pTablet, pBottom, add;
    [SerializeField]
    private Translation translation;
    [SerializeField]
    private Language setLanguage;

    void OnEnable()
    {
        translation = setLanguage.translation;
        title.text = translation.experiencesTitle;
        next.text = translation.experiencesNext;
        experiment.text = translation.experiencesExperiment;
        difficulty.text = translation.experiencesDifficulty;
        active.text = translation.experiencesActive;
        pTop.text = translation.experiencesPTop;
        pTablet.text = translation.experiencesPTablet;
        pBottom.text = translation.experiencesPBottom;
        add.text = translation.experiencesAdd;
    }
}

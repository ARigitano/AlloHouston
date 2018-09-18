using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{

    [SerializeField]
    private Text title, next, cameras, logs, ai, timer, experiments, end, launch, abort, actions;
    [SerializeField]
    private Translation translation;
    [SerializeField]
    private Language setLanguage;

    void OnEnable()
    {
        translation = setLanguage.translation;
        title.text = translation.gameTitle;
        next.text = translation.gameNext;
        cameras.text = translation.gameCameras;
        logs.text = translation.gameLogs;
        ai.text = translation.gameAI;
        timer.text = translation.gameTimer;
        experiments.text = translation.gameExperiments;
        end.text = translation.gameEnd;
        launch.text = translation.gameLaunch;
        abort.text = translation.gameAbort;
        actions.text = translation.gameActions;
    }
}

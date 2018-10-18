using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using VRCalibrationTool;

public class GamePanel : MonoBehaviour
{

    [SerializeField]
    private Text title, next, cameras, logs, ai, timer, experiments, end, launch, abort, actions, time;
    [SerializeField]
    private Translation translation;
    [SerializeField]
    private Language setLanguage;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private AIScreen aiScreen;
    [SerializeField]
    private RoomManager roomManager;
    [SerializeField]
    private TextMeshPro aiText;
    [SerializeField]
    private Camera aiCamera;
    [SerializeField]
    private InputField aiCommunication;



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

        aiScreen = roomManager._aiScreen;
        aiText = aiScreen._aiText;
        aiCamera = aiScreen._aiCamera;


    }

    private void Update()
    {
        time.text =  Mathf.Floor(gameManager._timerSeconds / 60).ToString("00") + ":" + Mathf.Floor(gameManager._timerSeconds % 60).ToString("00");
        aiText.text = aiCommunication.text;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRTK;
using UnityEngine.Video;

namespace CRI.HelloHouston.Experience.UI
{
    public class UIGameManagerPanel : UIPanel
    {
        /// <summary>
        /// The camera display, it shows a view of the different cameras in the game (player view, experiment block view...)
        /// </summary>
        [SerializeField]
        [Tooltip("The camera display, it shows a view of the different cameras in the game (player view, experiment block view...)")]
        private UICameraDisplay _cameraDisplay;
        /// <summary>
        /// The log display, it shows the logs of the experiments and the game, with filter options.
        /// </summary>
        [SerializeField]
        [Tooltip("The log display, it shows the logs of the experiments and the game, with filter options.")]
        private UILogDisplay _logDisplay;
        /// <summary>
        /// The hint display, it allows the user to send preset text hints to the player or custom written hints.
        /// </summary>
        [SerializeField]
        [Tooltip("The hint display, it allows the user to send preset text hints to the player or custom written hints.")]
        private UIHintDisplay _hintDisplay;
        /// <summary>
        /// The timer display, it shows the time since the beginning of the game and an estimate of the time remaining.
        /// </summary>
        [SerializeField]
        [Tooltip("The timer display, it shows the time since the beginning of the game and an estimate of the time remaining.")]
        private UITimerDisplay _timerDisplay;
        /// <summary>
        /// The experience display, it shows the status of each experience with options to launch, stop them or run custom actions.
        /// </summary>
        [SerializeField]
        [Tooltip("The experience display, it shows the status of each experience with options to launch, stop them or run custom actions.")]
        private UIExperienceDisplay _experienceDisplay;
        /// <summary>
        /// The action display, it shows a list of custom actions that can be run on the game.
        /// </summary>
        [SerializeField]
        [Tooltip("The action display, it shows a list of custom actions that can be run on the game.")]
        private UIActionDisplay _actionDisplay;
        /// <summary>
        /// The player gameobject.
        /// </summary>
        [SerializeField]
        [Tooltip("The player gameobject")]
        private VRTK_SDKManager _player = null;
        /// <summary>
        /// Layer setup for the player in game.
        /// </summary>
        [SerializeField]
        [Tooltip("Layer setup for the player in game.")]
        private LayerMask _gameLayerMask = new LayerMask();
        [SerializeField]
        private LayerMask _introLayerMask = new LayerMask();
        [SerializeField]
        private GameObject _introScreen;
        private float _distance = 1.1f;
        private GameObject _screenInstance;
        private Camera[] playerCameras;

        private void Reset()
        {
            _cameraDisplay = GetComponentInChildren<UICameraDisplay>();
            _logDisplay = GetComponentInChildren<UILogDisplay>();
            _hintDisplay = GetComponentInChildren<UIHintDisplay>();
            _timerDisplay = GetComponentInChildren<UITimerDisplay>();
            _experienceDisplay = GetComponentInChildren<UIExperienceDisplay>();
            _actionDisplay = GetComponentInChildren<UIActionDisplay>();
        }

        public override void Init(object obj)
        {
            var rxpp = (RoomSettings)obj;
            Init(rxpp);
        }

        private void Init(RoomSettings rst)
        {
            GameManager gameManager = GameManager.instance;
            XPManager[] synchronizers;
            foreach (var zone in rst.vroom.GetZones())
            {
                Collider col = zone.GetComponentInChildren<Collider>();
                if (col)
                    col.enabled = false;
            }
            if (rst.seed <= 0)
                synchronizers = gameManager.InitGame(rst.xpContexts, rst.vroom, rst.timeEstimate);
            else
                synchronizers = gameManager.InitGame(rst.xpContexts, rst.vroom, rst.timeEstimate, rst.seed);
            var cameras = _player.GetComponentsInChildren<Camera>().Where(x => x.tag == "DisplayCamera").Concat(rst.vroom.GetComponentsInChildren<Camera>(true).Where(x => x.tag == "DisplayCamera"));
            _cameraDisplay.Init(cameras.ToArray());

            if (_player != null && _player.loadedSetup != null)
            {
                playerCameras = _player.loadedSetup.actualHeadset.GetComponentsInChildren<Camera>();
                playerCameras[0].cullingMask = _introLayerMask;
                _screenInstance = (GameObject)Instantiate(_introScreen, playerCameras[0].transform.position /*+ playerCameras[0].transform.forward * _distance*/, Quaternion.identity);
                //_screenInstance.transform.LookAt(playerCameras[1].transform.forward);
                /*foreach (Camera playerCamera in playerCameras)
                {
                    playerCamera.cullingMask = _introLayerMask;
                    _screenInstance = (GameObject)Instantiate(_introScreen, playerCamera.transform.position + playerCamera.transform.forward * _distance, Quaternion.identity);
                    _screenInstance.transform.LookAt(playerCamera.transform.forward);
                }*/
            }
            // Needs to be initialized before the start of the game.
            _logDisplay.Init(gameManager.logManager);
            _hintDisplay.Init(gameManager);
            _timerDisplay.Init(gameManager);

            gameManager.StartGame(rst.starting);
            
            //Needs to be initialized after the start of the game.
            _experienceDisplay.Init(synchronizers);
            _actionDisplay.Init(gameManager.actions, gameManager.gameActionController);
        }

        private void Update()
        {
            if (_screenInstance != null && !_screenInstance.GetComponent<VideoPlayer>().isPlaying)
            {
                IntroVideoStopped();
            }
        }

        private void IntroVideoStopped()
        {
            playerCameras[0].cullingMask = _gameLayerMask;
            Destroy(_screenInstance);

            /*foreach (Camera playerCamera in playerCameras)
            {
                playerCamera.cullingMask = _gameLayerMask;
                Destroy(_screenInstance);
            }*/
        }
    }
}

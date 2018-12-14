using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            var rxpp = (RoomXPPair)obj;
            Init(rxpp);
        }

        private void Init(RoomXPPair rxpp)
        {
            GameManager gameManager = GameManager.instance;
            XPSynchronizer[] synchronizers = gameManager.Init(rxpp.xpContexts);
            _cameraDisplay.Init(rxpp.vroom.GetComponentsInChildren<Camera>(true));

            // Needs to be initialized before the start of the game.
            _logDisplay.Init(gameManager.logManager);
            _hintDisplay.Init(gameManager);
            _timerDisplay.Init(gameManager);

            gameManager.StartGame(rxpp.starting);
            
            //Needs to be initialized after the start of the game.
            _experienceDisplay.Init(synchronizers);
            _actionDisplay.Init(gameManager.actions, gameManager.gameActionController);
        }
    }
}

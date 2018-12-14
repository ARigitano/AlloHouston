using CRI.HelloHouston.Experience.Actions;
using System.Linq;
using UnityEngine;
using System;

namespace CRI.HelloHouston.Experience
{
    public class GameManager : MonoBehaviour, ISource
    {
        private static GameManager s_instance;

        public static GameManager instance
        {
            get
            {
                if (!s_instance)
                    s_instance = GameObject.FindObjectOfType<GameManager>();
                if (!s_instance)
                    s_instance = new GameObject("GameManager").AddComponent<GameManager>();
                return s_instance;
            }
        }
        public delegate void GameManagerEvent();
        public static GameManagerEvent onExperienceChange;
        /// <summary>
        /// The Game action controller.
        /// </summary>
        public GameActionController gameActionController { get; private set; }
        [SerializeField]
        private XPMainSettings _mainSettings = null;
        /// <summary>
        /// The Log controller.
        /// </summary>
        public LogManager logManager { get; private set; }
        /// <summary>
        /// The log general controller.
        /// </summary>
        public LogGeneralController logGeneralController { get { return logManager.logGeneralController; } }
        /// <summary>
        /// Experience list.
        /// </summary>
        public XPSynchronizer[] xpSynchronizers { get; private set; }
        /// <summary>
        /// All the available game actions.
        /// </summary>
        public GameAction[] actions
        {
            get
            {
                return _mainSettings.actions;
            }
        }
        private float _startTime;
        /// <summary>
        /// Time since the game start.
        /// </summary>
        public float timeSinceGameStart {
            get
            {
                return Time.time - _startTime;
            }
        }

        public int xpTimeEstimate
        {
            get
            {
                return xpSynchronizers.Sum(x => x.xpContext.xpSettings.duration);
            }
        }

        public string sourceName
        {
            get
            {
                return "Main";
            }
        }

        public XPSynchronizer[] Init(XPContext[] xpContexts)
        {
            gameActionController = new GameActionController(this);
            logManager = new LogManager(this);
            _mainSettings = Resources.Load<XPMainSettings>("Settings/MainSettings");
            xpSynchronizers = xpContexts.Select(x => x.InitSynchronizer(logManager.logExperienceController)).ToArray();
            _startTime = Time.time;
            return xpSynchronizers;
        }

        public GameHint[] GetAllCurrentHints()
        {
            return _mainSettings.hints.Select(hint => new GameHint(hint, this)).Concat(xpSynchronizers.Where(x => x.active).SelectMany(x => x.xpContext.hints)).ToArray();
        }

        public void SendHintToPlayers(string hint)
        {
            logGeneralController.AddLog(string.Format("Hint sent to players: \"{0}\"", hint), this, Log.LogType.Automatic);
            Debug.Log(hint);
        }

        public void StartGame(bool[] starting)
        {
            for (int i = 0; i < starting.Length; i++)
            {
                if (starting[i])
                    xpSynchronizers[i].Activate();
            }
        }

        public void TurnLightOn()
        {
            logGeneralController.AddLog("Turn Light On", this, Log.LogType.Automatic);
        }

        public void TurnLightOff()
        {
            logGeneralController.AddLog("Turn Light Off", this, Log.LogType.Automatic);
        }

        public void PlaySound(AudioSource source)
        {
            logGeneralController.AddLog(string.Format("Play Sound {0}", source), this, Log.LogType.Automatic);
        }

        public void PlayMusic(AudioSource source)
        {
            logGeneralController.AddLog(string.Format("Play Music {0}", source), this, Log.LogType.Automatic);
        }

        public void StopMusic()
        {
            logGeneralController.AddLog(string.Format("Stop Music"), this, Log.LogType.Automatic);
        }

    }
}

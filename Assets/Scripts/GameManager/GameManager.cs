using CRI.HelloHouston.Experience.Actions;
using System.Linq;
using UnityEngine;
using System;
using CRI.HelloHouston.Calibration;
using CRI.HelloHouston.Audio;
using CRI.HelloHouston.Translation;
using UnityEngine.SceneManagement;
using CRI.HelloHouston.Settings;
using CRI.HelloHouston.GameElements;

namespace CRI.HelloHouston.Experience
{
    public class GameManager : MonoBehaviour, ISource, ILangManager
    {
        private static int s_randomSeed;

        public static int randomSeed
        {
            get
            {
                return s_randomSeed;
            }
        }

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
        private AppSettings _appSettings = null;
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
        /// globalSoundManager
        /// </summary>
        public SoundManager globalSoundManager { get; private set; }
        /// <summary>
        /// Language manager.
        /// </summary>
        public LangManager langManager { get; protected set; }
        /// <summary>
        /// Text manager.
        /// </summary>
        public TextManager textManager
        {
            get
            {
                return langManager.textManager;
            }
        }
        /// <summary>
        /// Experience list.
        /// </summary>
        public XPManager[] xpManagers { get; private set; }
        /// <summary>
        /// The communication screen.
        /// </summary>
        private UIComScreen _comScreen;
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
                return xpManagers.Sum(x => x.xpContext.xpSettings.duration);
            }
        }

        public string sourceName
        {
            get
            {
                return "Main";
            }
        }

        private void Awake()
        {
            InitGameManager();
        }

        private void InitGameManager()
        {
            globalSoundManager = GetComponent<SoundManager>();
            gameActionController = new GameActionController(this);
            logManager = new LogManager(this);
            langManager = new LangManager(_appSettings.langSettings);
        }

        public XPManager[] InitGame(XPContext[] xpContexts, VirtualRoom room)
        {
            return InitGame(xpContexts, room, UnityEngine.Random.Range(0, int.MaxValue));
        }

        public XPManager[] InitGame(XPContext[] xpContexts, VirtualRoom room, int seed)
        {
            s_randomSeed = seed;
            xpManagers = xpContexts.Select(xpContext => xpContext.InitManager(logManager.logExperienceController, room.GetZones().Where(zone => zone.xpContext == xpContext).ToArray(), s_randomSeed)).ToArray();
            _startTime = Time.time;
            logGeneralController.AddLog(string.Format("Random Seed: {0}", randomSeed), this, Log.LogType.Important);
            _comScreen = room.GetComponentInChildren<UIComScreen>();
            _comScreen.Init(this, xpManagers);
            return xpManagers;
        }

        public GameHint[] GetAllCurrentHints()
        {
            return _mainSettings.hints.Select(hint => new GameHint(hint, this)).Concat(xpManagers.Where(x => x.active).SelectMany(x => x.xpContext.hints)).ToArray();
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
                if (starting[i] && !xpManagers[i].active)
                {
                    xpManagers[i].Activate();
                }
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

        public void PlaySound(PlayableSound source)
        {
            globalSoundManager.Play(source);
            logGeneralController.AddLog(string.Format("Play Sound <{0}>", source.clip.name), this, Log.LogType.Automatic);
        }

        public void PlayMusic(PlayableMusic source)
        {
            globalSoundManager.Play(source);
            logGeneralController.AddLog(string.Format("Play Music <{0}>", source.clip.name), this, Log.LogType.Automatic);
        }

        public void StopMusic()
        {
            globalSoundManager.StopAllMusic();
            logGeneralController.AddLog(string.Format("Stop Music"), this, Log.LogType.Automatic);
        }
    }
}

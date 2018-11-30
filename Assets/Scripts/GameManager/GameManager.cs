using CRI.HelloHouston.Experience.Actions;
using System.Linq;
using UnityEngine;

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
        private GameActionController _gameActionController;
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
        /// The log experience controller.
        /// </summary>
        public LogExperienceController logExperienceController { get { return logManager.logExperienceController; } }
        /// <summary>
        /// Experience list.
        /// </summary>
        public XPSynchronizer[] xpSynchronizers { get; private set; }
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

        private void Awake()
        {
            _gameActionController = new GameActionController(this);
            logManager = new LogManager(this);
            _mainSettings = Resources.Load<XPMainSettings>("Settings/MainSettings");
            Init(Resources.LoadAll<XPContext>("AllExperiences/Electricity/XpContext"));
        }

        public void Init(XPContext[] xpContexts)
        {
            xpSynchronizers = xpContexts.Select(x => x.InitSynchronizer(logManager.logExperienceController)).ToArray();
            _startTime = Time.time;
        }

        public GameHint[] GetAllCurrentHints()
        {
            return _mainSettings.hints.Select(hint => new GameHint(hint, this)).Concat(xpSynchronizers.Where(x => x.active).SelectMany(x => x.xpContext.hints)).ToArray();
        }

        public void SendHintToPlayers(string hint)
        {
            Debug.Log(hint);
        }

        /// <summary>
        /// Resolve the first action of the queue if there's at least one action in the queue and the current action has finished.
        /// </summary>
        /// <param name="force">If true, it will resolve the first action of the queue even if the current action didn't finish yet.</param>
        /// <returns>True if an action was resolved. False if it didn't.</returns>
        public bool ResolveFirstAction()
        {
            return _gameActionController.ResolveFirstAction();
        }

        /// <summary>
        /// Adds an action to the queue of actions.
        /// </summary>
        /// <param name="action">An instance of GameAction</param>
        public void AddAction(GameAction action)
        {
            _gameActionController.AddAction(action);
        }
    }
}

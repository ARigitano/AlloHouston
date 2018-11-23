using CRI.HelloHouston.Experience.Actions;
using System.Linq;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    public class GameManager : MonoBehaviour
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
        /// <summary>
        /// The Game action controller.
        /// </summary>
        private GameActionController _gameActionController;
        [SerializeField]
        private XPMainParameter _mainParameter = null;
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
        private XPContext[] _xpContexts;
        /// <summary>
        /// Time since the game start.
        /// </summary>
        public float timeSinceGameStart { get; private set; }

        private void Awake()
        {
            _gameActionController = new GameActionController(this);
            logManager = new LogManager(this);
        }

        public void Init(XPContext[] xpContexts)
        {
            _xpContexts = xpContexts;
        }

        public string[] GetAllCurrentHints()
        {
            return _mainParameter.hints.Concat(_xpContexts.Where(x => x.xpSynchronizer != null && x.xpSynchronizer.active).SelectMany(x => x.xpParameter.availableHints)).ToArray();
        }

        public void SendHintToPlayers()
        {

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

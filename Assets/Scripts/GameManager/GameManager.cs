using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// The Game action controller.
        /// </summary>
        private GameActionController _gameActionController;
        /// <summary>
        /// The Log controller.
        /// </summary>
        private LogController _logController;
        /// <summary>
        /// Time since the game start.
        /// </summary>
        public float timeSinceGameStart { get; private set; }

        private void Awake()
        {
            _gameActionController = new GameActionController(this);
            _logController = new LogController(this);
        }

        /// <summary>
        /// Adds a log that will be displayed on the GameManager UI and saved in the log file.
        /// </summary>
        /// <param name="str">The message of the log.</param>
        public void AddLog(string str)
        {
            _logController.AddLog(str, timeSinceGameStart);
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

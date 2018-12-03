using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.Actions
{
    public class GameActionController : GeneralActionController<GameAction>
    {
        public GameManager gameManager { get; private set; }

        /// <summary>
        /// Resolve the first action of the queue if there's at least one action in the queue and the current action has finished.
        /// </summary>
        /// <param name="force">If true, it will resolve the first action of the queue even if the current action didn't finish yet.</param>
        /// <returns>True if an action was resolved. False if it didn't.</returns>
        public override bool ResolveFirstAction(bool force = false)
        {
            if (actionQueue.Peek() != null && (force || canResolveFirstAction))
            {
                GameAction action = actionQueue.Dequeue();
                _currentAction = action;
                _currentAction.Act(gameManager);
                _lastActionResolutionTime = Time.time;
                return true;
            }
            return false;
        }

        public GameActionController(GameManager gameManager)
        {
            actionQueue = new Queue<GameAction>();
            this.gameManager = gameManager;
        }
    }
}

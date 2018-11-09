using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Action
{
    public class GameActionController
    {
        public Queue<GameAction> actionQueue = new Queue<GameAction>();
        /// <summary>
        /// Time when the last action resolved
        /// </summary>
        private float _lastActionResolutionTime;
        /// <summary>
        /// The current action.
        /// </summary>
        private GameAction _currentAction = null;
        
        public List<Light> lights;
        /// <summary>
        /// Returns true if there's at least one action if the action queue and the currentAction is null or has finished.
        /// </summary>
        public bool canResolveFirstAction
        {
            get
            {
                return (actionQueue.Peek() != null && (_currentAction == null || (Time.time - _lastActionResolutionTime) > _currentAction.actionDuration));
            }
        }

        /// <summary>
        /// Resolve the first action of the queue if there's at least one action in the queue and the current action has finished.
        /// </summary>
        /// <param name="force">If true, it will resolve the first action of the queue even if the current action didn't finish yet.</param>
        /// <returns>True if an action was resolved. False if it didn't.</returns>
        public bool ResolveFirstAction(bool force = false)
        {
            if (actionQueue.Peek() != null && (force || canResolveFirstAction))
            {
                GameAction action = actionQueue.Dequeue();
                _currentAction = action;
                _currentAction.Act(this);
                _lastActionResolutionTime = Time.time;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds an action to the queue of actions.
        /// </summary>
        /// <param name="action">An instance of GameAction</param>
        public void AddAction(GameAction action)
        {
            actionQueue.Enqueue(action);
        }
    }
}

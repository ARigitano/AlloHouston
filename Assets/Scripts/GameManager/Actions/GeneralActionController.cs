using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CRI.HelloHouston.Experience.Actions
{
    public abstract class GeneralActionController<T> where T: GeneralAction
    {
        public Queue<T> actionQueue;
        /// <summary>
        /// Time when the last action resolved
        /// </summary>
        protected float _lastActionResolutionTime;
        /// <summary>
        /// The current action.
        /// </summary>
        protected T _currentAction = null;
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

        public abstract bool ResolveFirstAction(bool force = false);

        /// <summary>
        /// Adds an action to the queue of actions.
        /// </summary>
        /// <param name="action">An instance of GameAction</param>
        public void AddAction(T action)
        {
            actionQueue.Enqueue(action);
            ResolveFirstAction();
        }
    }
}

using CRI.HelloHouston.Experience.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CRI.HelloHouston.Experience.Actions
{
    public class ExperienceActionController
    {
        public XPMainSynchronizer synchronizer { get; private set; }
        public Queue<ExperienceAction> actionQueue;
        /// <summary>
        /// Time when the last action resolved
        /// </summary>
        private float _lastActionResolutionTime;
        /// <summary>
        /// The current action.
        /// </summary>
        private ExperienceAction _currentAction = null;
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
                var action = actionQueue.Dequeue();
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
        public void AddAction(ExperienceAction action)
        {
            actionQueue.Enqueue(action);
        }

        public ExperienceActionController(XPMainSynchronizer synchronizer)
        {
            actionQueue = new Queue<ExperienceAction>();
            this.synchronizer = synchronizer;
        }
    }
}

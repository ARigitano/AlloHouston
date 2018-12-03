using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.Actions
{
    public class ExperienceActionController : GeneralActionController<ExperienceAction>
    {

        public XPSynchronizer synchronizer { get; private set; }
        /// <summary>
        /// Resolve the first action of the queue if there's at least one action in the queue and the current action has finished.
        /// </summary>
        /// <param name="force">If true, it will resolve the first action of the queue even if the current action didn't finish yet.</param>
        /// <returns>True if an action was resolved. False if it didn't.</returns>
        public override bool ResolveFirstAction(bool force = false)
        {
            if (actionQueue.Peek() != null && (force || canResolveFirstAction))
            {
                ExperienceAction action = actionQueue.Dequeue();
                _currentAction = action;
                _currentAction.Act(synchronizer);
                _lastActionResolutionTime = Time.time;
                return true;
            }
            return false;
        }

        public ExperienceActionController(XPSynchronizer synchronizer)
        {
            actionQueue = new Queue<ExperienceAction>();
            this.synchronizer = synchronizer;
        }
    }
}

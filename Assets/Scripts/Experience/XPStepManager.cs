using System;
using System.Linq;

namespace CRI.HelloHouston.Experience
{
    public class XPStepManager
    {
        public class StepAction
        {
            public string name;
            public int value;
            public Action action;

            public StepAction(string name, int value, Action action)
            {
                this.name = name;
                this.value = value;
                this.action = action;
            }
        }

        public XPStepEvent onStepChange;
        public delegate void XPStepEvent(int step);

        public int maxSteps { get; private set; }

        private int _currentStepValue;
        /// <summary>
        /// The current step of the experiment. The value should be lower than the max number of step in the context settings.
        /// </summary>
        public int currentStepValue
        {
            get
            {
                if (_currentStepValue > maxSteps)
                    _currentStepValue = maxSteps;
                return _currentStepValue;
            }
            set
            {
                _currentStepValue = value > maxSteps ? maxSteps : value;
                if (onStepChange != null)
                    onStepChange(_currentStepValue);
            }
        }

        public StepAction currentStepAction
        {
            get
            {
                return GetCurrentStepAction();
            }
        }

        public StepAction[] stepActions;

        public XPStepManager(int maxSteps)
        {
            this.maxSteps = maxSteps;
        }

        /// <summary>
        /// Advance the steps by a set number (default = 1).
        /// </summary>
        /// <param name="step">The number of steps to advance to.</param>
        public void AdvanceStep(int step = 1)
        {
            currentStepValue += step;
        }

        public void AdvanceStep(string stepName)
        {
            int count = 0;
            bool found = false;
            foreach (var stepAction in stepActions)
            {
                count += stepAction.value;
                if (stepAction.name == stepName)
                {
                    found = true;
                    break;
                }
            }
            if (found)
                currentStepValue = count;
        }

        private StepAction GetCurrentStepAction()
        {
            StepAction res = null;
            int count = 0;
            for (int i = 0; stepActions != null && i < stepActions.Length; i++)
            {
                StepAction stepAction = stepActions[i];
                if (count + stepAction.value > currentStepValue || (i + 1) == stepActions.Length)
                {
                    res = stepAction;
                    break;
                }
                count += stepAction.value;
            }
            return res;
        }
    }
}

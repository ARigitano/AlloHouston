using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace CRI.HelloHouston.Experience
{
    [Serializable]
    public class XPStepManager
    {
        [Serializable]
        public class StepAction
        {
            public string name;
            public int value;
            public UnityEvent action;

            public StepAction(string name, int value, UnityEvent action)
            {
                this.name = name;
                this.value = value;
                this.action = action;
            }
        }

        public XPStepEvent onStepChange;
        public delegate void XPStepEvent(StepAction currentStep, int currentStepValue);

        private int? _maxStepValue;

        public int maxStepValue
        {
            get
            {
                if (_maxStepValue == null)
                {
                    _maxStepValue = stepActions.Sum(x => x.value);
                }
                return _maxStepValue.Value;
            }
        }
        
        /// <summary>
        /// The current step value of the experiment. The value should be lower than the max number of step in the context settings.
        /// </summary>
        public int sumValue
        {
            get
            {
                return GetCurrentStepValue();
            }
        }

        private int _currentStepIndex;

        public int currentStepIndex {
            get
            {
                return _currentStepIndex;
            }
            private set
            {
                _currentStepIndex = value;
                if (onStepChange != null)
                    onStepChange(currentStep, sumValue);
            }
        }

        public StepAction currentStep
        {
            get
            {
                return stepActions != null ? stepActions[currentStepIndex] : null;
            }
        }

        public StepAction[] stepActions;

        /// <summary>
        /// Advance the steps by a set number (default = 1).
        /// </summary>
        /// <param name="step">The number of steps to advance to.</param>
        public void AdvanceStep(int val = 1)
        {
            currentStepIndex = Mathf.Clamp((currentStepIndex + val), 0, stepActions.Length - 1);
        }

        /// <summary>
        /// Skip to the step with the name in the parameter.
        /// </summary>
        /// <param name="stepName">The name of the index this method will skip top</param>
        /// <returns>True if the skip was successful, false otherwise.</returns>
        public bool SkipToStep(string stepName)
        {
            for (int i = 0; stepActions != null && i < stepActions.Length; i++)
            {
                if (stepActions[i].name == stepName)
                {
                    currentStepIndex = i;
                    return true;
                }
            }
            return false;
        }

        public bool SkipToStep(int stepIndex)
        {
            if (stepActions != null && stepIndex >= 0 && stepIndex < stepActions.Length)
            {
                currentStepIndex = stepIndex;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get the sum of all the step values before the current step.
        /// </summary>
        /// <returns>The sum of all the step values before the current step.</returns>
        private int GetCurrentStepValue()
        {
            int count = 0;
            for (int i = 0; stepActions != null && i < stepActions.Length; i++)
            {
                if (i == currentStepIndex)
                    return count;
                count += stepActions[i].value;
            }
            return 0;
        }
    }
}

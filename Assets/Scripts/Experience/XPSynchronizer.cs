using CRI.HelloHouston.Calibration;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using CRI.HelloHouston.Experience.Actions;

namespace CRI.HelloHouston.Experience
{
    public enum XPState
    {
        Inactive,
        Visible,
        Hidden,
        Success,
        Failure,
    }

    /// <summary>
    /// The XpSynchronizer is responsible for the communication of every prefabs of one particular experiment among themselves as well as with the Gamecontroller.
    /// </summary>
    [System.Serializable]
    public abstract class XPSynchronizer : MonoBehaviour
    {
        public delegate void XPStateEvent(XPState state);
        public delegate void XPSynchronizerEvent(XPSynchronizer synchronizer);
        public XPStateEvent onStateChange;
        public static XPSynchronizerEvent onActivation;
        public static XPSynchronizerEvent onEnd;
        /// <summary>
        /// The xp context.
        /// </summary>
        public XPContext xpContext { get; protected set; }
        /// <summary>
        /// All the contents.
        /// </summary>
        protected List<XPElement> elements = new List<XPElement>();
        /// <summary>
        /// The error of the experiment chosen randomly that will be displayed on the table screen for this game.
        /// </summary>
        [HideInInspector]
        public string error;
        /// <summary>
        /// The state of the XPSynchronizer.
        /// </summary>
        public XPState state { get; protected set; }
        
        public LogExperienceController logController { get; protected set; }

        public ExperienceActionController actionController { get; protected set;}

        protected XPState _stateOnActivation;

        public bool active
        {
            get
            {
                return state == XPState.Visible || state == XPState.Hidden;
            }
        }

        public string sourceName
        {
            get
            {
                return xpContext.xpGroup.experimentName;
            }
        }

        public T GetElement<T>(string name) where T : XPElement, new()
        {
            return (T)elements.FirstOrDefault(x => x.GetType() == typeof(T) && x.elementName == name);
        }

        public T[] GetElements<T>() where T : XPElement, new()
        {
            return (T[]) elements.Where(x => x.GetType() == typeof(T)).ToArray();
        }


        /// <summary>
        /// To be called in case of success of the experiment.
        /// </summary>
        public void Success()
        {
            CustomSuccess();
            state = XPState.Success;
            if (onStateChange != null)
                onStateChange(state);
            if (onEnd != null)
                onEnd(this);
            logController.AddLog("Success", xpContext, Log.LogType.Automatic);
            foreach (var element in elements)
            {
                element.OnSuccess();
            }
        }

        /// <summary>
        /// Called before the state changed to the success state.
        /// </summary>
        protected virtual void CustomSuccess()
        {

        }

        /// <summary>
        /// To be called in case of failure of the experiment.
        /// </summary>
        public void Fail()
        {
            CustomFail();
            state = XPState.Failure;
            if (onStateChange != null)
                onStateChange(state);
            if (onEnd != null)
                onEnd(this);
            logController.AddLog("Failure", xpContext, Log.LogType.Automatic);
            foreach (var element in elements)
            {
                element.OnFailure();
            }
        }

        /// <summary>
        /// Called before the state changes to failure.
        /// </summary>
        protected virtual void CustomFail()
        {

        }

        /// <summary>
        /// To be called to activate the incident of the experiment.
        /// </summary>
        public void Activate()
        {
            CustomActivate();
            state = _stateOnActivation;
            if (onStateChange != null)
                onStateChange(state);
            if (onActivation != null)
                onActivation(this);
            logController.AddLog("Activation", xpContext, Log.LogType.Automatic);
            foreach (var element in elements)
            {
                element.OnActivation();
            }
        }

        /// <summary>
        /// Called before the state changes to the default state on activation.
        /// </summary>
        protected virtual void CustomActivate()
        {
        }

        /// <summary>
        /// To be called to pause the experiment during the game.
        /// </summary>
        public void Hide()
        {
            CustomHide();
            state = XPState.Hidden;
            if (onStateChange != null)
                onStateChange(state);
            logController.AddLog("Hide", xpContext, Log.LogType.Automatic);
            foreach (var element in elements)
            {
                element.OnHide();
            }
        }

        /// <summary>
        /// Called before the state changes to the hidden state.
        /// </summary>
        protected virtual void CustomHide()
        {

        }

        /// <summary>
        /// To be called to call back the experiment after it has been paused.
        /// </summary>
        public void Show()
        {
            CustomShow();
            state = XPState.Visible;
            if (onStateChange != null)
                onStateChange(state);
            logController.AddLog("Show", xpContext, Log.LogType.Automatic);
            foreach (var element in elements)
            {
                element.OnShow();
            }
        }

        /// <summary>
        /// Called before the state changes to the visible state.
        /// </summary>
        protected virtual void CustomShow()
        {

        }

        public void Init(XPContext xpContext, LogExperienceController logController, XPState stateOnActivation = XPState.Hidden)
        {
            this.xpContext = xpContext;
            state = XPState.Inactive;
            _stateOnActivation = stateOnActivation;
            actionController = new ExperienceActionController(this);
            this.logController = logController;
            CustomInit(xpContext, logController, stateOnActivation);
            if (logController != null)
                logController.AddLog("Ready", xpContext, Log.LogType.Automatic);
        }

        protected virtual void CustomInit(XPContext xpContext, LogExperienceController logController, XPState stateOnActivation)
        {

        }

        public void AddAction(ExperienceAction action)
        {
            actionController.AddAction(action);
        }
    }
}
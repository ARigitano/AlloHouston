﻿using CRI.HelloHouston.Calibration;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CRI.HelloHouston.Experience.Actions;
using System;
using CRI.HelloHouston.Audio;
using CRI.HelloHouston.Translation;

namespace CRI.HelloHouston.Experience
{
    public enum XPState
    {
        Inactive,
        InProgress,
        Success,
        Failure,
    }

    public enum XPVisibility
    {
        Visible,
        Hidden
    }

    public struct XPManagerEventArgs
    {
        public XPManager manager;
        public XPState currentState;
        public XPVisibility currentVisiblity;
    }


    public delegate void XPManagerEventHandler(object sender, XPManagerEventArgs e);

    /// <summary>
    /// The XPManager is responsible for the communication of every prefabs of one particular experiment among themselves as well as with the Gamecontroller.
    /// </summary>
    [System.Serializable]
    public class XPManager : MonoBehaviour, ILangManager
    {
        [System.Serializable]
        public struct ElementInfo
        {
            public XPElement xpElement;
            public VirtualElement virtualElement;
            public VirtualZone virtualZone;

            public ElementInfo(XPElement xpElement, VirtualElement virtualElement, VirtualZone virtualZone)
            {
                this.xpElement = xpElement;
                this.virtualElement = virtualElement;
                this.virtualZone = virtualZone;
            }
        }
        public event XPManagerEventHandler onStateChange;
        public event XPManagerEventHandler onVisibilityChange;
        public static event XPManagerEventHandler onActivation;
        public static event XPManagerEventHandler onDeactivation;
        public static event XPManagerEventHandler onEnd;
        /// <summary>
        /// The xp context.
        /// </summary>
        public XPContext xpContext { get; protected set; }
        /// <summary>
        /// The wall top zone.
        /// </summary>
        public VirtualWallTopZone wallTopZone { get; protected set; }
        /// <summary>
        /// The hologram zone.
        /// </summary>
        public VirtualHologramZone hologramZone { get; protected set; }
        /// <summary>
        /// All the contents.
        /// </summary>
        protected List<ElementInfo> elements = new List<ElementInfo>();
        /// <summary>
        /// The error of the experiment chosen randomly that will be displayed on the table screen for this game.
        /// </summary>
        [HideInInspector]
        public string error;
        /// <summary>
        /// The state of the XPManager.
        /// </summary>
        public XPState state { get; protected set; }
        /// <summary>
        /// The visibility of the XPManager.
        /// </summary>
        public XPVisibility visibility { get; protected set; }

        public LogExperienceController logController { get; protected set; }

        public ExperienceActionController actionController { get; protected set; }

        public LangManager langManager { get; protected set; }

        public TextManager textManager {
            get
            {
                return langManager.textManager;
            }
        }

        [SerializeField]
        [Tooltip("The step manager.")]
        protected XPStepManager _stepManager;

        public XPStepManager stepManager
        {
            get
            {
                return _stepManager;
            }
        }

        public int randomSeed { get; private set; }

        protected XPVisibility _visibilityOnActivation;

        protected XPState _previousState;
        protected XPVisibility _previousVisibility;

        public bool visible { get; private set; }

        public T GetElement<T>(string name) where T : XPElement, new()
        {
            return (T)elements.Select(x => x.xpElement).FirstOrDefault(xpElement => xpElement is T && xpElement.elementName == name);
        }

        public T GetElement<T>() where T : XPElement, new()
        {
            return (T)elements.Select(x => x.xpElement).FirstOrDefault(xpElement => xpElement is T) as T;
        }

        public XPElement GetElement(string name)
        {
            return elements.Select(element => element.xpElement).FirstOrDefault(xpElement => xpElement.elementName == name);
        }

        public T[] GetElements<T>() where T : XPElement, new()
        {
            T[] res = elements.Select(element => element.xpElement).Where(xpElement => xpElement is T).Select(xpElement => (T)xpElement).ToArray();
            return res;
        }

        /// <summary>
        /// Called before the state changed to the reset state.
        /// </summary>
        protected virtual void PreReset() { }
        /// <summary>
        /// Called after the state changed to the reset state.
        /// </summary>
        /// <returns></returns>
        protected virtual void PostReset() { }


        private void InvokeStateChangeEvent()
        {
            if (onStateChange != null && _previousState != state)
                onStateChange(this, new XPManagerEventArgs() { manager = this, currentState = state, currentVisiblity = visibility });
        }

        private void InvokeEndEvent()
        {
            if (onEnd != null && _previousState != state)
                onEnd(this, new XPManagerEventArgs() { manager = this, currentState = state, currentVisiblity = visibility });
        }

        private void InvokeVisibilityEvent()
        {
            if (onVisibilityChange != null && _previousVisibility != visibility)
            {
                Debug.Log("Event fired");
                onVisibilityChange(this, new XPManagerEventArgs() { manager = this, currentState = state, currentVisiblity = visibility });
            }
        }

        private void InvokeActivationEvent()
        {
            if (onActivation != null && _previousState != state)
                onActivation(this, new XPManagerEventArgs() { manager = this, currentState = state, currentVisiblity = visibility });
        }

        private void InvokeDeactivationEvent()
        {
            if (onDeactivation != null && _previousState != state)
                onDeactivation(this, new XPManagerEventArgs() { manager = this, currentState = state, currentVisiblity = visibility });
        }

        public void ResetExperiment()
        {
            PreReset();
            _previousState = state;
            state = XPState.InProgress;
            stepManager.SkipToStep(0);
            InvokeStateChangeEvent();
            logController.AddLog("Reset", xpContext, Log.LogType.Automatic);
            foreach (var element in elements)
            {
                element.xpElement.OnReset();
            }
            PostReset();
        }


        /// <summary>
        /// To be called in case of success of the experiment.
        /// </summary>
        public void Success()
        {
            PreSuccess();
            _previousState = state;
            state = XPState.Success;
            if (onStateChange != null && _previousState != state)
                onStateChange(this, new XPManagerEventArgs() { manager = this, currentState = state, currentVisiblity = visibility });
            if (onEnd != null && _previousState != state)
                onEnd(this, new XPManagerEventArgs() { manager = this, currentState = state, currentVisiblity = visibility });
            logController.AddLog("Success", xpContext, Log.LogType.Automatic);
            foreach (var element in elements)
            {
                element.xpElement.OnSuccess();
            }
            PostSuccess();
        }

        /// <summary>
        /// Called before the state changed to the success state.
        /// </summary>
        protected virtual void PreSuccess() { }
        /// <summary>
        /// Called after the state changed to the success state.
        /// </summary>
        /// <returns></returns>
        protected virtual void PostSuccess() { }

        /// <summary>
        /// To be called in case of failure of the experiment.
        /// </summary>
        public void Fail()
        {
            PreFail();
            _previousState = state;
            state = XPState.Failure;
            InvokeStateChangeEvent();
            InvokeEndEvent();
            logController.AddLog("Failure", xpContext, Log.LogType.Automatic);
            foreach (var element in elements)
            {
                element.xpElement.OnFailure();
            }
            PostFail();
        }

        /// <summary>
        /// Called before the state changes to failure.
        /// </summary>
        protected virtual void PreFail() { }
        /// <summary>
        /// Called after that state changes to failure.
        /// </summary>
        protected virtual void PostFail() { }

        /// <summary>
        /// To be called to activate the incident of the experiment.
        /// </summary>
        public void Activate()
        {
            PreActivate();
            _previousState = state;
            _previousVisibility = visibility;
            state = XPState.InProgress;
            visibility = _visibilityOnActivation;
            InvokeStateChangeEvent();
            InvokeActivationEvent();
            InvokeVisibilityEvent();
            logController.AddLog("Activation", xpContext, Log.LogType.Automatic);
            foreach (var element in elements)
            {
                element.xpElement.OnActivation();
            }
            PostActivate();
        }

        /// <summary>
        /// Called before the state changed to the default state on activation.
        /// </summary>
        protected virtual void PreActivate() { }
        /// <summary>
        /// Called before the state changes to the default state on activation.
        /// </summary>
        protected virtual void PostActivate() { }

        public void Deactivate()
        {
            _previousState = state;
            _previousVisibility = visibility;
            state = XPState.Inactive;
            InvokeStateChangeEvent();
            InvokeDeactivationEvent();
            InvokeVisibilityEvent();
        }

        /// <summary>
        /// To be called to pause the experiment during the game.
        /// </summary>
        public void Hide()
        {
            PreHide();
            if (this.wallTopZone != null)
                CleanZone(wallTopZone);
            if (this.hologramZone != null)
                CleanZone(hologramZone);
            _previousVisibility = visibility;
            visibility = XPVisibility.Hidden;
            InvokeVisibilityEvent();
            logController.AddLog("Hide", xpContext, Log.LogType.Automatic);
            foreach (var element in elements)
            {
                element.xpElement.OnHide();
            }
            PostHide();
        }

        /// <summary>
        /// Called before the state changes to the hidden state.
        /// </summary>
        protected virtual void PreHide() { }
        /// <summary>
        /// Called after the state changes to the hidden state.
        /// </summary>
        protected virtual void PostHide() { }

        /// <summary>
        /// To be called to call back the experiment after it has been paused.
        /// </summary>
        public void Show(VirtualWallTopZone wallTopZone, VirtualHologramZone hologramZone)
        {
            this.wallTopZone = wallTopZone;
            wallTopZone.Place(xpContext.xpWallTopZone, xpContext);
            InitZone(wallTopZone);
            this.hologramZone = hologramZone;
            if (xpContext.xpHologramZone != null && hologramZone != null)
            {
                hologramZone.Place(xpContext.xpHologramZone, xpContext);
                InitZone(hologramZone);
            }
            PreShow(wallTopZone, hologramZone, elements.ToArray());
            _previousVisibility = visibility;
            visibility = XPVisibility.Visible;
            InvokeVisibilityEvent();
            logController.AddLog("Show", xpContext, Log.LogType.Automatic);
            foreach (var element in elements)
            {
                element.xpElement.OnShow(stepManager.sumValue);
            }
            PostShow(wallTopZone, hologramZone, elements.ToArray());
        }

        /// <summary>
        /// Called before the state changes to the visible state.
        /// </summary>
        protected virtual void PreShow(VirtualWallTopZone wallTopZone, VirtualHologramZone hologramZone, ElementInfo[] info) { }
        /// <summary>
        /// Called after the state changes to the visible state.
        /// </summary>
        protected virtual void PostShow(VirtualWallTopZone wallTopZone, VirtualHologramZone hologramZone, ElementInfo[] info) { }

        protected virtual void CleanZone(VirtualZone zone)
        {
            IEnumerable<XPElement> res = zone.CleanAll().ToList();
            elements.RemoveAll(x => res.Contains(x.xpElement));
        }

        protected virtual ElementInfo[] InitZone(VirtualZone zone)
        {
            var res = zone.InitAll(this).Select(xpElement => new ElementInfo(xpElement, xpElement.virtualElement, zone));
            foreach (var element in res)
            {
                element.xpElement.OnInit(this, randomSeed);
            }
            elements.AddRange(res);
            return res.ToArray();
        }

        protected virtual ElementInfo[] InitZones(IEnumerable<VirtualZone> zones)
        {
            var res = zones.SelectMany(zone => InitZone(zone));
            return res.ToArray();
        }

        public void Init(
            XPContext xpContext, VirtualZone[] zones,
            LogExperienceController logController, int randomSeed,
            XPVisibility visibilityOnActivation = XPVisibility.Visible
            )
        {
            PreInit(xpContext, logController, randomSeed, visibilityOnActivation);
            this.xpContext = xpContext;
            this.randomSeed = randomSeed;
            _previousState = state;
            state = XPState.Inactive;
            _visibilityOnActivation = visibilityOnActivation;
            actionController = new ExperienceActionController(this);
            if (xpContext.xpGroup.settings != null)
                langManager = new LangManager(xpContext.xpGroup.settings.langSettings);
            else
                langManager = new LangManager();
            this.logController = logController;
            if (logController != null)
                logController.AddLog("Ready", xpContext, Log.LogType.Automatic);
            ElementInfo[] zoneInfo = InitZones(zones.Where(zone => !zone.switchableZone));
            PostInit(xpContext, zoneInfo, logController, randomSeed, visibilityOnActivation);
        }

        /// <summary>
        /// Called before the initialization.
        /// </summary>
        protected virtual void PreInit(XPContext xpContext, LogExperienceController logController, int randomSeed, XPVisibility visibilityOnActivation) { }
        /// <summary>
        /// Called after the initialization.
        /// </summary>
        protected virtual void PostInit(XPContext xpContext, ElementInfo[] info, LogExperienceController logController, int randomSeed, XPVisibility visibilityOnActivation) { }

        public virtual void SkipToNextStep()
        {
            if (stepManager.currentStepIndex < stepManager.stepActions.Length - 1)
            {
                SkipToStep(stepManager.currentStepIndex + 1);
            }
            else
            {
                logController.AddLogError(string.Format("Can't skip to next step."), xpContext);
            }
        }

        public virtual void SkipToPreviousStep()
        {
            if (stepManager.currentStepIndex > 0)
            {
                SkipToStep(stepManager.currentStepIndex - 1);
            }
            else
            {
                logController.AddLogError(string.Format("Can't skip to previous step."), xpContext);
            }
        }

        public virtual void RestartStep()
        {
            SkipToStep(stepManager.currentStepIndex);
        }
        
        public virtual void SkipToStep(int stepValue)
        {
            if (stepManager.SkipToStep(stepValue))
            {
                logController.AddLogAutomatic(string.Format("Skip to step {0}", stepValue), xpContext);
                XPStepManager.StepAction stepAction = stepManager.currentStep;
                if (stepAction != null && stepAction.action != null)
                    stepAction.action.Invoke();
                if ((state == XPState.Success || state == XPState.Failure) && stepManager.sumValue < stepManager.maxStepValue)
                {
                    var tmp = state;
                    state = _previousState;
                    _previousState = tmp;
                    if (onStateChange != null)
                        onStateChange(this, new XPManagerEventArgs() { manager = this, currentState = state });
                }
            }
            else
            {
                logController.AddLogError(string.Format("Skip to step unsuccessful for step {0}", stepValue), xpContext);
            }
        }

        public virtual void SkipToStep(string stepName)
        {
            if (stepManager.SkipToStep(stepName))
            {
                logController.AddLogAutomatic(string.Format("Skip to step {0}", stepName), xpContext);
                XPStepManager.StepAction stepAction = stepManager.currentStep;
                if (stepAction != null && stepAction.action != null)
                    stepAction.action.Invoke();
                if ((state == XPState.Success || state == XPState.Failure) && stepManager.sumValue < stepManager.maxStepValue)
                {
                    var tmp = state;
                    state = _previousState;
                    _previousState = tmp;
                    if (onStateChange != null)
                        onStateChange(this, new XPManagerEventArgs() { manager = this, currentState = state });
                }
            }
            else
            {
                logController.AddLogError(string.Format("Skip to step unsuccessful for step {0}", stepName), xpContext);
            }
        }

        public virtual void PlaySound(PlayableSound sound)
        {
            logController.AddLog(string.Format("Play sound {0}", sound), xpContext, Log.LogType.Automatic);
            if (wallTopZone != null)
                wallTopZone.leftSpeaker.PlaySound(sound);
        }

        public virtual void PlayMusic(PlayableMusic music)
        {
            logController.AddLog(string.Format("Play music {0}", music), xpContext, Log.LogType.Automatic);
            if (wallTopZone != null)
                wallTopZone.leftSpeaker.PlayMusic(music);
        }
    }
}
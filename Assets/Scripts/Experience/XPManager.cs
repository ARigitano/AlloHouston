using CRI.HelloHouston.Calibration;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CRI.HelloHouston.Experience.Actions;
using System;
using CRI.HelloHouston.Audio;

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
    /// The XPManager is responsible for the communication of every prefabs of one particular experiment among themselves as well as with the Gamecontroller.
    /// </summary>
    [System.Serializable]
    public abstract class XPManager : MonoBehaviour
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
        public delegate void XPStateEvent(XPState state);
        public delegate void XPManagerEvent(XPManager synchronizer);
        public XPStateEvent onStateChange;
        public static XPManagerEvent onActivation;
        public static XPManagerEvent onEnd;
        /// <summary>
        /// The xp context.
        /// </summary>
        public XPContext xpContext { get; protected set; }
        /// <summary>
        /// The wall top zone.
        /// </summary>
        public VirtualWallTopZone wallTopZone { get; protected set; }
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
        /// The state of the XPSynchronizer.
        /// </summary>
        public XPState state { get; protected set; }

        public LogExperienceController logController { get; protected set; }

        public ExperienceActionController actionController { get; protected set; }

        public int randomSeed { get; private set; }

        protected XPState _stateOnActivation;

        public bool active
        {
            get
            {
                return state == XPState.Visible || state == XPState.Hidden;
            }
        }

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

        public void ResetExperiment()
        {
            PreReset();
            var previousState = state;
            state = XPState.Visible;
            if (onStateChange != null && previousState != state)
                onStateChange(state);
            logController.AddLog("Reset", xpContext, Log.LogType.Automatic);
            foreach (var element in elements)
            {
                element.xpElement.OnReset ();
            }
            PostReset();
        }


        /// <summary>
        /// To be called in case of success of the experiment.
        /// </summary>
        public void Success()
        {
            PreSuccess();
            XPState state = XPState.Success;
            if (onStateChange != null)
                onStateChange(state);
            if (onEnd != null)
                onEnd(this);
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
            state = XPState.Failure;
            if (onStateChange != null)
                onStateChange(state);
            if (onEnd != null)
                onEnd(this);
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
            state = _stateOnActivation;
            if (onStateChange != null)
                onStateChange(state);
            if (onActivation != null)
                onActivation(this);
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

        /// <summary>
        /// To be called to pause the experiment during the game.
        /// </summary>
        public void Hide()
        {
            PreHide();
            if (this.wallTopZone != null)
                wallTopZone.CleanAll();
            state = XPState.Hidden;
            if (onStateChange != null)
                onStateChange(state);
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
        public void Show(VirtualWallTopZone wallTopZone)
        {
            PreShow(wallTopZone, elements.ToArray());
            this.wallTopZone = wallTopZone;
            wallTopZone.Place(xpContext.xpWallTopZone, xpContext);
            state = XPState.Visible;
            if (onStateChange != null)
                onStateChange(state);
            logController.AddLog("Show", xpContext, Log.LogType.Automatic);
            foreach (var element in elements)
            {
                element.xpElement.OnShow();
            }
            PostShow(wallTopZone, elements.ToArray());
        }

        /// <summary>
        /// Called before the state changes to the visible state.
        /// </summary>
        protected virtual void PreShow(VirtualWallTopZone wallTopZone, ElementInfo[] info) { }
        /// <summary>
        /// Called after the state changes to the visible state.
        /// </summary>
        protected virtual void PostShow(VirtualWallTopZone wallTopZone, ElementInfo[] info) { }

        protected virtual ElementInfo[] InitZone(VirtualZone zone)
        {
            var res = zone.InitAll().Select(xpElement => new ElementInfo(xpElement, xpElement.virtualElement, zone));
            elements.AddRange(res);
            return res.ToArray();
        }

        protected virtual ElementInfo[] InitZones(VirtualZone[] zones)
        {
            var res = zones.SelectMany(zone => InitZone(zone));
            return res.ToArray();
        }

        public void Init(XPContext xpContext, VirtualZone[] zones, LogExperienceController logController, int randomSeed, XPState stateOnActivation = XPState.Hidden)
        {
            PreInit(xpContext, logController, randomSeed, stateOnActivation);
            this.xpContext = xpContext;
            this.randomSeed = randomSeed;
            state = XPState.Inactive;
            _stateOnActivation = stateOnActivation;
            actionController = new ExperienceActionController(this);
            this.logController = logController;
            if (logController != null)
                logController.AddLog("Ready", xpContext, Log.LogType.Automatic);
            ElementInfo[] zoneInfo = InitZones(zones);
            foreach (var element in elements)
                element.xpElement.OnInit(this, randomSeed);
            PostInit(xpContext, zoneInfo, logController, randomSeed, stateOnActivation);
        }

        /// <summary>
        /// Called before the initialization.
        /// </summary>
        protected virtual void PreInit(XPContext xpContext, LogExperienceController logController, int randomSeed, XPState stateOnActivation) { }
        /// <summary>
        /// Called after the initialization.
        /// </summary>
        protected virtual void PostInit(XPContext xpContext, ElementInfo[] info, LogExperienceController logController, int randomSeed, XPState stateOnActivation) { }

        public virtual void SkipToStep(int step)
        {
            logController.AddLog(string.Format("Skip to step {0}", step), xpContext, Log.LogType.Automatic);
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

        internal void PlayableSound(AudioSource sound)
        {
            throw new NotImplementedException();
        }
    }
}
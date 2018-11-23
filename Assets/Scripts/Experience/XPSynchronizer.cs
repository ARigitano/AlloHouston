using CRI.HelloHouston.Calibration;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    /// <summary>
    /// The XpSynchronizer is responsible for the communication of every prefabs of one particular experiment among themselves as well as with the Gamecontroller.
    /// </summary>
    [System.Serializable]
    public abstract class XPSynchronizer : ScriptableObject
    {
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

        public bool active { get; protected set; }

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
        public virtual void OnResolved()
        {
            foreach (var element in elements)
            {
                element.OnResolved();
            }
        }

        /// <summary>
        /// To be called in case of failure of the experiment.
        /// </summary>
        public virtual void OnFailed()
        {
            foreach (var element in elements)
            {
                element.OnFailed();
            }
        }
        /// <summary>
        /// To be called to activate the incident of the experiment.
        /// </summary>
        public virtual void OnActivated()
        {
            active = true;
            foreach (var element in elements)
            {
                element.OnActivated();
            }
        }

        /// <summary>
        /// To be called to pause the experiment during the game.
        /// </summary>
        public virtual void OnPause()
        {
            active = false;
            foreach (var element in elements)
            {
                element.OnPause();
            }
        }

        /// <summary>
        /// To be called to call back the experiment after it has been paused.
        /// </summary>
        public virtual void OnUnpause()
        {
            foreach (var element in elements)
            {
                element.OnUnpause();
            }
        }

        public virtual void Init(XPContext xpContext)
        {
            this.xpContext = xpContext;
        }
    }
}
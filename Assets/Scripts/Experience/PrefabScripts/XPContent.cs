using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    public abstract class XPContent : MonoBehaviour
    {
        /// <summary>
        /// The experience synchronizer.
        /// </summary>
        protected XPSynchronizer _xpSynchronizer;

        public string index;

        public virtual void Init(XPSynchronizer xpSynchronizer)
        {
            _xpSynchronizer = xpSynchronizer;
        }

        public abstract void OnResolved();

        public abstract void OnFailed();

        public abstract void OnActivated();

        public abstract void OnPause();

        public abstract void OnUnpause();
    }
}

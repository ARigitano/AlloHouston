using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    public abstract class XPElement : MonoBehaviour
    {
        /// <summary>
        /// The experience synchronizer.
        /// </summary>
        public XPMainSynchronizer synchronizer { get; protected set; }
        /// <summary>
        /// The element name
        /// </summary>
        [SerializeField]
        [Tooltip("The element name.")]
        private string _elementName;
        /// <summary>
        /// The element name.
        /// </summary>
        public string elementName
        {
            get
            {
                return _elementName;
            }
        }

        public virtual void Init(XPMainSynchronizer synchronizer)
        {
            this.synchronizer = synchronizer;
        }

        public abstract void OnResolved();

        public abstract void OnFailed();

        public abstract void OnActivated();

        public abstract void OnPause();

        public abstract void OnUnpause();
    }
}

using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    public abstract class XPElement : MonoBehaviour
    {
        /// <summary>
        /// The experience synchronizer.
        /// </summary>
        public XPSynchronizer synchronizer { get; protected set; }
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

        public virtual void Init(XPSynchronizer synchronizer)
        {
            this.synchronizer = synchronizer;
        }

        public abstract void OnSuccess();

        public abstract void OnFailure();

        public abstract void OnActivation();

        public abstract void OnHide();

        public abstract void OnShow();
    }
}

using CRI.HelloHouston.Calibration;
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
        /// The virtual element.
        /// </summary>
        public VirtualElement virtualElement { get; protected set; }
        /// <summary>
        /// The element's type.
        /// </summary>
        public ElementType elementType { get; protected set; }
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

        public virtual void Init(XPSynchronizer synchronizer, VirtualElement virtualElement)
        {
            this.synchronizer = synchronizer;
            this.virtualElement = virtualElement;
            elementType = virtualElement.elementType;
        }

        public virtual void Dismiss()
        {
            Destroy(gameObject);
        }

        public virtual void OnSuccess() { }

        public virtual void OnFailure() { }

        public virtual void OnActivation() { }

        public virtual void OnHide() { }

        public virtual void OnShow() { }
    }
}

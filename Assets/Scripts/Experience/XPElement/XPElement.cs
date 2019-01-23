using CRI.HelloHouston.Calibration;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    public abstract class XPElement : MonoBehaviour
    {
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

        public virtual void Init(VirtualElement virtualElement)
        {
            this.virtualElement = virtualElement;
            elementType = virtualElement.elementType;
        }

        public virtual void Dismiss()
        {
            Destroy(gameObject);
        }

        public virtual void OnSuccess() { }

        public virtual void OnFailure() { }

        public virtual void OnActivation(XPManager synchronizer) { }

        public virtual void OnHide() { }

        public virtual void OnShow() { }
    }
}

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
        [Header("XPElement Attributes")]
        /// <summary>
        /// The element name
        /// </summary>
        [SerializeField]
        [Tooltip("The element name.")]
        private string _elementName;
        /// <summary>
        /// If true, this element will be put on a Canvas.
        /// </summary>
        [SerializeField]
        [Tooltip("If true, this element will be put on a Canvas.")]
        private bool _canvasElement = true;

        public bool canvasElement
        {
            get
            {
                return _canvasElement;
            }
        }
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

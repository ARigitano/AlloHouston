using CRI.HelloHouston.Calibration;

namespace CRI.HelloHouston.Experience
{
    public class XPHologramElement : XPElement
    {
        /// <summary>
        /// If true, the Hologram Element will call the Show method whenever it becomes visible. If false, no method will be called.
        /// </summary>
        public bool visible
        {
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;
                if (_visible && hologramZone != null && hologramZone.visible)
                    Show();
                else if (!_visible)
                    Hide();
            }
        }

        private bool _visible;

        public VirtualHologramZone hologramZone;

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}

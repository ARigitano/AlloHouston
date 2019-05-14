using System;
using CRI.HelloHouston.Experience;
using CRI.HelloHouston.GameElement;
using UnityEngine;

namespace CRI.HelloHouston.Calibration
{
    public class VirtualHologramZone : VirtualZone, IHologram
    {
        public override ZoneType zoneType
        {
            get
            {
                return ZoneType.Hologram;
            }
        }

        public override bool switchableZone
        {
            get
            {
                return true;
            }
        }

        public override VirtualElement[] virtualElements
        {
            get
            {
                return virtualHologramElements;
            }
        }

        public override XPZone xpZone
        {
            get
            {
                return null;
            }
        }
        /// <summary>
        /// The index of the hologram zone.
        /// </summary>
        [HideInInspector]
        [Tooltip("The index of the hologram zone.")]
        public int index;
        [SerializeField]
        private VirtualHologramElement _elementPrefab = null;

        public bool visible
        {
            get
            {
                return _visible;
            }
        }
        private bool _visible;

        private void ClearHolograms()
        {
            for (int i = 0; virtualHologramElements != null && i < virtualHologramElements.Length; i++)
            {
                Destroy(virtualHologramElements[i].gameObject);
                virtualHologramElements[i] = null;
            }
        }

        public virtual void ShowHologram()
        {
            _visible = true;
            for (int i = 0; i < virtualHologramElements.Length; i++)
            {
                var element = (XPHologramElement)virtualElements[i].currentElement;
                if (element != null && element.visible)
                    element.Show();
            }
        }

        public virtual void HideHologram()
        {
            _visible = false;
            for (int i = 0; i < virtualHologramElements.Length; i++)
            {
                var element = (XPHologramElement)virtualElements[i].currentElement;
                if (element != null && element.visible)
                    element.Hide();
            }
        }

        protected override void AddXPZone(XPZone xpZone, XPContext xpContext)
        {
            if (xpZone == null)
                return;
            var hologramZone = xpZone as XPHologramZone;
            if (!hologramZone)
                throw new WrongZoneTypeException();
            this.hologramZone = hologramZone;
            int length = hologramZone.elementPrefabs.Length;
            ClearHolograms();
            virtualHologramElements = new VirtualHologramElement[hologramZone.elementPrefabs.Length];
            for (int i = 0; i < length; i++)
            {
                VirtualHologramElement ve = Instantiate(_elementPrefab, transform);
                ve.PlaceObject(hologramZone.elementPrefabs[i], xpContext);
                if (ve.currentElement != null && ve.currentElement is XPHologramElement)
                    ((XPHologramElement)ve.currentElement).hologramZone = this;
                virtualHologramElements[i] = ve;
            }
        }

        public VirtualHologramElement[] virtualHologramElements { get; protected set; }
        public XPHologramZone hologramZone { get; protected set; }
    }
}

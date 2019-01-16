using CRI.HelloHouston.Experience;
using UnityEngine;

namespace CRI.HelloHouston.Calibration
{
    public class VirtualHologramZone : VirtualZone
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
                return false;
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

        [SerializeField]
        private VirtualHologramElement _elementPrefab;

        private void ClearHolograms()
        {
            for (int i = 0; virtualHologramElements != null && i < virtualHologramElements.Length; i++)
            {
                Destroy(virtualHologramElements[i].gameObject);
                virtualHologramElements[i] = null;
            }
        }

        protected override void AddXPZone(XPZone xpZone, XPContext xpContext)
        {
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
                virtualHologramElements[i] = ve;
            }
        }

        public VirtualHologramElement[] virtualHologramElements { get; protected set; }
        public XPHologramZone hologramZone { get; protected set; }
    }
}

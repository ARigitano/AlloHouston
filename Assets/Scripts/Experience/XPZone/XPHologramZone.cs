using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    [CreateAssetMenu(fileName = "New HologramZone", menuName = "Experience/Zone/HologramZone", order = 1)]
    public class XPHologramZone : XPZone<XPHologramSynchronizer>
    {
        public override XPElement[] elementPrefabs
        {
            get
            {
                return elementPrefabs;
            }
        }
        /// <summary>
        /// The holograms.
        /// </summary>
        [SerializeField]
        [Tooltip("The holograms.")]
        public XPHologramElement[] hologramElementPrefabs;

        public override ZoneType GetZoneType()
        {
            return ZoneType.Hologram;
        }
    }
}


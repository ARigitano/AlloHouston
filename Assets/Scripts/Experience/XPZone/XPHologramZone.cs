﻿using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    [CreateAssetMenu(fileName = "New HologramZone", menuName = "Experience/Zone/HologramZone", order = 1)]
    public class XPHologramZone : XPZone
    {
        public override XPElement[] elementPrefabs
        {
            get
            {
                return hologramElementPrefabs;
            }
        }
        /// <summary>
        /// The holograms.
        /// </summary>
        [SerializeField]
        [Tooltip("The holograms.")]
        public XPHologramElement[] hologramElementPrefabs;

        public override ZoneType zoneType
        {
            get
            {
                return ZoneType.Hologram;
            }
        }
    }
}


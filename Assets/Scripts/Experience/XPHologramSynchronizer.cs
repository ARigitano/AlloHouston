using CRI.HelloHouston.Calibration;
using System;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    [Serializable]
    public class XPHologramSynchronizer : XPSynchronizer
    {
        public override XPContent[] contents
        {
            get
            {
                return hologramContent;
            }
        }
        /// <summary>
        /// The holograms.
        /// </summary>
        [SerializeField]
        [Tooltip("The holograms.")]
        public XPContent[] hologramContent;

        public void Init(HologramZoneContent zoneContent, VirtualHologramZone virtualZone)
        {
            hologramContent = new XPContent[zoneContent.hologramContentPrefabs.Length];
            for (int i = 0; i < hologramContent.Length; i++)
            {
                if (hologramContent[i] != null)
                {
                    hologramContent[i] = Instantiate(zoneContent.hologramContentPrefabs[i]);
                    virtualZone.hologramPlaceholder.PlaceObject(hologramContent[i]);
                }
            }
        }
    }
}
using CRI.HelloHouston.Calibration;
using System;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    [Serializable]
    public class XPHologramSynchronizer : XPSynchronizer
    {
        public override XPElement[] contents
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
        public XPElement[] hologramContent;

        public void Init(XPHologramZone zoneContent, VirtualHologramZone virtualZone)
        {
            hologramContent = new XPElement[zoneContent.hologramElementPrefabs.Length];
            for (int i = 0; i < hologramContent.Length; i++)
            {
                if (hologramContent[i] != null)
                {
                    hologramContent[i] = Instantiate(zoneContent.hologramElementPrefabs[i]);
                    virtualZone.hologramVirtualElement.PlaceObject(hologramContent[i]);
                }
            }
        }
    }
}
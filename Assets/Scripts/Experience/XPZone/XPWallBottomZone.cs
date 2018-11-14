using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    [CreateAssetMenu(fileName = "New WallBottomZone", menuName = "Experience/Zone/WallBottom", order = 1)]
    public class XPWallBottomZone : XPZone<XPWallBottomSynchronizer>
    {
        public override XPElement[] elementPrefabs
        {
            get
            {
                if (element != null)
                    return new XPElement[] { element };
                return new XPElement[0];
            }
        }
        /// <summary>
        /// The left side of the wall top.
        /// </summary>
        [SerializeField]
        [Tooltip("The left side of the wall top.")]
        public XPElement element;

        public override ZoneType GetZoneType()
        {
            return ZoneType.WallBottom;
        }
    }
}

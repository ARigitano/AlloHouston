using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    [CreateAssetMenu(fileName = "New CornerZone", menuName = "Experience/Zone/CornerZone", order = 1)]
    public class XPCornerZone : XPZone<XPCornerSynchronizer>
    {
        public override XPElement[] elementPrefabs
        {
            get
            {
                if (cornerContentPrefab != null)
                    return new XPElement[] { cornerContentPrefab };
                return new XPElement[0];
            }
        }
        /// <summary>
        /// The corner content.
        /// </summary>
        [SerializeField]
        [Tooltip("The corner content.")]
        public XPElement cornerContentPrefab;

        public override ZoneType GetZoneType()
        {
            return ZoneType.Corner;
        }
    }
}

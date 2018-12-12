using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    [CreateAssetMenu(fileName = "New WallTopZone", menuName = "Experience/Zone/WallTop", order = 1)]
    public class XPWallTopZone : XPZone
    {
        public override XPElement[] elementPrefabs
        {
            get
            {
                var list = new List<XPElement>();
                if (elementLeftPrefab != null)
                    list.Add(elementLeftPrefab);
                if (elementRightPrefab != null)
                    list.Add(elementRightPrefab);
                if (elementTabletPrefab != null)
                    list.Add(elementTabletPrefab);
                return list.ToArray();
            }
        }
        /// <summary>
        /// The left side of the wall top.
        /// </summary>
        [SerializeField]
        [Tooltip("The left side of the wall top.")]
        public XPElement elementLeftPrefab;
        /// <summary>
        /// The right side of the wall top.
        /// </summary>
        [SerializeField]
        [Tooltip("The right side of the wall top.")]
        public XPElement elementRightPrefab;
        /// <summary>
        /// The tablet of the wall top.
        /// </summary>
        [SerializeField]
        [Tooltip("The tablet of the wall top.")]
        public XPElement elementTabletPrefab;

        public override ZoneType zoneType
        {
            get
            {
                return ZoneType.WallTop;
            }
        }
    }
}

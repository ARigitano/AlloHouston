using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    [CreateAssetMenu(fileName = "New CornerZone", menuName = "Experience/Zone/CornerZone", order = 1)]
    public class XPCornerZone : XPZone
    {
        public override XPElement[] elementPrefabs
        {
            get
            {
                if (cornerElementPrefab != null)
                    return new XPElement[] { cornerElementPrefab };
                return new XPElement[0];
            }
        }
        /// <summary>
        /// The corner content.
        /// </summary>
        [SerializeField]
        [Tooltip("The corner content.")]
        public XPElement cornerElementPrefab;

        public override ZoneType zoneType
        {
            get
            {
                return ZoneType.Corner;
            }
        }
    }
}

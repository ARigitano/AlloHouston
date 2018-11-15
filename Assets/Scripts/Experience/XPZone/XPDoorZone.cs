using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    [CreateAssetMenu(fileName = "New DoorZone", menuName = "Experience/Zone/DoorZone", order = 1)]
    public class XPDoorZone : XPZone
    {
        public override XPElement[] elementPrefabs
        {
            get
            {
                if (doorContentPrefab != null)
                    return new XPElement[] { doorContentPrefab };
                return new XPElement[0];
            }
        }
        /// <summary>
        /// The door content.
        /// </summary>
        [SerializeField]
        [Tooltip("The door content.")]
        public XPElement doorContentPrefab;

        public override ZoneType GetZoneType()
        {
            return ZoneType.Door;
        }
    }
}

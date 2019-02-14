using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    [System.Serializable]
    public abstract class XPZone : ScriptableObject
    {
        public abstract ZoneType zoneType { get; }
        public abstract XPElement[] elementPrefabs
        {
            get;
        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    [System.Serializable]
    public abstract class XPZone<T> : ScriptableObject where T : XPSynchronizer, new()
    {
        public abstract ZoneType GetZoneType();
        public T synchronizer;
        public abstract XPElement[] elementPrefabs
        {
            get;
        }
    }
}


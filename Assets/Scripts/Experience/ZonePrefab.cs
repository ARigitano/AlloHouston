using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    [System.Serializable]
    public class WallTopZoneContent : ZoneContent<XPWallTopSynchronizer>
    {
        public override XPContent[] contentPrefabs
        {
            get
            {
                var list = new List<XPContent>();
                if (contentLeftPrefab != null)
                    list.Add(contentLeftPrefab);
                if (contentRightPrefab != null)
                    list.Add(contentRightPrefab);
                if (contentTabletPrefab != null)
                    list.Add(contentTabletPrefab);
                return list.ToArray();
            }
        }
        /// <summary>
        /// The left side of the wall top.
        /// </summary>
        [SerializeField]
        [Tooltip("The left side of the wall top.")]
        public XPContent contentLeftPrefab;
        /// <summary>
        /// The right side of the wall top.
        /// </summary>
        [SerializeField]
        [Tooltip("The right side of the wall top.")]
        public XPContent contentRightPrefab;
        /// <summary>
        /// The tablet of the wall top.
        /// </summary>
        [SerializeField]
        [Tooltip("The tablet of the wall top.")]
        public XPContent contentTabletPrefab;

        public override ZoneType GetZoneType()
        {
            return ZoneType.WallTop;
        }
    }

    [System.Serializable]
    public class WallBottomZoneContent : ZoneContent<XPWallBottomSynchronizer>
    {
        public override XPContent[] contentPrefabs
        {
            get
            {
                var list = new List<XPContent>();
                if (contentLeftPrefab != null)
                    list.Add(contentLeftPrefab);
                if (contentRightPrefab != null)
                    list.Add(contentRightPrefab);
                return list.ToArray();
            }
        }
        /// <summary>
        /// The left side of the wall top.
        /// </summary>
        [SerializeField]
        [Tooltip("The left side of the wall top.")]
        public XPContent contentLeftPrefab;
        /// <summary>
        /// The right side of the wall top.
        /// </summary>
        [SerializeField]
        [Tooltip("The right side of the wall top.")]
        public XPContent contentRightPrefab;

        public override ZoneType GetZoneType()
        {
            return ZoneType.WallBottom;
        }
    }

    [System.Serializable]
    public class HologramZoneContent : ZoneContent<XPHologramSynchronizer>
    {
        public override XPContent[] contentPrefabs
        {
            get
            {
                return contentPrefabs;
            }
        }
        /// <summary>
        /// The holograms.
        /// </summary>
        [SerializeField]
        [Tooltip("The holograms.")]
        public XPContent[] hologramContentPrefabs;

        public override ZoneType GetZoneType()
        {
            return ZoneType.Hologram;
        }
    }

    [System.Serializable]
    public class CornerZoneContent : ZoneContent<XPCornerSynchronizer>
    {
        public override XPContent[] contentPrefabs
        {
            get
            {
                if (cornerContentPrefab != null)
                    return new XPContent[] { cornerContentPrefab };
                return new XPContent[0];
            }
        }
        /// <summary>
        /// The corner content.
        /// </summary>
        [SerializeField]
        [Tooltip("The corner content.")]
        public XPContent cornerContentPrefab;

        public override ZoneType GetZoneType()
        {
            return ZoneType.Corner;
        }
    }

    [System.Serializable]
    public class DoorZoneContent : ZoneContent<XPDoorSynchronizer>
    {
        public override XPContent[] contentPrefabs
        {
            get
            {
                if (doorContentPrefab != null)
                    return new XPContent[] { doorContentPrefab };
                return new XPContent[0];
            }
        }
        /// <summary>
        /// The door content.
        /// </summary>
        [SerializeField]
        [Tooltip("The door content.")]
        public XPContent doorContentPrefab;

        public override ZoneType GetZoneType()
        {
            return ZoneType.Door;
        }
    }

    [System.Serializable]
    public abstract class ZoneContent<T> where T : XPSynchronizer, new()
    {
        public abstract ZoneType GetZoneType();
        public T synchronizer;
        public abstract XPContent[] contentPrefabs
        {
            get;
        }
    }
}


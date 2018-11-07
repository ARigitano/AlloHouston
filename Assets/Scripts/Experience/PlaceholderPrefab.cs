using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    [System.Serializable]
    public class WallTopZonePrefab : ZonePrefab
    {
        public GameObject placeholderLeft;
        public GameObject placeholderRight;
        public GameObject placeholderTablet;


        public override List<GameObject> placeholders
        {
            get
            {
                var list = new List<GameObject>();
                if (placeholderLeft != null)
                    list.Add(placeholderLeft);
                if (placeholderRight != null)
                    list.Add(placeholderRight);
                if (placeholderTablet != null)
                    list.Add(placeholderTablet);
                return list;
            }
        }

        public override ZoneType GetZoneType()
        {
            return ZoneType.WallTop;
        }
    }

    [System.Serializable]
    public class WallBottomZonePrefab : ZonePrefab
    {
        public GameObject placeholderLeft;
        public GameObject placeholderRight;

        public override List<GameObject> placeholders
        {
            get
            {
                var list = new List<GameObject>();
                if (placeholderLeft != null)
                    list.Add(placeholderLeft);
                if (placeholderRight != null)
                    list.Add(placeholderRight);
                return list;
            }
        }

        public override ZoneType GetZoneType()
        {
            return ZoneType.WallBottom;
        }
    }

    [System.Serializable]
    public class HologramZonePrefab : ZonePrefab
    {
        public GameObject[] hologramPrefabs;

        public override List<GameObject> placeholders
        {
            get
            {
                return hologramPrefabs.ToList();
            }
        }

        public override ZoneType GetZoneType()
        {
            return ZoneType.Hologram;
        }
    }

    [System.Serializable]
    public class CornerZonePrefab : ZonePrefab
    {
        public GameObject screenPrefab;

        public override List<GameObject> placeholders
        {
            get
            {
                var list = new List<GameObject>();
                if (screenPrefab != null)
                    list.Add(screenPrefab);
                return list;
            }
        }

        public override ZoneType GetZoneType()
        {
            return ZoneType.Corner;
        }
    }

    [System.Serializable]
    public class DoorZonePrefab : ZonePrefab
    {
        public GameObject doorPrefab;

        public override List<GameObject> placeholders
        {
            get
            {
                var list = new List<GameObject>();
                if (doorPrefab != null)
                    list.Add(doorPrefab);
                return list;
            }
        }

        public override ZoneType GetZoneType()
        {
            return ZoneType.Door;
        }
    }

    [System.Serializable]
    public abstract class ZonePrefab
    {
        public abstract ZoneType GetZoneType();
        public abstract List<GameObject> placeholders { get; }
    }
}


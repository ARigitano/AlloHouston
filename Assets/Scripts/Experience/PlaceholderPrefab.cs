﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience
{
    [System.Serializable]
    public class WallTopZonePrefab : ZonePrefab
    {
        public GameObject placeholderLeft;
        public GameObject placeholderRight;
        public GameObject placeholderTablet;

        public override ZoneType GetZoneType()
        {
            return ZoneType.WallTop;
        }
    }

    [System.Serializable]
    public class BottomPlaceholder
    {
        public GameObject placeholderLeft;
        public GameObject placeholderRight;
    }

    [System.Serializable]
    public class WallBottomZonePrefab : ZonePrefab
    {
        public List<BottomPlaceholder> bottomPlaceholders;

        public override ZoneType GetZoneType()
        {
            return ZoneType.WallBottom;
        }
    }

    [System.Serializable]
    public class HologramZonePrefab : ZonePrefab
    {
        public GameObject[] hologramPrefabs;

        public override ZoneType GetZoneType()
        {
            return ZoneType.Hologram;
        }
    }

    [System.Serializable]
    public class CornerPlaceholder
    {
        public GameObject screenPrefab;
    }

    [System.Serializable]
    public class CornerZonePrefab : ZonePrefab
    {
        public List<CornerPlaceholder> cornerPlaceholders;

        public override ZoneType GetZoneType()
        {
            return ZoneType.Corner;
        }
    }

    [System.Serializable]
    public class DoorZonePrefab : ZonePrefab
    {
        public GameObject doorPrefab;

        public override ZoneType GetZoneType()
        {
            return ZoneType.Door;
        }
    }

    [System.Serializable]
    public abstract class ZonePrefab
    {
        public abstract ZoneType GetZoneType();
    }
}


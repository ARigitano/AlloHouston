using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WallTopZonePrefab : ZonePrefab
{
    public GameObject placeholderLeft;
    public GameObject placeholderRight;
    public GameObject placeholderTablet;

    public override ZoneType GetType()
    {
        return ZoneType.WallTop;
    }
}

[System.Serializable]
public class WallBottomZonePrefab : ZonePrefab
{
    public GameObject placeholderLeft;
    public GameObject placeholderRight;

    public override ZoneType GetType()
    {
        return ZoneType.WallBottom;
    }
}

[System.Serializable]
public class HologramZonePrefab : ZonePrefab
{
    public GameObject[] hologramPrefabs;

    public override ZoneType GetType()
    {
        return ZoneType.Hologram;
    }
}

[System.Serializable]
public class CornerZonePrefab : ZonePrefab
{
    public GameObject screenPrefab;

    public override ZoneType GetType()
    {
        return ZoneType.Corner;
    }
}

[System.Serializable]
public class DoorZonePrefab : ZonePrefab
{
    public GameObject doorPrefab;

    public override ZoneType GetType()
    {
        throw new System.NotImplementedException();
    }
}

[System.Serializable]
public abstract class ZonePrefab
{
    public abstract ZoneType GetType();


}


using System;
using System.Collections;
using System.Collections.Generic;
using CRI.HelloHouston.Experience;
using UnityEngine;
using UnityEngine.UI;
using CRI.HelloHouston.Calibration;

public class UIExperienceTotalPanel : MonoBehaviour {
    [SerializeField]
    private Text _totalWallTopText = null;
    [SerializeField]
    private Text _totalWallBottomText = null;
    [SerializeField]
    private Text _totalCornerText = null;
    [SerializeField]
    private Text _totalDoorText = null;
    [SerializeField]
    private Text _totalHologramText = null;
    [SerializeField]
    private Text _totalDurationText = null;

    public VirtualRoom virtualRoom = null;

    private Dictionary<int, XPContext> _contextTable = new Dictionary<int, XPContext>();

    public bool overflow { get; private set; }

    public void UpdateValues()
    {
        int totalWallTop = 0;
        int totalWallBottom = 0;
        int totalCorner = 0;
        int totalDoor = 0;
        int totalHologram = 0;
        int totalDuration = 0;
        foreach (var context in _contextTable.Values)
        {
            if (context != null)
            {
                totalWallTop += context.totalWallTop;
                totalWallBottom += context.totalWallBottom;
                totalCorner += context.totalCorners;
                totalDoor += context.totalDoors;
                totalHologram += context.totalHolograms;
                totalDuration += context.duration;
            }
        }
        int roomWallTop = virtualRoom.GetZones(ZoneType.WallTop).Length;
        int roomWallBottom = virtualRoom.GetZones(ZoneType.WallBottom).Length;
        int roomCorner = virtualRoom.GetZones(ZoneType.Corner).Length;
        int roomDoor = virtualRoom.GetZones(ZoneType.Door).Length;
        SetText(_totalWallTopText, totalWallTop, roomWallTop);
        SetText(_totalWallBottomText, totalWallBottom, roomWallBottom);
        SetText(_totalCornerText, totalCorner, roomCorner);
        SetText(_totalDoorText, totalDoor, roomDoor);
        SetText(_totalHologramText, totalHologram);
        SetText(_totalDurationText, totalDuration);
        overflow = totalWallBottom > roomWallBottom || totalDoor > roomDoor || totalCorner > roomCorner;
    }

    /// <summary>
    /// Sets a text of the zone totals.
    /// </summary>
    /// <param name="text">A text</param>
    /// <param name="zoneNumber">The number of zones in the experience.</param>
    /// <param name="roomNumber">The number of zones offered by the room.</param>
    private void SetText(Text text,  int zoneNumber, int roomNumber)
    {
        text.text = string.Format("{0} / {1}", zoneNumber, roomNumber);
        text.fontStyle = zoneNumber > roomNumber ? FontStyle.BoldAndItalic : FontStyle.Normal;
    }
    /// <summary>
    /// Sets a text of the zone totals.
    /// </summary>
    /// <param name="text">A text</param>
    /// <param name="zoneNumber">The number of zones in the experience.</param>
    private void SetText(Text text, int numberPlaceholder)
    {
        text.text = numberPlaceholder.ToString();
    }
    /// <summary>
    /// Removes a context.
    /// </summary>
    /// <param name="id">The context id.</param>
    public void RemoveContext(int id)
    {
        _contextTable.Remove(id);
        UpdateValues();
    }
    /// <summary>
    /// Adds a context at an id.
    /// </summary>
    /// <param name="id">The context id.</param>
    public void AddContext(int id)
    {
        _contextTable.Add(id, null);
    }
    /// <summary>
    /// Set the context for the id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="currentContext"></param>
    public void SetContext(int id, XPContext currentContext)
    {
        _contextTable[id] = currentContext;
        UpdateValues();
    }
}

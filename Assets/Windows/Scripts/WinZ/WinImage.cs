using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinImage : LegacyWindow
{
    public Image image;

    public void SetImage(Sprite tmpImg)
    {
        image.sprite = tmpImg;
    }
}

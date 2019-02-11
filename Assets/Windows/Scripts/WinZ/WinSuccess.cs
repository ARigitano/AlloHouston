using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinSuccess : Window
{
    public Text txtTitle;
    public Text txtMessage;

    public void SetTitle(string tmpTitle)
    {
        txtTitle.text = tmpTitle;
    }

    public void SetMessage(string tmpMsg)
    {
        txtMessage.text = tmpMsg;
    }
}

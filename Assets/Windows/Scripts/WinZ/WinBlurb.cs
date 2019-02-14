using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinBlurb : Window
{
    public Text txtMessage;

    public void SetMessage(string tmpMsg)
    {
        txtMessage.text = tmpMsg;
    }
}

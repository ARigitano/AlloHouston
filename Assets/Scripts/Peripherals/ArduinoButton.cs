// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoButton : MonoBehaviour
{
    public static SerialPort sp = new SerialPort("COM9", 9600);

    // Use this for initialization
    private void Start()
    {
        if (sp != null)
        {
            if (sp.IsOpen)
            {
                sp.Close();
                print("Closing port, because it was already open!");
            }
            else
            {
                sp.Open();  // opens the connection
                sp.ReadTimeout = 500;  // sets the timeout value before reporting error
                print("Port Opened!");
                //		message = "Port Opened!";
            }
        }
        else
        {
            if (sp.IsOpen)
            {
                print("Port is already open");
            }
            else
            {
                print("Port == null");
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (sp.IsOpen)
        {
            try
            {
                Debug.Log("ttata");
                //ButtonAction(sp.ReadByte());
                print(sp.ReadByte());
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }

    private void ButtonAction(int signal)
    {
        Debug.Log("ArDUINO");
    }
}

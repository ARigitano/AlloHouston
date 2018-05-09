using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoButton : MonoBehaviour {

	public static SerialPort sp = new SerialPort("COM9", 9600);

	// Use this for initialization
	void Start () {
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
	void Update () {

		if (sp.IsOpen) {

			try {
				Debug.Log("ttata");
				//ButtonAction(sp.ReadByte());
				print(sp.ReadByte());
			} catch (System.Exception) {

				throw;
			}

		}
		
	}

	void ButtonAction(int signal) {
		Debug.Log ("ArDUINO");
	}
}

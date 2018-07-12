using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRCalibrationTool;
using UnityEngine.UI;

public class Experimentation : MonoBehaviour {

    public TextMesh _expNumber;             //Number of this instance of the experiment in the room
    public RoomManager _roomManager;
    public Room table;
    public GameObject textPrefab;           //Prefab of the error text for the table screen
    public GameObject panelToAttachTextTo;  //Errors panel of the table screen
    public int error;                       //Number of the error for this instance of the experiment
    public GameObject text;                //Error text displayed on the table screen
    public bool _fixed = false;
    public string _errorReference;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

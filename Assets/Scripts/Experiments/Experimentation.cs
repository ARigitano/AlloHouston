using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRCalibrationTool;
using UnityEngine.UI;

public class Experimentation : MonoBehaviour {

    public TextMesh _expNumber;             //Number of this instance of the experiment in the room
    protected RoomManager _roomManager;
    protected Room table;
    public GameObject textPrefab;           //Prefab of the error text for the table screen
    public GameObject panelToAttachTextTo;  //Errors panel of the table screen
    public int error;                       //Number of the error for this instance of the experiment
    protected GameObject text;                //Error text displayed on the table screen
    public bool _fixed = false;
    public string _errorReference;
    protected GameManager _gameManager;
    public AudioSource _success, _fail;

       protected void AttachPanel()
    {
        panelToAttachTextTo = table.canvas;
        text = (GameObject)Instantiate(textPrefab);
        text.transform.SetParent(panelToAttachTextTo.transform);
        text.transform.localRotation = Quaternion.identity;
        text.transform.position = panelToAttachTextTo.transform.position;
    }

    // Use this for initialization
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        table = GameObject.FindGameObjectWithTag("Table").GetComponent<Room>();
    }


	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Displays a status message depending of the outcome of the experiment
    /// </summary>
    /// <param name="index">Number chose by pressin a colored button</param>
    public void Resolved(int index)
    {
        if (index == error)
        {
            Debug.Log("Experiment solved");
            _success.Play();
            text.transform.GetComponent<Text>().text = _expNumber.text + ": Cleared";
            _expNumber.color = Color.green;
            _gameManager.incidentsFixed++;
            _gameManager.EndGame();
        }
        else
        {
            Debug.Log("Experiment failed");
            _fail.Play();
            text.transform.GetComponent<Text>().text = _expNumber.text + ": Failed";
            _expNumber.color = Color.red;
        }
    }
}

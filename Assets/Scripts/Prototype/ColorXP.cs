//Experiment in which the player must press the right color button


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRCalibrationTool;
using UnityEngine.UI;


public class ColorXP : Experimentation {

    /*public TextMesh _expNumber;             //Number of this instance of the experiment in the room
	private RoomManager _roomManager;
    private Room table;
    public GameObject textPrefab;           //Prefab of the error text for the table screen
    public GameObject panelToAttachTextTo;  //Errors panel of the table screen
    public int error;                       //Number of the error for this instance of the experiment
    private GameObject text;                //Error text displayed on the table screen*/
    private GameManager _gameManager;
    public AudioSource _success, _fail;

    // Use this for initialization
    void Start ()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        table = GameObject.FindGameObjectWithTag("Table").GetComponent<Room>();

        if (table != null && table.canvas)
        {
            panelToAttachTextTo = table.canvas;
            text = (GameObject)Instantiate(textPrefab);
            text.transform.SetParent(panelToAttachTextTo.transform);
            text.transform.localRotation = Quaternion.identity;
            text.transform.position = panelToAttachTextTo.transform.position;
            error = Random.Range(0, 3);

            switch(error)
            {

                    case 0:
                    _errorReference = "AB484";
                    break;
                    case 1:
                    _errorReference = "KJ208";
                    break;
                    case 2:
                    _errorReference = "CR101";
                    break;
                case 3:
                    _errorReference = "AH332";
                    break;
                default:
                    break;
            }

            text.transform.GetComponent<Text>().text = _expNumber.text +": Error "+_errorReference;
        }
    }

    /// <summary>
    /// Displays a status message depending of the outcome of the experiment
    /// </summary>
    /// <param name="index">Number chose by pressin a colored button</param>
    public void Resolved(int index)
    {
        if(index == error)
        {
            Debug.Log("Experiment solved");
            _success.Play();
            text.transform.GetComponent<Text>().text = _expNumber.text + ": Cleared";
            _expNumber.color = Color.green;
            _gameManager.incidentsFixed++;
            _gameManager.EndGame();
        } else
        {
            Debug.Log("Experiment failed");
            _fail.Play();
            text.transform.GetComponent<Text>().text = _expNumber.text + ": Failed";
            _expNumber.color = Color.red;
        }
    }
}

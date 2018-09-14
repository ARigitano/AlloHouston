using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRCalibrationTool;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Parent class of the experiments that the player will have to solve.
/// </summary>
public class Experimentation : MonoBehaviour {

    public TextMeshPro _expNumber;              //Number of this instance of the experiment in the room
    protected RoomManager _roomManager;     
    protected Room _table;
    public GameObject _textPrefab;              //Prefab of the error text for the table screen
    public GameObject _panelToAttachTextTo;     //Panel that displays error codes on top of the table screen
    public int _error;                          //Number of the error randomly chosen for this instance of the experiment
    protected GameObject _text;                 //Error text prefab attached to the table screen
    public bool _fixed = false;                 //True if an answer to the enigma has been offered, even if the answer is wrong
    public string _errorReference;              //Error code displayed on the table screen to help clear the experiment     
    protected GameManager _gameManager;
    public AudioSource _success, _fail;         //Sounds that play when an answer is given
    public string _errorText;                   //Answer that has to be given to clear this experiment

    /// <summary>
    /// Attach the error prefab to the canvas of the table screen
    /// </summary>
    protected void AttachPanel()
    {
        _panelToAttachTextTo = _table._canvas;
        _text = (GameObject)Instantiate(_textPrefab);
        _text.transform.SetParent(_panelToAttachTextTo.transform);
        _text.transform.localRotation = Quaternion.identity;
        _text.transform.position = _panelToAttachTextTo.transform.position;
    }

    /// <summary>
    /// Displays a status message depending of the outcome of the experiment
    /// </summary>
    /// <param name="inputValue">Answer given by the player in the attempt to clear the incident</param>
    public void Resolved(string inputValue)
    {
        if (inputValue == _errorText)
        {
            Debug.Log("Experiment solved");
            _success.Play();
            _text.transform.GetComponent<Text>().text = _expNumber.text + ": Cleared";
            _expNumber.color = Color.green;
            _gameManager._incidentsFixed++;
            _gameManager.EndGame();
        }
        else
        {
            Debug.Log("Experiment failed");
            _fail.Play();
            _text.transform.GetComponent<Text>().text = _expNumber.text + ": Failed";
            _expNumber.color = Color.red;
        }
    }
}

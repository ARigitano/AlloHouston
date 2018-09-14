using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Manages the different steps of the game
/// </summary>
public class GameManager : MonoBehaviour
{
    public int _incidentsFixed = 0;                  //Number of experiments that have been solved
    [SerializeField] public TextMeshPro _tableText; //Table screen 
    public bool _gameStarted = false;        

    /// <summary>
    /// Events at the beginning of the calibration
    /// </summary>
    public void StartCalibration()
    {

	}

    /// <summary>
    /// Events at the end of the calibration
    /// </summary>
    public void EndCalibration()
    {
        GameObject[] calibrationPoints = GameObject.FindGameObjectsWithTag("PositionTag");

        foreach(GameObject calibrationPoint in calibrationPoints)
        {
            calibrationPoint.GetComponent<MeshRenderer>().enabled = false;
        }

        GameObject[] viveTrackers = GameObject.FindGameObjectsWithTag("ViveTracker");

        foreach (GameObject viveTracker in viveTrackers)
        {
            viveTracker.GetComponent<MeshRenderer>().enabled = false;
        }

        _gameStarted = true;
    }

    /// <summary>
    /// Events at the beginning of the game
    /// </summary>
    public void StartGame()
    {

	}

    /// <summary>
    /// Events at the end of the game
    /// </summary>
    public void EndGame()
    {
        if (_incidentsFixed == 3)
        {
            GameObject[] errors = GameObject.FindGameObjectsWithTag("Error");
            foreach(GameObject error in errors)
            {
                Destroy(error);
            }

            _tableText.text = "All incidents have\nbeen cleared.";
            Debug.Log("End of the game");
        }
    }
}

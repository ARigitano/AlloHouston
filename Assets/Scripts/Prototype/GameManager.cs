//Manages the different steps of the game

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int incidentsFixed = 0;                  //Number of experiments that have been solved
    [SerializeField] public TextMesh _tableText;    //Table screen 

    // Use this for initialization
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
    }

    /*public void StartCalibration() {

	}*/

    public void EndCalibration() {
        GameObject[] calibrationPoints = GameObject.FindGameObjectsWithTag("PositionTag");

        foreach(GameObject calibrationPoint in calibrationPoints)
        {
            calibrationPoint.GetComponent<MeshRenderer>().enabled = false;
        }
	}

    /*public void StartGame() {

	}*/

    public void EndGame()
    {
        if (incidentsFixed == 3)
        {
            GameObject[] errors = GameObject.FindGameObjectsWithTag("Error");
            foreach(GameObject error in errors)
            {
                Destroy(error);
            }

            _tableText.text = "All incidents have\nbeen cleared.";
            Debug.Log("End of game");
        }
    }
}

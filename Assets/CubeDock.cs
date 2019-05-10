using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDock : MonoBehaviour
{
    public string face;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "StationFace")
        {
            face = "station";
            Debug.Log("face");
            other.GetComponent<TextMesh>().text = "station";

        }
        else if (other.tag == "TubexFace")
        {
            face = "tubex";
            Debug.Log("tubex");
            other.GetComponent<TextMesh>().text = "tubex";
        }
        else if (other.tag == "ExperimentFace")
        {
            face = "experiment";
            Debug.Log("experiment");
            other.GetComponent<TextMesh>().text = "experiment";
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "StationFace" || other.tag == "TubexFace" || other.tag == "ExperimentFace")
        {
            face = "";
            Debug.Log("none");
            other.GetComponent<TextMesh>().text = "";
        }
        
    }

}


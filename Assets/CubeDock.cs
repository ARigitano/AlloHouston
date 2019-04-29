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

        }
        else if (other.tag == "TubexFace")
        {
            face = "tubex";
        }
        else if (other.tag == "ExperimentFace")
        {
            face = "experiment";
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "StationFace" || other.tag == "TubexFace" || other.tag == "ExperimentFace")
        {
            face = "";
        }
        
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handCalibration : MonoBehaviour {

    public GameObject cube;
    public float moveNumber;
    public int rotateNumber;

	

    public void XMoveLeft()
    {
        cube.transform.position += new Vector3(-moveNumber, 0f, 0f);
    }

    public void XMoveRight()
    {
        cube.transform.position += new Vector3(moveNumber, 0f, 0f);
    }

    public void YMoveLeft()
    {
        cube.transform.position += new Vector3(0f, moveNumber, 0f);
    }

    public void YMoveRight()
    {
        cube.transform.position += new Vector3(0f, -moveNumber, 0f);
    }

    public void ZMoveLeft()
    {
        cube.transform.position += new Vector3(0f, 0f, moveNumber);
    }

    public void ZMoveRight()
    {
        cube.transform.position += new Vector3(0f, 0f, -moveNumber);
    }




    public void XRotateLeft()
    {
        cube.transform.eulerAngles += new Vector3(rotateNumber, 0f, 0f);
    }

    public void XRotateRight()
    {
        cube.transform.eulerAngles += new Vector3(-rotateNumber, 0f, 0f);
    }

    public void YRotateLeft()
    {
        cube.transform.eulerAngles += new Vector3(0f, -rotateNumber, 0f);
    }

    public void YRotateRight()
    {
        cube.transform.eulerAngles += new Vector3(0f, rotateNumber, 0f);
    }

    public void ZRotateLeft()
    {
        cube.transform.eulerAngles += new Vector3(0f, 0f, rotateNumber);
    }

    public void ZRotateRight()
    {
        cube.transform.eulerAngles += new Vector3(0f, 0f, -rotateNumber);
    }

    


}

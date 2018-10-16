using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.ManusVR.Scripts;

public class handCalibration : MonoBehaviour {

    public GameObject cube;
    public float moveNumber;
    public int rotateNumber;

    public TrackingManager manus;

	

    public void XMoveLeft()
    {
        manus.LCustomPositionOffset += new Vector3(-moveNumber, 0f, 0f);
        //cube.transform.position += new Vector3(-moveNumber, 0f, 0f);
    }

    public void XMoveRight()
    {
        manus.LCustomPositionOffset += new Vector3(moveNumber, 0f, 0f);
        //cube.transform.position += new Vector3(moveNumber, 0f, 0f);
    }

    public void YMoveLeft()
    {
       
        manus.LCustomPositionOffset += new Vector3(0f, moveNumber, 0f);
        //cube.transform.position += new Vector3(0f, moveNumber, 0f);
    }

    public void YMoveRight()
    {
        manus.LCustomPositionOffset += new Vector3(0f, -moveNumber, 0f);
        //cube.transform.position += new Vector3(0f, -moveNumber, 0f);
    }

    public void ZMoveLeft()
    {
        manus.LCustomPositionOffset += new Vector3(0f, 0f, moveNumber);
        //cube.transform.position += new Vector3(0f, 0f, moveNumber);
    }

    public void ZMoveRight()
    {
        manus.LCustomPositionOffset += new Vector3(0f, 0f, -moveNumber);
        //cube.transform.position += new Vector3(0f, 0f, -moveNumber);
    }




    public void XRotateLeft()
    {
        manus.LCustomRotationOffset += new Vector3(rotateNumber, 0f, 0f);
        //cube.transform.eulerAngles += new Vector3(rotateNumber, 0f, 0f);
    }

    public void XRotateRight()
    {
        //cube.transform.eulerAngles += new Vector3(-rotateNumber, 0f, 0f);
    }

    public void YRotateLeft()
    {
        //cube.transform.eulerAngles += new Vector3(0f, -rotateNumber, 0f);
    }

    public void YRotateRight()
    {
        //cube.transform.eulerAngles += new Vector3(0f, rotateNumber, 0f);
    }

    public void ZRotateLeft()
    {
        //cube.transform.eulerAngles += new Vector3(0f, 0f, rotateNumber);
    }

    public void ZRotateRight()
    {
        //cube.transform.eulerAngles += new Vector3(0f, 0f, -rotateNumber);
    }

    


}

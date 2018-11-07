using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.ManusVR.Scripts;

public class HandCalibration : MonoBehaviour {

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
        manus.LCustomRotationOffset += new Vector3(-rotateNumber, 0f, 0f);
        //cube.transform.eulerAngles += new Vector3(-rotateNumber, 0f, 0f);
    }

    public void YRotateLeft()
    {
        manus.LCustomRotationOffset += new Vector3(0f, -rotateNumber, 0f);
        //cube.transform.eulerAngles += new Vector3(0f, -rotateNumber, 0f);
    }

    public void YRotateRight()
    {
        manus.LCustomRotationOffset += new Vector3(0f, rotateNumber, 0f);
        //cube.transform.eulerAngles += new Vector3(0f, rotateNumber, 0f);
    }

    public void ZRotateLeft()
    {
        manus.LCustomRotationOffset +=new Vector3(0f, 0f, rotateNumber);
        //cube.transform.eulerAngles += new Vector3(0f, 0f, rotateNumber);
    }

    public void ZRotateRight()
    {
        manus.LCustomRotationOffset += new Vector3(0f, 0f, -rotateNumber);
        //cube.transform.eulerAngles += new Vector3(0f, 0f, -rotateNumber);
    }





    public void XMoveLeft2()
    {
        manus.RCustomPositionOffset += new Vector3(-moveNumber, 0f, 0f);
        //cube.transform.position += new Vector3(-moveNumber, 0f, 0f);
    }

    public void XMoveRight2()
    {
        manus.RCustomPositionOffset += new Vector3(moveNumber, 0f, 0f);
        //cube.transform.position += new Vector3(moveNumber, 0f, 0f);
    }

    public void YMoveLeft2()
    {

        manus.RCustomPositionOffset += new Vector3(0f, moveNumber, 0f);
        //cube.transform.position += new Vector3(0f, moveNumber, 0f);
    }

    public void YMoveRight2()
    {
        manus.RCustomPositionOffset += new Vector3(0f, -moveNumber, 0f);
        //cube.transform.position += new Vector3(0f, -moveNumber, 0f);
    }

    public void ZMoveLeft2()
    {
        manus.RCustomPositionOffset += new Vector3(0f, 0f, moveNumber);
        //cube.transform.position += new Vector3(0f, 0f, moveNumber);
    }

    public void ZMoveRight2()
    {
        manus.RCustomPositionOffset += new Vector3(0f, 0f, -moveNumber);
        //cube.transform.position += new Vector3(0f, 0f, -moveNumber);
    }




    public void XRotateLeft2()
    {
        manus.RCustomRotationOffset += new Vector3(rotateNumber, 0f, 0f);
        //cube.transform.eulerAngles += new Vector3(rotateNumber, 0f, 0f);
    }

    public void XRotateRight2()
    {
        manus.RCustomRotationOffset += new Vector3(-rotateNumber, 0f, 0f);
        //cube.transform.eulerAngles += new Vector3(-rotateNumber, 0f, 0f);
    }

    public void YRotateLeft2()
    {
        manus.RCustomRotationOffset += new Vector3(0f, -rotateNumber, 0f);
        //cube.transform.eulerAngles += new Vector3(0f, -rotateNumber, 0f);
    }

    public void YRotateRight2()
    {
        manus.RCustomRotationOffset += new Vector3(0f, rotateNumber, 0f);
        //cube.transform.eulerAngles += new Vector3(0f, rotateNumber, 0f);
    }

    public void ZRotateLeft2()
    {
        manus.RCustomRotationOffset += new Vector3(0f, 0f, rotateNumber);
        //cube.transform.eulerAngles += new Vector3(0f, 0f, rotateNumber);
    }

    public void ZRotateRight2()
    {
        manus.RCustomRotationOffset += new Vector3(0f, 0f, -rotateNumber);
        //cube.transform.eulerAngles += new Vector3(0f, 0f, -rotateNumber);
    }




}

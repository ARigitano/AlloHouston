using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    [Header("Parameters")]

    public Vector3 destXYZ = new Vector3(0.0f, 0.0f, 0.0f);

    private float widthWin;
    private float heightWin;

    private bool isMoving = false;
    private float speed = 500f;
    private float distmin = 0.5f;
    private float delay = 0.0f;
    private float frequency = 0.02f;

    //private Screen parentScreen;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(Vector3 initPosition)
    {
        destXYZ = initPosition;
    }

    public void ComeIn()
    {
        Debug.Log("In");
    }

    public void ComeOut()
    {
        Debug.Log("Out");
    }

    public void Move2XYZ()
    {
        if(isMoving) CancelInvoke("Movement");
        isMoving = true;
        InvokeRepeating("Movement", delay, frequency);
    }

    private void Movement()
    {
        float sqrDist = (transform.localPosition - destXYZ).sqrMagnitude;
        if (sqrDist > distmin)
        {
            float step = speed * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, destXYZ, step);
        }
        else
        {
            isMoving = false;
            CancelInvoke("Movement");
        }
    }

    // SETGET
    public void SetWidth(float tmpWidth)
    {
        widthWin = tmpWidth;
    }

    public float GetWidth()
    {
        return widthWin;
    }

    public void SetHeight(float tmpHeight)
    {
        heightWin = tmpHeight;
    }

    public float GetHeight()
    {
        return heightWin;
    }

    public void SetDestXYZ(Vector3 tmpDestXYZ)
    {
        destXYZ = tmpDestXYZ;
    }

    public Vector3 GetDestXYZ()
    {
        return destXYZ;
    }

}

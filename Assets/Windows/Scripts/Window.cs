using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    [Header("Parameters")]
    public Vector3 dest = new Vector3(0.0f, 0.0f, 0.0f);

    public float widthWin { get; set; }
    public float heightWin { get; set; }

    private bool isMoving = false;
    private float speed = 500f;
    private float distmin = 0.5f;
    private float
        delay = 0.0f;
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
        dest = initPosition;
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
        float sqrDist = (transform.localPosition - dest).sqrMagnitude;
        if (sqrDist > distmin)
        {
            float step = speed * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, dest, step);
        }
        else
        {
            isMoving = false;
            CancelInvoke("Movement");
        }
    }

}

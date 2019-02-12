using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private Vector3 _offset;
    [SerializeField]
    private Transform _target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, _camera.transform.position + _camera.transform.forward +_offset, Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, _target.position, Time.deltaTime);
        transform.LookAt(_camera.transform.position);
    }
}

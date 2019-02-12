using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private Transform _target;
    private float _initialRotation;
    private float _secondRotation;
    private bool _isMoving;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("DetectRotation");
    }

    IEnumerator DetectRotation()
    {
        while (true)
        {
            _initialRotation = _camera.transform.eulerAngles.y;
            yield return new WaitForSeconds(1f);
            _secondRotation = _camera.transform.eulerAngles.y;
            if (Mathf.Abs(_initialRotation - _secondRotation) > 30f)
            {
                _isMoving = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, Time.deltaTime);
            transform.LookAt(_camera.transform.position);
            if(transform.position == _target.position)
            {
                _secondRotation = _initialRotation;
                _isMoving = false;
            }
        }
    }
}

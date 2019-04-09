using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private Transform _target;
    private float _initialRotation, _secondRotation;
    private Vector3 _initialPosition, _secondPosition;
    private bool _isMoving;
    [SerializeField]
    private float _angle;
    [SerializeField]
    private float _distance;

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
            _initialPosition = transform.position;
            yield return new WaitForSeconds(1f);
            _secondRotation = _camera.transform.eulerAngles.y;
            _secondPosition = transform.position;
            if (Mathf.Abs(_initialRotation - _secondRotation) > _angle || Mathf.Abs(_initialPosition.x - _secondPosition.x) > _distance || Mathf.Abs(_initialPosition.z - _secondPosition.z) > _distance)
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
                _secondPosition = _initialPosition;
                _isMoving = false;
            }
        }
    }
}

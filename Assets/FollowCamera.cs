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
            yield return new WaitForSeconds(2f);
            _secondRotation = _camera.transform.eulerAngles.y;
            if (Mathf.Abs(_initialRotation - _secondRotation) > 10f)
            {
                transform.position = Vector3.MoveTowards(transform.position, _target.position, Time.deltaTime);
                transform.LookAt(_camera.transform.position);
                _secondRotation = _initialRotation;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

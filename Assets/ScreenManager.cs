using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _screen;
    private bool _displayed = true;
    [SerializeField]
    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            if(_displayed)
            {
                _screen.alpha = 0f;
                //_camera.targetDisplay = 1;
                _camera.enabled = false;
                _displayed = false;
            }
            else
            {
                _screen.alpha = 1f;
                //_camera.targetDisplay = 0;
                _camera.enabled = true;
                _displayed = true;
            }
        }
    }
}

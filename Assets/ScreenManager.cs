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
    [SerializeField]
    private GameObject _langPanel;

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
                _langPanel.SetActive(false);
                _camera.enabled = false;
                _displayed = false;
            }
            else
            {
                _screen.alpha = 1f;
                _langPanel.SetActive(true);
                _camera.enabled = true;
                _displayed = true;
            }
        }
    }
}

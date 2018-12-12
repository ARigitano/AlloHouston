using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLaserClicker : MonoBehaviour, ILaserPointer, ITrackedController {
    [SerializeField]
    [Tooltip("Camera of the mouse.")]
    private Camera _camera = null;
    [SerializeField]
    [Tooltip("The layer mask.")]
    private LayerMask _layerMask = 0;
    private Transform _previousContact;

    public event PointerEventHandler PointerIn;
    public event PointerEventHandler PointerOut;
    public event ClickedEventHandler TriggerClicked;
    public event ClickedEventHandler Gripped;

    private void Update()
    {
        if (_camera != null)
        {
            RaycastHit hit;
            bool bHit = Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, _layerMask);

            if (_previousContact && _previousContact != hit.transform)
            {
                PointerEventArgs args = new PointerEventArgs();
                args.distance = 0f;
                args.flags = 0;
                args.target = _previousContact;
                OnPointerOut(args);
                _previousContact = null;
            }
            if (bHit && _previousContact != hit.transform)
            {
                Debug.Log(hit);
                PointerEventArgs argsIn = new PointerEventArgs();
                argsIn.distance = hit.distance;
                argsIn.flags = 0;
                argsIn.target = hit.transform;
                OnPointerIn(argsIn);
                _previousContact = hit.transform;
            }
            if (!bHit)
            {
                _previousContact = null;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (TriggerClicked != null)
                TriggerClicked(this, new ClickedEventArgs());
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (Gripped != null)
                Gripped(this, new ClickedEventArgs());
        }
    }

    private void OnPointerIn(PointerEventArgs e)
    {
        if (PointerIn != null)
            PointerIn(this, e);
    }

    private void OnPointerOut(PointerEventArgs e)
    {
        if (PointerOut != null)
            PointerOut(this, e);
    }
}

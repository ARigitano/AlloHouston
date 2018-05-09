// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(SteamVR_LaserPointer))]
public class ViveInputs : MonoBehaviour
{
    private SteamVR_LaserPointer _laserPointer;
    private SteamVR_TrackedController _trackedController;

    private void OnEnable()
    {
        _laserPointer = GetComponent<SteamVR_LaserPointer>();
        _laserPointer.PointerIn -= HandlePointerIn;
        _laserPointer.PointerIn += HandlePointerIn;
        _laserPointer.PointerOut -= HandlePointerOut;
        _laserPointer.PointerOut += HandlePointerOut;

        _trackedController = GetComponent<SteamVR_TrackedController>();
        if (_trackedController == null)
        {
            _trackedController = GetComponentInParent<SteamVR_TrackedController>();
        }
        _trackedController.TriggerClicked -= HandleTriggerClicked;
        _trackedController.TriggerClicked += HandleTriggerClicked;
    }

    private void HandleTriggerClicked(object sender, ClickedEventArgs e)
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, new PointerEventData(EventSystem.current), ExecuteEvents.submitHandler);
        }
    }

    private void HandlePointerIn(object sender, PointerEventArgs e)
    {
        var button = e.target.GetComponent<Button>();
        if (button != null)
        {
            button.Select();
            Debug.Log("HandlePointerIn", e.target.gameObject);
        }
    }

    private void HandlePointerOut(object sender, PointerEventArgs e)
    {
        var button = e.target.GetComponent<Button>();
        if (button != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            Debug.Log("HandlePointerOut", e.target.gameObject);
        }
    }
}

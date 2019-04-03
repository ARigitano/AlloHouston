using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticTouch : MonoBehaviour
{
    public bool hapticFlag = false;
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;


    // Start is called before the first frame update
    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        device = SteamVR_Controller.Input((int)trackedObject.index);

        if(hapticFlag)
        {
            device.TriggerHapticPulse(4000);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        hapticFlag = true;
    }

    private void OnTriggerExit(Collider other)
    {
        hapticFlag = false;
    }
}

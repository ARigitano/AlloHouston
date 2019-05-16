using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Haptic : MonoBehaviour
{
    private Hand hand;
    // Start is called before the first frame update
    void Start()
    {
        hand = GetComponent<Hand>();
        hand.controller.TriggerHapticPulse(500);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

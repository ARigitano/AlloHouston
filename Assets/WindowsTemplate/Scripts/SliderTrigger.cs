using UnityEngine;
using Valve.VR.InteractionSystem;

public class SliderTrigger : UIElement
{
    //-------------------------------------------------
    protected override void OnHandHoverBegin(Hand hand)
    {
        base.OnHandHoverBegin(hand);
        if (!hand.noSteamVRFallbackCamera)
            InputModule.instance.OnBeginDrag(gameObject, hand);
    }

    protected override void HandHoverUpdate(Hand hand)
    {
        base.HandHoverUpdate(hand);
        if (!hand.noSteamVRFallbackCamera)
            InputModule.instance.OnDragUpdate(gameObject, hand);
    }

    protected override void OnHandHoverEnd(Hand hand)
    {
        base.OnHandHoverEnd(hand);
        if (!hand.noSteamVRFallbackCamera)
            InputModule.instance.OnEndDrag(gameObject, hand);
    }
}

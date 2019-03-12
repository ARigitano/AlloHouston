using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class ButtonTrigger : UIElement {

    private bool _controllerIn = false;
    private bool _activated = false;
    private Sprite _touchedSprite;
    private Sprite _untouchedSprite;
    private Image _buttonImage;

    // Use this for initialization
    void Start()
    {
        _buttonImage = GetComponent<Button>().targetGraphic.GetComponent<Image>();
        _untouchedSprite = _buttonImage.sprite;
        _touchedSprite = GetComponent<Button>().spriteState.pressedSprite;
    }

    //-------------------------------------------------
    protected override void OnHandHoverBegin(Hand hand)
    {
        base.OnHandHoverBegin(hand);
        GetComponent<Button>().onClick.Invoke();
        _buttonImage.sprite = _touchedSprite;
    }

    protected override void OnHandHoverEnd(Hand hand)
    {
        base.OnHandHoverEnd(hand);
        _buttonImage.sprite = _untouchedSprite;
    }
}

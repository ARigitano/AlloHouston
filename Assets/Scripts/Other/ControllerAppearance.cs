using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ControllerAppearance : MonoBehaviour
{
    public bool highlightBodyOnlyOnCollision = false;
    public bool pulseTriggerHighlightColor = false;

    private VRTK_ControllerHighlighter highligher;
    private VRTK_ControllerEvents events;
    private VRTK_InteractGrab interactGrab;
    private VRTK_InteractUse interactUse;
    private VRTK_InteractTouch interactTouch;
    private VRTK_UIPointer uipointer;
    private Color highlightColor = Color.yellow;
    private Color clickColor = Color.red;
    private Color pulseColor = Color.black;
    private Color currentPulseColor;
    private float highlightTimer = 0.5f;
    private float pulseTimer = 0.5f;
    private float dimOpacity = 0.8f;
    private float defaultOpacity = 1f;
    private float clickTimer = 0.1f;
    private bool highlighted;

    private void OnEnable()
    {
        if (GetComponent<VRTK_ControllerEvents>() == null)
        {
            VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, "VRTK_ControllerAppearance_Example", "VRTK_ControllerEvents", "the same"));
            return;
        }

        events = GetComponent<VRTK_ControllerEvents>();
        highligher = GetComponent<VRTK_ControllerHighlighter>();
        interactGrab = GetComponent<VRTK_InteractGrab>();
        interactTouch = GetComponent<VRTK_InteractTouch>();
        interactUse = GetComponent<VRTK_InteractUse>();
        uipointer = GetComponent<VRTK_UIPointer>();
        currentPulseColor = pulseColor;
        highlighted = false;

        //Setup controller event listeners
        events.TriggerPressed += DoTriggerPressed;
        events.ButtonOnePressed += DoButtonOnePressed;
        events.ButtonTwoPressed += DoButtonTwoPressed;
        events.StartMenuPressed += DoStartMenuPressed;
        events.GripPressed += DoGripPressed;
        events.TouchpadPressed += DoTouchpadPressed;
        events.TouchpadReleased += DoTouchPadReleased;
        uipointer.UIPointerElementEnter += DoUIPointerElementEnter;
        uipointer.UIPointerElementExit += DoUIPointerElementExit;
        uipointer.UIPointerElementClick += DoUIPointerElementClick;
    }

    private void OnDisable()
    {
        events.TriggerPressed -= DoTriggerPressed;
        events.ButtonOnePressed -= DoButtonOnePressed;
        events.ButtonTwoPressed -= DoButtonTwoPressed;
        events.StartMenuPressed -= DoStartMenuPressed;
        events.GripPressed -= DoGripPressed;
        events.TouchpadPressed -= DoTouchpadPressed;
        uipointer.UIPointerElementEnter -= DoUIPointerElementEnter;
        uipointer.UIPointerElementExit -= DoUIPointerElementExit;
        uipointer.UIPointerElementClick -= DoUIPointerElementClick;
    }

    private void PulseTrigger()
    {
        highligher.HighlightElement(SDK_BaseController.ControllerElements.Trigger, currentPulseColor, pulseTimer);
        currentPulseColor = (currentPulseColor == pulseColor ? highlightColor : pulseColor);
    }

    private void RestoreHighlight()
    {
        if (highlighted)
            highligher.HighlightElement(SDK_BaseController.ControllerElements.Body, highlightColor, highlightTimer);
        else
            highligher.UnhighlightElement(SDK_BaseController.ControllerElements.Body);
    }

    private void DoTriggerPressed(object sender, ControllerInteractionEventArgs e)
    {
        highligher.UnhighlightElement(SDK_BaseController.ControllerElements.Trigger);
        if (pulseTriggerHighlightColor)
        {
            CancelInvoke("PulseTrigger");
        }
        VRTK_ObjectAppearance.SetOpacity(VRTK_DeviceFinder.GetModelAliasController(events.gameObject), defaultOpacity);
    }

    private void DoButtonOnePressed(object sender, ControllerInteractionEventArgs e)
    {
        highligher.UnhighlightElement(SDK_BaseController.ControllerElements.ButtonOne);
        VRTK_ObjectAppearance.SetOpacity(VRTK_DeviceFinder.GetModelAliasController(events.gameObject), defaultOpacity);
    }

    private void DoButtonTwoPressed(object sender, ControllerInteractionEventArgs e)
    {
        highligher.UnhighlightElement(SDK_BaseController.ControllerElements.ButtonTwo);
        VRTK_ObjectAppearance.SetOpacity(VRTK_DeviceFinder.GetModelAliasController(events.gameObject), defaultOpacity);
    }

    private void DoStartMenuPressed(object sender, ControllerInteractionEventArgs e)
    {
        highligher.UnhighlightElement(SDK_BaseController.ControllerElements.StartMenu);
        VRTK_ObjectAppearance.SetOpacity(VRTK_DeviceFinder.GetModelAliasController(events.gameObject), defaultOpacity);
    }

    private void DoGripPressed(object sender, ControllerInteractionEventArgs e)
    {
        highligher.UnhighlightElement(SDK_BaseController.ControllerElements.GripLeft);
        highligher.UnhighlightElement(SDK_BaseController.ControllerElements.GripRight);
        VRTK_ObjectAppearance.SetOpacity(VRTK_DeviceFinder.GetModelAliasController(events.gameObject), defaultOpacity);
    }

    private void DoTouchpadPressed(object sender, ControllerInteractionEventArgs e)
    {
        highligher.UnhighlightElement(SDK_BaseController.ControllerElements.Touchpad);
        VRTK_ObjectAppearance.SetOpacity(VRTK_DeviceFinder.GetModelAliasController(events.gameObject), defaultOpacity);
    }

    private void DoTouchPadReleased(object sender, ControllerInteractionEventArgs e)
    {
        highligher.UnhighlightElement(SDK_BaseController.ControllerElements.Body);
    }

    private void DoUIPointerElementExit(object sender, UIPointerEventArgs e)
    {
        highligher.UnhighlightElement(SDK_BaseController.ControllerElements.Body);
        highlighted = false;
    }

    private void DoUIPointerElementEnter(object sender, UIPointerEventArgs e)
    {
        highligher.HighlightElement(SDK_BaseController.ControllerElements.Body, highlightColor);
        highlighted = true;
    }

    private void DoUIPointerElementClick(object sender, UIPointerEventArgs e)
    {
        highligher.HighlightElement(SDK_BaseController.ControllerElements.Body, clickColor);
        Invoke("RestoreHighlight", clickTimer);
    }

    private void DoUIPointerElementDragStart(object sender, UIPointerEventArgs e)
    {
        Debug.Log("DragStart");
        highligher.HighlightElement(SDK_BaseController.ControllerElements.Body, clickColor);
    }

    private void DoUIPointerElementDragEnd(object sender, UIPointerEventArgs e)
    {
        Debug.Log("DragEnd");
        highligher.UnhighlightElement(SDK_BaseController.ControllerElements.Body);
        RestoreHighlight();
    }

    private void OnTriggerEnter(Collider collider)
    {
        OnTriggerStay(collider);
    }

    private void OnTriggerStay(Collider collider)
    {
        if (!VRTK_PlayerObject.IsPlayerObject(collider.gameObject) && !highlighted)
        {
            VRTK_InteractableObject interactable = collider.gameObject.GetComponentInParent<VRTK_InteractableObject>();
            if (interactable != null)
            {
                if (interactable.isGrabbable && !events.IsButtonPressed(interactGrab.grabButton))
                    HighlightButton(interactGrab.grabButton, highlightColor, highlightTimer);
                if (interactable.isUsable && !events.IsButtonPressed(interactUse.useButton))
                    HighlightButton(interactUse.useButton, highlightColor, highlightTimer);
                highligher.HighlightElement(SDK_BaseController.ControllerElements.Body, highlightColor, highlightTimer);
                VRTK_ObjectAppearance.SetOpacity(VRTK_DeviceFinder.GetModelAliasController(events.gameObject), dimOpacity);
                highlighted = true;
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (!VRTK_PlayerObject.IsPlayerObject(collider.gameObject))
        {
            VRTK_InteractableObject interactable = collider.gameObject.GetComponentInParent<VRTK_InteractableObject>();
            if (interactable != null)
            {
                if (interactable.isGrabbable)
                    UnhighlightButton(interactGrab.grabButton);
                if (interactable.isUsable)
                    UnhighlightButton(interactUse.useButton);
                highligher.UnhighlightElement(SDK_BaseController.ControllerElements.Body);
                VRTK_ObjectAppearance.SetOpacity(VRTK_DeviceFinder.GetModelAliasController(events.gameObject), defaultOpacity);
                highlighted = false;
            }
        }
    }
    
    private void UnhighlightButton(VRTK_ControllerEvents.ButtonAlias button)
    {
        switch (button)
        {
            case VRTK_ControllerEvents.ButtonAlias.TriggerPress:
                highligher.UnhighlightElement(SDK_BaseController.ControllerElements.Trigger);
                if (pulseTriggerHighlightColor)
                {
                    CancelInvoke("PulseTrigger");
                }
                break;
            case VRTK_ControllerEvents.ButtonAlias.TouchpadPress:
                highligher.UnhighlightElement(SDK_BaseController.ControllerElements.Touchpad);
                break;
            case VRTK_ControllerEvents.ButtonAlias.TouchpadTwoPress:
                highligher.UnhighlightElement(SDK_BaseController.ControllerElements.TouchpadTwo);
                break;
            case VRTK_ControllerEvents.ButtonAlias.StartMenuPress:
                highligher.UnhighlightElement(SDK_BaseController.ControllerElements.StartMenu);
                break;
            case VRTK_ControllerEvents.ButtonAlias.GripPress:
                highligher.UnhighlightElement(SDK_BaseController.ControllerElements.GripLeft);
                highligher.UnhighlightElement(SDK_BaseController.ControllerElements.GripRight);
                break;
            case VRTK_ControllerEvents.ButtonAlias.ButtonTwoPress:
                highligher.UnhighlightElement(SDK_BaseController.ControllerElements.ButtonTwo);
                break;
            case VRTK_ControllerEvents.ButtonAlias.ButtonOnePress:
                highligher.UnhighlightElement(SDK_BaseController.ControllerElements.ButtonOne);
                break;
        }
    }

    private void HighlightButton(VRTK_ControllerEvents.ButtonAlias button, Color highlightColor, float highlightTimer)
    {
        switch (button)
        {
            case VRTK_ControllerEvents.ButtonAlias.TriggerPress:
                highligher.HighlightElement(SDK_BaseController.ControllerElements.Trigger, highlightColor, highlightTimer);
                if (pulseTriggerHighlightColor)
                {
                    CancelInvoke("PulseTrigger");
                    InvokeRepeating("PulseTrigger", pulseTimer, pulseTimer);
                }
                break;
            case VRTK_ControllerEvents.ButtonAlias.TouchpadPress:
                highligher.HighlightElement(SDK_BaseController.ControllerElements.Touchpad, highlightColor, highlightTimer);
                break;
            case VRTK_ControllerEvents.ButtonAlias.TouchpadTwoPress:
                highligher.HighlightElement(SDK_BaseController.ControllerElements.TouchpadTwo, highlightColor, highlightTimer);
                break;
            case VRTK_ControllerEvents.ButtonAlias.StartMenuPress:
                highligher.HighlightElement(SDK_BaseController.ControllerElements.StartMenu, highlightColor, highlightTimer);
                break;
            case VRTK_ControllerEvents.ButtonAlias.GripPress:
                highligher.HighlightElement(SDK_BaseController.ControllerElements.GripLeft, highlightColor, highlightTimer);
                highligher.HighlightElement(SDK_BaseController.ControllerElements.GripRight, highlightColor, highlightTimer);
                break;
            case VRTK_ControllerEvents.ButtonAlias.ButtonTwoPress:
                highligher.HighlightElement(SDK_BaseController.ControllerElements.ButtonTwo, highlightColor, highlightTimer);
                break;
            case VRTK_ControllerEvents.ButtonAlias.ButtonOnePress:
                highligher.HighlightElement(SDK_BaseController.ControllerElements.ButtonOne, highlightColor, highlightTimer);
                break;
        }
    }
}

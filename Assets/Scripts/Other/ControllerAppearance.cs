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
    private Color highlightColor = Color.yellow;
    private Color pulseColor = Color.black;
    private Color currentPulseColor;
    private float highlightTimer = 0.5f;
    private float pulseTimer = 0.25f;
    private float dimOpacity = 0.8f;
    private float defaultOpacity = 1f;
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
        currentPulseColor = pulseColor;
        highlighted = false;

        //Setup controller event listeners
        events.TriggerPressed += DoTriggerPressed;
        events.ButtonOnePressed += DoButtonOnePressed;
        events.ButtonTwoPressed += DoButtonTwoPressed;
        events.StartMenuPressed += DoStartMenuPressed;
        events.GripPressed += DoGripPressed;
        events.TouchpadPressed += DoTouchpadPressed;
    }

    private void OnDisable()
    {
        events.TriggerPressed -= DoTriggerPressed;
        events.ButtonOnePressed -= DoButtonOnePressed;
        events.ButtonTwoPressed -= DoButtonTwoPressed;
        events.StartMenuPressed -= DoStartMenuPressed;
        events.GripPressed -= DoGripPressed;
        events.TouchpadPressed -= DoTouchpadPressed;
    }

    private void PulseTrigger()
    {
        highligher.HighlightElement(SDK_BaseController.ControllerElements.Trigger, currentPulseColor, pulseTimer);
        currentPulseColor = (currentPulseColor == pulseColor ? highlightColor : pulseColor);
    }

    private void DoTriggerPressed(object sender, ControllerInteractionEventArgs e)
    {
        highligher.UnhighlightElement(SDK_BaseController.ControllerElements.Trigger);
        if (pulseTriggerHighlightColor)
        {
            CancelInvoke("PulseTrigger");
        }
        VRTK_ObjectAppearance.SetOpacity(VRTK_DeviceFinder.GetModelAliasController(events.gameObject), 1.0f);
    }

    private void DoButtonOnePressed(object sender, ControllerInteractionEventArgs e)
    {
        highligher.UnhighlightElement(SDK_BaseController.ControllerElements.ButtonOne);
        VRTK_ObjectAppearance.SetOpacity(VRTK_DeviceFinder.GetModelAliasController(events.gameObject), 1.0f);
    }

    private void DoButtonTwoPressed(object sender, ControllerInteractionEventArgs e)
    {
        highligher.UnhighlightElement(SDK_BaseController.ControllerElements.ButtonTwo);
        VRTK_ObjectAppearance.SetOpacity(VRTK_DeviceFinder.GetModelAliasController(events.gameObject), 1.0f);
    }

    private void DoStartMenuPressed(object sender, ControllerInteractionEventArgs e)
    {
        highligher.UnhighlightElement(SDK_BaseController.ControllerElements.StartMenu);
        VRTK_ObjectAppearance.SetOpacity(VRTK_DeviceFinder.GetModelAliasController(events.gameObject), 1.0f);
    }

    private void DoGripPressed(object sender, ControllerInteractionEventArgs e)
    {
        highligher.UnhighlightElement(SDK_BaseController.ControllerElements.GripLeft);
        highligher.UnhighlightElement(SDK_BaseController.ControllerElements.GripRight);
        VRTK_ObjectAppearance.SetOpacity(VRTK_DeviceFinder.GetModelAliasController(events.gameObject), 1.0f);
    }

    private void DoTouchpadPressed(object sender, ControllerInteractionEventArgs e)
    {
        highligher.UnhighlightElement(SDK_BaseController.ControllerElements.Touchpad);
        VRTK_ObjectAppearance.SetOpacity(VRTK_DeviceFinder.GetModelAliasController(events.gameObject), 1.0f);
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
            if (interactable != null && interactable.isGrabbable && !events.IsButtonPressed(interactGrab.grabButton))
            {
                HighlightButton(interactGrab.grabButton, highlightColor, highlightTimer);
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
            if (interactable != null && interactable.isGrabbable)
            {
                UnhighlightButton(interactGrab.grabButton);
                VRTK_ObjectAppearance.SetOpacity(VRTK_DeviceFinder.GetModelAliasController(events.gameObject), 1.0f);
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

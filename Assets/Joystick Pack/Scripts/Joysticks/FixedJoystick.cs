using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedJoystick : Joystick
{
     public override void OnPointerDown(PointerEventData eventData)
    {
        // carController.PointerDown = true;
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        // carController.PointerDown = false;
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }
}
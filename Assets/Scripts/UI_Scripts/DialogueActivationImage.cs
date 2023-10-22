using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueActivationImage : MonoBehaviour, IPointerClickHandler
{
    public event EventHandler OnDialogueImageClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnDialogueImageClicked.Invoke(this, EventArgs.Empty);
    }
}

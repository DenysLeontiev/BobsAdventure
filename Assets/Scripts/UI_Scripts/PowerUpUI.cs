using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PowerUpUI : MonoBehaviour, IPointerDownHandler
{
    public class OnPowerUpClickedEventArgs : EventArgs
    {
        public PowerUp powerUp;
    }

    public event EventHandler<OnPowerUpClickedEventArgs> OnPowerUpClicked;

    [SerializeField] private PowerUp powerUp;

    private PowerUpCanvas powerUpCanvas;

    private void Start()
    {
        powerUpCanvas = GetComponentInParent<PowerUpCanvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnPowerUpClicked != null)
            OnPowerUpClicked?.Invoke(this, new OnPowerUpClickedEventArgs { powerUp = this.powerUp });

        powerUpCanvas.Hide(); // hide the canvas, when we clicked on any powerUp
    }
}

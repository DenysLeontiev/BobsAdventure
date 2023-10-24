using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCanvas : MonoBehaviour
{
    public event EventHandler OnPowerUpCanvasActive;
    public event EventHandler OnPowerUpCanvasInactive;

    [SerializeField] private GameObject poweUpBackground;
    private Level level;

    private Animator powerUpWindowAnimator;

    private void Start()
    {
        level = PlayerController.Instance.GetLevelManager();
        level.OnLevelUp += Level_OnLevelUp;

        powerUpWindowAnimator = GetComponentInChildren<Animator>();

        //Hide();
    }

    private void Level_OnLevelUp(object sender, Level.OnLevelUpEventArgs e)
    {
        Show();
    }

    private void Show()
    {
        if(OnPowerUpCanvasActive != null)
            OnPowerUpCanvasActive?.Invoke(this, EventArgs.Empty);

        //poweUpBackground.SetActive(true);
        powerUpWindowAnimator.SetBool("appear", true);
    }

    public void Hide()
    {
        if (OnPowerUpCanvasInactive != null)
            OnPowerUpCanvasInactive?.Invoke(this, EventArgs.Empty);

        //poweUpBackground.SetActive(false);
        powerUpWindowAnimator.SetBool("appear", false);
    }
}

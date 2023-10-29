using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public class OnMenuOpenEventArgs : EventArgs
    {
        public bool isMenuActive;
    }

    public event EventHandler<OnMenuOpenEventArgs> OnMenuOpen;

    [SerializeField] private KeyCode triggerMenuButton = KeyCode.Escape;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Slider slider;

    [SerializeField] private TextMeshProUGUI levelTextUI;
    [SerializeField] private TextMeshProUGUI currentHealthTextUI;

    private HealthSystem playerHealthSystem;


    private bool isActive = false;

    private Level levelManager;

    private int currentLevel = 1;

    private float experienceForNextLevelNormalized;

    private void Start()
    {
        levelManager = PlayerController.Instance.GetLevelManager();
        playerHealthSystem = FindObjectOfType<HealthSystem>();

        Debug.Log("playerHealthSystem: " + playerHealthSystem);

        levelManager.OnLevelUp += LevelManager_OnLevelUp;
        levelManager.OnExperienceAdded += LevelManager_OnExperienceAdded;


        playerHealthSystem.OnHealthChanged += PlayerHealthSystem_OnHealthChanged;

        menuPanel.SetActive(isActive);

        SetLevelTextUI(levelManager.currentLevel); // init level text field with initial data
        SetHealthTextUI(playerHealthSystem.GetCurrentHealth()); // init health text field with initial data
    }

    private void PlayerHealthSystem_OnHealthChanged(object sender, EventArgs e)
    {
        SetHealthTextUI(playerHealthSystem.GetCurrentHealth());
    }

    private void LevelManager_OnLevelUp(object sender, Level.OnLevelUpEventArgs e)
    {
        currentLevel = e.level;
        SetLevelTextUI(currentLevel);
    }

    private void LevelManager_OnExperienceAdded(object sender, Level.OnExperienceAddedEventArgs e)
    {
        experienceForNextLevelNormalized = e.experienceToNextLevelNormalized;
    }

    private void Update()
    {
        HandleMenuState();

        if(menuPanel.activeInHierarchy)
        {
            slider.value = experienceForNextLevelNormalized;
        }
    }

    private void HandleMenuState()
    {
        if (Input.GetKeyDown(triggerMenuButton))
        {
            isActive = !isActive;
            if(OnMenuOpen != null)
                OnMenuOpen?.Invoke(this, new OnMenuOpenEventArgs { isMenuActive = isActive });
            menuPanel.SetActive(!isActive);
        }
    }

    private void SetLevelTextUI(int level)
    {
        levelTextUI.text = $"Level:{level.ToString()}/100";
    }

    private void SetHealthTextUI(int health)
    {
        Debug.Log("Setting UI led to " + health);
        currentHealthTextUI.text = $"Health:{health.ToString()}/" + playerHealthSystem.GetMaxHealth();
    }
}

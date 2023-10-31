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

    [SerializeField] private InventoryManager inventoryManager;

    [SerializeField] private KeyCode triggerMenuButton = KeyCode.Escape;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Slider slider;

    [SerializeField] private TextMeshProUGUI levelTextUI;
    [SerializeField] private TextMeshProUGUI currentHealthTextUI;
    [SerializeField] private TextMeshProUGUI manaPointsTextUI;
    [SerializeField] private TextMeshProUGUI goldenCoinsTextUI;

    private HealthSystem playerHealthSystem;
    private ManaSystem playerManaSystem;

    private bool isActive = false;

    private Level levelManager;

    private int currentLevel = 1;

    private float experienceForNextLevelNormalized;

    private void Start()
    {
        levelManager = PlayerController.Instance.GetLevelManager();

        InventoryManager.Instance.OnItemAdded += Instance_OnItemAdded;

        playerHealthSystem = FindObjectOfType<HealthSystem>();
        playerManaSystem = FindObjectOfType<ManaSystem>();

        levelManager.OnLevelUp += LevelManager_OnLevelUp;
        levelManager.OnExperienceAdded += LevelManager_OnExperienceAdded;

        playerHealthSystem.OnHealthChanged += PlayerHealthSystem_OnHealthChanged;
        playerManaSystem.OnManaAmountChaged += PlayerManaSystem_OnManaAmountChaged;

        menuPanel.SetActive(isActive);

        SetLevelTextUI(levelManager.currentLevel); // init level text field with initial data
        SetHealthTextUI(playerHealthSystem.GetCurrentHealth()); // init health text field with initial data
        SetManaTextUI(playerManaSystem.GetCurrentMana());
        SetGoldenCointTextUI(InventoryManager.Instance.GetAmountOfCoins());
    }

    private void Instance_OnItemAdded(object sender, InventoryManager.OnItemAddedEventArgs e)
    {
        if(e.itemAdded!= null)
        {
            if(e.itemAdded.Type == Item.ItemType.Coin)
            {
                SetGoldenCointTextUI(InventoryManager.Instance.GetAmountOfCoins());
            }
        }
    }

    private void PlayerManaSystem_OnManaAmountChaged(object sender, ManaSystem.OnManaAmountChagedEventArgs e)
    {
        SetManaTextUI(e.manaPoints);
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
            {
                OnMenuOpen?.Invoke(this, new OnMenuOpenEventArgs { isMenuActive = isActive });
            }
            inventoryManager.ListItems();
            menuPanel.SetActive(!isActive);
        }
    }

    private void SetLevelTextUI(int level)
    {
        levelTextUI.text = $"Level:{level}/100";
    }

    private void SetHealthTextUI(int health)
    {
        currentHealthTextUI.text = $"Health:{health}/" + playerHealthSystem.GetMaxHealth();
    }

    private void SetManaTextUI(int mana)
    {
        manaPointsTextUI.text = $"Mana:{mana}/" + playerManaSystem.GetMaxManaAmount();
    }

    private void SetGoldenCointTextUI(int coins)
    {
        goldenCoinsTextUI.text = $"Golden Coins:{coins}";
    }
}

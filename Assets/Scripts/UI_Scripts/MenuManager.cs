using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private KeyCode triggerMenuButton = KeyCode.Escape;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Slider slider;


    private bool isActive = false;

    private Level levelManager;

    private int currentLevel = 1;

    private float experienceForNextLevelNormalized;

    private void Start()
    {
        levelManager = PlayerController.Instance.GetLevelManager();

        levelManager.OnLevelUp += LevelManager_OnLevelUp;
        levelManager.OnExperienceAdded += LevelManager_OnExperienceAdded;

        menuPanel.SetActive(isActive);
    }

    private void LevelManager_OnLevelUp(object sender, Level.OnLevelUpEventArgs e)
    {
        currentLevel = e.level;
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
            print("experienceForNextLevelNormalized: " + experienceForNextLevelNormalized);
            slider.value = experienceForNextLevelNormalized;
        }
    }

    private void HandleMenuState()
    {
        if (Input.GetKeyDown(triggerMenuButton))
        {
            isActive = !isActive;
            menuPanel.SetActive(!isActive);
        }
    }


}

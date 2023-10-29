using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    private PowerUpCanvas powerUpCanvas;
    private MenuManager menuManager;

    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;
    private HealthSystem healthSystem;

    [SerializeField] private Level level;

    [SerializeField] private float moveSpeed = 1f;

    // to restrict player moving out of the map
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    private int startingLevel = 1;

    private bool canMove = true; // disable movement when dialogue is on 

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        level = new Level(startingLevel);

        level.OnLevelUp += Level_OnLevelUp;
        level.OnExperienceAdded += Level_OnExperienceAdded;

        healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        menuManager = FindObjectOfType<MenuManager>();

        powerUpCanvas = GameObject.FindObjectOfType<PowerUpCanvas>();  


        DialogueManager.Instance.OnDialogueStarted += Instance_OnDialogueStarted;
        DialogueManager.Instance.OnDialogueFinished += Instance_OnDialogueFinished;

        powerUpCanvas.OnPowerUpCanvasActive += PowerUpCanvas_OnPowerUpCanvasActive;
        powerUpCanvas.OnPowerUpCanvasInactive += PowerUpCanvas_OnPowerUpCanvasInActive;

        menuManager.OnMenuOpen += MenuManager_OnMenuOpen;
    }

    private void MenuManager_OnMenuOpen(object sender, MenuManager.OnMenuOpenEventArgs e)
    {
        canMove = e.isMenuActive;
    }

    private void PowerUpCanvas_OnPowerUpCanvasActive(object sender, System.EventArgs e)
    {
        canMove = false;
    }

    private void PowerUpCanvas_OnPowerUpCanvasInActive(object sender, System.EventArgs e)
    {
        canMove = true;
    }

    private void Level_OnLevelUp(object sender, Level.OnLevelUpEventArgs e)
    {
        
    }

    private void Level_OnExperienceAdded(object sender, Level.OnExperienceAddedEventArgs e)
    {
        
    }

    private void Instance_OnDialogueStarted(object sender, System.EventArgs e)
    {
        canMove = false;
    }
    private void Instance_OnDialogueFinished(object sender, System.EventArgs e)
    {
        canMove = true;
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            level.AddExperience(50);
        }

        float mouseX, mouseY;

        if(canMove)
        {
            HandleMovement(out mouseX, out mouseY);
            HandleAnimations(mouseX, mouseY);
        }
        else
        {
            playerRigidbody.velocity = Vector3.zero;
        }

        float clampedX = Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x);
        float clampedY = Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);

        
    }

    public Level GetLevelManager()
    {
        return level;
    }

    public HealthSystem GetHealthSystem()
    {
        return healthSystem;
    }

    private void HandleMovement(out float mouseX, out float mouseY)
    {
        mouseX = Input.GetAxisRaw("Horizontal");
        mouseY = Input.GetAxisRaw("Vertical");
        playerRigidbody.velocity = new Vector2(mouseX, mouseY) * moveSpeed;
    }

    // this method is called in camera, because in camera script we know exact tilemap bounds and we pass them here from there
    public void SetBounds(Vector3 bottomLeftLimit, Vector3 topRightLimit)
    {
        Vector3 bottomOffset = new Vector3(1f, 1f, 0f);
        Vector3 topOffset = new Vector3(1f, 1f, 0f);

        this.bottomLeftLimit = bottomLeftLimit + bottomOffset; // add offset
        this.topRightLimit = topRightLimit - topOffset; // add offset
    }

    private void HandleAnimations(float mouseX, float mouseY)
    {
        playerAnimator.SetFloat("moveX", playerRigidbody.velocity.x);
        playerAnimator.SetFloat("moveY", playerRigidbody.velocity.y);

        if (mouseX == 1 || mouseX == -1 || mouseY == 1 || mouseY == -1)
        {
            playerAnimator.SetFloat("lastMoveX", mouseX);
            playerAnimator.SetFloat("lastMoveY", mouseY);
        }
    }
}

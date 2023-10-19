using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;

    [SerializeField] private float moveSpeed = 1f;

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
    }

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Horizontal");
        float mouseY = Input.GetAxisRaw("Vertical");
        playerRigidbody.velocity = new Vector2(mouseX, mouseY) * moveSpeed;

        playerAnimator.SetFloat("moveX", playerRigidbody.velocity.x);
        playerAnimator.SetFloat("moveY", playerRigidbody.velocity.y);

        if (mouseX == 1 || mouseX == -1 || mouseY == 1 || mouseY == -1)
        {
            playerAnimator.SetFloat("lastMoveX", mouseX);
            playerAnimator.SetFloat("lastMoveY", mouseY);
        }

        Debug.Log($"inputX: {mouseX} | velX: {playerRigidbody.velocity.x}");
        Debug.Log($"inputY: {mouseY} | velY: {playerRigidbody.velocity.y}");
    }
}

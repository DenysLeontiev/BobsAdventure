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

    // to restrict player moving out of the map
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

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

        float clampedX = Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x);
        float clampedY = Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);

        HandleAnimations(mouseX, mouseY);
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

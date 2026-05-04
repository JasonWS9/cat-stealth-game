using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public static Player instance;
    private CharacterController controller;

    [Header("Speed Values")]
    private float speed;
    public float defaultSpeed = 5;
    public float sprintSpeed = 8;

    [Header("Jump Values")]
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpHeight = 40f;
    [SerializeField] private float lowJumpMultiplier = 1.5f;
    [SerializeField] private float fallingGravityMultiplier = 2f;

    [SerializeField] private float coyoteTimeDuration = 0.15f;
    private float coyoteTimer;

    private Vector3 playerVelocity;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    private InputAction crouchAction;

    private Vector3 spawnPoint;
    public float crouchHeightChange;
    private float originalHeight;


    void Awake()
    {
        instance = this;
        controller = GetComponent<CharacterController>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        sprintAction = InputSystem.actions.FindAction("Sprint");
        crouchAction = InputSystem.actions.FindAction("Crouch");
        speed = defaultSpeed;
        spawnPoint = transform.position;
        originalHeight = controller.height;
    }

    // Update is called once per frame
    void Update()
    {
        // Caches isGrounded so it doesn’t change mid-frame after Move() for consistency
        bool isGrounded = controller.isGrounded;

        HandleGrounding(isGrounded);
        HandleMovement();
        HandleJump(isGrounded);

        controller.Move(playerVelocity * Time.deltaTime);

    }
#region Movement
    void HandleGrounding(bool isGrounded)
    {
        if (isGrounded)
        {
            //Debug.Log("should be grounded");
            coyoteTimer = coyoteTimeDuration;

            // Reset downward velocity if player is grounded
            if (playerVelocity.y < 0)
            {
                playerVelocity.y = -2f;
            }
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }
    }

    void HandleMovement()
    {
        // Get horizontal and vertical input
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        float moveX = moveInput.x;
        float moveZ = moveInput.y;

        // Calculate movement direction relative to the player's orientation (camera)
        Vector3 finalMove = transform.right * moveX + transform.forward * moveZ;
        playerVelocity.x = finalMove.x * speed;
        playerVelocity.z = finalMove.z * speed;

        if (sprintAction.IsPressed())
        {
            speed = sprintSpeed;
        } else
        {
            speed = defaultSpeed;
        }

        if (crouchAction.WasPressedThisFrame())
        {
            controller.height -= crouchHeightChange;
            Camera.Instance.Crouch(crouchHeightChange);
        }
        if (crouchAction.WasReleasedThisFrame())
        {
            controller.height = originalHeight;
            Camera.Instance.UnCrouch(crouchHeightChange);
        }
    }

    private void HandleJump(bool isGrounded)
    {
        // Handle jump input
        if (coyoteTimer > 0f && jumpAction.WasPressedThisFrame())
        {
            //Math equation so that you get a consistant jump height
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            coyoteTimer = 0f;
        }

        // Increases gravity based on if the player releases the key early or is falling
        if (playerVelocity.y > 0 && !jumpAction.IsPressed())
        {
            playerVelocity.y += gravity * lowJumpMultiplier * Time.deltaTime;
        }
        else if (playerVelocity.y < 0)
        {
            playerVelocity.y += gravity * fallingGravityMultiplier * Time.deltaTime;
        }
        else
        {
            playerVelocity.y += gravity * Time.deltaTime;
        }
    }
    #endregion

#region Collision

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            PlayerCarDeath();
        }
    if (other.CompareTag("Enemy"))
        {
            PlayerDeath();
        } 
    }

    void OnTriggerExit(Collider other)
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        
    }

    void OnCollisionExit(Collision collision)
    {
        
    }

    #endregion

    private void PlayerDeath()
    {
        SceneManager.LoadScene("Ending 3 (Caught)");
        Debug.Log("died to enemy");
    }

    private void PlayerCarDeath()
    {
        SceneManager.LoadScene("Ending 1 (Car)");
        Debug.Log("died to car");
    }

}
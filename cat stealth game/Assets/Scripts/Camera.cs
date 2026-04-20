using UnityEngine;
using UnityEngine.InputSystem;

public class Camera : MonoBehaviour
{
    public static Camera Instance;

    public float sensitivity = 2.5f;
    public float minPitch = -80f;
    public float maxPitch = 80f;

    public Transform playerBody; // assign your Player object

    private float pitch = 0f;
    private InputAction lookAction;

    private Vector3 originalPosition;

    void Awake()
    {
        Instance = this;
        lookAction = InputSystem.actions.FindAction("Look");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        originalPosition = transform.position;
    }

    void Update()
    {
        Vector2 lookInput = lookAction.ReadValue<Vector2>();

        float mouseX = lookInput.x * sensitivity;
        float mouseY = lookInput.y * sensitivity;

        // Rotate player left/right (yaw)
        playerBody.Rotate(Vector3.up * mouseX);

        // Rotate camera up/down (pitch)
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    public void Crouch(float amount)
    {
        transform.position -= new Vector3(0, amount, 0) ;
    }

    public void UnCrouch(float amount)
    {
        transform.position += new Vector3(0, amount, 0) ;
    }
}
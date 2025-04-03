using UnityEngine;
using UnityEngine.InputSystem; // New Input System namespace
using UnityEngine.InputSystem.XR; // If you want XR-specific stuff (optional)
public class VRBikeControllerWithInputActions : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The XR Rig or Player root transform.")]
    public Transform playerRig;
    [Tooltip("Child transform on the bike where the player sits.")]
    public Transform seatTransform;
    [Header("Bike Movement Settings")]
    [Tooltip("How fast the bike moves forward/back.")]
    public float moveSpeed = 5f;
    [Tooltip("How fast the bike turns (degrees per second).")]
    public float turnSpeed = 45f;
    [Header("Input Actions")]
    [Tooltip("2D Action: X for turning, Y for forward/back.")]
    public InputActionProperty moveAction;
    [Tooltip("Button Action: Press to toggle mount/dismount.")]
    public InputActionProperty mountDismountAction;
    // Track whether the player is currently on the bike
    private bool isMounted = false;
    private void OnEnable()
    {
        // Enable actions and subscribe to events
        if (moveAction != null && moveAction.action != null)
            moveAction.action.Enable();
        if (mountDismountAction != null && mountDismountAction.action != null)
        {
            mountDismountAction.action.Enable();
            mountDismountAction.action.performed += OnMountDismount;
        }
    }
    private void OnDisable()
    {
        // Disable actions and unsubscribe
        if (moveAction != null && moveAction.action != null)
            moveAction.action.Disable();
        if (mountDismountAction != null && mountDismountAction.action != null)
        {
            mountDismountAction.action.performed -= OnMountDismount;
            mountDismountAction.action.Disable();
        }
    }
    private void Update()
    {
        if (!isMounted) return;
        // Read the 2D input from the joystick or motion controller
        if (moveAction != null && moveAction.action != null)
        {
            Vector2 inputValue = moveAction.action.ReadValue<Vector2>();
            float forward = inputValue.y; // Forward/back
            float turn = inputValue.x; // Left/right
                                       // Move the bike forward/back
            transform.Translate(Vector3.forward * forward * moveSpeed * Time.deltaTime);
            // Turn the bike
            transform.Rotate(Vector3.up, turn * turnSpeed * Time.deltaTime);
        }
    }
    // Called when the "MountDismount" button is pressed
    private void OnMountDismount(InputAction.CallbackContext context)
    {
        if (!isMounted)
        {
            MountBike();
        }
        else
        {
            DismountBike();
        }
    }
    public void MountBike()
    {
        if (playerRig == null || seatTransform == null)
            return;
        // Parent the XR Rig to the bike seat
        playerRig.SetParent(seatTransform);
        // Zero out local position/rotation so camera lines up with seat
        playerRig.localPosition = Vector3.zero;
        playerRig.localRotation = Quaternion.identity;
        isMounted = true;
        Debug.Log("Player mounted the bike.");
    }
    public void DismountBike()
    {
        if (!isMounted) return;
        // Unparent the player rig
        playerRig.SetParent(null);
        // Optionally reposition them away from the bike
        Vector3 dismountPosition = transform.position - transform.forward * 1f;
        playerRig.position = dismountPosition;
        isMounted = false;
        Debug.Log("Player dismounted the bike.");
    }
}
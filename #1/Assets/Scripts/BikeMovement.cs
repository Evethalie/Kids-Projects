using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets; // Allows us to use the new Input System (VR Core Template)

public class BikeMovement : MonoBehaviour
{
    // These variables show up in the Inspector, and we can change them in Unity
    public float baseSpeed = 5f;   // The basic forward speed without any extra parts
    public float baseTurnSpeed = 40f; // How fast we turn left/right

    [Header("VR Core Input")]
    // We'll assume we have an InputActionProperty from the VR Core template
    // that gives us a 2D value for movement (x = turn, y = forward/back).
    public DynamicMoveProvider moveProvider;
    public ContinuousTurnProvider continuousTurnProvider;

    void Update()
    {
        /* 1. Read the VR input from the 'moveInput' action.
        // It's usually a Vector2 (x, y).
        // Example: x = left/right (turn), y = forward/back.
        Vector2 input = moveInput.action.ReadValue<Vector2>();

        // 2. Extract the forward (y) and turn (x) values from the Vector2.
        float forward = input.y;  // forward/back from the VR thumbstick
        float turn = input.x;      // left/right from the VR thumbstick

        // 3. Calculate how fast we move and turn, based on our base speeds.
        // (we'll modify these later when we attach Bike Parts.)
        float finalSpeed = baseSpeed * Time.deltaTime;
        float finalTurnSpeed = baseTurnSpeed * Time.deltaTime;

        // 4. Move the bike forward/back.
        // "transform.Translate" moves this object relative to its orientation.
        // Vector3.forward means "in front of me."
        transform.Translate(Vector3.forward * forward * finalSpeed);

        // 5. Turn the bike left/right.
        // "transform.Rotate" spins the object around the Y-axis (Vector3.up).
        transform.Rotate(Vector3.up, turn * finalTurnSpeed); */

      //  moveProvider.moveSpeed = baseSpeed;
        continuousTurnProvider.turnSpeed = baseTurnSpeed;
        moveProvider.SetSpeed(baseSpeed);
    }
}

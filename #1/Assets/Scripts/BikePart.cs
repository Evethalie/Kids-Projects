using UnityEngine;

using UnityEngine;
public class BikePart : MonoBehaviour
{

    // We'll assume that each part can modify the bike's speed and turn speed by certain amounts.

    [Header("Part Settings")]
    public float speedBonus = 1f; // Multiply the bike's base speed by this amount
    public float turnSpeedBonus = 1f; // Multiply the bike's base turn speed by this amount

    // We'll store a reference to the BikeMovement script
    private BikeMovement bikeMovement;

    void Start()
    {

        // 1. Find the BikeMovement script on the parent or the same object
        //  (assuming the BikePart is a child of the bike or on the bike).
        bikeMovement = GetComponentInParent<BikeMovement>();

        // 2. If we found BikeMovement, adjust its base speed by our bonus amounts.
        if (bikeMovement != null)
        {
            // Example: add 2 to speed if speedBonus is 2, meaning multiply by (baseSpeed * 2).
            bikeMovement.baseSpeed *= speedBonus;

            // Similarly for turn speed.
            bikeMovement.baseTurnSpeed *= turnSpeedBonus;
        }
    }
}

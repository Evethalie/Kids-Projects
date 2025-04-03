using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

//This script must be added as a component of the Cart that you want to move.
public class MoveCart : MonoBehaviour
{
    // This array isn't assigned in code. You must manually add the tracks into the array via Unity's Inspector:
    public GameObject[] Tracks;
    private int index = 0;
    // The speed that the cart moves at:
    public float cartSpeed = 10;

    private bool startMovement;
    private bool isColliding;

    public Vector3 cartMovement;

    // Game Objects
    public GameObject VRParent; // VR XR Elements (Parent)
    public GameObject XRRig; // XR Origin (XR Rig) (Player, Child)
    private GameObject cartObject; // The object that moves the player; set to the current object this script is attached to
    public GameObject moveObject; // The object that allows the player to move

    private void Start()
    {
        cartObject = gameObject;
    }

    // When the Player collides with the Cart platform
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isColliding = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isColliding = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // This determines whether the cart should start moving or not, and is a Scene Variable, set in the CartButton Visual Script Graph:
        startMovement = (bool)Variables.ActiveScene.Get("Start Movement");

        if (startMovement == true)
        {
            if (isColliding == true)
            {
                // Sets the VR Parent to be the parent of the Cart Object (this object)
                cartObject.transform.SetParent(null);
                cartObject.transform.parent = VRParent.transform;
                // Sets the XR Rig (the Player) to be a child of the Cart Object (this object)
                XRRig.transform.parent = cartObject.transform;

                // Restrict the player's movement while they're on the cart:
                moveObject.SetActive(false);
            }

            cartMove();
        }
    }

    public void cartMove()
    {
        // If it's no longer able to move:
        if (index >= Tracks.Length)
        {
            // If the player is at the end of the track, allow them to move again:
            moveObject.SetActive(true);
            Variables.ActiveScene.Set("Start Movement", false);
            startMovement = false;

            // Sets parenting back to normal
            cartObject.transform.SetParent(null);
            XRRig.transform.parent = VRParent.transform;

            cartMovement = Vector3.zero;
        }
        // Otherwise, it moves:
        else
        {
            Vector3 currentTransform = transform.position;


            float step = cartSpeed * Time.deltaTime;
            // Move the cart:
            transform.position = Vector3.MoveTowards(transform.position, Tracks[index].transform.position, step);

            cartMovement = transform.position - currentTransform;

            // This gets the distance to the next track:
            float dist = Vector3.Distance(transform.position, Tracks[index].transform.position);

            if (dist < 0.5f)
            {
                index++;
            }
        }
        Variables.ActiveScene.Set("Cart Movement", cartMovement);
    }
}
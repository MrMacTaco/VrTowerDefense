using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerMovement : LocomotionProvider //Inherites from XR Locomotion Provider
{
    public List<XRController> controllers = null; //List that stores active XR Controllers
    public float speed = 1.0f;
    public float gravityScale = 1.0f;

    private CharacterController charController = null; //Stores active XR Controller as player's main controller
    private GameObject playerHead = null; // Store GameObject that corespondes to player's head ingame

    //This function is called when game is initallized, to ensure these values are set properly
    protected override void Awake()
    {
        charController = GetComponent<CharacterController>();
        playerHead = GetComponent<XRRig>().cameraGameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        PositionController();
    }

    // Update is called once per frame
    void Update()
    {
        PositionController();
        CheckInput();
        ApplyGravity();
    }

    //Updates the player's location to match the moving camera
    private void PositionController()
    {
        //Start by getting player's head location, then clamping it between values to pervent player from being too short or too tall
        float playerHeight = Mathf.Clamp(playerHead.transform.localPosition.y, 1, 2);
        charController.height = playerHeight;

        //Locate the new center of our player height
        Vector3 newPosition = Vector3.zero;
        newPosition.y = charController.height / 2;
        newPosition.y += charController.skinWidth;

        //Find the X and Z values of our new player location
        newPosition.x = playerHead.transform.localPosition.x;
        newPosition.z = playerHead.transform.localPosition.z;

        //Apply new position coorinates onto player
        charController.center = newPosition;
    }

    //Checks list of game controllers to see if any of them are providing input
    private void CheckInput()
    {
        foreach (XRController controller in controllers)
        {
            if (controller.enableInputActions)
            {
                CheckMovement(controller.inputDevice);
            }
        }
    }

    //Checks given device for movement input
    private void CheckMovement(InputDevice controller)
    {
        if(controller.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 position))
        {
            StartMovement(position);
        }
    }

    //Moves player based on given position vector
    private void StartMovement(Vector2 position)
    {
        //Apply given position input to direction player is facing
        //In other words, movement is based on direction head is facing, not where thumbstick is pointing
        Vector3 direction = new Vector3(position.x, 0, position.y); //Thumb direction
        Vector3 headDirection = new Vector3(0, playerHead.transform.eulerAngles.y, 0); //Head facing direction

        //Orientent the thumb direction to be relative to head direction
        direction = Quaternion.Euler(headDirection) * direction;

        //Apply our movement speed to our movement direction, then update player position
        Vector3 movement = direction * speed;
        charController.Move(movement * Time.deltaTime);
    }


    //Applies the effects of gravity onto the player
    private void ApplyGravity()
    {
        Vector3 gravity = new Vector3(0, Physics.gravity.y * gravityScale, 0);
        gravity.y *= Time.deltaTime;
        charController.Move(gravity * Time.deltaTime);
    }
}

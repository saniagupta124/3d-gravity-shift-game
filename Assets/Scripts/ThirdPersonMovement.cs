using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;
    public float gravity = -9.81f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    private Vector3 velocity; // For gravity
    private bool isGrounded;
    public Vector3 direction;

    private float currentAngleY; // To keep track of Y rotation during movement

    private void Update()
    {
        // Check if the character is grounded
        isGrounded = controller.isGrounded;

        // If grounded, reset vertical velocity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // To stick the character to the ground
        }

        // Get input values for movement (WASD)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Calculate movement direction
         direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Only process movement if there is input
        if (direction.magnitude >= 0.1f)
        {
            // Calculate target angle based on camera's rotation
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            // Smoothly rotate the character towards the target angle
            float angle = Mathf.SmoothDampAngle(currentAngleY, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            // Apply the Y-axis rotation to the character (only when moving)
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            currentAngleY = angle; // Update the current Y angle

            // Calculate movement direction based on rotation
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // Move the player
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            // Keep the character's rotation fixed when idle (Y-axis should not change)
            transform.rotation = Quaternion.Euler(0f, currentAngleY, 0f);
        }

        // Apply gravity to the velocity vector
        velocity.y += gravity * Time.deltaTime;

        // Move the player based on velocity
        controller.Move(velocity * Time.deltaTime);

        // Ensure the character stays upright
        LimitUprightRotation();
    }

    // Lock the rotation on X and Z to keep the character upright
    private void LimitUprightRotation()
    {
        // Get the current rotation
        Vector3 currentRotation = transform.eulerAngles;

        // Lock rotation on X and Z to 0 (to prevent falling over)
        transform.rotation = Quaternion.Euler(0f, currentRotation.y, 0f);
    }
    public void ResetVerticalVelocity()
    {
        velocity.y = 0f;
    }

    public void ApplyGravityKick()
    {
        velocity.y = Mathf.Sign(gravity) * 10f; // Or whatever feels good
    }

}

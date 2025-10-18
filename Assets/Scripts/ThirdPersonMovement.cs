using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

/*
 * Third-person character movement with camera-relative controls and custom gravity.
 * Supports dynamic gravity direction changes for gravity manipulation gameplay.
 */
public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    // Movement Settings
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;

    // Gravity Settings
    public float gravity = -9.81f;

    private float turnSmoothVelocity;
    private Vector3 velocity;
    private bool isGrounded;
    public Vector3 direction;
    private float currentAngleY;

    private void Update()
    {
        HandleGrounding();
        HandleMovement();
        ApplyGravity();
        LimitUprightRotation();
    }

    private void HandleGrounding()
    {
        isGrounded = controller.isGrounded;

        // Reset vertical velocity when grounded to prevent accumulation
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small downward force keeps character grounded
        }
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Calculate movement direction relative to camera orientation
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(currentAngleY, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            currentAngleY = angle;

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            // Maintain rotation when idle
            transform.rotation = Quaternion.Euler(0f, currentAngleY, 0f);
        }
    }

    private void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    /*
     * Locks X and Z rotation to keep character upright regardless of gravity direction.
     */
    private void LimitUprightRotation()
    {
        Vector3 currentRotation = transform.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, currentRotation.y, 0f);
    }

    /*
     * Resets vertical velocity. Called when gravity direction changes.
     */
    public void ResetVerticalVelocity()
    {
        velocity.y = 0f;
    }

    /*
     * Applies an initial impulse when gravity direction changes.
     * Creates smoother transition when flipping gravity orientation.
     * 
     */
    public void ApplyGravityKick()
    {
        velocity.y = Mathf.Sign(gravity) * 10f;
    }
}
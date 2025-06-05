using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGravity : MonoBehaviour
{
    [SerializeField] ThirdPersonMovement movement;
    [SerializeField] GameObject playerModel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("NormalGravity Trigger Entered");

            // Set gravity to normal (downward)
            movement.gravity = -9.81f;

            // Instantly reset vertical motion
            movement.ResetVerticalVelocity();

            // Optional: Give a small "kick" to start falling faster
            movement.ApplyGravityKick();

            // Rotate the model upright
            playerModel.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}

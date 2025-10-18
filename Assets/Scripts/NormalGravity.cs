using System.Numerics;
using UnityEngine;

/*
 * Trigger zone that resets gravity to normal downward direction.
 * Reorients player model and applies transition for smooth gravity shift.
 */
public class NormalGravity : MonoBehaviour
{
    [SerializeField] ThirdPersonMovement movement;
    [SerializeField] GameObject playerModel;

    private const float NORMAL_GRAVITY = -9.81f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            movement.gravity = NORMAL_GRAVITY;
            movement.ResetVerticalVelocity();
            movement.ApplyGravityKick();

            // Reset player orientation to upright
            playerModel.transform.localRotation = Quaternion.identity;
        }
    }
}
using UnityEngine;

public class FlipGravity : MonoBehaviour
{
    [SerializeField] ThirdPersonMovement movement;
    [SerializeField] GameObject playerModel; // This should be the child object with the Animator

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Flip gravity
            movement.gravity = 9.81f;

            // Get the current rotation of the player model
            Vector3 currentRotation = playerModel.transform.localEulerAngles;

            // Set Z to 180, keep X and Y the same
            playerModel.transform.localRotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 180f);
        }
    }
}

using UnityEngine;

/*
 * Trigger zone that inverts gravity to upward direction.
 * Rotates player model 180 degrees to match inverted gravity orientation.
 */
public class FlipGravity : MonoBehaviour
{
    [SerializeField] ThirdPersonMovement movement;
    [SerializeField] GameObject playerModel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            movement.gravity = 9.81f;

            Vector3 currentRotation = playerModel.transform.localEulerAngles;

            playerModel.transform.localRotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 180f);
        }
    }
}

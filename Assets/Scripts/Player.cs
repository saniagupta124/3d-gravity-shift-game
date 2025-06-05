using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator; // Reference to Animator on the model
    public ThirdPersonMovement movementScript; // Reference to movement script
    public AudioSource walkAudio; // Reference to walking AudioSource

    void Update()
    {
        bool isWalking = movementScript.direction.magnitude > 0.02f; // Small threshold to detect walking
        animator.SetBool("isWalking", isWalking);

        if (isWalking)
        {
            if (!walkAudio.isPlaying)
                walkAudio.Play();
        }
        else
        {
            if (walkAudio.isPlaying)
                walkAudio.Stop();
        }
    }
}

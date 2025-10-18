using UnityEngine;

/*
 * Manages player animation states and audio based on movement input.
 * Syncs walking animation and footstep sounds with character movement.
 */
public class Player : MonoBehaviour
{
    public Animator animator; 
    public ThirdPersonMovement movementScript; 
    public AudioSource walkAudio;

    private const float WALK_THRESHOLD = 0.02f;

    void Update()
    {
        bool isWalking = movementScript.direction.magnitude > WALK_THRESHOLD;

        // Update animation state
        animator.SetBool("isWalking", isWalking);

        // Sync footstep audio with movement
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

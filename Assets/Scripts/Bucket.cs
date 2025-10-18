using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Collectible bucket item that increments the player's bucket count on interaction.
 */
public class Bucket : MonoBehaviour
{
    [SerializeField] GameObject interactionText;
    [SerializeField] GameManager manager;
    private bool counted = false;

    private void Start()
    {
        interactionText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        interactionText.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (!counted) manager.bucketCount++;
            counted = true;
            interactionText.SetActive(false); 
            Destroy(gameObject);
            manager.bucketCountText.SetActive(true);
            Debug.Log("bucket " + manager.bucketCount);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactionText.SetActive(false);
    }
}

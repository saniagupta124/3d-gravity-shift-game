using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Collectible seed item that increments the player's seed count on interaction.
 */
public class Seed : MonoBehaviour
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
            if (!counted) manager.seedCount++;
            counted = true;
            interactionText.SetActive(false); 
            Destroy(gameObject); 
            manager.seedCountText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactionText.SetActive(false);
        counted = false;
    }
}

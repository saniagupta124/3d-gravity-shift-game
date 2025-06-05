using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bucket : MonoBehaviour
{
    [SerializeField] GameObject interactionText;
    [SerializeField] GameManager manager;
    private bool counted = false;

    private void Start()
    {
        interactionText.SetActive(false); // Hide on start
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
            interactionText.SetActive(false); // Hide text
            Destroy(gameObject); // Destroy this trigger object
            manager.bucketCountText.SetActive(true);
            Debug.Log("bucket " + manager.bucketCount);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactionText.SetActive(false);
    }
}

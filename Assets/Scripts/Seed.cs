using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;


public class Seed : MonoBehaviour
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
            if (!counted) manager.seedCount++;
            counted = true;
            interactionText.SetActive(false); // Hide text
            Destroy(gameObject); // Destroy this trigger object
            manager.seedCountText.SetActive(true);
            Debug.Log("seed " + manager.seedCount);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactionText.SetActive(false);
        counted = false;
    }
}

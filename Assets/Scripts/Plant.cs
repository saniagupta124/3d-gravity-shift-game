using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * Interactive planting spaces require seeds and water to generate platform steps.
 * Provides feedback when resources are insufficient.
 */
public class Plant : MonoBehaviour
{
    // UI Elements
    [SerializeField] GameObject interactionText;
    [SerializeField] GameObject notEnoughSeed;
    [SerializeField] GameObject notEnoughWater;
    [SerializeField] GameObject notEnough;
    [SerializeField] GameObject button;

    // Game Elements
    [SerializeField] GameManager manager;
    [SerializeField] List<GameObject> steps;
    [SerializeField] GameObject bucket;

    private bool planted = false;

    private void Start()
    {
        interactionText.SetActive(false);
        planted = false;
}

    private void OnTriggerEnter(Collider other)
    {
        interactionText.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.R))
        {
            // Check if player has required resources
            if (manager.seedCount >= 1 && manager.bucketCount >= 1)
            {
                StartCoroutine(GenerateSteps());
                planted = true;
            }
            else
            {
                ShowInsufficientResourcesFeedback();
            }
        }
    }

    private void ShowInsufficientResourcesFeedback()
    {
        if (!planted)
        {
            interactionText.SetActive(false);

            // Display appropriate error message
            if (manager.seedCount < 1 && manager.bucketCount < 1)
                notEnough.SetActive(true);
            else if (manager.seedCount < 1)
                notEnoughSeed.SetActive(true);
            else
                notEnoughWater.SetActive(true);

            button.SetActive(true);
        }
    }

    /*
     * Sequentially activates platform steps with animation delay.
     * Consumes one seed and one bucket of water.
     */
    private IEnumerator GenerateSteps()
    {
        foreach(GameObject step in steps)
        {
            step.SetActive(true);
            yield return new WaitForSeconds(.2f);

            // Reset resources after steps are generated
            if (planted)
            {
                manager.seedCount--;
                manager.bucketCount--;
            }
            planted = false;
        }
        bucket.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        interactionText.SetActive(false);
        notEnough.SetActive(false);
        notEnoughSeed.SetActive(false);
        notEnoughWater.SetActive(false);
        button.SetActive(false);
        planted = true;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Plant : MonoBehaviour
{
    [SerializeField] GameObject interactionText;
    [SerializeField] GameObject notEnoughSeed;
    [SerializeField] GameObject notEnoughWater;
    [SerializeField] GameObject notEnough;
    [SerializeField] GameManager manager;
    [SerializeField] List<GameObject> steps;
    [SerializeField] GameObject bucket;
    [SerializeField] GameObject button;
    private bool planted = false;

    private void Start()
    {
        interactionText.SetActive(false); // Hide on start
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
            if (manager.seedCount >= 1 && manager.bucketCount >= 1)
            {
                StartCoroutine(GenerateSteps());
                planted = true;
            }
            else
            {
                if (!planted)
                {
                    interactionText.SetActive(false);
                    if (manager.seedCount < 1 && manager.bucketCount < 1)
                        notEnough.SetActive(true);
                    else if (manager.seedCount < 1)
                        notEnoughSeed.SetActive(true);
                    else
                        notEnoughWater.SetActive(true);
                    button.SetActive(true);
                }
            }
            
        }
    }

    private IEnumerator GenerateSteps()
    {
        foreach(GameObject step in steps)
        {
            step.SetActive(true);
            yield return new WaitForSeconds(.2f);
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

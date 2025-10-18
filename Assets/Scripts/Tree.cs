using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Handles player interaction with the tree object for watering mechanic.
 * Transitions from grayscale to color upon successful watering.
 */
public class Tree : MonoBehaviour
{
    [SerializeField] GameObject interactionText;
    [SerializeField] GameManager manager;
    private bool planted = false;

    [SerializeField] GameObject notEnoughWater;
    [SerializeField] GameObject button;

    [SerializeField] GrayscaleEffect grayscaleEffect;
    private bool isGrayscaleOff = false;

    private void Start()
    {
        interactionText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        interactionText.SetActive(true);
        interactionText.GetComponent<TextMeshProUGUI>().text = "Press 'R' to water";
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.R))
        {
            // Require 2 buckets of water to plant the tree
            if (manager.bucketCount >= 2)
            {
                planted = true;

                if (!isGrayscaleOff)
                {
                    StartCoroutine(GraduallyRemoveGrayscale());
                    isGrayscaleOff = true;
                    StartCoroutine(RestartGame());
                }
            }
            else
            {
                if (!planted)
                {
                    // Show feedback for insufficient water 
                    interactionText.SetActive(false);
                    if (manager.bucketCount < 1)
                        notEnoughWater.SetActive(true);
                    button.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactionText.SetActive(false);
        notEnoughWater.SetActive(false);
        button.SetActive(false);
        planted = true;
    }

    /*
     * Smoothly transitions from grayscale to full color over 2 seconds using 
     * linear interpolation on the grayscale blend value.
     */
    private IEnumerator GraduallyRemoveGrayscale()
    {
        interactionText.SetActive(false);
        float targetBlend = 0f;
        float duration = 2f;
        float currentBlend = grayscaleEffect.blend;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            grayscaleEffect.blend = Mathf.Lerp(currentBlend, targetBlend, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        grayscaleEffect.blend = targetBlend;
    }

    public IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Home");
    }
}

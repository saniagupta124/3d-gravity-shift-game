using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tree : MonoBehaviour
{
    [SerializeField] GameObject interactionText;
    [SerializeField] GameManager manager;
    private bool planted = false;

    [SerializeField] GameObject notEnoughWater;
    [SerializeField] GameObject button;

    // Reference to the GrayscaleEffect component
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
            if (manager.bucketCount >= 2)
            {
                planted = true;

                // Start the gradual transition to remove grayscale effect
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

    // Coroutine to gradually remove grayscale effect
    private IEnumerator GraduallyRemoveGrayscale()
    {
        interactionText.SetActive(false);
        float targetBlend = 0f;
        float duration = 2f; // Duration of the transition
        float currentBlend = grayscaleEffect.blend;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            grayscaleEffect.blend = Mathf.Lerp(currentBlend, targetBlend, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        grayscaleEffect.blend = targetBlend; // Ensure it reaches exactly 0
    }

    public IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Home");
    }
}

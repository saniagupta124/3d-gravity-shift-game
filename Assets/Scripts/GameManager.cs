using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/*
 * Manages game state, resource tracking, and scene reloading.
 * Tracks player collectibles and handles out-of-bounds scenarios.
 */
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject button;
    [SerializeField] public GameObject seedCountText;
    [SerializeField] public GameObject bucketCountText;
    public int seedCount;
    public int bucketCount;

    private void Start()
    {
        seedCountText.SetActive(false);
        bucketCountText.SetActive(false);
    }

    private void Update()
    {
        // Show restart button if player falls out of bounds
        if (player.transform.position.y > 315f || player.transform.position.y < 13f)
        {
            button.SetActive(true);
        }
        seedCountText.GetComponent<TextMeshProUGUI>().text = "seeds " + seedCount;
        bucketCountText.GetComponent<TextMeshProUGUI>().text = "buckets " + bucketCount;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}

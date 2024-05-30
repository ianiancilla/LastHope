using System.Collections;
using UnityEngine;

public class IntroSequence : MonoBehaviour
{
    public GameObject[] svgImages;  // Array to hold SVG game objects
    public float[] displayTimes;    // Array to hold display times for each SVG

    void Start()
    {
        if (svgImages.Length != displayTimes.Length)
        {
            Debug.LogError("The number of SVG images and display times must match!");
            return;
        }
        
        StartCoroutine(ShowSVGSequence());
    }

    IEnumerator ShowSVGSequence()
    {
        for (int i = 0; i < svgImages.Length; i++)
        {
            svgImages[i].SetActive(true);    // Show the SVG
            yield return new WaitForSeconds(displayTimes[i]); // Wait for specified time
            svgImages[i].SetActive(false);   // Hide the SVG
        }
    }
}

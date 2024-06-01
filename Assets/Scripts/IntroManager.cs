using System.Collections;
using UnityEngine;

public class IntroSequence : MonoBehaviour
{
    [SerializeField] GameObject[] svgImages;  // Array to hold SVG game objects
    [SerializeField] float[] displayTimes;    // Array to hold display times for each SVG
    [SerializeField] bool loadSceneAutomatically = true;

    void Start()
    {
        if (!loadSceneAutomatically) { return; }

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

        SceneLoader.LoadLevel();
    }

    public void SkipIntro()
    {
        SceneLoader.LoadLevel();
    }

    public void ToTitle()
    {
        SceneLoader.LoadMainMenu();
    }
}

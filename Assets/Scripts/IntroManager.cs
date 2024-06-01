using System.Collections;
using UnityEngine;

public class IntroSequence : MonoBehaviour
{
    [SerializeField] bool loadSceneAutomatically = true;

    [SerializeField] GameObject[] svgImages;  // Array to hold SVG game objects
    [SerializeField] float[] displayTimes;    // Array to hold display times for each SVG

    [SerializeField] float fadeInDuration = 0.5f;
    [SerializeField] float skipSceneDelay = 0.3f;
    [SerializeField] float endSceneDelay = 0.5f;
    [SerializeField] float fadeBetweenPanels = 0.5f;


    [SerializeField] FadeInOut fader;
    [SerializeField] AudioSource audioSource;


    void Start()
    {
        fader.FadeIn(fadeInDuration);

        StartPanelReel();
    }

    private void StartPanelReel()
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

            yield return new WaitForSeconds(displayTimes[i] - fadeBetweenPanels); // Wait for specified time
            StartCoroutine(fader.FadeOut(fadeBetweenPanels));
            yield return new WaitForSeconds(fadeBetweenPanels);
            StartCoroutine (fader.FadeIn(fadeBetweenPanels));
            svgImages[i].SetActive(false);   // Hide the SVG
        }

        SceneLoader.LoadLevel();
    }

    public void SkipIntro()
    {
        StartCoroutine(TransitionToLevelAfterSec(skipSceneDelay));
    }

    public void TransitionToLevel()
    {
        StartCoroutine(TransitionToLevelAfterSec(endSceneDelay));
    }
    public void ToTitle()
    {
        StartCoroutine(TransitionToTitleAfterSec(endSceneDelay));
    }


    public IEnumerator TransitionToLevelAfterSec(float duration)
    {
        StartCoroutine(fader.FadeOut(duration));

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            yield return new WaitForEndOfFrame();
            audioSource.volume = 1f - elapsedTime / duration;
            elapsedTime += Time.deltaTime;
        }

        audioSource.volume = 0f;

        SceneLoader.LoadLevel();
    }

    public IEnumerator TransitionToTitleAfterSec(float duration)
    {
        StartCoroutine(fader.FadeOut(duration));

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            yield return new WaitForEndOfFrame();
            audioSource.volume = 1f - elapsedTime / duration;
            elapsedTime += Time.deltaTime;
        }

        audioSource.volume = 0f;

        SceneLoader.LoadMainMenu();
    }

}

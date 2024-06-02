using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSceneManager : MonoBehaviour
{
    [SerializeField] FadeInOut sceneFader;
    [SerializeField] float fadeInDuration = 1f;
    [SerializeField] float fadeOutDuration = 1f;

    public static event Action<float> OnVolumeChanged;

    private void Start()
    {
        sceneFader.FadeIn(fadeInDuration);
    }

    private void OnEnable()
    {
        SectorsManager.OnGameEnd += TriggerEnding;
    }

    private void OnDisable()
    {
        SectorsManager.OnGameEnd -= TriggerEnding;
    }

    private void TriggerEnding(SectorsManager.Ending ending)
    {
        switch (ending)
        {
            case SectorsManager.Ending.allDead:
                StartCoroutine(TransitionToBadEndingAfterSec(fadeOutDuration));
                break;
            case SectorsManager.Ending.someSaved:
                StartCoroutine(TransitionToMehEndingAfterSec(fadeOutDuration));
                break;
            case SectorsManager.Ending.allSaved:
                StartCoroutine(TransitionToGoodEndingAfterSec(fadeOutDuration));
                break;
            default:
                break;
        }
    }

    public void BackToTitle()
    {
        StartCoroutine(TransitionToTitleAfterSec(fadeOutDuration));
    }


    // TODO repeatede code
    public IEnumerator TransitionToTitleAfterSec(float duration)
    {
        StartCoroutine(sceneFader.FadeOut(duration));

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            yield return new WaitForEndOfFrame();
            OnVolumeChanged?.Invoke(1f - elapsedTime / duration);
            elapsedTime += Time.deltaTime;
        }
        SceneLoader.LoadMainMenu();
    }

    public IEnumerator TransitionToBadEndingAfterSec(float duration)
    {
        StartCoroutine(sceneFader.FadeOut(duration));

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            yield return new WaitForEndOfFrame();
            OnVolumeChanged?.Invoke(1f - elapsedTime / duration);
            elapsedTime += Time.deltaTime;
        }
        SceneLoader.LoadBadEnding();
    }
    public IEnumerator TransitionToMehEndingAfterSec(float duration)
    {
        StartCoroutine(sceneFader.FadeOut(duration));

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            yield return new WaitForEndOfFrame();
            OnVolumeChanged?.Invoke(1f - elapsedTime / duration);
            elapsedTime += Time.deltaTime;
        }
        SceneLoader.LoadMehEnding();
    }
    public IEnumerator TransitionToGoodEndingAfterSec(float duration)
    {
        StartCoroutine(sceneFader.FadeOut(duration));

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            yield return new WaitForEndOfFrame();
            OnVolumeChanged?.Invoke(1f - elapsedTime / duration);
            elapsedTime += Time.deltaTime;
        }
        SceneLoader.LoadGoodEnding();
    }
}

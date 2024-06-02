using System;
using System.Collections;
using UnityEngine;
public class SceneManager : MonoBehaviour
{
    [SerializeField] bool panelReelThenLoadNewScene = true;

    [SerializeField] SlidePanel[] panels;

    [SerializeField] float fadeInDuration = 0.5f;
    [SerializeField] float skipSceneDelay = 0.3f;
    [SerializeField] float endSceneDelay = 0.5f;
    [SerializeField] float fadeInBetweenPanels = 0.5f;
    [SerializeField] float fadeOutBetweenPanels = 0.5f;
    [SerializeField] float darkBetweenPanels = 0.5f;



    [SerializeField] FadeInOut sceneFader;
    [SerializeField] FadeInOut betweenPanelsFader;


    public static event Action<float> OnVolumeChanged;

    void Start()
    {
        sceneFader.FadeIn(fadeInDuration);

        StartPanelReel();
    }

    private void StartPanelReel()
    {
        if (!panelReelThenLoadNewScene) { return; }

        StartCoroutine(PanelSequence());
    }

    IEnumerator PanelSequence()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (panels[i].fadeIn)
            {
                StartCoroutine(betweenPanelsFader.FadeOut(fadeInBetweenPanels)); //panels fader works backwards because fadeout makes the canvas VISIBLE, and it is applied to the panel canvas
            }

            panels[i].panelGO.SetActive(true);

            yield return new WaitForSeconds(panels[i].displayTime - fadeOutBetweenPanels - darkBetweenPanels); 

            if (panels[i].fadeOut)
            {
                StartCoroutine(betweenPanelsFader.FadeIn(fadeOutBetweenPanels));//panels fader works backwards because fadeout makes the canvas VISIBLE, and it is applied to the panel canvas
            }

            yield return new WaitForSeconds(fadeOutBetweenPanels + darkBetweenPanels);

            panels[i].panelGO.SetActive(false);
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


    // TODO repeatede code
    public IEnumerator TransitionToLevelAfterSec(float duration)
    {
        StartCoroutine(sceneFader.FadeOut(duration));

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            yield return new WaitForEndOfFrame();

            OnVolumeChanged?.Invoke(1f - elapsedTime / duration);
            elapsedTime += Time.deltaTime;
        }

        SceneLoader.LoadLevel();
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

}

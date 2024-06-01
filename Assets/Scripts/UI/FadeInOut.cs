using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{

    public IEnumerator FadeIn(float duration)
    {
        float elapsedTime = 0f;
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        while (elapsedTime < duration)
        {
            yield return new WaitForEndOfFrame();
            canvasGroup.alpha = 1f - elapsedTime / duration;
            //Debug.Log(canvasGroup.alpha);
            elapsedTime += Time.deltaTime;
        }

        canvasGroup.alpha = 0f;
    }

    public IEnumerator FadeOut(float duration)
    {
        float elapsedTime = 0f;
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        while (elapsedTime < duration)
        {
            yield return new WaitForEndOfFrame();
            canvasGroup.alpha = elapsedTime / duration;
            //Debug.Log(canvasGroup.alpha);
            elapsedTime += Time.deltaTime;
        }

        canvasGroup.alpha = 1f;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// makes canvas apper
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public IEnumerator FadeOut(float time)
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime / time;
            yield return null; // waits for one frame
        }
    }

    /// <summary>
    /// Makes canvas disapper
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public IEnumerator FadeIn(float time)
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / time;
            yield return null;
        }

    }
}

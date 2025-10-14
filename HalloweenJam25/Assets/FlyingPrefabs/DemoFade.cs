using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoFade : MonoBehaviour
{
    private float fadeTime = 1.5f;
    private float currTime;

    private Image fadeImage;
    private Color color;
    private void Start()
    {
        fadeImage = GetComponent<Image>();
    }

    private void StartFadeOut()
    {
        StartCoroutine(Fade(0.3f,1));
    }

    private void StartFadeIn()
    {
        StartCoroutine(Fade(1,0));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            StartFadeIn();
        }
        else if(Input.GetKeyDown(KeyCode.O))
        {
            StartFadeOut();
        }
    }
    private IEnumerator FadeIn()
    {
        color = fadeImage.color;
        color.a = 0.0f;
        fadeImage.color = color;
        
        currTime = 0.0f;

        while (currTime < fadeTime)
        {
            color.a = Mathf.Lerp(0,1, currTime/fadeTime);
            fadeImage.color = color;

            currTime += Time.deltaTime;
            yield return null;
        }

        color.a = 1;
        fadeImage.color = color;

        yield return null;
    }

    private IEnumerator FadeOut()
    {
        color = fadeImage.color;
        color.a = 1;
        fadeImage.color = color;

        currTime = 0.0f;

        while (currTime < fadeTime)
        {
            color.a = Mathf.Lerp(0,1, currTime / fadeTime);
            fadeImage.color = color;

            currTime += Time.deltaTime;
            yield return null;
        }

        color.a = 0;
        fadeImage.color = color;

        yield return null;
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        color = fadeImage.color;
        color.a = startAlpha;
        fadeImage.color = color;

        currTime = 0.0f;

        while (currTime < fadeTime)
        {
            color.a = Mathf.Lerp(startAlpha, endAlpha, currTime / fadeTime);
            fadeImage.color = color;

            currTime += Time.deltaTime;
            yield return null;
        }

        color.a = endAlpha;
        fadeImage.color = color;

        yield return null;
    }
}

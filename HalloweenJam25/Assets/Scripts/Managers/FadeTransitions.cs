using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;

public class FadeTransitions : MonoBehaviour
{
    public static FadeTransitions Instance;

    [SerializeField] private float fadeDuration;

    [SerializeField] Image fadeImage;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        fadeDuration = fadeDuration == 0 ? 2 : fadeDuration;

        if(fadeImage.color.a == 1)
        {
            FadeOut(fadeDuration);
        }
    }

    public void FadeIn(float duration)
    {
        fadeImage.DOFade(1, duration).SetUpdate(true);
    }

    public void FadeOut(float duration)
    {
        fadeImage.DOFade(0, duration).SetUpdate(true).OnComplete(() => CheckTime());
    }

    public void SwitchScenes(string sceneName, SceneScript scene)
    {
        StartCoroutine(LoadScene(sceneName, scene));
    }

    private IEnumerator LoadScene(string sceneName, SceneScript scene)
    {
        FadeIn(fadeDuration);

        yield return new WaitForSecondsRealtime(fadeDuration);

        AsyncOperation load = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        load.allowSceneActivation = false;

        while(load.progress < 0.9f)
        {
            yield return null;
        }

        load.allowSceneActivation = true;

        yield return null;

        FadeOut(fadeDuration);
        UIManager.Instance.LoadNextMenu(scene);

    }

    private void CheckTime()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }
}

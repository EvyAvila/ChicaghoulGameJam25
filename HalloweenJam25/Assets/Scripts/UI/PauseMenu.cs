using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    private Button restartBtn, quitGameBtn;
    private Slider sfxSlider, mouseSenSlider;

    public VisualElement root;
    public UIDocument uiDocument;
    
    [SerializeField] private Canvas fadeCanvas;

    private void OnEnable()
    {
        if (uiDocument == null)
        {
            Debug.LogError("PauseMenu: UIDocument is not assigned!");
            return;
        }

        StartCoroutine(SetupUI());
    }

  
    private void OnDisable()
    {
        UnSetProperties();
    }

    protected void SetProperties()
    {
        var pauseRoot = root.Q("PauseMenu");
        restartBtn = pauseRoot.Q<Button>("ResumeBtn");
        quitGameBtn = pauseRoot.Q<Button>("QuitBtn");
        //sfxSlider = pauseRoot.Q<Slider>("SoundFXSlider");
        mouseSenSlider = pauseRoot.Q<Slider>("MouseSensitivitySlider");

        restartBtn.RegisterCallback<ClickEvent>(ResumeGameplay);
        quitGameBtn.RegisterCallback<ClickEvent>(QuitGameplay);

        //sfxSlider.RegisterValueChangedCallback(SetSFXVol);
        mouseSenSlider.RegisterValueChangedCallback(SetMouseSense);

        if (mouseSenSlider != null && MouseSensitivity.Instance != null)
        {
            mouseSenSlider.SetValueWithoutNotify(MouseSensitivity.Instance.sensitivity);
        }
    }

    private void SetMouseSense(ChangeEvent<float> e)
    {
        if (MouseSensitivity.Instance != null)
        {
            MouseSensitivity.Instance.SetMouseSense(e.newValue);
        }
    }

    private void SetSFXVol(ChangeEvent<float> e)
    {
        if (AudioSettings.Instance != null)
        {
            AudioSettings.Instance.SetSFXVol(e.newValue);
        }
    }

    protected void UnSetProperties()
    {
        if (restartBtn != null)
            restartBtn.UnregisterCallback<ClickEvent>(ResumeGameplay);

        if (quitGameBtn != null)
            quitGameBtn.UnregisterCallback<ClickEvent>(QuitGameplay);

        if (fadeCanvas != null)
            fadeCanvas.sortingOrder = 1;

        if (mouseSenSlider != null)
            mouseSenSlider.UnregisterValueChangedCallback(SetMouseSense);

        //if (sfxSlider != null)
        //    sfxSlider.UnregisterValueChangedCallback(SetSFXVol);
    }

    private void ResumeGameplay(ClickEvent evt)
    {
        UIManager.Instance.DisplayPauseMenu(false);
        fadeCanvas.sortingOrder = 1;

        UnityEngine.Cursor.lockState = UnityEngine.CursorLockMode.Locked;
    }

    private void QuitGameplay(ClickEvent evt)
    {
        FadeTransitions.Instance.SwitchScenes("MainMenu", SceneScript.MainMenu);
        UIManager.Instance.DisplayPauseMenu(false);
    }

    private IEnumerator SetupUI() //Slight delay
    {
        yield return null;

        root = uiDocument.rootVisualElement;

        SetProperties();

        if (fadeCanvas != null)
            fadeCanvas.sortingOrder = 0;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    private Button restartBtn, quitGameBtn;

    protected VisualElement root;
    public UIDocument uiDocument;


    private void OnEnable()
    {
        root = uiDocument.rootVisualElement;
        SetProperties();
    }

    private void OnDisable()
    {
        UnSetProperties();
    }

    protected void SetProperties()
    {
        restartBtn = root.Q("RestartBtn") as Button;
        quitGameBtn = root.Q("QuitBtn") as Button;

        restartBtn.RegisterCallback<ClickEvent>(RestartGameplay);
        quitGameBtn.RegisterCallback<ClickEvent>(QuitGameplay);
    }

    protected void UnSetProperties()
    {
        restartBtn.UnregisterCallback<ClickEvent>(RestartGameplay);
        quitGameBtn.UnregisterCallback<ClickEvent>(QuitGameplay);
    }

    private void RestartGameplay(ClickEvent evt)
    {
        Debug.Log("Restarts game here");
    }

    private void QuitGameplay(ClickEvent evt)
    {

        FadeTransitions.Instance.SwitchScenes("MainMenu", SceneScript.MainMenu);
        //UIManager.Instance.DisplayPauseMenu(false);
    }
}

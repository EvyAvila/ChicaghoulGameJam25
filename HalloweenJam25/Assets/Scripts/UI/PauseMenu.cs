using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenu : BaseMenu
{
    private Button restartBtn, quitGameBtn;

    protected override void Awake()
    {
        scriptName = SceneScript.PauseMenu;
    }

    protected override void SetProperties()
    {
        restartBtn = root.Q("RestartBtn") as Button;
        quitGameBtn = root.Q("QuitBtn") as Button;

        restartBtn.RegisterCallback<ClickEvent>(RestartGameplay);
        quitGameBtn.RegisterCallback<ClickEvent>(QuitGameplay);
    }

    protected override void UnSetProperties()
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
        UIManager.Instance.LoadNextMenu(SceneScript.MainMenu);
    }
}

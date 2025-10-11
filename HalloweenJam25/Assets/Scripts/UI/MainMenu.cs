using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : BaseMenu
{
    private Button startBtn, quitBtn;

    protected override void Awake()
    {
        scriptName = SceneScript.MainMenu;
    }

    protected override void SetProperties()
    {
        startBtn = root.Q("StartBtn") as Button;
        quitBtn = root.Q("QuitBtn") as Button;

        startBtn.RegisterCallback<ClickEvent>(StartGame);
        quitBtn.RegisterCallback<ClickEvent>(QuitGame);
    }

    protected override void UnSetProperties()
    {
        startBtn.UnregisterCallback<ClickEvent>(StartGame);
        quitBtn.UnregisterCallback<ClickEvent>(QuitGame);
    }

    private void StartGame(ClickEvent evt)
    {
        //UIManager.Instance.LoadNextMenu(SceneScript.GameMenu);
        Debug.Log("Starts game");
    }

    private void QuitGame(ClickEvent evt)
    {
        UIManager.Instance.LoadNextMenu(SceneScript.QuitMenu);
    }
    
}

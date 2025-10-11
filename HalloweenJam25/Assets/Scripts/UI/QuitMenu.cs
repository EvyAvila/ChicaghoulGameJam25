using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QuitMenu : BaseMenu
{
    private Button yesBtn, noBtn;

    protected override void Awake()
    {
        scriptName = SceneScript.QuitMenu;
    }

    protected override void SetProperties()
    {
        yesBtn = root.Q("YesBtn") as Button;
        noBtn = root.Q("NoBtn") as Button;

        yesBtn.RegisterCallback<ClickEvent>(Exit);
        noBtn.RegisterCallback<ClickEvent>(ReturnToMain);
    }

    protected override void UnSetProperties()
    {
        yesBtn.UnregisterCallback<ClickEvent>(Exit);
        noBtn.UnregisterCallback<ClickEvent>(ReturnToMain);
    }

    private void ReturnToMain(ClickEvent evt)
    {
        UIManager.Instance.LoadNextMenu(SceneScript.MainMenu);
    }

    private void Exit(ClickEvent evt)
    {
        #if UNITY_STANDALONE
                Application.Quit();
        #endif
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}

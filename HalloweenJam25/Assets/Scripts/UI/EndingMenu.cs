using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EndingMenu : MonoBehaviour
{
    private Button returnToMenuBtn;
    protected VisualElement root;
    public UIDocument uiDocument;
    private bool isActive;

    private void Start()
    {
        isActive = false;
        UIManager.Instance.DisplayPauseMenu(isActive);
    }

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
        returnToMenuBtn = root.Q("ReturnToMenuBtn") as Button;
       

        returnToMenuBtn.RegisterCallback<ClickEvent>(ExitGame);
        
    }

    protected void UnSetProperties()
    {
        returnToMenuBtn.UnregisterCallback<ClickEvent>(ExitGame);
        
    }

    private void ExitGame(ClickEvent evt)
    {
        isActive = false;
        UIManager.Instance.DisplayPauseMenu(isActive);
        UIManager.Instance.LoadNextMenu(SceneScript.MainMenu);
    }
}

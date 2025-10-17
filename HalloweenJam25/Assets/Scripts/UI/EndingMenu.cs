using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EndingMenu : MonoBehaviour
{
    private Button returnToMenuBtn;
    private Label endingResult;
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
        endingResult = root.Q("EndingLabel") as Label;

        returnToMenuBtn.RegisterCallback<ClickEvent>(ExitGame);
        if(GameEndingPicker.Instance != null)
            DisplayEnding();
    }

    protected void UnSetProperties()
    {
        returnToMenuBtn.UnregisterCallback<ClickEvent>(ExitGame);
        
    }

    private void ExitGame(ClickEvent evt)
    {
        isActive = false;
        UIManager.Instance.DisplayPauseMenu(isActive);
        //UIManager.Instance.LoadNextMenu(SceneScript.MainMenu);
        FadeTransitions.Instance.SwitchScenes("MainMenu", SceneScript.MainMenu);
    }

    private void DisplayEnding()
    {
        string output = "";
        var endingType = GameEndingPicker.Instance.ending;
        var failType = GameEndingPicker.Instance.failType;
        
        if(endingType == EndingType.FAILURE && failType == FailureType.SOULS)
        {
            output = "You've awoken the souls";
        }
        else if (endingType == EndingType.FAILURE && failType == FailureType.SUN)
        {
            output = "Cooked by the sun";
        }
        else if(endingType == EndingType.BAD)
        {
            output = "Not enough blood...";
        }
        else if (endingType == EndingType.NORMAL)
        {
            output = "Normal Ending, try more blood";
        }
        else if (endingType == EndingType.SUPER)
        {
            output = "Super Ending";
        }

        endingResult.text = output;
    }
}

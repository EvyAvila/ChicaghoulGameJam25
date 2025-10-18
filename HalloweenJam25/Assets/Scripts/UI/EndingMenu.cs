using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EndingMenu : MonoBehaviour
{
    private Button returnToMenuBtn;
    private VisualElement endingBG;
    private Label endingResult;
    protected VisualElement root;
    public UIDocument uiDocument;

    [SerializeField] private Canvas fadeCanvas;
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
        endingBG = root.Q("EndingElements") as VisualElement;

        returnToMenuBtn.RegisterCallback<ClickEvent>(ExitGame);
        if(GameEndingPicker.Instance != null)
            DisplayEnding();


        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        UnityEngine.Cursor.visible = true;

    }

    protected void UnSetProperties()
    {
        if (fadeCanvas != null)
            fadeCanvas.sortingOrder = 1;

        if (returnToMenuBtn != null)
            returnToMenuBtn.UnregisterCallback<ClickEvent>(ExitGame);        
    }

    private void ExitGame(ClickEvent evt)
    {
        isActive = false;
        //UIManager.Instance.LoadNextMenu(SceneScript.MainMenu);
        FadeTransitions.Instance.SwitchScenes("MainMenu", SceneScript.MainMenu);
        UIManager.Instance.DisplayEndingMenu(isActive);
    }

    private void SetBGOpacity(float t)
    {
        StyleColor bg = endingBG.style.backgroundColor;
        Color b = bg.value;
        b.a = t;
        bg.value = b;
        endingBG.style.backgroundColor = bg;
    }

    private void DisplayEnding()
    {
        string output = "";
        var endingType = GameEndingPicker.Instance.ending;
        var failType = GameEndingPicker.Instance.failType;

        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        UnityEngine.Cursor.visible = true;


        SetBGOpacity(0);

        if (endingType == EndingType.FAILURE && failType == FailureType.SOULS)
        {
            SetBGOpacity(1);

            if (fadeCanvas != null)
                fadeCanvas.sortingOrder = 0;

            output = "You've awoken the souls";
        }
        else if (endingType == EndingType.FAILURE && failType == FailureType.SUN)
        {
            SetBGOpacity(1);

            if (fadeCanvas != null)
                fadeCanvas.sortingOrder = 0;

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

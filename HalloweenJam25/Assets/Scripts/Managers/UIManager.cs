using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using UnityEngine.Device;

public enum SceneScript { MainMenu, QuitMenu, GameMenu}

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public UIDocument uiDocument;

    [SerializeField]
    private List<Menus> menus;

    private BaseMenu currentMenu;

    [SerializeField]
    private SceneScript startingUIScript;

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
        LoadNextMenu(startingUIScript);
    }

    private void SwitchUIMenu(VisualTreeAsset screen, BaseMenu menu)
    {
        currentMenu?.Deactivate();

        uiDocument.rootVisualElement.Clear();
        if (screen != null)
        {
            uiDocument.visualTreeAsset = screen;
        }

        currentMenu = menu;
        currentMenu?.Activate(uiDocument);
    }


    public void LoadNextMenu(SceneScript scriptName)
    {
        int index = menus.FindIndex(x => x.MenuScript.scriptName == scriptName);
        SwitchUIMenu(menus[index].MenuAsset, menus[index].MenuScript);
    }

}

[System.Serializable]
public class Menus
{
    public VisualTreeAsset MenuAsset;
    public BaseMenu MenuScript;
}

public abstract class BaseMenu : MonoBehaviour
{
    protected VisualElement root;

    public SceneScript scriptName { get; set; }

    public virtual void Activate(UIDocument document)
    {
        root = document.rootVisualElement;
        SetProperties();
    }

    public virtual void Deactivate()
    {
        UnSetProperties();
        root = null;
    }

    protected abstract void SetProperties();
    protected abstract void UnSetProperties();

    protected abstract void Awake();

}
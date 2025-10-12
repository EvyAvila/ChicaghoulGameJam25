using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPause : InteractableObject
{
    //public static event Action OnPauseTest;
    //public PauseMenu menu;
    private bool isActive;

    protected override void Start()
    {
        isActive = false;
        //menu.gameObject.SetActive(false);
        UIManager.Instance.DisplayPauseMenu(isActive);
    }

    public override void Interact()
    {
        isActive = !isActive;

        if(isActive)
        {
            UIManager.Instance.DisplayPauseMenu(isActive);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            UIManager.Instance.DisplayPauseMenu(isActive);
        }    
        //UIManager.Instance.LoadNextMenu(SceneScript.PauseMenu);
        //OnPauseTest?.Invoke();
    }
}

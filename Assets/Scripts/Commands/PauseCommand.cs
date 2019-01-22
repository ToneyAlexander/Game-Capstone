﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCommand : ICommand
{
    
    private GameObject pauseMenu;
    public PauseCommand(GameObject menu)
    {
        pauseMenu = menu;
    }
    // Start is called before the first frame update
    public void InvokeCommand()

    {
        pauseMenu.SetActive(true);
        GameSystem.setPaused(false);
        Cursor.visible = true;
        
    }
}
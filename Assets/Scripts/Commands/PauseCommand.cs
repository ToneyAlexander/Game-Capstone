using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCommand : ICommand
{
    
    private GameObject pauseMenu;
    public PauseCommand(GameObject menu)
    {
        pauseMenu = menu;
    }
    public PauseCommand()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        if (pauseMenu == null)
        {
            Debug.Log(pauseMenu);
            pauseMenu = GameObject.Find("/Canvas/PauseMenu");
        }
        
    }
    // Start is called before the first frame update
    public void InvokeCommand()

    {
        pauseMenu.SetActive(true);
        GameSystem.setPaused(false);
        Cursor.visible = true;
        
    }
}

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
        pauseMenu = GameSystem.getMenu("Pause");
        
    }
    // Start is called before the first frame update
    public void InvokeCommand()

    {
        pauseMenu.SetActive(true);
        GameSystem.setPaused(true);
        Debug.Log(GameSystem.getPaused());
        Cursor.visible = true;
        
    }
}

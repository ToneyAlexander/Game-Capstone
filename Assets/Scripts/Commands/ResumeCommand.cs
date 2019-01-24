using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeCommand : ICommand
{
    private GameObject pauseMenu;
    public ResumeCommand(GameObject menu)
    {
        pauseMenu = menu;
    }
    // Start is called before the first frame update
    public void InvokeCommand()

    {
        pauseMenu.SetActive(false);
        GameSystem.setPaused(false);
        Cursor.visible = true;

    }
}

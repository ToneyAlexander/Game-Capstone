using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuCommand : ICommand
{

    public void InvokeCommand()
    {
        GameSystem.setPaused(false);
        GameSystem.resetMenu();
        SceneManager.LoadScene("Scenes/Menu");
       
    }
}

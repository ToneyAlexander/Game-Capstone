using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitMenuCommand : ICommand
{
    private GameObject pauseMenu;
    private GameObject quitMenu;
    private string menu1;
    private string menu2;
    public QuitMenuCommand(GameObject menu, GameObject qMenu)
    {
        menu1 = "Pause";
        menu2 = "Quit";
        pauseMenu = menu;
        quitMenu = qMenu;
    }
    public QuitMenuCommand()
    {
        menu1 = "Pause";
        menu2 = "Quit";
        pauseMenu = GameSystem.getMenu(menu1);
        quitMenu = GameSystem.getMenu(menu2);
    }
    public QuitMenuCommand(string m1, string m2)
    {
        menu1 = m1;
        menu2 = m2;
        pauseMenu = GameSystem.getMenu(menu1);
        quitMenu = GameSystem.getMenu(menu2);
    }
    // Start is called before the first frame update
    public void InvokeCommand()

    {
        if (pauseMenu == null)
        {
            pauseMenu = GameSystem.getMenu(menu1);
        }
        if (quitMenu == null)
        {
            quitMenu = GameSystem.getMenu(menu2);
        }
        pauseMenu.SetActive(false);
        quitMenu.SetActive(true);
        GameSystem.setPaused(true);
        Cursor.visible = true;

    }
}

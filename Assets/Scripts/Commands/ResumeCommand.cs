using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeCommand : ICommand
{
    // Start is called before the first frame update
    public void InvokeCommand()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitCommand : ICommand
{

    public void InvokeCommand()
    {
        //Application.Quit();
        Debug.Log("Working");
    }
}

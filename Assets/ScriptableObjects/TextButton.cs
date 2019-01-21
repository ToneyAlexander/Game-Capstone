using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextButton : MonoBehaviour
{
    [SerializeField]
    private string textField = "Run";

    public ICommand command;
    private Text text;
    [SerializeField]
    public commandType buttonType;
    public enum commandType{
        resume,
        quit
    }

    void Start()
    {
        if (buttonType == commandType.quit)
        {
            command = new QuitCommand();
        }
        if (buttonType == commandType.resume)
        {
            command = new ResumeCommand();
        }
    }
    void highlight()
    {

    }
    void OnMouseDown()
    {
        command.InvokeCommand();
    }
}

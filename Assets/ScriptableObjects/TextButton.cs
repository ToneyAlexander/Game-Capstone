using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


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
        quit,
        quitmenu,
        pausemenu
    }

    void Start()
    {
        if (buttonType == commandType.quit)
        {
            command = new QuitCommand();
        }
        else if (buttonType == commandType.resume)
        {
            command = new ResumeCommand(gameObject.transform.parent.gameObject);
        }
        else if (buttonType == commandType.quitmenu)
        {
            command = new QuitMenuCommand();
        }
        else if (buttonType == commandType.pausemenu)
        {
            command = new QuitMenuCommand("Quit","Pause");
        }
        EventTrigger ev = gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => { OnClick(); });
        ev.triggers.Add(entry);
    }
    void highlight()
    {
        command.InvokeCommand();
    }

    void OnMouseDown()
    {
        command.InvokeCommand();
    }
    void OnClick()
    {
        command.InvokeCommand();
    }
    void Click()
    {
        command.InvokeCommand();
    }
}

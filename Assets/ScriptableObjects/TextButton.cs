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
        EventTrigger ev = gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener(delegate { OnClick(); });
        ev.triggers.Add(entry);
    }
    void highlight()
    {

    }
    void OnMouseDown()
    {
        command.InvokeCommand();
    }
    void OnClick()
    {
        command.InvokeCommand();
    }
}

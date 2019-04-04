using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecruitButton : MonoBehaviour
{
    public CrewController crewController;

    private Image img;
    // Start is called before the first frame update
    void Start()
    {
        EventTrigger ev = gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => { OnMouseClick((PointerEventData)eventData); });
        ev.triggers.Add(entry);
        img = this.transform.gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Image image = gameObject.GetComponent<Image>();
        if (!crewController.recruited)
        {
            image.color = new Color(0.8f, 0.8f, 0.5f);
        }
        else
        {
            image.color = new Color(0.5f, 0.5f, 0.5f);
        }
    }
    void OnMouseClick(PointerEventData data)
    {
        if (!crewController.recruited)
        {
            crewController.fillCrew();
        }
    }

}

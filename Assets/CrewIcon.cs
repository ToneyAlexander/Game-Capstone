using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CrewIcon : MonoBehaviour
{
    public CrewMember cMember;
    public CrewController cController;
    private Text name;
    private Text skill;

    void Start()
    {
        EventTrigger ev = gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => { OnMouseClick((PointerEventData)eventData); });
        ev.triggers.Add(entry);

       // EventTrigger ev = storedButtons[i].GetComponent<EventTrigger>();
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerEnter;
        entry2.callback.AddListener((eventData) => { OnMouseOver(); });
        ev.triggers.Add(entry2);

       // EventTrigger ev = storedButtons[i].GetComponent<EventTrigger>();
        EventTrigger.Entry entry3 = new EventTrigger.Entry();
        entry3.eventID = EventTriggerType.PointerExit;
        entry3.callback.AddListener((eventData) => { OnMouseExit(); });
        ev.triggers.Add(entry3);

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name.Equals("Name"))
            {
                name = transform.GetChild(i).GetComponent<Text>();
            }
            if (transform.GetChild(i).name.Equals("Skill"))
            {
                skill = transform.GetChild(i).GetComponent<Text>();
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        string stars = new String('☆', cMember.StarRating);
        name.text = cMember.name + "    " + stars;
        skill.text = cMember.CType + cMember.addendum();
    }
    void OnMouseOver()
    {
        Debug.Log("woke");
    }
    void OnMouseClick(PointerEventData data)
    {
        if (data.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Dismiss");
        }
        if (data.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Toggle");
        }
    }
    void OnMouseExit()
    {
        Debug.Log("broke");
    }
}
